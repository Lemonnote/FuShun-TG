<Command("Wait LA-302", , , , "15", CommandType.Standard), _
TranslateCommand("zh-TW", "等待助劑備藥"), _
Description("Wait LA-302"), _
TranslateDescription("zh-TW", "等待助劑備藥")> _
Public NotInheritable Class Command42
    Inherits MarshalByRefObject                       'Inheritsg是繼承Windows Form的應用程式要繼承System.Windows.Forms.Form，可先參考物件導向程式設計相關書籍
    Implements ACCommand

    Public Enum S42
        Off
        WaitAuto
        WaitLA302
        DispenseWaitResponse
        CheckReady
        Ready
    End Enum
    Public StateString As String
    Public CallAlarm As Boolean


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
            .Command61.Cancel() : .Command62.Cancel() : .Command64.Cancel() : .Command65.Cancel() : .Command90.Cancel()
            .Command67.Cancel() : .Command68.Cancel() : .Command01.Cancel()
      .TemperatureControl.Cancel()
      .Command01.Cancel()
      .TemperatureControlFlag = False
            .Command41.Cancel() : .Command42.Cancel() : .Command43.Cancel() : .Command44.Cancel()
            .TempControlFlag = False
            .SPCConnectError = False

            State = S42.WaitAuto
        End With
    End Function

    Public Function Run() As Boolean Implements ACCommand.Run
        With ControlCode
            Select Case State
                Case S42.Off
                    CallAlarm = False
                    StateString = ""

                Case S42.WaitAuto
                    StateString = If(.Language = LanguageValue.ZhTw, "系統手動中", "Interlocked not In auto")
                    If Not .IO.SystemAuto Then Exit Select
                    State = S42.WaitLA302

                Case S42.WaitLA302

                    If Not ControlCode.LA302Ready Then
                        StateString = If(.Language = LanguageValue.ZhTw, "等待LA-302備藥中", "Wait LA-302 Ready")
                        State = S42.WaitLA302
                    Else
                        State = S42.DispenseWaitResponse
                    End If
                    'If ControlCode.CTank = "0" And ControlCode.CCallOff = "0" And ControlCode.ChemicalTank = 0 And ControlCode.ChemicalCallOff = 0 And .CState = "101" Then
                    '    State = S42.DispenseWaitResponse
                    'End If
                    If .CallLA302.State = CallLA302_.CallLA302.WaitDispenseReady Then
                        StateString = If(.Language = LanguageValue.ZhTw, "等待水量到達", "Wait LA-302 Ready")
                    End If

                Case S42.DispenseWaitResponse
                    If .ChemicalState = 309 Or .DState = "309" Then
                        StateString = If(.Language = LanguageValue.ZhTw, "計量異常309", "Side Tank Empty, LA-302 Dispense Error")

                        CallAlarm = True
                        Exit Select
                    End If

                    StateString = If(.Language = LanguageValue.ZhTw, "藥缸沒料，LA-302輸送異常", "Side Tank Empty, LA-302 Dispense Error")

                    If .CTank = "0" And .CCallOff = "0" And .ChemicalTank = 0 And .ChemicalCallOff = 0 And .CState = "101" Then
                        StateString = If(.Language = LanguageValue.ZhTw, "藥缸沒料", "Side Tank Empty, LA-302 Dispense Error")

                    End If

                    .LA302Ready = False
                    CallAlarm = True
                    If (.Parameters.ChemicalEnable = 1 And .BTankLowLevel) Or .IO.CallAck Then
                        CallAlarm = False
                        .TankBReady = True
                        State = S42.Ready
                    End If
                    If (.Parameters.ChemicalEnable = 2 And .CTankLowLevel) Or .IO.CallAck Then
                        CallAlarm = False
                        .TankCReady = True
                        State = S42.Ready
                    End If

                Case S42.Ready
                    .ChemicalCallOff = 0
                    .ChemicalTank = 0
                    State = S42.Off

            End Select

        End With
    End Function

    Public Sub Cancel() Implements ACCommand.Cancel
        State = S42.Off
    End Sub

    Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged

    End Sub


#Region " Standard Definitions "

    Private ReadOnly ControlCode As ControlCode
    Public Sub New(ByVal controlCode As ControlCode)
        Me.ControlCode = controlCode
    End Sub
    Friend ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
        Get
            Return State <> S42.Off
        End Get
    End Property
    Public ReadOnly Property IsCall() As Boolean
        Get
            Return (State = S42.DispenseWaitResponse) And CallAlarm
        End Get
    End Property
    Public ReadOnly Property IsReady() As Boolean
        Get
            Return (State = S42.Ready)
        End Get
    End Property
    <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S42
    Public Property State() As S42          'Property	屬性名稱() As 傳回值型別
        Get                                 'Get
            Return state_                   '屬性名稱 = 私有資料成員        '讀取私有資料成員的值
        End Get                             'End Get
        Private Set(ByVal value As S42)     'Set(ByVal Value As傳回值型別)
            state_ = value                  '私有資料成員 = Value          '設定私有資料成員的值
        End Set                             'End Set
    End Property
    'Property score() As Integer
    '    Get
    '        score = a           '讀取私有資料成員a的值
    '    End Get
    '
    '    Set(ByVal Value As Integer)
    '        a = Value           '設定私有資料成員a的值
    '    End Set
    'End Property

#End Region

End Class

#Region " Class Instance "

Partial Public Class ControlCode
    Public ReadOnly Command42 As New Command42(Me)
End Class

#End Region
