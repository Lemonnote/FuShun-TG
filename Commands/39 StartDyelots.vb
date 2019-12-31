<Command("Finish", , , , ), _
 TranslateCommand("zh-TW", "染程開始")> _
Public NotInheritable Class Command39
    Inherits MarshalByRefObject
    Implements ACCommand

    Public Enum S40
        Off
        WaitAuto
        WaitCallAck
        WaitTime
        WaitCallAck2
    End Enum

    Public Wait As New Timer
    Public StateString As String

    Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
        With ControlCode
      .Command16.Cancel() : .Command02.Cancel() : .Command10.Cancel() : .Command03.Cancel() : .Command04.Cancel()
            .Command05.Cancel() : .Command06.Cancel() : .Command07.Cancel() : .Command08.Cancel()
            .Command09.Cancel() : .Command11.Cancel() : .Command12.Cancel() : .Command13.Cancel()
            .Command14.Cancel() : .Command15.Cancel() : .Command17.Cancel() : .Command18.Cancel()
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

            .TempControlFlag = False
            .MessageLoadFabric = True
            State = S40.WaitAuto
        End With
    End Function

    Public Function Run() As Boolean Implements ACCommand.Run
        With ControlCode
            Select Case State
                Case S40.Off
                    .MessageUnloadFiber = False

                Case S40.WaitAuto
                    StateString = If(.Language = LanguageValue.ZhTw, "染程開始，請按確認鈕！", "Rinsing")
                    If Not .IO.CallAck Then Exit Select
                    Wait.TimeRemaining = 2
                    State = S40.WaitTime

                Case S40.WaitTime
                    If Not Wait.Finished Then Exit Select
                    State = S40.Off

            End Select
        End With
    End Function

    Public Sub Cancel() Implements ACCommand.Cancel
        State = S40.Off
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
            Return State <> S40.Off
        End Get
    End Property


    <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S40
    Public Property State() As S40
        Get
            Return state_
        End Get
        Private Set(ByVal value As S40)
            state_ = value
        End Set
    End Property
#End Region
End Class

#Region "Class Instance"
Partial Public Class ControlCode
  Public ReadOnly Command40 As New Command40(Me)
End Class
#End Region
