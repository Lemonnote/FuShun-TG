<Command("PhControlAdjust", "TARGET PH |4-9|.|00-99| OPEN  ADD TIME |2-60| ", , , "'3", ), _
 TranslateCommand("zh-TW", "PH控制-調整", "PH值設定 |4-9|.|00-99|  保持目標酸時間 |0-60|  "), _
 Description("CONTROL PH MAX=PH 9.9,MIN=PH 4.0  ; ADD TIME MAX=60M MIN=FAST  "), _
 TranslateDescription("zh-TW", "PH控制  最高=PH 9.9,最低=PH 4.0  ; 保持目標酸時間 最高=60分 快速=0 ")> _
Public NotInheritable Class Command77
    Inherits MarshalByRefObject
    Implements ACCommand
    Public Enum S77
        Off
        Start
        KeepTime
        Pause
        Finish
        Finish_Wash
    End Enum

    Public PhTarget, PhOpenTemp, AddTime As Integer
    Public Wait, WaitKeepTime, WaitOverTime As New Timer, RunBackWait As New Timer
    Public StateString As String
    Public AddLogTime As Boolean

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
            .Command73.Cancel() : .Command74.Cancel() : .Command75.Cancel() : .Command76.Cancel() : .Command78.Cancel() : .Command79.Cancel() : .Command80.Cancel()
            '---------------------------------------------------------------------------------------------------------

            PhTarget = Maximum(param(1) * 100 + param(2), 999) '60*60
            AddTime = MinMax(param(3), 0, 60)
            WaitOverTime.TimeRemaining = 60 * 30
            AddLogTime = False
            State = S77.Start


        End With
    End Function

    Public Function Run() As Boolean Implements ACCommand.Run
        With ControlCode
            Select Case State
                Case S77.Off
                    StateString = ""
                    AddLogTime = False
                    If .Command75.State = Command75.S75.Off And .Command74.State = Command74.S74.Off And .Command76.State = Command76.S76.Off And .Command78.State = Command78.S78.Off And .Command80.State = Command80.S80.Off Then
                        .PhControlFlag = False
                        .PhControl.OpenKeepTime = False
                    End If


                Case S77.Start
                    StateString = ""
                    .StepNumber = .Parent.StepNumber
                    If .PhControlFlag = False Then
                        .PhControlFlag = True
                    End If
                    If WaitOverTime.Finished And Not .IO.CallAck Then
                        AddLogTime = True
                        StateString = If(.Language = LanguageValue.ZhTw, "調整PH時間過長", "Running")
                    ElseIf WaitOverTime.Finished And .IO.CallAck Then
                        StateString = ""
                        WaitOverTime.TimeRemaining = 60 * 30
                        AddLogTime = False
                    End If

                    If .Parent.IsPaused Or Not .IO.MainPumpFB Or Not .IO.SystemAuto Or .IO.PhCheckTemp > 1050 Then


                        'pause the timer
                        WaitKeepTime.Pause()
                        State = S77.Pause
                    End If


                    If .PhControl.OpenKeepTime = True Then
                        WaitKeepTime.TimeRemaining = AddTime * 60
                        State = S77.KeepTime
                    End If

                Case S77.KeepTime

                    StateString = If(.Language = LanguageValue.ZhTw, "PH調整剩餘時間", "Running") & " " & TimerString(WaitKeepTime.TimeRemaining)
                    If .Parent.IsPaused Or Not .IO.MainPumpFB Or Not .IO.SystemAuto Or .IO.PhCheckTemp > 1050 Then


                        'pause the timer
                        WaitKeepTime.Pause()
                        State = S77.Pause
                    End If
                    AddLogTime = False
                    If WaitKeepTime.Finished Then
                        .PhControl.OpenKeepTime = False
                        State = S77.Finish_Wash
                    End If

                Case S77.Pause
                    If Not .IO.MainPumpFB Then
                        StateString = If(.Language = LanguageValue.ZhTw, "馬達未啟動！", "Not Running")
                    End If
                    If .IO.PhCheckTemp > 1050 Then
                        StateString = If(.Language = LanguageValue.ZhTw, "PH檢測桶溫度過高！", "Hot temp.")
                    End If
                    'no longer pause restart the timer and go back to the wait state
                    If (Not .Parent.IsPaused) And .IO.MainPumpFB And .IO.SystemAuto And .IO.PhCheckTemp < 1050 Then

                        If .Parent.StepNumber = .StepNumber Then
                            WaitKeepTime.Restart()
                            State = S77.Start
                        Else
                            State = S77.Off
                        End If

                    End If

                Case S77.Finish_Wash
                    StateString = ""
                    If .PhControlFlag = True Then
                        .PhControlFlag = False
                    End If
                    '.PhWash.Run()
                    'If .PhWash.State = PhWash_.PhWash.Finish Then
                    State = S77.Finish
                    'End If

                Case S77.Finish

                    State = S77.Off



            End Select

        End With
    End Function

    Public Sub Cancel() Implements ACCommand.Cancel
        State = S77.Off
        Wait.Cancel()

    End Sub
    Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged

        PhTarget = Maximum(param(1) * 100 + param(2), 999) '60*60
        AddTime = MinMax(param(3), 0, 60)
    End Sub
#Region "Standard Definitions"
    Private ReadOnly ControlCode As ControlCode
    Public Sub New(ByVal controlCode As ControlCode)
        Me.ControlCode = controlCode
    End Sub
    Friend ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
        Get
            Return State <> S77.Off
        End Get
    End Property
    Public ReadOnly Property IsActive() As Boolean
        Get
            Return State > S77.Start
        End Get
    End Property
    <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S77
    <EditorBrowsable(EditorBrowsableState.Advanced)> Private statewas_ As S77

    Public Property State() As S77
        Get
            Return state_
        End Get
        Private Set(ByVal value As S77)
            state_ = value
        End Set
    End Property
    Public ReadOnly Property Istest() As Boolean
        Get
            Return (State = S77.Start)
        End Get
    End Property
    Public ReadOnly Property IsAlarm() As Boolean
        Get
            Return (State = S77.Start And AddLogTime = True)
        End Get
    End Property
#End Region
End Class

#Region "Class Instance"
Partial Public Class ControlCode
    Public ReadOnly Command77 As New Command77(Me)
End Class
#End Region
