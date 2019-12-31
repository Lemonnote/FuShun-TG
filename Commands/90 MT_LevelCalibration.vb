<Command("MT_LevelCalibration Qty|1-50|", "", , , "'1"), _
Description(""), _
 TranslateCommand("zh-TW", "主缸類比水位校正", "校正次數|1-50|"), _
TranslateDescription("zh-TW", "")> _
Public NotInheritable Class Command90
    Inherits MarshalByRefObject
    Implements ACCommand
    Public StateString As String

    Public Enum S90
        Off
        WaitAuto
        WaitLevelOff
        ClearHistory
        KeyInFirstAnalogy
        KeyInFirstAnalogy2
        Step1
        Step2
        Step3
        Step4
        Step5
        Step6
        Step7
        Step8
        Step9
        Pause
    End Enum

    Public Wait As New Timer
    Public MainLevelSet(50) As Integer
    Public MainLevelAnalogy(50) As Integer
    Public MemoryAnalogy(3) As Short
    Public 次數, 次數1 As Integer
    Public 記錄藥缸水位 As Double
    Public 校正次數 As Integer
    Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
        With ControlCode
      .Command01.Cancel() : .Command02.Cancel() : .Command10.Cancel() : .Command03.Cancel() : .Command04.Cancel()
            .Command05.Cancel() : .Command06.Cancel() : .Command07.Cancel() : .Command08.Cancel()
            .Command09.Cancel() : .Command11.Cancel() : .Command12.Cancel() : .Command13.Cancel()
            .Command14.Cancel() : .Command15.Cancel() : .Command17.Cancel() : .Command18.Cancel()
            .Command19.Cancel() : .Command20.Cancel() : .Command21.Cancel() : .Command22.Cancel()
            .Command32.Cancel() : .Command40.Cancel() : .Command51.Cancel() : .Command52.Cancel()
            .Command54.Cancel() : .Command55.Cancel() : .Command57.Cancel() : .Command58.Cancel()
            .Command61.Cancel() : .Command62.Cancel() : .Command64.Cancel() : .Command65.Cancel()
            .Command61.Cancel() : .Command62.Cancel() : .Command64.Cancel() : .Command65.Cancel()
            .Command67.Cancel() : .Command68.Cancel()
            '--------------------------------------------------------------------------------------------------------PH用
            .PhControl.Cancel() : .PhWash.Cancel() : .PhControlFlag = False : .PhCirculateRun.Cancel() : .Command78.Cancel() : .Command80.Cancel()
            .Command73.Cancel() : .Command74.Cancel() : .Command75.Cancel() : .Command76.Cancel() : .Command77.Cancel() : .Command79.Cancel()
            '---------------------------------------------------------------------------------------------------------


            校正次數 = MinMax(param(1), 0, 50)
            State = S90.WaitAuto
        End With
    End Function

    Public Function Run() As Boolean Implements ACCommand.Run
        With ControlCode


            Select Case State
                Case S90.Off
                    次數 = 0
                    次數1 = 0
                    StateString = ""

                Case S90.WaitAuto
                    StateString = If(.Language = LanguageValue.ZhTw, "系統手動中", "System Manual")
                    If Not .IO.SystemAuto Then Exit Select
                    State = S90.WaitLevelOff

                Case S90.WaitLevelOff
                    StateString = If(.Language = LanguageValue.ZhTw, "請檢查主缸排空、藥缸排空、關掉主馬達", "")
                    If Not .IO.CallAck Then Exit Select
                    Wait.TimeRemaining = 1
                    State = S90.ClearHistory

                Case S90.ClearHistory
                    StateString = If(.Language = LanguageValue.ZhTw, "準備清除-參數 類比水位讀取值跟設定值", "")
                    If Not Wait.Finished Then Exit Select
                    If Not .IO.CallAck Then Exit Select
                    ''*****************************************************************
                    For i = 0 To 50
                        .Parameters.SetMainTankAnalogInput(i) = 0
                        .Parameters.SetMainTankVolume(i) = 0
                    Next
                    ''*****************************************************************
                    Wait.TimeRemaining = 1
                    State = S90.KeyInFirstAnalogy

                Case S90.KeyInFirstAnalogy
                    StateString = If(.Language = LanguageValue.ZhTw, "請手動完成第1次水位讀取值跟設定值", "")
                    If Not Wait.Finished Then Exit Select
                    If Not .IO.CallAck Then Exit Select


                    MainLevelAnalogy(1) = .Parameters.SetMainTankAnalogInput(1)
                    MainLevelSet(1) = .Parameters.SetMainTankVolume(1)


                    '=====================================================清除
                    For i = 0 To 3
                        MemoryAnalogy(i) = 0
                    Next
                    次數 = 0
                    '======================================================
                    Wait.TimeRemaining = 2
                    State = S90.KeyInFirstAnalogy2
                Case S90.KeyInFirstAnalogy2
                    StateString = If(.Language = LanguageValue.ZhTw, "請手動將藥桶水 補至" & .Parameters.MainLevelAnalogyRangeLiter & "L", "")
                    If Not .IO.CallAck Then Exit Select
                    If Not Wait.Finished Then Exit Select
                    State = S90.Step1

                Case S90.Step1  '記憶水位--------------------------------------------------------------------
                    StateString = If(.Language = LanguageValue.ZhTw, "記憶藥缸水位" & 次數 & "次", "")
                    MemoryAnalogy(次數) = .IO.TankBLevel
                    Wait.TimeRemaining = 1
                    State = S90.Step2

                Case S90.Step2  '記憶水位--------------------------------------------------------------------
                    If Not Wait.Finished Then Exit Select

                    If 次數 <= 2 Then

                        次數 = 次數 + 1
                        State = S90.Step1
                    Else
                        記錄藥缸水位 = (MemoryAnalogy(0) + MemoryAnalogy(1) + MemoryAnalogy(2) + MemoryAnalogy(3)) / 4
                        次數1 = 2
                        State = S90.Step3
                    End If

                Case S90.Step3  '藥缸入染機--------------------------------------------------------------------開大加藥、開馬達
                    StateString = If(.Language = LanguageValue.ZhTw, "藥缸-->主缸" & 次數1 & "次", "")
                    If .IO.TankBLevel < 50 Then
                        Wait.TimeRemaining = .Parameters.AddTransferTimeBeforeRinse
                        State = S90.Step4
                    End If

                Case S90.Step4  '藥缸入染機 ，延遲時間--------------------------------------------------------------------
                    StateString = If(.Language = LanguageValue.ZhTw, "藥缸-->主缸" & 次數1 & "次 " & TimerString(Wait.TimeRemaining), "")
                    If Not Wait.Finished Then Exit Select
                    State = S90.Step5


                Case S90.Step5  '藥缸入水--------------------------------------------------------------------入水到80%
                    StateString = If(.Language = LanguageValue.ZhTw, "藥缸進水" & .IO.TankBLevel & " -> " & 記錄藥缸水位, "")
                    If .IO.TankBLevel >= (記錄藥缸水位 * 0.9) Then
                        Wait.TimeRemaining = 2
                        State = S90.Step6
                    End If

                Case S90.Step6  '藥缸入水--------------------------------------------------------------------停2S
                    StateString = If(.Language = LanguageValue.ZhTw, "藥缸進水" & .IO.TankBLevel & " -> " & 記錄藥缸水位, "")
                    If Not Wait.Finished Then Exit Select
                    Wait.TimeRemaining = 2
                    State = S90.Step7

                Case S90.Step7  '藥缸入水--------------------------------------------------------------------開2S
                    StateString = If(.Language = LanguageValue.ZhTw, "藥缸進水" & .IO.TankBLevel & " -> " & 記錄藥缸水位, "")
                    If .IO.TankBLevel >= (記錄藥缸水位 * 0.98) Then
                        Wait.TimeRemaining = 2
                        次數1 = 次數1 + 1
                        State = S90.Step8
                    End If
                    If Not Wait.Finished Then Exit Select
                    Wait.TimeRemaining = 2
                    State = S90.Step6

                Case S90.Step8
                    StateString = If(.Language = LanguageValue.ZhTw, "記憶藥缸水位" & 次數1 & "次", "")

                    If Not Wait.Finished Then Exit Select

                    '===================
                    ' .IO.Parameters_RealValue3 = CType(.Parameters.SetMainTankAnalogInput(次數1 - 2) * 1.1, Short)
                    '==================

                    .Parameters.SetMainTankAnalogInput(次數1 - 1) = .IO.Parameters_RealValue3
                    .Parameters.SetMainTankVolume(次數1 - 1) = .Parameters.SetMainTankVolume(次數1 - 2) + .Parameters.MainLevelAnalogyRangeLiter
                    If 次數1 - 1 >= 校正次數 Then
                        State = S90.Step9
                    Else

                        State = S90.Step3

                    End If

                Case S90.Step9
                    StateString = If(.Language = LanguageValue.ZhTw, "校正完成", "")

            End Select
        End With
    End Function

    Public Sub Cancel() Implements ACCommand.Cancel
        With ControlCode
            State = S90.Off

        End With
    End Sub

    Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
        校正次數 = MinMax(param(1), 0, 50)
    End Sub


