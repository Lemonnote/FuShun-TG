Public Class ManualDose
    Public Enum ManualDose
        Off
        Dose
        AddDelay
        WashTank
        WashAdd
        Finish
        Pause
    End Enum

    Public Tank, DoseTime, Curve As Integer
    Public Timer As New Timer, LevelTimer As New Timer
    Public AddLevel, StartLevel As Integer
    Public DoseOutput As Integer
    Public AddType As Integer
    Public DoseON As Boolean
    Public StateString As String

    Public Sub Run()
        With ControlCode
            If .IO.ManualDoseBC_Switch Then
                AddLevel = .IO.TankCLevel
            Else
                AddLevel = .IO.TankBLevel
            End If


            Select Case State
                Case ManualDose.Off
                    If .B1Add And .IO.TankBLevel > 50 And Not .IO.ManualDoseBC_Switch Then
                        Tank = 5
                        DoseTime = .Parameters.ManualDoseTime1 * 60
                        AddType = 1
                    ElseIf .B2Add And .IO.TankBLevel > 50 And Not .IO.ManualDoseBC_Switch Then
                        Tank = 5
                        DoseTime = .Parameters.ManualDoseTime2 * 60
                        AddType = 2
                    ElseIf .B3Add And .IO.TankBLevel > 50 And Not .IO.ManualDoseBC_Switch Then
                        Tank = 5
                        DoseTime = .Parameters.ManualDoseTime3 * 60
                        AddType = 3
                    ElseIf .B4Add And .IO.TankBLevel > 50 And Not .IO.ManualDoseBC_Switch Then
                        Tank = 5
                        DoseTime = .Parameters.ManualDoseTime4 * 60
                        AddType = 4
                    ElseIf .B5Add And .IO.TankBLevel > 50 And Not .IO.ManualDoseBC_Switch Then
                        Tank = 5
                        DoseTime = .Parameters.ManualDoseTime5 * 60
                        AddType = 5

                    ElseIf .B1Add And .IO.TankCLevel > 50 And .IO.ManualDoseBC_Switch Then
                        Tank = 4
                        DoseTime = .Parameters.ManualDoseTime1 * 60
                        AddType = 1
                    ElseIf .B2Add And .IO.TankCLevel > 50 And .IO.ManualDoseBC_Switch Then
                        Tank = 4
                        DoseTime = .Parameters.ManualDoseTime2 * 60
                        AddType = 2
                    ElseIf .B3Add And .IO.TankCLevel > 50 And .IO.ManualDoseBC_Switch Then
                        Tank = 4
                        DoseTime = .Parameters.ManualDoseTime3 * 60
                        AddType = 3
                    ElseIf .B4Add And .IO.TankCLevel > 50 And .IO.ManualDoseBC_Switch Then
                        Tank = 4
                        DoseTime = .Parameters.ManualDoseTime4 * 60
                        AddType = 4
                    ElseIf .B5Add And .IO.TankCLevel > 50 And .IO.ManualDoseBC_Switch Then
                        Tank = 4
                        DoseTime = .Parameters.ManualDoseTime5 * 60
                        AddType = 5
                    Else
                        .B1Add = False
                        .B2Add = False
                        .B3Add = False
                        .B4Add = False
                        .B5Add = False
                        Exit Select
                    End If
                    Curve = .Parameters.ManualDoseCurve

                    ' Ok, so let's start dosing
                    Timer.TimeRemaining = DoseTime
                    ' Sample the start level
                    If Tank = 4 Then
                        StartLevel = .IO.TankCLevel
                    Else
                        StartLevel = .IO.TankBLevel
                    End If
                    State = ManualDose.Dose

                Case ManualDose.Dose
                    If Not (.B1Add Or .B2Add Or .B3Add Or .B4Add Or .B5Add) Then
                        State = ManualDose.Off
                    End If
                    If Tank = 4 Then
                        StateString = If(.Language = LanguageValue.ZhTw, "C缸手動Dosing ", "Tank C manual dosing ") & TimerString(Timer.TimeRemaining)
                    Else
                        StateString = If(.Language = LanguageValue.ZhTw, "B缸手動Dosing ", "Tank B manual dosing ") & TimerString(Timer.TimeRemaining)

                    End If
                    Static delay7 As New DelayTimer
                    DoseOutput = MinMax(((.IO.TankBLevel - SetPoint()) * .Parameters.DosingPumpSpeed), 0, 1000)
                    'If DoseOutput <= 1 Then
                    '    DoseOutput = 1
                    'End If
                    DoseON = delay7.Run((DoseOutput > 0), 2)
                    If Timer.Finished Then
                        DoseOutput = 1000
                        State = ManualDose.AddDelay
                        LevelTimer.TimeRemaining = .Parameters.AddTransferTimeBeforeRinse

                    End If
                    If Not .IO.MainPumpFB Then
                        Timer.Pause()
                        State = ManualDose.Pause

                    End If
                    If Tank = 4 And ControlCode.CAddStop Then State = ManualDose.Finish
                    If Tank = 5 And ControlCode.BAddStop Then State = ManualDose.Finish

                Case ManualDose.AddDelay
                    If Not (.B1Add Or .B2Add Or .B3Add Or .B4Add Or .B5Add) Then
                        State = ManualDose.Off
                    End If
                    If AddLevel > 50 Then LevelTimer.TimeRemaining = .Parameters.AddTransferTimeBeforeRinse
                    If LevelTimer.Finished Then
                        Wait.TimeRemaining = .Parameters.AddTransferTimeAfterRinse
                        State = ManualDose.WashTank
                    End If
                Case ManualDose.WashTank
                    If Wait.Finished Then
                        Wait.TimeRemaining = .Parameters.AddTransferTimeBeforeRinse
                        State = ManualDose.WashAdd
                    End If

                Case ManualDose.WashAdd
                    If Wait.Finished Then
                        State = ManualDose.Finish
                    End If

                Case ManualDose.Finish
                    If Tank = 4 Then
                        .TankCReady = False
                    Else
                        .TankBReady = False
                    End If
                    DoseOutput = 0
                    Tank = 0
                    AddType = 0
                    DoseON = False
                    .B1Add = False
                    .B2Add = False
                    .B3Add = False
                    .B4Add = False
                    .B5Add = False
                    State = ManualDose.Off
                Case ManualDose.Pause
                    If Tank = 4 Then
                        StateString = If(.Language = LanguageValue.ZhTw, "C缸手動Dosing暫停", "Tank C manual dosing paused") & TimerString(Timer.TimeRemaining)
                    Else
                        StateString = If(.Language = LanguageValue.ZhTw, "B缸手動Dosing暫停", "Tank B manual dosing paused") & TimerString(Timer.TimeRemaining)

                    End If
                    If .IO.MainPumpFB Then
                        Timer.Restart()
                        State = ManualDose.Dose
                    End If

            End Select
        End With
    End Sub
    Public ReadOnly Property SetPoint() As Integer
        Get
            'If timer has finished, just return 0
            If Timer.Finished Then Return 0

            'Amount we should have transferred so far
            Dim elapsedTime = (DoseTime - Timer.TimeRemaining) / DoseTime
            Dim timeToGo = 1 - elapsedTime
            Dim linearTerm = elapsedTime
            Dim transferAmount = StartLevel * linearTerm

            'Calculate scaling factor (0-1) for progressive and digressive curves
            If Curve > 0 Then
                Dim scalingFactor = (10 - Curve) / 10
                'Calculate term for progressive transfer (0-1) if odd curve
                If (Curve Mod 2) = 1 Then
                    Dim maxOddCurve = 1 - Math.Sqrt(1 - (elapsedTime * elapsedTime * elapsedTime))
                    Dim oddTerm = (((9 - Curve) * elapsedTime) + ((Curve + 1) * maxOddCurve)) / 10
                    transferAmount = StartLevel * oddTerm
                Else
                    'Calculate term for digressive transfer (0-1) if even curve
                    Dim maxEvenCurve = 1 - Math.Sqrt(1 - (timeToGo * timeToGo * timeToGo))
                    Dim evenTerm = (((10 - Curve) * timeToGo) + (Curve * maxEvenCurve)) / 10
                    transferAmount = StartLevel * (1 - evenTerm)
                End If
            End If

            'Calculate and limit to 0-1000
            Return Math.Min(Math.Max(0, StartLevel - CType(transferAmount, Integer)), 1000)
        End Get
    End Property

