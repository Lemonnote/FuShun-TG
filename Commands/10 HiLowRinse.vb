<Command("Hi Low Rinse", "Type |0-3| Time |1-99|", , "60", "'2"), _
 TranslateCommand("zh-TW", "高低水洗", "水源選擇 |0-3| 水洗時間 |0-99|"), _
 Description("0=COLD 1=HOT 2=COLD+HOT 3=A Tank   99=MAX 1=MIN"), _
 TranslateDescription("zh-TW", "0=冷水 1=熱水 2=冷+熱水 3=降溫,99=最高 0=最小")> _
Public NotInheritable Class Command10
  Inherits MarshalByRefObject
  Implements ACCommand

  Public Enum S10
    Off
    WaitAuto
    WaitTempSafe
    WaitMiddleLevel
    WaitTime3
    WaitMiddleLevel2
    WaitMiddleLevel3
    WaitTime4
    WaitMainPumpFB
    WaitHighLevel
    WaitDrainToMiddleLevel
    WaitTime5
    Pause
  End Enum

  Public Wait As New Timer
  Public RinseTime As Integer
  Public WaterType As Integer
  Public StateString As String

  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode
      .Command01.Cancel() : .Command16.Cancel() : .Command02.Cancel() : .Command10.Cancel() : .Command03.Cancel() : .Command04.Cancel()
      .Command05.Cancel() : .Command06.Cancel() : .Command07.Cancel() : .Command08.Cancel()
      .Command09.Cancel() : .Command11.Cancel() : .Command12.Cancel() : .Command13.Cancel()
      .Command14.Cancel() : .Command15.Cancel() : .Command17.Cancel() : .Command18.Cancel() : .Command39.Cancel() : .Command40.Cancel()
      .Command19.Cancel() : .Command20.Cancel() : .Command21.Cancel() : .Command22.Cancel()
      .Command32.Cancel() : .Command40.Cancel() : .Command51.Cancel() : .Command52.Cancel()
      .Command54.Cancel() : .Command55.Cancel() : .Command57.Cancel() : .Command58.Cancel()
      .Command61.Cancel() : .Command62.Cancel() : .Command64.Cancel() : .Command65.Cancel()
      .Command61.Cancel() : .Command62.Cancel() : .Command64.Cancel() : .Command65.Cancel()
      .TemperatureControl.Cancel()
      .TemperatureControlFlag = False

      '--------------------------------------------------------------------------------------------------------PH用
      .PhControl.Cancel() : .PhWash.Cancel() : .PhControlFlag = False : .PhCirculateRun.Cancel() : .Command78.Cancel() : .Command80.Cancel()
      .Command73.Cancel() : .Command74.Cancel() : .Command75.Cancel() : .Command76.Cancel() : .Command77.Cancel() : .Command79.Cancel()
      '---------------------------------------------------------------------------------------------------------

      WaterType = MinMax(param(1), 0, 3)
      RinseTime = param(2)
      State = S10.WaitAuto
    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
    With ControlCode
      Select Case State
        Case S10.Off
          StateString = ""

        Case S10.WaitAuto
          StateString = If(.Language = LanguageValue.ZhTw, "系統手動中", "System Manual")
          If Not .IO.SystemAuto Then Exit Select
          State = S10.WaitTempSafe

        Case S10.WaitTempSafe
          StateString = If(.Language = LanguageValue.ZhTw, "溫度異常", "Interlocked Temperature")
          If .IO.MainTemperature >= .Parameters.SetSafetyTemp * 10 Then
            .Alarms.HighTempNoFill = True
            Exit Select
          End If

          .Alarms.HighTempNoFill = False
          'If .WaterType = 0 Then
          ' .IO.HotFill = False
          ' .IO.ColdFill = True
          ' If .Parameters.CoolFillYes0No1 > 0 Then
          ' .IO.Cool = False
          ' Else
          ' .IO.Cool = True
          ' End If
          ' ElseIf .WaterType = 1 Then
          ' .IO.ColdFill = False
          ' .IO.HotFill = True
          '.IO.Cool = False
          'Else
          '.IO.ColdFill = True
          '.IO.HotFill = True
          '.IO.Cool = False
          'End If
          State = S10.WaitMiddleLevel

        Case S10.WaitMiddleLevel
          StateString = If(.Language = LanguageValue.ZhTw, "主缸進水至中水位", "Filling to middle level ")
          If Not .IO.MiddleLevel Then Exit Select
          Wait.TimeRemaining = 2
          State = S10.WaitTime3

        Case S10.WaitTime3
          StateString = If(.Language = LanguageValue.ZhTw, "主缸進水至中水位", "Filling to middle level ")
          If Not Wait.Finished Then Exit Select
          State = S10.WaitMiddleLevel2

        Case S10.WaitMiddleLevel2
          StateString = If(.Language = LanguageValue.ZhTw, "主缸進水至中水位", "Filling to middle level ")
          If Not .IO.MiddleLevel Then Exit Select
          State = S10.WaitMiddleLevel3

        Case S10.WaitMiddleLevel3
          StateString = If(.Language = LanguageValue.ZhTw, "主缸進水至中水位", "Filling to middle level ")
          'If Not .LowLevel Then Exit Select
          ' .IO.PumpSpeedControl = CType(.PumpSpeed * 10, Short)
          .PumpStartRequest = True
          Wait.TimeRemaining = 1
          State = S10.WaitTime4

        Case S10.WaitTime4
          StateString = If(.Language = LanguageValue.ZhTw, "主缸進水至中水位", "Filling to middle level ")
          If Not Wait.Finished Then Exit Select
          .PumpStartRequest = False
          .PumpOn = True
          State = S10.WaitMainPumpFB

        Case S10.WaitMainPumpFB
          StateString = If(.Language = LanguageValue.ZhTw, "主泵沒有運行", "Main pump not running")
          If Not .IO.MainPumpFB Then Exit Select
          State = S10.WaitHighLevel

        Case S10.WaitHighLevel
          StateString = If(.Language = LanguageValue.ZhTw, "主缸進水至高水位", "Filling to high level ") & TimerString(Wait.TimeRemaining)
          If Not .IO.HighLevel Then Exit Select
          '.OverRinse1.Start()
          Wait.TimeRemaining = 60 * RinseTime
          State = S10.WaitTime5

        Case S10.WaitDrainToMiddleLevel
          If .Parent.IsPaused Or Not .IO.MainPumpFB Then
            StateWas = State
            State = S10.Pause
            Wait.Pause()
          End If
          StateString = If(.Language = LanguageValue.ZhTw, "排水至中水位", "Draining to middle level ") & TimerString(Wait.TimeRemaining)
          If Not .IO.MiddleLevel Then
            State = S10.WaitTime5
          End If
          If Wait.Finished Then
            State = S10.Off
          End If

        Case S10.WaitTime5
          If .Parent.IsPaused Or Not .IO.MainPumpFB Then
            StateWas = State
            State = S10.Pause
            Wait.Pause()
          End If
          StateString = If(.Language = LanguageValue.ZhTw, "進水至高水位", "Filling to high level ") & TimerString(Wait.TimeRemaining)
          If Wait.Finished Then
            State = S10.Off
          End If
          If .IO.HighLevel Then State = S10.WaitDrainToMiddleLevel

        Case S10.Pause
          StateString = If(.Language = LanguageValue.ZhTw, "暫停", "Paused") & " " & TimerString(Wait.TimeRemaining)
          If Not .Parent.IsPaused And .IO.MainPumpFB Then
            State = StateWas
            StateWas = S10.Off
            Wait.Restart()
          End If

      End Select
    End With
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    State = S10.Off
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
      Return State <> S10.Off
    End Get
  End Property

  <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S10
  <EditorBrowsable(EditorBrowsableState.Advanced)> Private statewas_ As S10

  Public Property State() As S10
    Get
      Return state_
    End Get
    Private Set(ByVal value As S10)
      state_ = value
    End Set
  End Property
  Public Property StateWas() As S10
    Get
      Return statewas_
    End Get
    Private Set(ByVal value As S10)
      statewas_ = value
    End Set
  End Property
  Public ReadOnly Property IsFillHot() As Boolean
    Get
      Return (WaterType = 1 Or WaterType = 2) AndAlso ((State = S10.WaitMiddleLevel) Or (State = S10.WaitTime3) Or (State = S10.WaitMiddleLevel2) Or _
                   (State = S10.WaitMiddleLevel3) Or (State = S10.WaitTime4) Or (State = S10.WaitHighLevel) Or (State = S10.WaitTime5))
    End Get
  End Property
  Public ReadOnly Property IsFillCold() As Boolean
    Get
      Return (WaterType = 0 Or WaterType = 2) AndAlso ((State = S10.WaitMiddleLevel) Or (State = S10.WaitTime3) Or (State = S10.WaitMiddleLevel2) Or _
                   (State = S10.WaitMiddleLevel3) Or (State = S10.WaitTime4) Or (State = S10.WaitHighLevel) Or (State = S10.WaitTime5))
    End Get
  End Property
  Public ReadOnly Property IsFill3() As Boolean
    Get
      Return WaterType = 3 AndAlso ((State = S10.WaitMiddleLevel) Or (State = S10.WaitTime3) Or (State = S10.WaitMiddleLevel2) Or _
                   (State = S10.WaitMiddleLevel3) Or (State = S10.WaitTime4) Or (State = S10.WaitHighLevel) Or (State = S10.WaitTime5))
    End Get
  End Property
  Public ReadOnly Property IsDrain() As Boolean
    Get
      Return (State = S10.WaitDrainToMiddleLevel)
    End Get
  End Property
#End Region
End Class

#Region "Class Instance"
Partial Public Class ControlCode
  Public ReadOnly Command10 As New Command10(Me)
End Class
#End Region
