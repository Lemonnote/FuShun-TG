<Command("Fill", "Level |0-2| Type |0-3|", , "30", "5"), _
 TranslateCommand("zh-TW", "主缸進水", "進水水位 |0-2| 水源選擇 |0-3|"), _
 Description("Level 0=LOW 1=MID 2=HIGH  Type 0=COLD 1=HOT 2=COLD+HOT 3=Water3"), _
 TranslateDescription("zh-TW", "水位 0=低水位 1=中水位 2=高水位 水源 0=冷水 1=熱水 2=冷+熱水 3=進水3")> _
Public NotInheritable Class Command04
    Inherits MarshalByRefObject
    Implements ACCommand

    Public Enum S04
        Off
        WaitAuto
        WaitTime
        WaitTime2
        WaitTempSafe
        WaitResetCounter
        WaitForLevel
        WaitTime4
        WaitForLevel2
        WaitLowLevel
    WaitTime5
    WaitTime7
    WaitMainPumpFB
    WaitTime6
        Pause
    End Enum
    Public StateString As String
    Public Wait As New Timer
    Public MainTankLevel As Integer
    Public WaterType As Integer
    Public CoolFill As Boolean
    Public DesiredVolume As Integer
    Public CounterRealPoint As Integer
    Public TotalVolume As Integer
    Public TargetPulses As Integer


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

            .TempControlFlag = False
            .TemperatureControlFlag = False

            WaterType = MinMax(param(2), 0, 3)                  '水源選擇
            MainTankLevel = MinMax(param(1), 0, 2)              '水位選擇
            .PumpStopRequest = True                                   'ControlCode.PumpStopRequest = 1

            TotalVolume = .TotalWeight * .LiquidRatio

            If TotalVolume > 0 And TotalVolume > .Parameters.MainTankFillMaxLiter Then
                DesiredVolume = .Parameters.MainTankFillMaxLiter
            ElseIf TotalVolume > 0 And TotalVolume < .Parameters.MainTankFillMinLiter Then
                DesiredVolume = .Parameters.MainTankFillMinLiter
            Else
                DesiredVolume = TotalVolume
            End If
            DesiredVolume = (DesiredVolume \ 100) * 100

            .ShowTotalVolume = DesiredVolume             '顯示總浴量
            .TargetVolume = DesiredVolume                '顯示進水量
            TargetPulses = DesiredVolume \ .Parameters.VolumePerCount

            State = S04.WaitAuto
        End With
    End Function

    Public Function Run() As Boolean Implements ACCommand.Run
        With ControlCode
            Select Case State
                Case S04.Off
                    StateString = ""

                Case S04.WaitAuto
                    StateString = If(.Language = LanguageValue.ZhTw, "系統手動中", "System Manual")
                    If Not .IO.SystemAuto Then Exit Select
                    State = S04.WaitTime
                    Wait.TimeRemaining = 1

                Case S04.WaitTime
                    StateString = If(.Language = LanguageValue.ZhTw, "停止主泵", "Stopping pump")
                    If Not Wait.Finished Then Exit Select
                    .PumpOn = False                   '在ControlCode內 impco -->pump off + pumpFB no = error 
                    .PumpStopRequest = True             'IO.PumpOff（馬達關） = PumpStopRequest                     '
                    State = S04.WaitTime2

                Case S04.WaitTime2
                    StateString = If(.Language = LanguageValue.ZhTw, "停止主泵", "Stopping pump")
                    If Not .IO.MainPumpFB = False Then
                        StateString = If(.Language = LanguageValue.ZhTw, "主泵異常", "Pump Stop Error")
                        Exit Select
                    End If
                    .PumpStopRequest = False            '.IO.MainPumpFB(馬達訊號)如果還有，將無法執行下一步
                    State = S04.WaitTempSafe

                Case S04.WaitTempSafe
                    If .IO.MainTemperature >= .Parameters.SetSafetyTemp * 10 Then       '實際溫度大於或等於安全溫度 跳"溫度異常"
                        StateString = If(.Language = LanguageValue.ZhTw, "溫度異常", "Interlocked Temperature")
                        .Alarms.HighTempNoFill = True
                        Exit Select
                    End If

                    .Alarms.HighTempNoFill = False
                    If .ShowTotalVolume = 0 Then
                        State = S04.WaitForLevel
                    Else
                        State = S04.WaitResetCounter
                    End If

                Case S04.WaitResetCounter
                    StateString = If(.Language = LanguageValue.ZhTw, "流量計重置", "Reset flowmeter")
                    If (Not Wait.Finished) Or .IO.HSCounter1 > 10 Then Exit Select
                    State = S04.WaitForLevel


                Case S04.WaitForLevel
                    If .Parent.IsPaused Or Not .IO.SystemAuto Then
                        StateWas = State
                        State = S04.Pause
                        Wait.Pause()
                    End If
                    If TotalVolume > 0 Then
                        CounterRealPoint = .IO.HSCounter1 * .Parameters.VolumePerCount
                        StateString = If(.Language = LanguageValue.ZhTw, "主缸進水中:", "Filling:") & CounterRealPoint & "L"
                        If CounterRealPoint < .TargetVolume Then Exit Select
                        CounterRealPoint = .IO.HSCounter1 * .Parameters.VolumePerCount
                        State = S04.WaitLowLevel
                    Else
                        If MainTankLevel = 0 Then
                            StateString = If(.Language = LanguageValue.ZhTw, "主缸進水至低水位", "Filling to low level ")
                            If Not .IO.LowLevel Then Exit Select
                        ElseIf MainTankLevel = 1 Then
                            StateString = If(.Language = LanguageValue.ZhTw, "主缸進水至中水位", "Filling to middle level ")
                            If Not .IO.MiddleLevel Then Exit Select
                        Else
                            StateString = If(.Language = LanguageValue.ZhTw, "主缸進水至高水位", "Filling to high level ")
                            If Not .IO.HighLevel Then Exit Select
                        End If
                        Wait.TimeRemaining = 10
                        State = S04.WaitTime4
                    End If

                Case S04.WaitTime4
                    StateString = If(.Language = LanguageValue.ZhTw, "等待水位穩定", "Wait level stable")
                    If Not Wait.Finished Then Exit Select
                    State = S04.WaitForLevel2

                Case S04.WaitForLevel2
                    If .Parent.IsPaused Or Not .IO.SystemAuto Then
                        StateWas = State
                        State = S04.Pause
                        Wait.Pause()
                    End If
                    If MainTankLevel = 0 Then
                        StateString = If(.Language = LanguageValue.ZhTw, "主缸進水至低水位", "Filling to low level ")
                        If Not .IO.LowLevel Then Exit Select
                    ElseIf MainTankLevel = 1 Then
                        StateString = If(.Language = LanguageValue.ZhTw, "主缸進水至中水位", "Filling to middle level ")
                        If Not .IO.MiddleLevel Then Exit Select
                    Else
                        StateString = If(.Language = LanguageValue.ZhTw, "主缸進水至高水位", "Filling to high level ")
                        If Not .IO.HighLevel Then Exit Select
                    End If


                    State = S04.WaitLowLevel

                Case S04.WaitLowLevel
                    StateString = If(.Language = LanguageValue.ZhTw, "檢查低水位", "Check Low Level")
                    If Not .IO.LowLevel Then Exit Select
                    ' .IO.PumpSpeedControl = CType(.PumpSpeed * 10, Short)    '如果沒有低水位，將不啟動馬達
                    .PumpStartRequest = True                    '啟動馬達
                    Wait.TimeRemaining = 1
                    State = S04.WaitTime5

                Case S04.WaitTime5
                    If Not Wait.Finished Then Exit Select
                    .PumpStartRequest = False                   '關閉馬達
                    .PumpOn = True                              '在ControlCode內 impco -->pump off + pumpFB no = error 
                    State = S04.WaitMainPumpFB

                Case S04.WaitMainPumpFB
                    StateString = If(.Language = LanguageValue.ZhTw, "主泵沒有運行", "Main pump not running")
          If Not .IO.MainPumpFB Then Exit Select
          Wait.TimeRemaining = 10
          State = S04.WaitTime6

        Case S04.WaitTime6
          StateString = If(.Language = LanguageValue.ZhTw, "等待水位穩定", "Wait level stable")
          If Not Wait.Finished Then Exit Select
          State = S04.WaitTime7

        Case S04.WaitTime7
          If .Parent.IsPaused Or Not .IO.SystemAuto Then
            StateWas = State
            State = S04.Pause
            Wait.Pause()
          End If
          If MainTankLevel = 0 Then
            StateString = If(.Language = LanguageValue.ZhTw, "主缸進水至低水位", "Filling to low level ")
            If Not .IO.LowLevel Then Exit Select
          ElseIf MainTankLevel = 1 Then
            StateString = If(.Language = LanguageValue.ZhTw, "主缸進水至中水位", "Filling to middle level ")
            If Not .IO.MiddleLevel Then Exit Select
          Else
            StateString = If(.Language = LanguageValue.ZhTw, "主缸進水至高水位", "Filling to high level ")
            If Not .IO.HighLevel Then Exit Select
          End If
          State = S04.Off

                Case S04.Pause
                    StateString = If(.Language = LanguageValue.ZhTw, "暫停 ", "Paused ")
                    If (Not .Parent.IsPaused) And .IO.SystemAuto Then
                        State = StateWas
                        StateWas = S04.Off
                        Wait.Restart()
                    End If

            End Select
        End With
    End Function

    Public Sub Cancel() Implements ACCommand.Cancel
        State = S04.Off
        CoolFill = False
        Wait.Cancel()
    End Sub

    Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
        WaterType = MinMax(param(2), 0, 2)                  '水源選擇
        MainTankLevel = MinMax(param(1), 0, 2)              '水位選擇
    End Sub

