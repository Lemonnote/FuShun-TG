<Command("Parallel C Dose", "Time |0-60| Curve |0-9|", , , "'1", CommandType.ParallelCommand), _
 TranslateCommand("zh-TW", "平行C加藥", "加藥時間 |1-60| 曲線選擇 |0-9|"), _
 Description("Time=1-60, Curve=0-9"), _
 TranslateDescription("zh-TW", "時間=1-60, 曲線=0-9")> _
Public NotInheritable Class Command68
  Inherits MarshalByRefObject
  Implements ACCommand
  Public StateString As String

  Public Enum S68
    Off
    CheckSafetyTemp
    CheckReady
    Dose
    WaitAddFinish
    Wait
    Rinse1
    MixCir
    Add
    Rinse2
    Drain
    Pause
  End Enum

  Public Timer As New Timer, LevelTimer As New Timer
  Public AddTime, AddCurve As Integer
  Public StartLevel As Integer
  Public DoseOutput As Integer
  Public DoseON As Boolean
  Public 清洗次數 As Integer
  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode
      .Command67.Cancel()
      AddTime = Maximum(param(1) * 60, 3600)
      AddCurve = param(2)
      Timer.TimeRemaining = 2
      清洗次數 = .Parameters.B藥缸清洗次數
      State = S68.CheckReady
    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
    With ControlCode


      Select Case State
        Case S68.Off
          StateString = ""

        Case S68.CheckReady

          If .Parent.IsPaused Or Not .IO.MainPumpFB Then
            Timer.Pause()
            State = S68.Pause
          End If
          If .TankCReady And (Not (.Command65.IsOn Or .Command55.IsOn Or .Command57.IsOn Or .Command67.IsOn)) And Timer.Finished Then
            Timer.TimeRemaining = AddTime
            StartLevel = .IO.TankCLevel
            State = S68.Dose
          End If

          'state string stuff.
          If Not .TankCReady Then
            StateString = If(.Language = LanguageValue.ZhTw, "C缸未備藥", "Tank C not prepared")
          ElseIf .Command55.IsOn Or .Command65.IsOn Then
            StateString = If(.Language = LanguageValue.ZhTw, "等待C缸備藥中", "Waiting for tank C")
          ElseIf .Command52.IsActive Then
            StateString = If(.Language = LanguageValue.ZhTw, "等待C缸稀釋加藥中", "Waiting for tank C to dilute")
          ElseIf .Command51.IsActive Or .Command57.IsActive Then
            StateString = If(.Language = LanguageValue.ZhTw, "等待B缸動作", "Waiting for Tank B")
          Else
            StateString = Translations.Translate("Waiting for C level stable")
          End If

        Case S68.Dose
          StateString = If(.Language = LanguageValue.ZhTw, "C缸計量加藥中 ", "Tank C dosing ") & TimerString(Timer.TimeRemaining)
          Static delay10 As New DelayTimer
                    DoseOutput = MinMax(((.IO.TankCLevel - SetPoint()) * .Parameters.DosingPumpSpeed), 0, 1000)
          DoseON = delay10.Run((DoseOutput > 0), 2)
          If Timer.Finished Then
            Timer.TimeRemaining = .Parameters.AddTransferTimeBeforeRinse
            DoseOutput = 1000
            State = S68.WaitAddFinish
          End If
          If .Parent.IsPaused Or Not .IO.MainPumpFB Then
            Timer.Pause()
            State = S68.Pause
          End If

        Case S68.WaitAddFinish
          StateString = If(.Language = LanguageValue.ZhTw, "等待C缸加藥延遲", "Tank C transferring ") & TimerString(Timer.TimeRemaining)
          If (.CTankLowLevel And .Parameters.C藥缸水位種類 = 0) Or (.Parameters.C藥缸水位種類 = 1 And .IO.CTankLow) Then Timer.TimeRemaining = .Parameters.AddTransferTimeBeforeRinse
          If Not Timer.Finished Then Exit Select
          State = S68.Wait
            Timer.TimeRemaining = 2

        Case S68.Wait
          StateString = If(.Language = LanguageValue.ZhTw, "等待C缸加藥延遲", "Tank C transferring ")
          If Timer.Finished Then
            清洗次數 = 清洗次數 - 1
            State = S68.Rinse1
            Timer.TimeRemaining = .Parameters.AddTransferRinseTime
          End If

        Case S68.Rinse1
          StateString = If(.Language = LanguageValue.ZhTw, "C缸洗缸中", "Tank C rinsing ") & TimerString(Timer.TimeRemaining)
          If Timer.Finished Then
            Timer.TimeRemaining = .Parameters.AddCirculateTimeAfterRinse
            State = S68.MixCir
          End If

        Case S68.MixCir
          StateString = If(.Language = LanguageValue.ZhTw, "B缸循環中", "Tank B circulating ") & TimerString(Timer.TimeRemaining)
          If Timer.Finished Then
            Timer.TimeRemaining = .Parameters.AddTransferTimeAfterRinse
            State = S68.Add
          End If

        Case S68.Add
          StateString = If(.Language = LanguageValue.ZhTw, "C缸加藥中", "Tank C transferring ") & TimerString(Timer.TimeRemaining)
          If (.CTankLowLevel And .Parameters.C藥缸水位種類 = 0) Or (.Parameters.C藥缸水位種類 = 1 And .IO.CTankLow) Then Timer.TimeRemaining = .Parameters.AddTransferTimeAfterRinse
          If Timer.Finished Then
            Timer.TimeRemaining = .Parameters.AddTransferRinseTime
            State = S68.Rinse2
            DoseOutput = 0
            If 清洗次數 >= 1 Then

              State = S68.WaitAddFinish
            End If
          End If

        Case S68.Rinse2
          StateString = If(.Language = LanguageValue.ZhTw, "C缸洗缸中", "Tank C rinsing ") & TimerString(Timer.TimeRemaining)
          If Timer.Finished Then
            Timer.TimeRemaining = .Parameters.AddTransferDrainTime
            If .Parameters.加藥完是否排水 = 1 Then
              State = S68.Off
            Else
              State = S68.Drain
            End If
          End If

        Case S68.Drain
          StateString = If(.Language = LanguageValue.ZhTw, "C缸排水中", "Tank C draining ") & TimerString(Timer.TimeRemaining)
          If (.CTankLowLevel And .Parameters.C藥缸水位種類 = 0) Or (.Parameters.C藥缸水位種類 = 1 And .IO.CTankLow) Then Timer.TimeRemaining = .Parameters.AddTransferDrainTime
          If Timer.Finished Then
            State = S68.Off
            .TankCReady = False
          End If

        Case S68.Pause
          StateString = If(.Language = LanguageValue.ZhTw, "暫停 ", "Paused ") & " " & TimerString(Timer.TimeRemaining)
          'no longer pause restart the timer and go back to the wait state
          If .Parent.CurrentStep <> .Parent.ChangingStep Then
            State = S68.Off
            Timer.Cancel()
          End If
          If (Not .Parent.IsPaused) And .IO.MainPumpFB Then
            Timer.Restart()
            State = S68.Dose
          End If

      End Select
    End With
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    With ControlCode
      State = S68.Off
      LevelTimer.Cancel()
    End With
  End Sub
  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged

  End Sub


  '=100*(1-(SQRT(1-(A5*A5*A5))))
  '<GraphTrace(0, 1000, 0, 10000, , )> _
  Public ReadOnly Property SetPoint() As Integer
    Get
      'If timer has finished, just return 0
      If Timer.Finished Then Return 0

      'Amount we should have transferred so far
      Dim elapsedTime = (AddTime - Timer.TimeRemaining) / AddTime
      Dim timeToGo = 1 - elapsedTime
      Dim linearTerm = elapsedTime
      Dim transferAmount = StartLevel * linearTerm

      'Calculate scaling factor (0-1) for progressive and digressive curves
      If AddCurve > 0 Then
        Dim scalingFactor = (10 - AddCurve) / 10
        'Calculate term for progressive transfer (0-1) if odd curve
        If (AddCurve Mod 2) = 1 Then
          Dim maxOddCurve = 1 - Math.Sqrt(1 - (elapsedTime * elapsedTime * elapsedTime))
          Dim oddTerm = (((9 - AddCurve) * elapsedTime) + ((AddCurve + 1) * maxOddCurve)) / 10
          transferAmount = StartLevel * oddTerm
        Else
          'Calculate term for digressive transfer (0-1) if even curve
          Dim maxEvenCurve = 1 - Math.Sqrt(1 - (timeToGo * timeToGo * timeToGo))
          Dim evenTerm = (((10 - AddCurve) * timeToGo) + (AddCurve * maxEvenCurve)) / 10
          transferAmount = StartLevel * (1 - evenTerm)
        End If
      End If

      'Calculate and limit to 0-1000
            Return Math.Min(Math.Max(0, StartLevel - CType(transferAmount, Integer)), 2000)
    End Get
  End Property

