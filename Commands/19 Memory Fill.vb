<Command("MemoryFill", "Type |0-3|", , , "5"), _
 TranslateCommand("zh-TW", "記憶入水", "水源|0-3|"), _
 Description("Fill to Memory Level, Type 0=COLD 1=HOT 2=COLD+HOT 3=Fill3"), _
 TranslateDescription("zh-TW", " 進水到記憶水位, 水源 0=冷水 1=熱水 2=冷+熱水 3=進水3")> _
Public NotInheritable Class Command19
    Inherits MarshalByRefObject
    Implements ACCommand

    Public Enum S19
        Off
        WaitAuto
        WaitTime
        WaitTime2
        WaitTempSafe
        WaitForLevel
        WaitTime5
        WaitMainPumpFB
        Pause
    End Enum
    Public StateString As String
    Public Wait As New Timer
    Public MainTankLevel As Integer
    Public WaterType As Integer
    Public CoolFill As Boolean

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

            WaterType = MinMax(param(1), 0, 2)                  '水源選擇
            .PumpStopRequest = True                                   'ControlCode.PumpStopRequest = 1
            State = S19.WaitAuto
        End With
    End Function

    Public Function Run() As Boolean Implements ACCommand.Run
        With ControlCode
            Select Case State
                Case S19.Off
                    StateString = ""

                Case S19.WaitAuto
                    StateString = If(.Language = LanguageValue.ZhTw, "系統手動中", "System Manual")
                    If Not .IO.SystemAuto Then Exit Select
                    State = S19.WaitTime
                    Wait.TimeRemaining = 1

                Case S19.WaitTime
                    StateString = If(.Language = LanguageValue.ZhTw, "停止主泵", "Stopping pump")
                    If Not Wait.Finished Then Exit Select
                    .PumpOn = False                   '在ControlCode內 impco -->pump off + pumpFB no = error 
                    .PumpStopRequest = True             'IO.PumpOff（馬達關） = PumpStopRequest                     '
                    State = S19.WaitTime2

                Case S19.WaitTime2
                    StateString = If(.Language = LanguageValue.ZhTw, "停止主泵", "Stopping pump")
                    If Not .IO.MainPumpFB = False Then
                        StateString = If(.Language = LanguageValue.ZhTw, "主泵異常", "Main pump stop error")
                        Exit Select
                    End If
                    .PumpStopRequest = False            '.IO.MainPumpFB(馬達訊號)如果還有，將無法執行下一步
                    State = S19.WaitTempSafe

                Case S19.WaitTempSafe
                    If .IO.MainTemperature >= .Parameters.SetSafetyTemp * 10 Then       '實際溫度大於或等於安全溫度 跳"溫度異常"
                        StateString = If(.Language = LanguageValue.ZhTw, "溫度異常", "Interlocked Temperature")
                        Exit Select
                    End If

                    .Alarms.HighTempNoFill = False
                    State = S19.WaitForLevel

                Case S19.WaitForLevel
                    If .Parent.IsPaused Or Not .IO.SystemAuto Then
                        StateWas = State
                        State = S19.Pause
                        Wait.Pause()
                    End If
                    ''''''Dim i As Integer
                    ''''''For i = 1 To 50
                    ''''''    If .Parameters.SetMainTankVolume(i) > 0 And .Parameters.SetMainTankAnalogInput(i) > 0 And .IO.MainTankLevel <= .Parameters.SetMainTankAnalogInput(i) Then
                    ''''''        .MainTankActualVolume = .Parameters.SetMainTankVolume(i - 1)
                    ''''''        Exit For
                    ''''''    End If
                    ''''''Next
                    StateString = If(.Language = LanguageValue.ZhTw, "主缸進水至記憶水位 ", "Filling to recall level ") & .MainTankVolume & "/" & .RecallLevel
                    If (.RecallLevel > .MainTankVolume) And Not .HighLevel Then Exit Select
          .PumpStartRequest = True                    '啟動馬達
          Wait.TimeRemaining = 2
          State = S19.WaitTime5

                Case S19.WaitTime5
                    If Not Wait.Finished Then Exit Select
                    .PumpStartRequest = False                   '關閉馬達
                    .PumpOn = True                              '在ControlCode內 impco -->pump off + pumpFB no = error 
                    State = S19.WaitMainPumpFB

                Case S19.WaitMainPumpFB
                    StateString = If(.Language = LanguageValue.ZhTw, "主泵沒有運行", "Main pump not running")
                    If Not .IO.MainPumpFB Then Exit Select
                    State = S19.Off

                Case S19.Pause
                    StateString = If(.Language = LanguageValue.ZhTw, "暫停 ", "Paused ")
                    If (Not .Parent.IsPaused) And .IO.SystemAuto Then
                        State = StateWas
                        StateWas = S19.Off
                        Wait.Restart()
                    End If

            End Select
        End With
    End Function

    Public Sub Cancel() Implements ACCommand.Cancel
        State = S19.Off
        CoolFill = False
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
            Return State <> S19.Off
        End Get
    End Property

    <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S19
    Public Property State() As S19
        Get
            Return state_
        End Get
        Private Set(ByVal value As S19)
            state_ = value
        End Set
    End Property
    <EditorBrowsable(EditorBrowsableState.Advanced)> Private statewas_ As S19
    Public Property StateWas() As S19
        Get
            Return statewas_
        End Get
        Private Set(ByVal value As S19)
            statewas_ = value
        End Set
    End Property


    Public ReadOnly Property IsFillHot() As Boolean
        Get
            Return (WaterType = 1 Or WaterType = 2) AndAlso ((State = S19.WaitForLevel) Or (State = S19.WaitTime))
        End Get
    End Property
    Public ReadOnly Property IsFillCold() As Boolean
        Get
            Return (WaterType = 0 Or WaterType = 2) AndAlso ((State = S19.WaitForLevel) Or (State = S19.WaitTime))
        End Get
    End Property
    Public ReadOnly Property IsFill3() As Boolean
        Get
            Return (WaterType = 3) AndAlso ((State = S19.WaitForLevel) Or (State = S19.WaitTime))
        End Get
    End Property
    Public ReadOnly Property IsCoolFill() As Boolean
        Get
            Return CoolFill AndAlso ((State = S19.WaitForLevel) Or (State = S19.WaitTime))
        End Get
    End Property
#End Region
End Class

#Region "Class Instance"
Partial Public Class ControlCode
    Public ReadOnly Command19 As New Command19(Me)
End Class
#End Region
