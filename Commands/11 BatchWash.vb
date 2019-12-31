<Command("Batch Wash", "Type |0-3| Times |1-9| Time |1-30|", , "60", "'2*'3"), _
 TranslateCommand("zh-TW", "批次水洗", "水源|0-3|,次數|1-9|次,時間|1-30|分"), _
 Description("0=COLD 1=HOT 2=COLD+HOT 3=CoolRinse,30=MAX 1=MIN,MAX=9 MIN=1"), _
 TranslateDescription("zh-TW", "0=冷水 1=熱水 2=冷+熱水 3=進水3,30分=最高 1分=最小,最高=9次 最小=1次")> _
Public NotInheritable Class Command11
  Inherits MarshalByRefObject
  Implements ACCommand

  Public Enum S11
    Off
    WaitAuto
    WaitTime
    WaitTempSafe
    StartWash
    WaitLevel
    WaitLowLevel
    WaitTime4
    WaitMainPumpFB
    WaitTime5
    WaitLevel2
    WaitTime6
    WaitTime7
    WaitTime8
    WaitTime9
    WaitDrain
    WaitTime10
    WaitTime11
    Pause
  End Enum

  Public Wait As New Timer
  Public MainTankLevel As Integer
  Public WashTime As Integer
  Public WashesToGo As Integer
  Public WaterType As Integer
  Public StateString As String

  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode
      .Command16.Cancel() : .Command02.Cancel() : .Command10.Cancel() : .Command03.Cancel() : .Command04.Cancel()
      .Command05.Cancel() : .Command06.Cancel() : .Command07.Cancel() : .Command08.Cancel()
      .Command09.Cancel() : .Command11.Cancel() : .Command12.Cancel() : .Command13.Cancel()
      .Command14.Cancel() : .Command15.Cancel() : .Command17.Cancel() : .Command18.Cancel()
      .Command19.Cancel() : .Command20.Cancel() : .Command21.Cancel() : .Command22.Cancel() : .Command39.Cancel() : .Command40.Cancel()
      .Command32.Cancel() : .Command40.Cancel() : .Command51.Cancel() : .Command52.Cancel()
      .Command54.Cancel() : .Command55.Cancel() : .Command57.Cancel() : .Command58.Cancel()
      .Command61.Cancel() : .Command62.Cancel() : .Command64.Cancel() : .Command65.Cancel()
      .Command61.Cancel() : .Command62.Cancel() : .Command64.Cancel() : .Command65.Cancel()
      .Command67.Cancel() : .Command68.Cancel() : .Command01.Cancel() : .Command90.Cancel()
      .TemperatureControl.Cancel()
      '--------------------------------------------------------------------------------------------------------PH用
      .PhControl.Cancel() : .PhWash.Cancel() : .PhControlFlag = False : .PhCirculateRun.Cancel() : .Command78.Cancel() : .Command80.Cancel()
      .Command73.Cancel() : .Command74.Cancel() : .Command75.Cancel() : .Command76.Cancel() : .Command77.Cancel() : .Command79.Cancel()
      '---------------------------------------------------------------------------------------------------------


      .TemperatureControlFlag = False
      WaterType = MinMax(param(1), 0, 3)
      WashTime = Maximum(param(3), 30)
      WashesToGo = Maximum(param(2), 9)
      MainTankLevel = 1 '中水位

      .PumpStopRequest = True
      Wait.TimeRemaining = 1
      State = S11.WaitAuto
    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
    With ControlCode
      Select Case State

        Case S11.Off
          StateString = ""

        Case S11.WaitAuto
          StateString = If(.Language = LanguageValue.ZhTw, "系統手動中", "System Manual")
          If Not .IO.SystemAuto Then Exit Select
          State = S11.WaitTime

        Case S11.WaitTime
          StateString = If(.Language = LanguageValue.ZhTw, "停止主泵", "Stopping pump")
          If Not Wait.Finished Then Exit Select
          .PumpStopRequest = False
          '.IO.PumpSpeedControl = CType(.PumpSpeed * 10, Short)
          .PumpOn = False
          State = S11.WaitTempSafe

        Case S11.WaitTempSafe
          StateString = If(.Language = LanguageValue.ZhTw, "溫度異常", "Interlocked Temperature")
          If .IO.MainTemperature >= .Parameters.SetSafetyTemp * 10 Then
            .Alarms.HighTempNoFill = True
            Exit Select
          End If

          .Alarms.HighTempNoFill = False
          State = S11.StartWash

        Case S11.StartWash
          State = S11.WaitLevel

        Case S11.WaitLevel
          If MainTankLevel = 0 Then
            StateString = If(.Language = LanguageValue.ZhTw, "主缸進水至低水位", "Filling to low level ")
            If Not .IO.LowLevel Then Exit Select
          ElseIf MainTankLevel = 1 Then
            StateString = If(.Language = LanguageValue.ZhTw, "主缸進水至中水位", "Filling to middle level ")
            If Not .IO.MiddleLevel Then Exit Select
          ElseIf MainTankLevel = 2 Then
            StateString = If(.Language = LanguageValue.ZhTw, "主缸進水至高水位", "Filling to high level ")
            If Not .IO.HighLevel Then Exit Select
          End If
          State = S11.WaitLowLevel

        Case S11.WaitLowLevel
          StateString = If(.Language = LanguageValue.ZhTw, "主缸進水", "Level to low")
          If Not .IO.LowLevel Then Exit Select
          '.IO.PumpSpeedControl = CType(.PumpSpeed * 10, Short)
          .PumpStartRequest = True
          Wait.TimeRemaining = 1
          State = S11.WaitTime4

        Case S11.WaitTime4
          If Not Wait.Finished Then Exit Select
          .PumpStartRequest = False
          .PumpOn = True
          State = S11.WaitMainPumpFB

        Case S11.WaitMainPumpFB
          StateString = If(.Language = LanguageValue.ZhTw, "主泵沒有運行", "Main pump not running")
          If Not .IO.MainPumpFB Then Exit Select
          Wait.TimeRemaining = 2
          State = S11.WaitTime5

        Case S11.WaitTime5
          If Not Wait.Finished Then Exit Select
          State = S11.WaitLevel2

        Case S11.WaitLevel2
          StateString = If(.Language = LanguageValue.ZhTw, "主缸進水", "Level to low")
          If MainTankLevel = 0 Then
            If Not .IO.LowLevel Then Exit Select
          ElseIf MainTankLevel = 1 Then
            If Not .IO.MiddleLevel Then Exit Select
          Else
            If Not .IO.HighLevel Then Exit Select
          End If
          Wait.TimeRemaining = 2
          State = S11.WaitTime6

        Case S11.WaitTime6
          If Not Wait.Finished Then Exit Select
          '.IO.ColdFill = False
          '.IO.HotFill = False
          '.IO.Cool = False
          Wait.TimeRemaining = WashTime * 60 ' TODO: should be 60
          State = S11.WaitTime7

        Case S11.WaitTime7
          If .Parent.IsPaused Or Not .IO.MainPumpFB Then
            StateWas = State
            State = S11.Pause
            Wait.Pause()
          End If

          StateString = If(.Language = LanguageValue.ZhTw, "水洗中", "Washing ") & TimerString(Wait.TimeRemaining)
          If Not Wait.Finished Then Exit Select
          .PumpStopRequest = True
          Wait.TimeRemaining = 1
          State = S11.WaitTime8

        Case S11.WaitTime8
          If Not Wait.Finished Then Exit Select
          .PumpStopRequest = False
          '.IO.PumpSpeedControl = 0
          .PumpOn = False
          '.IO.Overflow = True
          '.IO.HotDrain = True
          '.IO.Drain = True

          'Wait.TimeRemaining = 600 ' after 10 minutes we maybe have a stuck LowLevel, so go on anyway
          State = S11.WaitDrain

        Case S11.WaitDrain
          StateString = If(.Language = LanguageValue.ZhTw, "排水到低水位", "Draining to low level") & TimerString(Wait.TimeRemaining)
          If .IO.LowLevel Then Exit Select 'And Not Wait.Finished 

          '.IO.Overflow = False
          '.IO.HotDrain = False
          '.IO.Drain = False
          WashesToGo = WashesToGo - 1
          If WashesToGo > 0 Then
            State = S11.StartWash
            Exit Select
          End If
          ' .IO.HotDrain = True
          ' .IO.Drain = True
          State = S11.WaitTime10

        Case S11.WaitTime10
          If .IO.DrainLevel Then Exit Select
          Wait.TimeRemaining = .Parameters.DrainDelay
          State = S11.WaitTime11

        Case S11.WaitTime11
          StateString = If(.Language = LanguageValue.ZhTw, "排水到排水水位", "Draining to drain level") & TimerString(Wait.TimeRemaining)
          If Not Wait.Finished Then Exit Select
          '.IO.HotDrain = False
          '.IO.Drain = False
          State = S11.Off

        Case S11.Pause
          StateString = If(.Language = LanguageValue.ZhTw, "暫停", "Paused") & " " & TimerString(Wait.TimeRemaining)
          If Not .Parent.IsPaused And .IO.MainPumpFB Then
            State = StateWas
            StateWas = S11.Off
            Wait.Restart()
          End If
      End Select
    End With
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    State = S11.Off
    Wait.Cancel()
  End Sub

  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged

  End Sub

