<Command("Call LA-252", "Call off |0-99|", , , "1", CommandType.Standard), _
TranslateCommand("zh-TW", "呼叫染料備藥", "呼叫染料備藥 |0-99|"), _
Description("0-99 Call off"), _
TranslateDescription("zh-TW", "0-99 呼叫染料備藥")> _
Public NotInheritable Class Command43
    Inherits MarshalByRefObject                       'Inheritsg是繼承Windows Form的應用程式要繼承System.Windows.Forms.Form，可先參考物件導向程式設計相關書籍
    Implements ACCommand

    Public Enum S43
        Off
        CheckLevel
        WaitAuto
        Ready
        DyeStepNumber1
        DyeStepNumber2
    End Enum

    Public StateString As String
    Public CallOff As Integer
    Public WaitTimer As New Timer
    Public BTankDrain As Boolean
    Public CTankDrain As Boolean

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
      .Command41.Cancel() : .Command42.Cancel() : .Command43.Cancel() : .Command44.Cancel()
      .Command01.Cancel()
      .TemperatureControlFlag = False
            .RunCallLA252 = False
            .CallLA252.Cancel()
            .DyeCallOff = 0   'Starts the handshake with the host / auto dispenser
            .DyeTank = 0
            BTankDrain = False
            CTankDrain = False
            WaitTimer.TimeRemaining = 0
            CallOff = param(1)
            .SPCConnectError = False

            State = S43.DyeStepNumber1
        End With
    End Function


    Public Function Run() As Boolean Implements ACCommand.Run
        With ControlCode
            Select Case State
                Case S43.Off
                    StateString = ""
                Case S43.DyeStepNumber1
                    For i = 0 To 40
                        Dim CalloffCheck As String
                        CalloffCheck = CType(CallOff, String)
                        If ControlCode.DyeStepNumber(i) = CalloffCheck Then
                            i = 40
                            State = S43.CheckLevel
                        Else
                            State = S43.DyeStepNumber2
                        End If
                    Next
                Case S43.DyeStepNumber2
                    StateString = "第 " & CallOff & " 步驟沒有染料可送，檢查配方"


                Case S43.CheckLevel
                    If .Parameters.DyeEnable = 1 Then
                        StateString = If(.Language = LanguageValue.ZhTw, "B缸有水，請檢查", "B Tank not empty")
                        BTankDrain = True
                     
                        If .IO.CallAck Or ControlCode.Parameters.DyeCheckTank = 0 Then
                            BTankDrain = False
                            WaitTimer.TimeRemaining = 3
                            State = S43.WaitAuto
                        ElseIf Not .BTankLowLevel And ControlCode.Parameters.DyeCheckTank = 1 Then
                            BTankDrain = False
                            WaitTimer.TimeRemaining = 3
                            State = S43.WaitAuto
                        End If

                    End If
                    If .Parameters.DyeEnable = 2 Then
                        StateString = If(.Language = LanguageValue.ZhTw, "C缸有水,自動排水", "C Tank not empty")
                        CTankDrain = True
                   
                        If .IO.CallAck Or ControlCode.Parameters.DyeCheckTank = 0 Then
                            CTankDrain = False
                            WaitTimer.TimeRemaining = 1
                            State = S43.WaitAuto
                        ElseIf Not .CTankLowLevel And ControlCode.Parameters.DyeCheckTank = 1 Then
                            CTankDrain = False
                            WaitTimer.TimeRemaining = 1
                            State = S43.WaitAuto
                        End If

                    End If

                Case S43.WaitAuto
                    .DyeCallOff = 0   'Starts the handshake with the host / auto dispenser
                    .DyeTank = 0
                    StateString = If(.Language = LanguageValue.ZhTw, "呼叫染料備藥252", "Call For Dyes")
                    'If Not .IO.SystemAuto Then Exit Select
                    If Not WaitTimer.Finished Then Exit Select
                    If Not .DyeState = 101 Then Exit Select
                    State = S43.Ready

                Case S43.Ready
                    .DyeCallOff = 0
                    .DyeTank = 0
                    .RunCallLA252 = True
                    State = S43.Off


            End Select
        End With
    End Function

    Public Sub Cancel() Implements ACCommand.Cancel
        State = S43.Off
        WaitTimer.Cancel()
    End Sub

    Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
        With ControlCode
            .RunCallLA252 = False
            .CallLA252.Cancel()
            .DyeCallOff = 0   'Starts the handshake with the host / auto dispenser
            .DyeTank = 0
            BTankDrain = False
            CTankDrain = False
            WaitTimer.TimeRemaining = 0
            CallOff = param(1)
            .SPCConnectError = False
            State = S43.DyeStepNumber1
        End With
    End Sub

#Region " Standard Definitions "

    Private ReadOnly ControlCode As ControlCode
    Public Sub New(ByVal controlCode As ControlCode)
        Me.ControlCode = controlCode
    End Sub
    Friend ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
        Get
            Return State <> S43.Off
        End Get
    End Property
    Public ReadOnly Property Is呼叫() As Boolean
        Get
            Return (State = S43.CheckLevel) And (BTankDrain)
        End Get
    End Property
    Public ReadOnly Property IsCTankDrain() As Boolean
        Get
            Return (State = S43.CheckLevel) And (CTankDrain)
        End Get
    End Property
    Public ReadOnly Property IsNotReady() As Boolean
        Get
            Return (State = S43.CheckLevel)
        End Get
    End Property
    Public ReadOnly Property IsNotEmpty() As Boolean
        Get
            Return (State = S43.CheckLevel)
        End Get
    End Property
    Public ReadOnly Property Is步驟錯誤呼叫() As Boolean
        Get
            Return (State = S43.DyeStepNumber2)
        End Get
    End Property
    <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S43
    Public Property State() As S43          'Property	屬性名稱() As 傳回值型別
        Get                                 'Get
            Return state_                   '屬性名稱 = 私有資料成員        '讀取私有資料成員的值
        End Get                             'End Get
        Private Set(ByVal value As S43)     'Set(ByVal Value As傳回值型別)
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
    Public ReadOnly Command43 As New Command43(Me)
End Class

#End Region