#Region "Standard Definitions"
  Private ReadOnly ControlCode As ControlCode
  Public Sub New(ByVal controlCode As ControlCode)
    Me.ControlCode = controlCode
  End Sub
  Friend ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
    Get
      Return State <> S68.Off
    End Get
  End Property
  Public ReadOnly Property IsActive() As Boolean
    Get
      Return State > S68.CheckReady
    End Get
  End Property
  Public ReadOnly Property IsWaitingForPrepare() As Boolean
    Get
      Return (State = S68.CheckReady)
    End Get

  End Property
  'this is for the dosing valve
  Public ReadOnly Property IsDosing() As Boolean
    Get
            Return ((State = S68.Dose) And DoseON) Or (State = S68.WaitAddFinish) Or (State = S68.Add)
    End Get
  End Property
  Public ReadOnly Property IsInject() As Boolean
    Get
      Return (DoseON) Or (State = S68.WaitAddFinish) Or (State = S68.Add) Or (State = S68.Rinse1) Or (State = S68.Rinse2)
    End Get
  End Property
  Public ReadOnly Property IsTransfer() As Boolean
    Get
      Return (State = S68.WaitAddFinish) Or (State = S68.Add)
    End Get
  End Property
  Public ReadOnly Property IsTransferPump() As Boolean
    Get
      Return (State = S68.Dose) Or (State = S68.WaitAddFinish) Or (State = S68.Add) Or (State = S68.MixCir)
    End Get
  End Property
  Public ReadOnly Property IsRinsing() As Boolean
    Get
      Return ((State = S68.Rinse1) Or (State = S68.Rinse2))
    End Get
  End Property
  Public ReadOnly Property IsDraining() As Boolean
    Get
      Return ((State = S68.Drain))
    End Get
  End Property
  Public ReadOnly Property IsCirculating() As Boolean
    Get
      Return (State = S68.MixCir)
    End Get
  End Property
  Public ReadOnly Property IsPaused() As Boolean
    Get
      Return State = S68.Pause
    End Get
  End Property
  <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S68
  <EditorBrowsable(EditorBrowsableState.Advanced)> Private statewas_ As S68
  Public Property State() As S68
    Get
      Return state_
    End Get
    Private Set(ByVal value As S68)
      state_ = value
    End Set
  End Property
  Public Property StateWas() As S68
    Get
      Return statewas_
    End Get
    Private Set(ByVal value As S68)
      statewas_ = value
    End Set
  End Property
#End Region
End Class

#Region "Class Instance"
Partial Public Class ControlCode
  Public ReadOnly Command68 As New Command68(Me)
End Class
#End Region
