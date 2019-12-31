<Command("Load", , , , "5"), _
 TranslateCommand("zh-TW", "入布")> _
Public NotInheritable Class Command16
    Inherits MarshalByRefObject
    Implements ACCommand

    Public Enum S01
        Off
        WaitAuto
        WaitCallAck
        WaitTime
        WaitCallAck2
    End Enum

    Public Wait As New Timer

    Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
        With ControlCode
      .Command16.Cancel() : .Command02.Cancel() : .Command10.Cancel() : .Command03.Cancel() : .Command04.Cancel()
            .Command05.Cancel() : .Command06.Cancel() : .Command07.Cancel() : .Command08.Cancel()
            .Command09.Cancel() : .Command11.Cancel() : .Command12.Cancel() : .Command13.Cancel() : .Command39.Cancel() : .Command40.Cancel()
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
            .TemperatureControlFlag = False

            ' .IO.CallLamp = True
            .MessageLoadFabric = True
            State = S01.WaitAuto
        End With
    End Function

    Public Function Run() As Boolean Implements ACCommand.Run
        With ControlCode
            Select Case State
                Case S01.Off
                    .MessageLoadFabric = False

                Case S01.WaitAuto
                    If Not .IO.SystemAuto Then Exit Select
                    State = S01.WaitCallAck

                Case S01.WaitCallAck
                    If Not .IO.CallAck Then Exit Select
                    Wait.TimeRemaining = 2
                    State = S01.WaitTime

                Case S01.WaitTime
                    If Not Wait.Finished Then Exit Select
                    State = S01.WaitCallAck2

                Case S01.WaitCallAck2
                    If Not .IO.CallAck Then Exit Select
                    '    .IO.CallLamp = False
                    .MessageLoadFabric = False
                    State = S01.Off
            End Select
        End With
    End Function

    Public Sub Cancel() Implements ACCommand.Cancel
        State = S01.Off
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
            Return State <> S01.Off
        End Get
    End Property
    Public ReadOnly Property IsCalling() As Boolean
        Get
            Return (State = S01.WaitCallAck)
        End Get
    End Property


    <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S01
    Public Property State() As S01
        Get
            Return state_
        End Get
        Private Set(ByVal value As S01)
            state_ = value
        End Set
    End Property
#End Region
End Class

#Region "Class Instance"
Partial Public Class ControlCode
    Public ReadOnly Command16 As New Command16(Me)
End Class
#End Region