#Region "Standard Definitions"
    Private ReadOnly ControlCode As ControlCode
    Public Sub New(ByVal controlCode As ControlCode)
        Me.ControlCode = controlCode
    End Sub
    Friend ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
        Get
            Return State <> S90.Off
        End Get
    End Property
    'Public ReadOnly Property IsActive() As Boolean
    '    Get
    '        Return State > S90.CheckReady
    '    End Get
    'End Property
    Public ReadOnly Property Is大加藥() As Boolean
        Get
            Return (State = S90.Step3) Or (State = S90.Step4)
        End Get
    End Property
    Public ReadOnly Property Is加藥馬達() As Boolean
        Get
            Return (State = S90.Step3) Or (State = S90.Step4)
        End Get
    End Property
    Public ReadOnly Property Is藥缸入水() As Boolean
        Get
            Return (State = S90.Step5) Or (State = S90.Step7)
        End Get
    End Property
    Public ReadOnly Property IsPaused() As Boolean
        Get
            Return State = S90.Pause
        End Get
    End Property
    <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S90
    <EditorBrowsable(EditorBrowsableState.Advanced)> Private statewas_ As S90
    Public Property State() As S90
        Get
            Return state_
        End Get
        Private Set(ByVal value As S90)
            state_ = value
        End Set
    End Property
    Public Property StateWas() As S90
        Get
            Return statewas_
        End Get
        Private Set(ByVal value As S90)
            statewas_ = value
        End Set
    End Property
#End Region
End Class

#Region "Class Instance"
Partial Public Class ControlCode
    Public ReadOnly Command90 As New Command90(Me)
End Class
#End Region
