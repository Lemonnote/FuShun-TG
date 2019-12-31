<Command("Analog Fill", "Type|0-2| Qty|1-9999|L", , "30", "5"), _
 TranslateCommand("zh-TW", "類比進水", "水源|0-2| 水量|1-9999|公升"), _
 Description("0=COLD 1=HOT 2=COLD+HOT"), _
 TranslateDescription("zh-TW", "0=冷水 1=熱水 2=冷+熱水")> _
Public NotInheritable Class Command17
    Inherits MarshalByRefObject
    Implements ACCommand

    Public Enum S17
        Off
        WaitTempSafe
        WaitAuto
        WaitResetCounter
        WaitWater1
        WaitStable1
        WaitWater2
        WaitStable2
        WaitLowLevel
        WaitTime
        WaitMainPumpFB
        Pause
    End Enum

    Public Wait As New Timer
    Public ATanktoCTankDelay As New Timer
    Public StateString As String
    Public DesiredVolume As Integer
    Public TargetPulses As Integer
    Public WaterType As Integer
    Public CoolFill As Boolean
    Public CounterSetPoint As Integer
    Public CounterRealPoint As Integer
    Public TotalVolume As Integer
    Public CounterTargetPoint As Integer
    Public FilltoHigh As Boolean
    Public FilltoMiddle As Boolean
    Public FilltoLow As Boolean
    Public ByQty As Boolean

    Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
        With ControlCode
      .Command16.Cancel() : .Command02.Cancel() : .Command10.Cancel() : .Command03.Cancel() : .Command04.Cancel()
            .Command05.Cancel() : .Command06.Cancel() : .Command07.Cancel() : .Command08.Cancel()
            .Command09.Cancel() : .Command11.Cancel() : .Command12.Cancel() : .Command13.Cancel()
            .Command14.Cancel() : .Command15.Cancel() : .Command17.Cancel() : .Command18.Cancel() : .Command39.Cancel() : .Command40.Cancel()
            .Command19.Cancel() : .Command20.Cancel() : .Command21.Cancel() : .Command22.Cancel()
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
            WaterType = MinMax(param(1), 0, 2)
            TotalVolume = .TotalWeight * .LiquidRatio

            If .TotalWeight = 0 Or .LiquidRatio = 0 Then
                DesiredVolume = param(2)
            ElseIf TotalVolume > .Parameters.MainTankFillMaxLiter Then
                DesiredVolume = .Parameters.MainTankFillMaxLiter
            ElseIf TotalVolume < .Parameters.MainTankFillMinLiter Then
                DesiredVolume = .Parameters.MainTankFillMinLiter
            Else
                DesiredVolume = TotalVolume
            End If
            DesiredVolume = (DesiredVolume \ 100) * 100

            .ShowTotalVolume = DesiredVolume             '顯示總浴量
            .TargetVolume = DesiredVolume                '顯示進水量
            CounterRealPoint = 0
            State = S17.WaitAuto

        End With
    End Function

    Public Function Run() As Boolean Implements ACCommand.Run
        With ControlCode
            Select Case State
                Case S17.Off
                    FilltoHigh = False
                    FilltoMiddle = False
                    FilltoLow = False
                    StateString = ""


                Case S17.WaitAuto
                    StateString = If(.Language = LanguageValue.ZhTw, "系統手動中", "System Manual")
                    If Not .IO.SystemAuto Then Exit Select
                    Wait.TimeRemaining = 5
                    State = S17.WaitTempSafe

                Case S17.WaitTempSafe
                    If .Parent.IsPaused Or Not .IO.SystemAuto Then
                        StateWas = State
                        State = S17.Pause
                        Wait.Pause()
                    End If
                    If .IO.MainTemperature >= .Parameters.SetSafetyTemp * 10 Then
                        StateString = If(.Language = LanguageValue.ZhTw, "溫度異常", "Interlocked Temperature")
                        .Alarms.HighTempNoFill = True
                        Exit Select
                    End If
                    .Alarms.HighTempNoFill = False
                    TargetPulses = DesiredVolume \ .Parameters.VolumePerCount
                    State = S17.WaitResetCounter

                Case S17.WaitResetCounter
                    StateString = If(.Language = LanguageValue.ZhTw, "流量計重置", "Reset flowmeter")
                    .FlowMeterCount = 0
                    CounterSetPoint = .FlowMeterCount
                    CounterTargetPoint = (TargetPulses + CounterSetPoint)
                    State = S17.WaitWater1

                Case S17.WaitWater1
                    If .Parent.IsPaused Or Not .IO.SystemAuto Then
                        StateWas = State
                        State = S17.Pause
                        Wait.Pause()
                    End If

                    'Dim j As Integer
                    'For j = 1 To 50
                    '  If .IO.MainTankLevel >= .SetMainTankAnalogInput(j) And .SetMainTankAnalogInput(j) > 0 Then
                    '    CounterRealPoint = .SetMainTankVolume(j)
                    '  End If
                    'Next
                    StateString = If(.Language = LanguageValue.ZhTw, "主缸進水中:", "Filling:") & .MainTankVolume & "L"
                    'Dim i As Integer
                    'For i = 1 To 50
                    '  If .TargetVolume <= .SetMainTankVolume(i) And .SetMainTankVolume(i) > 0 Then
                    '    If .IO.MainTankLevel >= .SetMainTankAnalogInput(i) And .SetMainTankAnalogInput(i) > 0 Then
                    '      Wait.TimeRemaining = .Parameters.AnalogFillWaitStableTime
                    '      State = S17.WaitStable1
                    '    End If
                    '  End If
                    'Next
                    If .TargetVolume < .MainTankVolume Then
                        State = S17.WaitStable1
                    End If

                Case S17.WaitStable1
                    StateString = If(.Language = LanguageValue.ZhTw, "等待水位穩定", "Wait Stable") & TimerString(Wait.TimeRemaining)
                    If Not Wait.Finished Then Exit Select
                    State = S17.WaitWater2


                Case S17.WaitWater2
                    If .Parent.IsPaused Or Not .IO.SystemAuto Then
                        StateWas = State
                        State = S17.Pause
                        Wait.Pause()
                    End If
                    StateString = If(.Language = LanguageValue.ZhTw, "主缸進水中:", "Filling:") & .MainTankVolume & "L"
                    If .TargetVolume < .MainTankVolume Then
                        State = S17.WaitStable2
                    End If
                    'Dim j As Integer
                    'For j = 1 To 50
                    '    If .IO.MainTankLevel >= .SetMainTankAnalogInput(j) And .SetMainTankAnalogInput(j) > 0 Then
                    '        CounterRealPoint = .SetMainTankVolume(j)
                    '    End If
                    'Next
                    'StateString = If(.Language = LanguageValue.ZhTw, "主缸進水中:", "Filling:") & CounterRealPoint & "L"
                    'Dim i As Integer
                    'For i = 1 To 50
                    '    If .TargetVolume <= .SetMainTankVolume(i) And .SetMainTankVolume(i) > 0 Then
                    '        If .IO.MainTankLevel >= .SetMainTankAnalogInput(i) And .SetMainTankAnalogInput(i) > 0 Then
                    '            Wait.TimeRemaining = .Parameters.AnalogFillWaitStableTime
                    '            State = S17.WaitStable2
                    '        End If
                    '    End If
                    'Next

                Case S17.WaitStable2
                    StateString = If(.Language = LanguageValue.ZhTw, "等待水位穩定", "Wait Stable") & TimerString(Wait.TimeRemaining)
                    If Not Wait.Finished Then Exit Select
                    State = S17.WaitLowLevel

                Case S17.WaitLowLevel
                    StateString = If(.Language = LanguageValue.ZhTw, "檢查低水位", "Check Low Level")
                    'If Not .LowLevel Then Exit Select
                    .PumpStartRequest = True
                    Wait.TimeRemaining = 5
                    State = S17.WaitTime

                Case S17.WaitTime
                    StateString = If(.Language = LanguageValue.ZhTw, "啟動主馬達", "Main Pump On")
                    If Not Wait.Finished Then Exit Select
                    .PumpStartRequest = False
                    .PumpOn = True
                    State = S17.WaitMainPumpFB

                Case S17.WaitMainPumpFB
                    StateString = If(.Language = LanguageValue.ZhTw, "主泵沒有運行", "Main pump not running")
                    If Not .IO.MainPumpFB Then Exit Select
                    State = S17.Off

                Case S17.Pause
                    StateString = If(.Language = LanguageValue.ZhTw, "暫停 ", "Paused ")
                    If .Parent.CurrentStep <> .Parent.ChangingStep Then
                        State = S17.Off
                        Wait.Cancel()
                    End If
                    If (Not .Parent.IsPaused) And .IO.SystemAuto Then
                        State = StateWas
                        StateWas = S17.Off
                        Wait.Restart()
                    End If

            End Select
        End With
    End Function

    Public Sub Cancel() Implements ACCommand.Cancel
        State = S17.Off
        CoolFill = False
        Wait.Cancel()
    End Sub

    Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
        WaterType = MinMax(param(1), 0, 2)
    End Sub

