<Command("Add Dose C", "Time |0-60| Curve |0-9|", , , "'1"), _
Description("Time=1-60, Curve=0-9"), _
TranslateCommand("zh-TW", "C計量加藥", "加藥時間 |1-60| 曲線選擇 |0-9|"), _
TranslateDescription("zh-TW", "時間=1-60, 曲線=0-9")> _
Public NotInheritable Class Command58
  Inherits MarshalByRefObject
  Implements ACCommand
  Public StateString As String

  Public Enum S58
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
      .Command01.Cancel() : .Command02.Cancel() : .Command10.Cancel() : .Command03.Cancel() : .Command04.Cancel()
            .Command05.Cancel() : .Command06.Cancel() : .Command07.Cancel() : .Command08.Cancel()
            .Command09.Cancel() : .Command11.Cancel() : .Command12.Cancel() : .Command13.Cancel()
            .Command14.Cancel() : .Command15.Cancel() : .Command17.Cancel() : .Command18.Cancel()
            .Command19.Cancel() : .Command20.Cancel() : .Command21.Cancel() : .Command22.Cancel()
            .Command32.Cancel() : .Command40.Cancel() : .Command51.Cancel() : .Command52.Cancel()
            .Command54.Cancel() : .Command55.Cancel() : .Command57.Cancel() : .Command58.Cancel()
            .Command61.Cancel() : .Command62.Cancel() : .Command64.Cancel() : .Command65.Cancel()
            .Command61.Cancel() : .Command62.Cancel() : .Command64.Cancel() : .Command65.Cancel()
            .Command67.Cancel() : .Command68.Cancel() : .Command90.Cancel()
      ' If ((.TemperatureControl.TempFinalTemp - .IO.MainTemperature) > 20 Or (.IO.MainTemperature - .TemperatureControl.TempFinalTemp) > 20) And .HeatNow Then
      .TemperatureControl.Cancel()
      .TemperatureControlFlag = False
      '  End If

      AddTime = Maximum(param(1) * 60, 3600)
      AddCurve = param(2)
      Timer.TimeRemaining = 10
      清洗次數 = .Parameters.C藥缸清洗次數
      State = S58.CheckReady
    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
    With ControlCode


      Select Case State
        Case S58.Off
          StateString = ""

        Case S58.CheckReady

          If .TankCReady And (Not (.Command55.IsOn Or .Command65.IsOn Or .Command51.IsOn Or .Command54.IsOn)) And Timer.Finished Then
            Timer.TimeRemaining = AddTime
            StartLevel = .IO.TankCLevel
            .DosingHoldTemperature = .IO.MainTemperature
            State = S58.Dose
          End If

          'state string stuff.
          If Not .TankCReady Then
            StateString = Translations.Translate("Tank C not prepared")
          ElseIf .Command55.IsOn Or .Command65.IsOn Then
            StateString = Translations.Translate("Waiting for tank C")
          ElseIf .Command52.IsActive Then
            StateString = Translations.Translate("Waiting for tank C to dilute")
          ElseIf .Command51.IsActive Or .Command57.IsActive Then
            StateString = Translations.Translate("Waiting for Tank B")
          Else
            StateString = Translations.Translate("Waiting for C level stable")
          End If

        Case S58.Dose
          StateString = Translations.Translate("Tank C dosing") & " " & TimerString(Timer.TimeRemaining)
          Static delay10 As New DelayTimer
                    DoseOutput = MinMax(((.IO.TankCLevel - SetPoint()) * .Parameters.DosingPumpSpeed), 0, 1000)
          DoseON = delay10.Run((DoseOutput > 0), 2)
          If Timer.Finished Or (Not .IO.CTankLow And .IO.TankCLevel < 30) Then
            DoseOutput = 1000
            Timer.TimeRemaining = .Parameters.AddTransferTimeBeforeRinse
            State = S58.WaitAddFinish
          End If
          If .Parent.IsPaused Or Not .IO.MainPumpFB Then
            Timer.Pause()
            State = S58.Pause
          End If

        Case S58.WaitAddFinish
          StateString = Translations.Translate("Tank C transferring") & " " & TimerString(Timer.TimeRemaining)
          If ((.CTankLowLevel And .Parameters.C藥缸水位種類 = 0) Or (.Parameters.C藥缸水位種類 = 1 And .IO.CTankLow)) Then Timer.TimeRemaining = .Parameters.AddTransferTimeBeforeRinse
          If Not Timer.Finished Then Exit Select
          Timer.TimeRemaining = 2
            State = S58.Wait

        Case S58.Wait
          StateString = Translations.Translate("Tank C transferring")
          If Timer.Finished Then
            清洗次數 = 清洗次數 - 1
            State = S58.Rinse1
            Timer.TimeRemaining = .Parameters.AddTransferRinseTime
          End If

        Case S58.Rinse1
          StateString = Translations.Translate("Tank C rinsing") & " " & TimerString(Timer.TimeRemaining)
          If Timer.Finished Then
            Timer.TimeRemaining = .Parameters.AddCirculateTimeAfterRinse
            State = S58.MixCir
          End If

        Case S58.MixCir
          StateString = Translations.Translate("Tank C circulating") & " " & TimerString(Timer.TimeRemaining)
          If Timer.Finished Then
            Timer.TimeRemaining = .Parameters.AddTransferTimeAfterRinse
            State = S58.Add
          End If

        Case S58.Add
          StateString = Translations.Translate("Tank C transferring") & " " & TimerString(Timer.TimeRemaining)
          If (.CTankLowLevel And .Parameters.C藥缸水位種類 = 0) Or (.Parameters.C藥缸水位種類 = 1 And .IO.CTankLow) Then Timer.TimeRemaining = .Parameters.AddTransferTimeAfterRinse
          If Timer.Finished Then
            Timer.TimeRemaining = .Parameters.AddTransferRinseTime
            State = S58.Rinse2
            DoseOutput = 0
            If 清洗次數 >= 1 Then

              State = S58.WaitAddFinish
            End If
          End If

        Case S58.Rinse2
          StateString = Translations.Translate("Tank C rinsing") & " " & TimerString(Timer.TimeRemaining)
          If Timer.Finished Then
            Timer.TimeRemaining = .Parameters.AddTransferDrainTime
            If .Parameters.加藥完是否排水 = 1 Then
              State = S58.Off
            Else
              State = S58.Drain
            End If
          End If

        Case S58.Drain
          StateString = Translations.Translate("Tank C draining") & " " & TimerString(Timer.TimeRemaining)
          If (.CTankLowLevel And .Parameters.C藥缸水位種類 = 0) Or (.Parameters.C藥缸水位種類 = 1 And .IO.CTankLow) Then Timer.TimeRemaining = .Parameters.AddTransferDrainTime
          If Timer.Finished Then
            State = S58.Off
            .TankCReady = False
          End If

        Case S58.Pause
          StateString = Translations.Translate("Paused") & " " & TimerString(Timer.TimeRemaining)
          'no longer pause restart the timer and go back to the wait state
          If (Not .Parent.IsPaused) And .IO.MainPumpFB Then
            Timer.Restart()
            State = S58.Dose
          End If

      End Select
    End With
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    With ControlCode
      State = S58.Off
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
      Return State <> S58.Off
    End Get
  End Property
  Public ReadOnly Property IsActive() As Boolean
    Get
      Return State > S58.CheckReady
    End Get
  End Property
  Public ReadOnly Property IsWaitingForPrepare() As Boolean
    Get
      Return (State = S58.CheckReady)
    End Get

  End Property
  'this is for the dosing valve
  Public ReadOnly Property IsDosing() As Boolean
    Get
            Return ((State = S58.Dose) And DoseON) Or (State = S58.WaitAddFinish) Or (State = S58.Add)
    End Get
  End Property
  Public ReadOnly Property IsInject() As Boolean
    Get
      Return (State = S58.WaitAddFinish) Or (State = S58.Add) Or (State = S58.Dose) Or (State = S58.Rinse1) Or (State = S58.Rinse2)
    End Get
  End Property
  Public ReadOnly Property IsTransfer() As Boolean
    Get
      Return (State = S58.WaitAddFinish) Or (State = S58.Add)
    End Get
  End Property
  Public ReadOnly Property IsTransferPump() As Boolean
    Get
      Return (State = S58.Dose) Or (State = S58.WaitAddFinish) Or (State = S58.Add) Or (State = S58.MixCir)
    End Get
  End Property
  Public ReadOnly Property IsRinsing() As Boolean
    Get
      Return ((State = S58.Rinse1) Or (State = S58.Rinse2))
    End Get
  End Property
  Public ReadOnly Property IsDraining() As Boolean
    Get
      Return ((State = S58.Drain))
    End Get
  End Property
  Public ReadOnly Property IsCirculating() As Boolean
    Get
      Return (State = S58.MixCir)
    End Get
  End Property
  Public ReadOnly Property IsPaused() As Boolean
    Get
      Return State = S58.Pause
    End Get
  End Property
  <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S58
  <EditorBrowsable(EditorBrowsableState.Advanced)> Private statewas_ As S58
  Public Property State() As S58
    Get
      Return state_
    End Get
    Private Set(ByVal value As S58)
      state_ = value
    End Set
  End Property
  Public Property StateWas() As S58
    Get
      Return statewas_
    End Get
    Private Set(ByVal value As S58)
      statewas_ = value
    End Set
  End Property
#End Region
End Class

#Region "Class Instance"
Partial Public Class ControlCode
  Public ReadOnly Command58 As New Command58(Me)
End Class
#End Region
