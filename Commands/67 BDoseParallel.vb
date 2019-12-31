<Command("B Dose Parallel", "Time |1-60| Curve |0-9|", , , "'1", CommandType.ParallelCommand), _
 TranslateCommand("zh-TW", "平行B加藥", "加藥時間 |1-60|分, 曲線選擇 |0-9|"), _
 Description("Time=1-60, Curve=0-9"), _
 TranslateDescription("zh-TW", "時間=1-60, 曲線=0-9")> _
Public NotInheritable Class Command67
  Inherits MarshalByRefObject
  Implements ACCommand
  Public StateString As String

  Public Enum S67
    Off
    WaitAuto
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
      .Command57.Cancel()
      AddTime = Maximum(param(1) * 60, 3600)
      AddCurve = param(2)
      清洗次數 = .Parameters.B藥缸清洗次數
      State = S67.WaitAuto
    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
    With ControlCode


      Select Case State
        Case S67.Off
          StateString = ""

        Case S67.WaitAuto
          StateString = If(.Language = LanguageValue.ZhTw, "系統手動中", "System Manual")
          If Not .IO.SystemAuto Then Exit Select
          State = S67.CheckSafetyTemp

        Case S67.CheckSafetyTemp
          StateString = If(.Language = LanguageValue.ZhTw, "溫度異常", "Interlocked Temperature")
          If .IO.MainTemperature >= .Parameters.SetSafetyTemp * 10 Then
            .Alarms.HighTempNoAdd = True
            Exit Select
          End If
          .Alarms.HighTempNoAdd = False
          State = S67.CheckReady


        Case S67.CheckReady

          If .Parent.IsPaused Or Not .IO.MainPumpFB Then
            Timer.Pause()
            State = S67.Pause
          End If
          If .IO.BTankReady And (Not (.Command54.IsOn Or .Command64.IsOn)) Then     '如果有備藥OK及其他沒用到藥缸的
            Timer.TimeRemaining = AddTime                   '將（加藥時間AddTime） 放到 （Timer.TimeRemaining）
            StartLevel = .IO.TankBLevel                     '將（藥缸水位.IO.TankBLevel） 放到 （StartLevel）
            State = S67.Dose
          End If

          'state string stuff.
          If Not .IO.BTankReady Then                             '如果沒備藥OK，將顯示"Tank B not prepared"，有備藥跳步驟
            StateString = If(.Language = LanguageValue.ZhTw, "B缸未備藥", "Tank B not prepared")
          ElseIf .Command54.IsOn Or .Command64.IsOn Then      '如果B缸備藥有使用，顯示"Waiting for Tank B"，不然跳步驟
            StateString = If(.Language = LanguageValue.ZhTw, "等待B缸備藥中", "Waiting for Tank B")
          End If

        Case S67.Dose
          StateString = If(.Language = LanguageValue.ZhTw, "B藥缸計量加藥 ", "Tank B dosing ") & TimerString(Timer.TimeRemaining)
          Static delay10 As New DelayTimer
                    DoseOutput = MinMax(((.IO.TankBLevel - SetPoint()) * .Parameters.DosingPumpSpeed), 0, 1000)
          DoseON = delay10.Run((DoseOutput > 0), 2)
          If Timer.Finished Then
            Timer.TimeRemaining = .Parameters.AddTransferTimeBeforeRinse
            DoseOutput = 1000
            State = S67.WaitAddFinish
          End If
          If .Parent.IsPaused Or Not .IO.MainPumpFB Then
            Timer.Pause()
            State = S67.Pause
          End If

        Case S67.WaitAddFinish
          StateString = If(.Language = LanguageValue.ZhTw, "B缸加藥延遲", "Tank B transferring ") & TimerString(Timer.TimeRemaining)
          If .BTankLowLevel Then Timer.TimeRemaining = .Parameters.AddTransferTimeBeforeRinse
          If Not Timer.Finished Then Exit Select
          Timer.TimeRemaining = 2
            State = S67.Wait

        Case S67.Wait
          StateString = If(.Language = LanguageValue.ZhTw, "B缸加藥延遲", "Tank B transferring ")
          If Timer.Finished Then
            清洗次數 = 清洗次數 - 1
            State = S67.Rinse1
            Timer.TimeRemaining = .Parameters.AddTransferRinseTime
          End If

        Case S67.Rinse1
          StateString = If(.Language = LanguageValue.ZhTw, "B缸洗缸中", "Tank B rinsing ") & TimerString(Timer.TimeRemaining)
          If Timer.Finished Then
            Timer.TimeRemaining = .Parameters.AddCirculateTimeAfterRinse
            State = S67.MixCir
          End If

        Case S67.MixCir
          StateString = If(.Language = LanguageValue.ZhTw, "B缸循環中", "Tank B circulating ") & TimerString(Timer.TimeRemaining)
          If Timer.Finished Then
            Timer.TimeRemaining = .Parameters.AddTransferTimeAfterRinse
            State = S67.Add
          End If

        Case S67.Add
          StateString = If(.Language = LanguageValue.ZhTw, "B缸加藥中", "Tank B transferring ") & TimerString(Timer.TimeRemaining)
          If .BTankLowLevel Then Timer.TimeRemaining = .Parameters.AddTransferTimeAfterRinse
          If Timer.Finished Then
            Timer.TimeRemaining = .Parameters.AddTransferRinseTime
            State = S67.Rinse2
            DoseOutput = 0
            If 清洗次數 >= 1 Then

              State = S67.WaitAddFinish
            End If
          End If

        Case S67.Rinse2
          StateString = If(.Language = LanguageValue.ZhTw, "B缸洗缸中", "Tank B rinsing ") & TimerString(Timer.TimeRemaining)
          If Timer.Finished Then
            Timer.TimeRemaining = .Parameters.AddTransferDrainTime
            If .Parameters.加藥完是否排水 = 1 Then
              State = S67.Off
            Else
              State = S67.Drain
            End If
          End If

        Case S67.Drain
          StateString = If(.Language = LanguageValue.ZhTw, "B缸排水", "Tank B draining ") & TimerString(Timer.TimeRemaining)
          If .BTankLowLevel Then Timer.TimeRemaining = .Parameters.AddTransferDrainTime
          If Timer.Finished Then
            State = S67.Off
            .TankBReady = False
          End If

        Case S67.Pause
          StateString = If(.Language = LanguageValue.ZhTw, "暫停 ", "Paused ") & " " & TimerString(Timer.TimeRemaining)
          'no longer pause restart the timer and go back to the wait state
          If .Parent.CurrentStep <> .Parent.ChangingStep Then
            State = S67.Off
            Timer.Cancel()
          End If
          If (Not .Parent.IsPaused) And .IO.MainPumpFB Then
            Timer.Restart()
            State = S67.Dose
          End If

      End Select
    End With
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    With ControlCode
      State = S67.Off
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
      Return State <> S67.Off
    End Get
  End Property
  Public ReadOnly Property IsActive() As Boolean
    Get
      Return State > S67.CheckReady
    End Get
  End Property
  Public ReadOnly Property IsWaitingForPrepare() As Boolean
    Get
      Return (State = S67.CheckReady)
    End Get

  End Property
  'this is for the dosing valve
  Public ReadOnly Property IsDosing() As Boolean
    Get
            Return ((State = S67.Dose) And DoseON) Or (State = S67.WaitAddFinish) Or (State = S67.Add)
    End Get
  End Property
  Public ReadOnly Property IsInject() As Boolean
    Get
      Return (State = S67.WaitAddFinish) Or (State = S67.Add) Or (State = S67.Dose) Or (State = S67.Rinse1) Or (State = S67.Rinse2)
    End Get
  End Property
  Public ReadOnly Property IsTransfer() As Boolean
    Get
      Return (State = S67.WaitAddFinish) Or (State = S67.Add)
    End Get
  End Property
  Public ReadOnly Property IsTransferPump() As Boolean
    Get
            Return (State = S67.Dose) Or (State = S67.WaitAddFinish) Or (State = S67.Add) Or (State = S67.MixCir)
    End Get
  End Property
  Public ReadOnly Property IsRinsing() As Boolean
    Get
      Return ((State = S67.Rinse1) Or (State = S67.Rinse2))
    End Get
  End Property
  Public ReadOnly Property IsDraining() As Boolean
    Get
      Return ((State = S67.Drain))
    End Get
  End Property
  Public ReadOnly Property IsCirculating() As Boolean
    Get
      Return (State = S67.MixCir)
    End Get
  End Property
  Public ReadOnly Property IsPaused() As Boolean
    Get
      Return State = S67.Pause
    End Get
  End Property
  <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S67
  <EditorBrowsable(EditorBrowsableState.Advanced)> Private statewas_ As S67
  Public Property State() As S67
    Get
      Return state_
    End Get
    Private Set(ByVal value As S67)
      state_ = value
    End Set
  End Property
  Public Property StateWas() As S67
    Get
      Return statewas_
    End Get
    Private Set(ByVal value As S67)
      statewas_ = value
    End Set
  End Property
#End Region
End Class

#Region "Class Instance"
Partial Public Class ControlCode
  Public ReadOnly Command67 As New Command67(Me)
End Class
#End Region