#Region "Standard Definitions"
    Private ReadOnly ControlCode As ControlCode
    Public Sub New(ByVal controlCode As ControlCode)
        Me.ControlCode = controlCode
    End Sub
    Friend ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
        Get
            Return State <> S17.Off
        End Get
    End Property

    <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S17
    <EditorBrowsable(EditorBrowsableState.Advanced)> Private statewas_ As S17
    Public Property State() As S17
        Get
            Return state_
        End Get
        Private Set(ByVal value As S17)
            state_ = value
        End Set
    End Property
    Public Property StateWas() As S17
        Get
            Return statewas_
        End Get
        Private Set(ByVal value As S17)
            statewas_ = value
        End Set
    End Property
    '    Public ReadOnly Property IsResetCounter() As Boolean
    '        Get
    '           Return (State = S03.WaitReset)
    '       End Get
    '  End Property
    Public ReadOnly Property IsFillHot() As Boolean
        Get
            Return (WaterType = 1 Or WaterType = 2) And (State = S17.WaitWater1 Or State = S17.WaitWater2)
        End Get
    End Property
    Public ReadOnly Property IsFillCold() As Boolean
        Get
            Return (WaterType = 0 Or WaterType = 2) And (State = S17.WaitWater1 Or State = S17.WaitWater2)
        End Get
    End Property
    Public ReadOnly Property IsResetCounter() As Boolean
        Get
            Return (State = S17.WaitResetCounter)
        End Get
    End Property
#End Region
End Class

#Region "Class Instance"
Partial Public Class ControlCode
    Public ReadOnly Command17 As New Command17(Me)
End Class
#End Region
