<Command("Add Dose B", "Time |0-60| Curve |0-9|", , , "'1"), _
Description("Time=1-60, Curve=0-9"), _
 TranslateCommand("zh-TW", "B計量加藥", "加藥時間 |0-60|分, 曲線選擇 |0-9|"), _
TranslateDescription("zh-TW", "時間=1-60, 曲線=0-9")> _
Public NotInheritable Class Command57
  Inherits MarshalByRefObject
  Implements ACCommand
  Public StateString As String

  Public Enum S57
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
    Public 保存紀錄1 As Integer
    Public 保存紀錄2 As Integer
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
      'End If
      AddTime = Maximum(param(1) * 60, 3600)
      AddCurve = param(2)
      清洗次數 = .Parameters.B藥缸清洗次數
      State = S57.WaitAuto
    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
    With ControlCode


      Select Case State
        Case S57.Off
          StateString = ""

        Case S57.WaitAuto
          StateString = If(.Language = LanguageValue.ZhTw, "系統手動中", "System Manual")
          If Not .IO.SystemAuto Then Exit Select
          State = S57.CheckSafetyTemp

        Case S57.CheckSafetyTemp
          StateString = If(.Language = LanguageValue.ZhTw, "溫度異常", "Interlocked Temperature")
          If .IO.MainTemperature >= .Parameters.SetSafetyTemp * 10 Then
            .Alarms.HighTempNoAdd = True
            Exit Select
          End If
          .Alarms.HighTempNoAdd = False
          State = S57.CheckReady

        Case S57.CheckReady
                    保存紀錄1 = AddTime
                    保存紀錄2 = AddCurve
          If .TankBReady And Not .Command64.IsOn Then     '如果有備藥OK及其他沒用到藥缸的
            Timer.TimeRemaining = AddTime                   '將（加藥時間AddTime） 放到 （Timer.TimeRemaining）
            StartLevel = .IO.TankBLevel                     '將（藥缸水位.IO.TankBLevel） 放到 （StartLevel）
            State = S57.Dose
          End If

          'state string stuff.
          If Not .TankBReady Then                             '如果沒備藥OK，將顯示"Tank B not prepared"，有備藥跳步驟
            StateString = If(.Language = LanguageValue.ZhTw, "B缸未備藥", "Tank B not prepared")
          ElseIf .Command54.IsOn Or .Command64.IsOn Then      '如果B缸備藥有使用，顯示"Waiting for Tank B"，不然跳步驟
            StateString = If(.Language = LanguageValue.ZhTw, "等待B缸備藥中", "Waiting for Tank B")
          End If

        Case S57.Dose
          StateString = If(.Language = LanguageValue.ZhTw, "B藥缸計量加藥 ", "Tank B dosing ") & TimerString(Timer.TimeRemaining)
          Static delay10 As New DelayTimer
                    DoseOutput = MinMax(((.IO.TankBLevel - SetPoint()) * .Parameters.DosingPumpSpeed), 0, 1000)
          DoseON = delay10.Run((DoseOutput > 0), 2)
          If Timer.Finished Then
            DoseOutput = 1000
            Timer.TimeRemaining = .Parameters.AddTransferTimeBeforeRinse
            State = S57.WaitAddFinish
          End If
          If .Parent.IsPaused Or Not .IO.MainPumpFB Then
            Timer.Pause()
            State = S57.Pause
          End If

        Case S57.WaitAddFinish
          StateString = If(.Language = LanguageValue.ZhTw, "B缸加藥延遲", "Tank B transferring ") & TimerString(Timer.TimeRemaining)
          If .BTankLowLevel Then Timer.TimeRemaining = .Parameters.AddTransferTimeBeforeRinse
          If Not Timer.Finished Then Exit Select
          Timer.TimeRemaining = 2
            State = S57.Wait

        Case S57.Wait
          StateString = If(.Language = LanguageValue.ZhTw, "B缸加藥延遲", "Tank B transferring ")
          If Timer.Finished Then
            清洗次數 = 清洗次數 - 1
            State = S57.Rinse1
            Timer.TimeRemaining = .Parameters.AddTransferRinseTime
          End If

        Case S57.Rinse1
          StateString = If(.Language = LanguageValue.ZhTw, "B缸洗缸中", "Tank B rinsing ") & TimerString(Timer.TimeRemaining)
          If Timer.Finished Then
            Timer.TimeRemaining = .Parameters.AddCirculateTimeAfterRinse
            State = S57.MixCir
          End If

        Case S57.MixCir
          StateString = If(.Language = LanguageValue.ZhTw, "B缸循環中", "Tank B circulating ") & TimerString(Timer.TimeRemaining)
          If Timer.Finished Then
            Timer.TimeRemaining = .Parameters.AddTransferTimeAfterRinse
            State = S57.Add
          End If

        Case S57.Add
          StateString = If(.Language = LanguageValue.ZhTw, "B缸加藥中", "Tank B transferring ") & TimerString(Timer.TimeRemaining)
          If .BTankLowLevel Then Timer.TimeRemaining = .Parameters.AddTransferTimeAfterRinse
          If Timer.Finished Then
            Timer.TimeRemaining = .Parameters.AddTransferRinseTime
            State = S57.Rinse2
            DoseOutput = 0
            If 清洗次數 >= 1 Then

              State = S57.WaitAddFinish
            End If
          End If

        Case S57.Rinse2
          StateString = If(.Language = LanguageValue.ZhTw, "B缸洗缸中", "Tank B rinsing ") & TimerString(Timer.TimeRemaining)
          If Timer.Finished Then

            Timer.TimeRemaining = .Parameters.AddTransferDrainTime
            If .Parameters.加藥完是否排水 = 1 Then
              State = S57.Off
            Else
              State = S57.Drain
            End If

          End If

        Case S57.Drain
          StateString = If(.Language = LanguageValue.ZhTw, "B缸排水", "Tank B draining ") & TimerString(Timer.TimeRemaining)
          If .BTankLowLevel Then Timer.TimeRemaining = .Parameters.AddTransferDrainTime
          If Timer.Finished Then
            State = S57.Off
            .TankBReady = False
          End If

        Case S57.Pause
          StateString = If(.Language = LanguageValue.ZhTw, "暫停 ", "Paused ") & " " & TimerString(Timer.TimeRemaining)
          'no longer pause restart the timer and go back to the wait state
          If (Not .Parent.IsPaused) And .IO.MainPumpFB Then
            Timer.Restart()
            State = S57.Dose
          End If

      End Select
    End With
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    With ControlCode
      State = S57.Off
      LevelTimer.Cancel()
    End With
  End Sub

  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
        AddTime = Maximum(param(1) * 60, 3600)
        AddCurve = param(2)
        If 保存紀錄1 <> AddTime Or 保存紀錄2 <> AddCurve Then
            保存紀錄1 = AddTime
            保存紀錄2 = AddCurve
            Timer.TimeRemaining = AddTime
            State = S57.Dose
        End If
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
      Return State <> S57.Off
    End Get
  End Property
  Public ReadOnly Property IsActive() As Boolean
    Get
      Return State > S57.CheckReady
    End Get
  End Property
  Public ReadOnly Property IsWaitingForPrepare() As Boolean
    Get
      Return (State = S57.CheckReady)
    End Get

  End Property
  'this is for the dosing valve
  Public ReadOnly Property IsDosing() As Boolean
    Get
            Return ((State = S57.Dose) And DoseON) Or (State = S57.WaitAddFinish) Or (State = S57.Add)
    End Get
  End Property
  Public ReadOnly Property IsInject() As Boolean
    Get
      Return (State = S57.WaitAddFinish) Or (State = S57.Add) Or (State = S57.Dose) Or (State = S57.Rinse1) Or (State = S57.Rinse2)
    End Get
  End Property
  Public ReadOnly Property IsTransfer() As Boolean
    Get
      Return (State = S57.WaitAddFinish) Or (State = S57.Add)
    End Get
  End Property
  Public ReadOnly Property IsTransferPump() As Boolean
    Get
      Return (State = S57.Dose) Or (State = S57.WaitAddFinish) Or (State = S57.Add) Or (State = S57.MixCir)
    End Get
  End Property
  Public ReadOnly Property IsRinsing() As Boolean
    Get
      Return ((State = S57.Rinse1) Or (State = S57.Rinse2))
    End Get
  End Property
  Public ReadOnly Property IsDraining() As Boolean
    Get
      Return ((State = S57.Drain))
    End Get
  End Property
  Public ReadOnly Property IsCirculating() As Boolean
    Get
      Return (State = S57.MixCir)
    End Get
  End Property
  Public ReadOnly Property IsPaused() As Boolean
    Get
      Return State = S57.Pause
    End Get
  End Property
  <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S57
  <EditorBrowsable(EditorBrowsableState.Advanced)> Private statewas_ As S57
  Public Property State() As S57
    Get
      Return state_
    End Get
    Private Set(ByVal value As S57)
      state_ = value
    End Set
  End Property
  Public Property StateWas() As S57
    Get
      Return statewas_
    End Get
    Private Set(ByVal value As S57)
      statewas_ = value
    End Set
  End Property
#End Region
End Class

#Region "Class Instance"
Partial Public Class ControlCode
  Public ReadOnly Command57 As New Command57(Me)
End Class
#End Region
