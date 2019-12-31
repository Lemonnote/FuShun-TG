<Command("Cool Rinse", "Temperature |0-99|", , "'1", "10"), _
 TranslateCommand("zh-TW", "降溫水洗", "水洗溫度 |0-99|"), _
 Description("99=MAX 0=MIN"), _
 TranslateDescription("zh-TW", "99=最高 0=最小")> _
Public NotInheritable Class Command13
    Inherits MarshalByRefObject
    Implements ACCommand

    Public Enum S13
        Off
        WaitTempSafe
        WaitLowLevel
        WaitMainPumpFB
        WaitReachTemp
        Pause
    End Enum

    Public Wait As New Timer
    Public RinseTemp As New Integer
    Public FillByBTank As Boolean
    Public StateString As String

    Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
        With ControlCode
      .Command16.Cancel() : .Command02.Cancel() : .Command10.Cancel() : .Command03.Cancel() : .Command04.Cancel()
            .Command05.Cancel() : .Command06.Cancel() : .Command07.Cancel() : .Command08.Cancel()
            .Command09.Cancel() : .Command11.Cancel() : .Command12.Cancel() : .Command13.Cancel()
            .Command14.Cancel() : .Command15.Cancel() : .Command17.Cancel() : .Command18.Cancel()
            .Command19.Cancel() : .Command20.Cancel() : .Command21.Cancel() : .Command22.Cancel()
            .Command32.Cancel() : .Command40.Cancel() : .Command51.Cancel() : .Command52.Cancel()
            .Command54.Cancel() : .Command55.Cancel() : .Command57.Cancel() : .Command58.Cancel() : .Command39.Cancel() : .Command40.Cancel()
            .Command61.Cancel() : .Command62.Cancel() : .Command64.Cancel() : .Command65.Cancel()
            .Command61.Cancel() : .Command62.Cancel() : .Command64.Cancel() : .Command65.Cancel()
            .Command67.Cancel() : .Command68.Cancel() : .Command01.Cancel() : .Command90.Cancel()
            .TemperatureControl.Cancel()
            '--------------------------------------------------------------------------------------------------------PH用
            .PhControl.Cancel() : .PhWash.Cancel() : .PhControlFlag = False : .PhCirculateRun.Cancel() : .Command78.Cancel() : .Command80.Cancel()
            .Command73.Cancel() : .Command74.Cancel() : .Command75.Cancel() : .Command76.Cancel() : .Command77.Cancel() : .Command79.Cancel()
            '---------------------------------------------------------------------------------------------------------

            .TempControlFlag = False
            .TemperatureControlFlag = False

            RinseTemp = param(1) * 10
            State = S13.WaitTempSafe
        End With
    End Function

    Public Function Run() As Boolean Implements ACCommand.Run
        With ControlCode
            Select Case State
                Case S13.Off
                    StateString = ""

                Case S13.WaitTempSafe
                    If .Parent.IsPaused Or Not .IO.SystemAuto Then
                        Wait.Pause()
                        StateWas = State
                        State = S13.Pause
                    End If
                    StateString = If(.Language = LanguageValue.ZhTw, "溫度異常", "Interlocked Temperature")
                    If .IO.MainTemperature >= .Parameters.SetSafetyTemp * 10 Then
                        .Alarms.HighTempNoFill = True
                        Exit Select
                    End If

                    .Alarms.HighTempNoFill = False
                    '.IO.Cool = True
                    If RinseTemp = 0 Then
                        State = S13.Off
                    Else
                        State = S13.WaitLowLevel
                    End If

                Case S13.WaitLowLevel
                    If .Parent.IsPaused Or Not .IO.SystemAuto Then
                        Wait.Pause()
                        StateWas = State
                        State = S13.Pause
                    End If
                    StateString = If(.Language = LanguageValue.ZhTw, "主缸沒水", "Main tank no water ")
                    If Not .IO.LowLevel Then Exit Select
                    .PumpStartRequest = True
                    State = S13.WaitMainPumpFB

                Case S13.WaitMainPumpFB
                    If .Parent.IsPaused Or Not .IO.SystemAuto Then
                        Wait.Pause()
                        StateWas = State
                        State = S13.Pause
                    End If
                    StateString = If(.Language = LanguageValue.ZhTw, "主泵沒有運行", "Main pump not running")
                    If Not .IO.MainPumpFB Then Exit Select
                    .PumpStartRequest = False
                    .PumpOn = True
                    State = S13.WaitReachTemp

                Case S13.WaitReachTemp
                    If .Parent.IsPaused Or Not .IO.SystemAuto Then
                        Wait.Pause()
                        StateWas = State
                        State = S13.Pause
                    End If
                    StateString = If(.Language = LanguageValue.ZhTw, "主缸溢流洗至設定溫度", "Rinse to target temp ")
                    If Not (.IO.MainTemperature < RinseTemp) Then Exit Select
                    State = S13.Off

                Case S13.Pause
                    StateString = If(.Language = LanguageValue.ZhTw, "暫停", "Paused") & " " & TimerString(Wait.TimeRemaining)
                    'no longer pause restart the timer and go back to the wait state
                    If (Not .Parent.IsPaused) And .IO.SystemAuto Then
                        Wait.Restart()
                        State = StateWas
                        StateWas = S13.Off
                    End If

            End Select
        End With
    End Function

    Public Sub Cancel() Implements ACCommand.Cancel
        State = S13.Off
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
            Return State <> S13.Off
        End Get
    End Property

    <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S13
    <EditorBrowsable(EditorBrowsableState.Advanced)> Private statewas_ As S13
    Public Property State() As S13
        Get
            Return state_
        End Get
        Private Set(ByVal value As S13)
            state_ = value
        End Set
    End Property
    Public Property StateWas() As S13
        Get
            Return statewas_
        End Get
        Private Set(ByVal value As S13)
            statewas_ = value
        End Set
    End Property
    Public ReadOnly Property IsCoolFill() As Boolean
        Get
            Return (State = S13.WaitReachTemp)
        End Get
    End Property
    Public ReadOnly Property IsOverFlowDrain() As Boolean
        Get
            Return (State = S13.WaitReachTemp)
        End Get
    End Property
#End Region
End Class

#Region "Class Instance"
Partial Public Class ControlCode
  Public ReadOnly Command13 As New Command13(Me)
End Class
#End Region