#Region "Standard Definitions"
    Private ReadOnly ControlCode As ControlCode
    Public Sub New(ByVal controlCode As ControlCode)
        Me.ControlCode = controlCode
    End Sub
    Friend ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
        Get
            Return State <> S04.Off
        End Get
    End Property

    <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S04
    <EditorBrowsable(EditorBrowsableState.Advanced)> Private statewas_ As S04
    Public Property State() As S04
        Get
            Return state_
        End Get
        Private Set(ByVal value As S04)
            state_ = value
        End Set
    End Property
    Public Property StateWas() As S04
        Get
            Return statewas_
        End Get
        Private Set(ByVal value As S04)
            statewas_ = value
        End Set
    End Property
    Public ReadOnly Property IsFillHot() As Boolean
        Get
      Return (WaterType = 1 Or WaterType = 2) AndAlso ((State = S04.WaitForLevel) Or (State = S04.WaitForLevel2) Or (State = S04.WaitTime7))
        End Get
    End Property
    Public ReadOnly Property IsFillCold() As Boolean
        Get
      Return (WaterType = 0 Or WaterType = 2) AndAlso ((State = S04.WaitForLevel) Or (State = S04.WaitForLevel2) Or (State = S04.WaitTime7))
        End Get
    End Property
    Public ReadOnly Property IsFill3() As Boolean
        Get
      Return (WaterType = 3) AndAlso ((State = S04.WaitForLevel) Or (State = S04.WaitForLevel2) Or (State = S04.WaitTime7))
        End Get
    End Property
    Public ReadOnly Property IsResetCounter() As Boolean
        Get
            Return (State = S04.WaitResetCounter)
        End Get
    End Property
#End Region
End Class

#Region "Class Instance"
Partial Public Class ControlCode
  Public ReadOnly Command04 As New Command04(Me)
End Class
#End Region
