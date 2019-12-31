<Command("Call LA-302", "Call off |0-99|", , , "1", CommandType.Standard), _
TranslateCommand("zh-TW", "呼叫助劑備藥", "呼叫助劑備藥 |0-99|"), _
Description("0-99 Call off"), _
TranslateDescription("zh", "0-99 呼叫助劑備藥")> _
Public NotInheritable Class Command41
    Inherits MarshalByRefObject                       'Inheritsg是繼承Windows Form的應用程式要繼承System.Windows.Forms.Form，可先參考物件導向程式設計相關書籍
    Implements ACCommand

    Public Enum S41
        Off
        CheckLevel
        WaitAuto
        Wash1
        Wash2
        Ready
        ChemicalStepNumber1
        ChemicalStepNumber2
    End Enum

    Public StateString As String
    Public CallOff As Integer
    Public WaitTimer As New Timer
    Public BTankDrain As Boolean
    Public CTankDrain As Boolean
    Public 洗桶 As New Timer
    Public 等待SQL As New Timer
    Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
        With ControlCode
            'cancels for all other forground functions
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
      .Command41.Cancel() : .Command42.Cancel() : .Command43.Cancel() : .Command44.Cancel()
      .Command01.Cancel()
      .TemperatureControlFlag = False
            If .ChemicalState = 202 Or .DState = "202" Or .ChemicalState = 102 Or .DState = "102" Then
                State = S41.Off
            Else
                .ChemicalCallOff = 0   'Starts the handshake with the host / auto dispenser
                .ChemicalTank = 0
                .RunCallLA302 = False
                .CallLA302.Cancel()
        CallOff = param(1)
                .SPCConnectError = False
                .UpdatePowderDispenseResult = False
                WaitTimer.TimeRemaining = 0
                BTankDrain = False
                CTankDrain = False
                '=============================================09/13

                State = S41.ChemicalStepNumber1
            End If

        End With
    End Function


    Public Function Run() As Boolean Implements ACCommand.Run
        With ControlCode
            Select Case State
                Case S41.Off
                    BTankDrain = False
                    CTankDrain = False
                    StateString = ""

                Case S41.ChemicalStepNumber1
                    For i = 0 To 40
                        Dim CalloffCheck As String
                        CalloffCheck = CType(CallOff, String)
                        If ControlCode.ChemicalStepNumber(i) = CalloffCheck Then
                            i = 40
                            State = S41.CheckLevel
                        Else
                            State = S41.ChemicalStepNumber2
                            等待SQL.TimeRemaining = 2
                        End If
                    Next
                Case S41.ChemicalStepNumber2
                    StateString = "第 " & CallOff & " 步驟沒有助劑可送，檢查配方"

                    If .IO.CallAck Then
                        StateString = ""
                        State = S41.Off
                    Else
                        If Not 等待SQL.Finished Then Exit Select
                        State = S41.ChemicalStepNumber1

                    End If



                Case S41.CheckLevel
                    If .Parameters.ChemicalEnable = 1 Then
                        StateString = If(.Language = LanguageValue.ZhTw, "B料桶有水, 請檢查", "B Tank Not Empty, Draining")
                        BTankDrain = True

                        If .IO.CallAck Or ControlCode.Parameters.ChemicalCheckTank = 0 Then
                            BTankDrain = False
                            WaitTimer.TimeRemaining = 3
                            State = S41.WaitAuto
                        ElseIf Not ControlCode.BTankLowLevel And ControlCode.Parameters.ChemicalCheckTank = 1 Then
                            BTankDrain = False
                            WaitTimer.TimeRemaining = 3
                            State = S41.WaitAuto
                        End If

                    End If
                    If .Parameters.ChemicalEnable = 2 Then
                        StateString = If(.Language = LanguageValue.ZhTw, "C料桶有水, 請檢查", "C Tank Not Empty, Draining")
                        CTankDrain = True

                        If .IO.CallAck Or ControlCode.Parameters.ChemicalCheckTank = 0 Then
                            CTankDrain = False
                            WaitTimer.TimeRemaining = 1
                            State = S41.WaitAuto
            ElseIf Not ControlCode.CTankLowLevel And ControlCode.Parameters.ChemicalCheckTank = 1 Then
              CTankDrain = False
              WaitTimer.TimeRemaining = 1
              State = S41.WaitAuto
                        End If

                    End If

                Case S41.WaitAuto
                    .ChemicalCallOff = 0   'Starts the handshake with the host / auto dispenser
                    .ChemicalTank = 0
                    StateString = If(.Language = LanguageValue.ZhTw, "呼叫助劑備藥", "Call For Chemicals")
                    'If Not .IO.SystemAuto Then Exit Select
                    If Not WaitTimer.Finished Then Exit Select
                    If Not .ChemicalState = 101 Then Exit Select
                    State = S41.Ready

                Case S41.Ready
                    .粉體步驟 = CallOff
                    .粉體藥桶 = 1
                    .ChemicalCallOff = 0   'Starts the handshake with the host / auto dispenser
                    .ChemicalTank = 0
                    .RunCallLA302 = True
                    State = S41.Off


            End Select
        End With
    End Function

    Public Sub Cancel() Implements ACCommand.Cancel
        State = S41.Off
        WaitTimer.Cancel()
    End Sub

    Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
        With ControlCode
            If State = S41.ChemicalStepNumber2 Then
                .RunCallLA302 = False
                .CallLA302.Cancel()

                CallOff = param(1)
                .SPCConnectError = False
                .UpdatePowderDispenseResult = False
                WaitTimer.TimeRemaining = 0
                BTankDrain = False
                CTankDrain = False
                '=============================================09/13

                State = S41.ChemicalStepNumber1
            End If

        End With
       
    End Sub

#Region " Standard Definitions "

    Private ReadOnly ControlCode As ControlCode
    Public Sub New(ByVal controlCode As ControlCode)
        Me.ControlCode = controlCode
    End Sub
    Friend ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
        Get
            Return State <> S41.Off
        End Get
    End Property
    Public ReadOnly Property Is呼叫() As Boolean
        Get
            Return ((State = S41.CheckLevel) And (BTankDrain))
        End Get
    End Property
    Public ReadOnly Property IsB洗桶() As Boolean
        Get
            Return (State = S41.Wash1)
        End Get
    End Property
    Public ReadOnly Property Is步驟錯誤呼叫() As Boolean
        Get
            Return (State = S41.ChemicalStepNumber2)
        End Get
    End Property
    Public ReadOnly Property IsCTankDrain() As Boolean
        Get
            Return (State = S41.CheckLevel) And (CTankDrain)
        End Get
    End Property
    Public ReadOnly Property IsNotReady() As Boolean
        Get
            Return (State = S41.CheckLevel)
        End Get
    End Property
    Public ReadOnly Property IsNotEmpty() As Boolean
        Get
            Return (State = S41.CheckLevel)
        End Get
    End Property
    <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S41
    Public Property State() As S41          'Property	屬性名稱() As 傳回值型別
        Get                                 'Get
            Return state_                   '屬性名稱 = 私有資料成員        '讀取私有資料成員的值
        End Get                             'End Get
        Private Set(ByVal value As S41)     'Set(ByVal Value As傳回值型別)
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
    Public ReadOnly Command41 As New Command41(Me)
End Class

#End Region
