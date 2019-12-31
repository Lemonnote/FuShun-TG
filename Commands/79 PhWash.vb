<Command("PH Wash", ""), _
 TranslateCommand("zh-TW", "PH管路清洗", ""), _
 Description(""), _
 TranslateDescription("zh-TW", "")> _
Public NotInheritable Class Command79
    Inherits MarshalByRefObject
    Implements ACCommand

    Public Enum S79
        Off
        Wash
        Wash1
        WashFinish
        Pause
    End Enum

    Public Wait As New Timer
    Public StateString As String
    Public CleanPipe As Double

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
            .PhControl.Cancel() : .PhWash.Cancel() : .PhControlFlag = False : .PhCirculateRun.Cancel()
            .Command77.Cancel() : .Command74.Cancel() : .Command75.Cancel() : .Command76.Cancel() : .Command78.Cancel() : .Command73.Cancel() : .Command80.Cancel()
            '---------------------------------------------------------------------------------------------------------
            '.PhFillLevel = param(1)             '進水量
            .PhCirRun = False
            CleanPipe = 1
            State = S79.Wash
        End With
    End Function

    Public Function Run() As Boolean Implements ACCommand.Run
        With ControlCode
            Select Case State

                Case S79.Wash
                    If CleanPipe = 1 Then

                        State = S79.Wash1
                    Else
                        State = S79.WashFinish
                    End If

                Case S79.Wash1
                    .PhWash.Run()
                    If .PhWash.State = PhWash_.PhWash.Finish Then

                        State = S79.WashFinish

                    End If

                Case S79.WashFinish

                    .PhWash.Cancel()

                    State = S79.Off
            End Select

        End With
    End Function

    Public Sub Cancel() Implements ACCommand.Cancel
        State = S79.Off
        Wait.Cancel()
    End Sub

    'Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged

    'End Sub
    Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged

        ControlCode.PhFillLevel = param(1)             '進水量
    End Sub
#Region "Standard Definitions"
    Private ReadOnly ControlCode As ControlCode
    Public Sub New(ByVal controlCode As ControlCode)
        Me.ControlCode = controlCode
    End Sub
    Friend ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
        Get
            Return State <> S79.Off
        End Get
    End Property

    <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S79
    Public Property State() As S79
        Get
            Return state_
        End Get
        Private Set(ByVal value As S79)
            state_ = value
        End Set
    End Property
#End Region
End Class

#Region "Class Instance"
Partial Public Class ControlCode
    Public ReadOnly Command79 As New Command79(Me)
End Class
#End Region
