<Command("Temperature", "SetTemp |0-145|C Gradient |0-9|.|0-9| HoldTime |0-999|", "(('2*10)+'3 )", "'1", "'4"), _
 TranslateCommand("zh-TW", "溫度控制", "溫度 |0-145|度 斜率 |0-9|.|0-9| 時間 |0-999|分")> _
Public NotInheritable Class Command01
    Inherits MarshalByRefObject
    Implements ACCommand

    Public Enum S01
        Off
        Start
        Run
        Hold
        Complete
        Pause
    End Enum

    Public Wait As New Timer
    Public TargetTemp As Integer
    Public Gradient As Integer
    Public StateString As String
    Public HoldTime As Integer
    Public HoldTimeWas As Integer
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
            .TemperatureControlFlag = False

            .PhControl.Cancel()
            .PhCheck.Cancel()
            .PhCheckError.Cancel()

            TargetTemp = Maximum(param(1) * 10, 1500)
            Gradient = param(2) * 10 + param(3)
      If param.Length >= 5 Then
        HoldTime = 60 * param(4)
      Else
        HoldTime = 0
      End If
      Wait.TimeRemaining = HoldTime
      Wait.Pause()

            'Check Temperature mode - change during TPHold if necessary
            '.TemperatureControl.TempMode = 0
            'If .TemperatureControl.Parameters_HeatCoolModeChange = 1 Then .TemperatureControl.TempMode = 2
            'If .TemperatureControl.Parameters_HeatCoolModeChange = 2 Then .TemperatureControl.TempMode = 2

            State = S01.Start

        End With
    End Function

    Public Function Run() As Boolean Implements ACCommand.Run
        With ControlCode
            Select Case State
                Case S01.Off
                    StateString = ""

                Case S01.Start
                    StateString = ""
                    .TempControlFlag = True
                    .TemperatureControlFlag = True
                    .TemperatureControl.CoolingIntegral = .TemperatureControl.Parameters_CoolIntegral
                    .TemperatureControl.CoolingMaxGradient = .TemperatureControl.Parameters_CoolMaxGradient
                    .TemperatureControl.CoolingPropBand = .TemperatureControl.Parameters_CoolPropBand
                    .TemperatureControl.CoolingStepMargin = .TemperatureControl.Parameters_CoolStepMargin
                    .TemperatureControl.Start(.IO.MainTemperature, TargetTemp, Gradient)
                    If .IO.MainTemperature - 30 > TargetTemp Then
                        .CoolNow = True
                        .HeatNow = False
                        .TemperatureControl.TempMode = 4
                    Else
                        .CoolNow = False
                        .HeatNow = True
                        .TemperatureControl.TempMode = 3
                    End If
                    State = S01.Run

                Case S01.Run
                    StateString = ""
                    'StateString = If(.Language = LanguageValue.ZhTw, "目標溫度 ", "SetTemp") & ":" & TargetTemp / 10 & " " & If(.Language = LanguageValue.ZhTw, "斜率", "Gradient") & ":" & Gradient & " " & If(.Language = LanguageValue.ZhTw, "時間", "Time") & ":" & HoldTime / 60
                    If .Parent.IsPaused Or Not .IO.MainPumpFB Or Not .IO.SystemAuto Then
                        Wait.Pause()
                        StateWas = State
                        State = S01.Pause
                    End If
                    If Not (.IO.MainTemperature < (TargetTemp + 40) And .IO.MainTemperature > (TargetTemp - 10)) Then Exit Select
                    Wait.Restart()
                    State = S01.Hold

                Case S01.Hold
                    StateString = If(.Language = LanguageValue.ZhTw, "持溫時間 ", "Hold Time ") & " " & TimerString(Wait.TimeRemaining)
                    If .Parent.IsPaused Or Not .IO.MainPumpFB Or Not .IO.SystemAuto Then
                        .TemperatureControl.Cancel()
                        .TemperatureControl.Start(.IO.MainTemperature, TargetTemp, Gradient)
                        Wait.Pause()
                        StateWas = State
                        State = S01.Pause
                        HoldTimeWas = HoldTime
                    End If
                    If Not Wait.Finished Then Exit Select
                    State = S01.Complete

                Case S01.Complete
                    If .CoolNow Then
                        .CoolNow = False
                        .TemperatureControlFlag = False
                    End If
                    State = S01.Off

                Case S01.Pause
                    StateString = If(.Language = LanguageValue.ZhTw, "暫停 ", "Paused ") & " " & TimerString(Wait.TimeRemaining)
                    If (Not .Parent.IsPaused) And .IO.MainPumpFB And .IO.SystemAuto Then
                        .TemperatureControl.Cancel()
                        .TemperatureControl.Start(.IO.MainTemperature, TargetTemp, Gradient)
                        State = StateWas
                        StateWas = S01.Off
                        If State = S01.Run Then
                            Wait.TimeRemaining = HoldTime
                            Wait.Pause()
                        ElseIf State = S01.Hold Then
                            If ((HoldTime - HoldTimeWas) + Wait.TimeRemaining) > 0 Then
                                Wait.TimeRemaining = (HoldTime - HoldTimeWas) + Wait.TimeRemaining
                            Else
                                Wait.Restart()
                            End If
                        End If
                    End If
            End Select
        End With
    End Function


    Public Sub Cancel() Implements ACCommand.Cancel
        State = S01.Complete
        Wait.Cancel()
    End Sub

    Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
        With ControlCode
            .PumpStopRequest = False
            TargetTemp = Maximum(param(1) * 10, 1500)
            Gradient = param(2) * 10 + param(3)
            HoldTime = 60 * param(4)
            Wait.TimeRemaining = HoldTime
            Wait.Pause()
            .TemperatureControl.Cancel()
            If .IO.MainPumpFB And .IO.LowLevel Then
                .PumpStopRequest = False
                .PumpOn = True
            End If

            If .Parameters.PhCirRuning = 1 Then
                .PhCirRun = True
            End If
            State = S01.Start
        End With
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

    <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S01
    Public Property State() As S01
        Get
            Return state_
        End Get
        Private Set(ByVal value As S01)
            state_ = value
        End Set
    End Property
    <EditorBrowsable(EditorBrowsableState.Advanced)> Private statewas_ As S01
    Public Property StateWas() As S01
        Get
            Return statewas_
        End Get
        Private Set(ByVal value As S01)
            statewas_ = value
        End Set
    End Property


    Public ReadOnly Property IsRamping() As Boolean
        Get
            Return (State = S01.Run)
        End Get
    End Property
    Public ReadOnly Property IsHolding() As Boolean
        Get
            Return (State = S01.Hold)
        End Get
    End Property
    Public ReadOnly Property IsPaused() As Boolean
        Get
            Return (State = S01.Pause)
        End Get
    End Property
#End Region
End Class

#Region "Class Instance"
Partial Public Class ControlCode
    Public ReadOnly Command01 As New Command01(Me)
End Class
#End Region
