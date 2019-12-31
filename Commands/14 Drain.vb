<Command("Drain", "Drain Type |0-1|", , "30", "5"), _
 TranslateCommand("zh-TW", "主缸排水", "水管選擇 |0-2|"), _
 Description("2=HOT+COLD,1=HOT 0=COLD"), _
 TranslateDescription("zh-TW", "2=高低排+溢流,1=高排 0=低排")> _
 Public NotInheritable Class Command14
  Inherits MarshalByRefObject
  Implements ACCommand

  Public Enum S14
    Off
    WaitTime
    WaitTempSafe
    WaitDrain
    WaitTime5
    WaitNotEntanglement2
    WaitTime6
    WaitTime7
    WaitTime8
  End Enum

  Public Wait As New Timer
  Public StateString As String

  Public DrainType As Integer
  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode
      .Command16.Cancel() : .Command02.Cancel() : .Command10.Cancel() : .Command03.Cancel() : .Command04.Cancel()
      .Command05.Cancel() : .Command06.Cancel() : .Command11.Cancel() : .Command12.Cancel()
      .Command13.Cancel() : .Command14.Cancel() : .Command15.Cancel() : .Command20.Cancel() : .Command39.Cancel() : .Command40.Cancel()
      .Command32.Cancel() : .Command51.Cancel() : .Command52.Cancel() : .Command54.Cancel()
      .Command55.Cancel() : .Command57.Cancel() : .Command58.Cancel() : .Command90.Cancel()
      .Command01.Cancel() : .TemperatureControl.Cancel()
      '--------------------------------------------------------------------------------------------------------PH用
      .PhControl.Cancel() : .PhWash.Cancel() : .PhControlFlag = False : .PhCirculateRun.Cancel() : .Command78.Cancel() : .Command80.Cancel()
      .Command73.Cancel() : .Command74.Cancel() : .Command75.Cancel() : .Command76.Cancel() : .Command77.Cancel() : .Command79.Cancel()
      '---------------------------------------------------------------------------------------------------------
      .TempControlFlag = False
      .TemperatureControlFlag = False
      DrainType = MinMax(param(1), 0, 2)
      .PumpStopRequest = True
      Wait.TimeRemaining = 1
      State = S14.WaitTime
    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
    With ControlCode
      Select Case State
        Case S14.Off
          StateString = ""

        Case S14.WaitTime
          If Not Wait.Finished Then Exit Select
          .PumpStopRequest = False
          '.IO.PumpSpeedControl = 0
          .PumpOn = False
          State = S14.WaitTempSafe

        Case S14.WaitTempSafe
          StateString = If(.Language = LanguageValue.ZhTw, "溫度異常", "Interlocked Temperature")
          If .IO.MainTemperature >= .Parameters.SetSafetyTemp * 10 Then
            .Alarms.HighTempNoFill = True
            Exit Select
          End If

          .Alarms.HighTempNoDrain = False
          Wait.TimeRemaining = .Parameters.SetDrainSafetyTime * 60  ' after 10 minutes we maybe have a stuck LowLevel, so go on anyway
          State = S14.WaitDrain

        Case S14.WaitDrain
          StateString = If(.Language = LanguageValue.ZhTw, "排水", "Draining ") & TimerString(Wait.TimeRemaining)
          If .IO.DrainLevel And Not Wait.Finished Then Exit Select
          Wait.TimeRemaining = .Parameters.LevelWash
          State = S14.WaitNotEntanglement2


        Case S14.WaitNotEntanglement2
          StateString = If(.Language = LanguageValue.ZhTw, "類比水位尺清洗", "Draining ") & TimerString(Wait.TimeRemaining)
          If Not Wait.Finished Then Exit Select
          'If .IO.Entanglement1 Then Exit Select
          Wait.TimeRemaining = .Parameters.DrainDelay
          State = S14.WaitTime6


        Case S14.WaitTime6
          StateString = If(.Language = LanguageValue.ZhTw, "排水", "Draining ") & TimerString(Wait.TimeRemaining)
          If Not Wait.Finished Then Exit Select
          State = S14.Off
      End Select
    End With
  End Function
  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
    DrainType = MinMax(param(1), 0, 2)
  End Sub

  Public Sub Cancel() Implements ACCommand.Cancel
    State = S14.Off
    Wait.Cancel()
  End Sub

#Region "Standard Definitions"
  Private ReadOnly ControlCode As ControlCode
  Public Sub New(ByVal controlCode As ControlCode)
    Me.ControlCode = controlCode
  End Sub
  Friend ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
    Get
      Return State <> S14.Off
    End Get
  End Property

  <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S14
  Public Property State() As S14
    Get
      Return state_
    End Get
    Private Set(ByVal value As S14)
      state_ = value
    End Set
  End Property
  Public ReadOnly Property IsHotDrain() As Boolean
    Get
      Return ((DrainType = 1) Or (DrainType = 2)) AndAlso ((State = S14.WaitDrain) Or (State = S14.WaitNotEntanglement2) Or (State = S14.WaitTime6))
    End Get
  End Property
  Public ReadOnly Property IsColdDrain() As Boolean
    Get
      Return ((DrainType = 0) Or (DrainType = 2)) AndAlso ((State = S14.WaitDrain) Or (State = S14.WaitNotEntanglement2) Or (State = S14.WaitTime6))
    End Get
  End Property
  Public ReadOnly Property Is清洗水位尺() As Boolean
    Get
      Return (State = S14.WaitNotEntanglement2)
    End Get
  End Property
  Public ReadOnly Property IsOverflowDrain() As Boolean
    Get
      Return (DrainType = 2) AndAlso ((State = S14.WaitDrain) Or (State = S14.WaitNotEntanglement2) Or (State = S14.WaitTime6))
    End Get
  End Property
#End Region
End Class

#Region "Class Instance"
Partial Public Class ControlCode
  Public ReadOnly Command14 As New Command14(Me)
End Class
#End Region