#Region "Standard Definitions"
  Private ReadOnly ControlCode As ControlCode
  Public Sub New(ByVal controlCode As ControlCode)
    Me.ControlCode = controlCode
  End Sub
  Friend ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
    Get
      Return State <> S11.Off
    End Get
  End Property

  <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S11
  <EditorBrowsable(EditorBrowsableState.Advanced)> Private statewas_ As S11
  Public Property State() As S11
    Get
      Return state_
    End Get
    Private Set(ByVal value As S11)
      state_ = value
    End Set
  End Property
  Public Property StateWas() As S11
    Get
      Return statewas_
    End Get
    Private Set(ByVal value As S11)
      statewas_ = value
    End Set
  End Property
  Public ReadOnly Property IsFillHot() As Boolean
    Get
      Return (WaterType = 1 Or WaterType = 2) AndAlso ((State = S11.WaitLevel) Or (State = S11.WaitLowLevel) Or _
       (State = S11.WaitTime4) Or (State = S11.WaitMainPumpFB) Or (State = S11.WaitTime5) Or _
      (State = S11.WaitLevel2) Or (State = S11.WaitTime6))
    End Get
  End Property
  Public ReadOnly Property IsFillCold() As Boolean
    Get
      Return (WaterType = 0 Or WaterType = 2) AndAlso ((State = S11.WaitLevel) Or (State = S11.WaitLowLevel) Or _
       (State = S11.WaitTime4) Or (State = S11.WaitMainPumpFB) Or (State = S11.WaitTime5) Or _
      (State = S11.WaitLevel2) Or (State = S11.WaitTime6))
    End Get
  End Property
  Public ReadOnly Property IsCoolFill() As Boolean
    Get
      Return WaterType = 3 AndAlso ((State = S11.WaitLevel) Or (State = S11.WaitLowLevel) Or _
 (State = S11.WaitTime4) Or (State = S11.WaitMainPumpFB) Or (State = S11.WaitTime5) Or _
(State = S11.WaitLevel2) Or (State = S11.WaitTime6))
    End Get
  End Property
  Public ReadOnly Property IsDrainingToLowLevel() As Boolean
    Get
      Return (State = S11.WaitDrain) Or (State = S11.WaitTime10)
    End Get
  End Property
  Public ReadOnly Property IsDrainingEmpty() As Boolean
    Get
      Return (State = S11.WaitTime11)
    End Get
  End Property
#End Region
End Class

#Region "Class Instance"
Partial Public Class ControlCode
  Public ReadOnly Command11 As New Command11(Me)
End Class
#End Region