#Region "Standard Definitions"

    Public Sub Cancel()
        With ControlCode
            State = ManualDose.Off
            Tank = 0
            AddType = 0
            LevelTimer.Cancel()

        End With
    End Sub
    Private ReadOnly ControlCode As ControlCode
    Public Sub New(ByVal controlCode As ControlCode)
        Me.ControlCode = controlCode
    End Sub
    <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As ManualDose
    Public Property State() As ManualDose
        Get
            Return state_
        End Get
        Private Set(ByVal value As ManualDose)
            state_ = value
        End Set
    End Property
    Public ReadOnly Wait As New Timer

    'this is for the dosing valve
    Public ReadOnly Property IsDosing() As Boolean
        Get
            Return ((State = ManualDose.Dose) And DoseON) Or (State = ManualDose.AddDelay) Or (State = ManualDose.WashAdd)
        End Get
    End Property
    Public ReadOnly Property IsOn() As Boolean
        Get
            Return (State <> ManualDose.Off)
        End Get
    End Property
    Public ReadOnly Property IsTransfer() As Boolean
        Get
            Return (State = ManualDose.AddDelay)
        End Get
    End Property
    Public ReadOnly Property IsTransferPump() As Boolean
        Get
            Return (State = ManualDose.Dose) Or (State = ManualDose.AddDelay) Or (State = ManualDose.WashAdd)
        End Get
    End Property
    Public ReadOnly Property IsWashtank() As Boolean
        Get
            Return (State = ManualDose.WashTank)
        End Get
    End Property
#End Region
End Class

Partial Class ControlCode
    Public ReadOnly ManualDose As New ManualDose(Me)
End Class
