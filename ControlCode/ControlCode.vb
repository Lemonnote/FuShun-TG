Imports System.IO
Imports System.Data.SqlClient
Public NotInheritable Class ControlCode
    Inherits MarshalByRefObject
    Implements ACControlCode
    Public Parent As ACParent
    Friend Language As LanguageValue
    Public TemperatureControl As New TemperatureControl
    Public Recipe As New Recipe
    Public StepNumber As Integer
    Public TotalWeight As Integer
    Public LiquidRatio As Integer
    Public ShowTotalVolume As Integer
    Public TargetVolume As Integer
    Public CheckCounter As Integer
    Public CheckCounterTimer As New Timer
    Public ATankFillHot As Boolean
    Public Setpoint As Integer
    Public BTankHighLevel As Boolean
    Public BTankMiddleLevel As Boolean
    Public BTankLowLevel As Boolean
    Public BTankHeatStartRequest As Boolean
    Public TankBReady As Boolean
    Public TankBMixOn As Boolean
    Public CTankHighLevel As Boolean
    Public CTankMiddleLevel As Boolean
    Public CTankLowLevel As Boolean
    Public CTankHeatStartRequest As Boolean
    Public TankCMixOn As Boolean
    Public PumpOn As Boolean
    Public TankCReady As Boolean
    Public CoolNow As Boolean
    Public HeatNow As Boolean
    Public TemperatureControlFlag As Boolean
    Public RemoteDisplayError As Boolean
    Public FlashFlag As Boolean
    Public FlashFlag2 As Boolean
    Public FlashFlag3 As Boolean
    Public PumpSpeed As Integer
    Public RollerSpeed As Integer
    Public PlaitingSpeed As Integer
    Public PressureInTemp As Integer
    Public PressureOutTemp As Integer
    Public BAddStop As Boolean
    Public CAddStop As Boolean
    Public TempControlFlag As Boolean
    Public PressureIn As Boolean
    Public DosingHoldTemperature As Integer
    Public RecallLevel As Integer
    Public MainTankTargetVolume As Integer
    Public MainTankActualVolume As Integer
    Public MainTankVolume As Integer
    Public TotalWeightWas As Integer
    Public LiquidRatioWas As Integer
    Public FlowMeterCount As Integer
    Public FlowMeterCountOn As Boolean
    Public LowLevel As Boolean
    Public MiddleLevel As Boolean
    Public HighLevel As Boolean
    Public OverflowLevel As Boolean
    Public SideTankNotReady As Boolean
    Public HeatOnWas As Boolean
    Public SetBTankTemperature As Integer
  Public SetCTankTemperature As Integer
  Public DyelotRemainMinutes As Integer
  Public ColorName As String
  '==============================================20170322
  Public ProgramStopCleanDatabase As Boolean
  Public SQL成功 As Integer
  Public SQL顯示狀態 As String
  Public StopProgram As Boolean
  Public NotAllowLoadProgramAlarmTimer As New Timer
  '==============================================09/13
  Public cs_DispenseState1 As String
  Public qs_DispenseState1 As String
  Public cn_DispenseState1 As SqlClient.SqlConnection
  Public cmd_DispenseState1 As SqlClient.SqlCommand
  Public da_DispenseState1 As SqlClient.SqlDataAdapter
  Public cb_DispenseState1 As SqlClient.SqlCommandBuilder
  Public ds_DispenseState1 As DataSet
  Public dt_DispenseState1 As DataTable
  Public cs_Recipe1 As String
  Public qs_Recipe1 As String
  Public cn_Recipe1 As SqlClient.SqlConnection
  Public cmd_Recipe1 As SqlClient.SqlCommand
  Public da_Recipe1 As SqlClient.SqlDataAdapter
  Public cb_Recipe1 As SqlClient.SqlCommandBuilder
  Public ds_Recipe1 As DataSet
  Public dt_Recipe1 As DataTable
    '*************************************************************************
    Public PhFillLevel As Integer
    Public phtest As Double
    Public value1 As Double = 0
    Public testdata As Integer
    Public PhControlFlag As Boolean
    Public SetPointShow As Double           '顯示MIMIC 設定溫度
    Public MainTempShow As Double            '顯示MIMIC 實際溫度
    Public MathHacTimes As New TimerUp
    Public MathHacFlag As Boolean
    Public UseHacThisValue As Integer
    Public UseHacAllTotal, UseHacAllTotal2 As Integer

    Public PhToCPump As Boolean 'PH回流專用的加藥泵
    Public PhToAdd As Boolean 'PH回流專用的加藥閥
    Public PhToDrain As Boolean 'PH回流專用的排水閥

    Public test1, test2, test3, test4, test5, test6, test7, test8, test9, test10, test11, test12, _
test13, test14, test15, test17, test18, test19, test20, test21, test22, test23, test26, test27, test28, test31, test32, test33 As Double
    Public 補償狀態 As String
    Public test24, test16 As String
    Public test25, test29, test30, test35 As Boolean
    Public PhShowPic As Boolean
    Public PhShowData As String
    Public P11, P22 As Double
    Private DelayAddTime As New Timer
    Private DelayAddHac As Boolean
    Public PH再檢測 As Integer
    Public PH檢測_短時間內不在檢測 As Boolean
    Public PH再檢測時間 As New Timer
    Public PhCirRun As Boolean
    Public 已經確保120秒檢測完成, 短時間內重新執行, 是否縮短檢測時間 As Boolean
    Public 加酸時間 As New Timer
    Public 加酸次數 As Integer
    Public 補酸狀態分析 As Integer
    '**************************************************************************
    '檢查溫度控制時，主缸溫度是否有變化，用以判定蒸氣不足或是冷卻水不足
    Public MainTempStopChange As Boolean
    Public CheckMainTempTimer As New Timer

    Public FirstScanDone As Boolean
    Public PumpStartRequest As Boolean
    Public PumpStopRequest As Boolean
    Public btankreadywas As Boolean
    Public ctankreadywas As Boolean
    Public slowflash As New Flasher
    'machine idle time stuf
    Public ProgramStoppedTimer As New TimerUp                       'Program Stopped Timer
    Public ProgramStoppedTime As Integer
    Public CycleTime As Integer
    Public DispenseStep As Integer
    Public DispenseTank As Integer

    'On Off升降溫
    Public HeatValve As Boolean
    Public CoolValve As Boolean
    Public TemperatureControlTimer As New Timer
    Public TemperatureControlTime As Integer
    Public CoolValveOpenTimer As New Timer
    Public CoolValveOpenTime As Integer
    Public HeatValveOpenTimer As New Timer
    Public HeatValveOpenTime As Integer
  Public CoolWaterForChange As Boolean

    'BC tank 
    Public BDosingOn As Boolean
    Public BAllinOn As Boolean
    Public BCirMixOn As Boolean
    Public BInjOn As Boolean
    Public BPumpOn As Boolean
    Public CDosingOn As Boolean
    Public CAllinOn As Boolean
    Public CCirMixOn As Boolean
    Public CInjOn As Boolean
    Public CPumpOn As Boolean
  Public 加藥馬達速度 As Integer
  Public C加藥馬達速度 As Integer
    '手動加藥按鈕
    Public B1Add As Boolean
    Public B1AddWas As Boolean
    Public B2Add As Boolean
    Public B2AddWas As Boolean
    Public B3Add As Boolean
    Public B3AddWas As Boolean
    Public B4Add As Boolean
    Public B4AddWas As Boolean
    Public B5Add As Boolean
    Public B5AddWas As Boolean

    '=====================================================================
    '連接SPC資料庫用的變數
    Public SPCConnectTimer As New Timer
    Public SPCConnectError As Boolean
    Public ComputerName As String
    Public SQL連線狀況 As Integer
    Public SQL連線狀況1 As Integer

    Public cs_DispenseState As String
    Public qs_DispenseState As String
    Public cn_DispenseState As SqlClient.SqlConnection
    Public cmd_DispenseState As SqlClient.SqlCommand
    Public da_DispenseState As SqlClient.SqlDataAdapter
    Public cb_DispenseState As SqlClient.SqlCommandBuilder
    Public ds_DispenseState As DataSet
    Public dt_DispenseState As DataTable

    Public cs_Recipe As String
    Public qs_Recipe As String
    Public cn_Recipe As SqlClient.SqlConnection
    Public cmd_Recipe As SqlClient.SqlCommand
    Public da_Recipe As SqlClient.SqlDataAdapter
    Public cb_Recipe As SqlClient.SqlCommandBuilder
    Public ds_Recipe As DataSet
    Public dt_Recipe As DataTable
    'Public SPCServerName As String = ".\SQLEXPRESS"
    'Public SPCUserName As String = "sa"
    'Public SPCPassword As String = "1234"
  Public SPCServerName As String
  Public SPCUserName As String
  Public SPCPassword As String
  Public StepNumber1(50) As String
  Public ProductCode(50) As String
  Public ProductType(50) As String
  Public Grams(50) As String
  Public DispenseGrams(50) As String
  Public DispenseResult(50) As String

  '染料資料
  Public DyeStepNumber(10) As String
  Public DyeCode(10) As String
  Public DyeGrams(10) As String
  Public DyeDispenseGrams(10) As String
  Public DyeDispenseResult(10) As String
  '助劑資料
  Public ChemicalStepNumber(40) As String
  Public ChemicalCode(40) As String
  Public ChemicalGrams(40) As String
  Public ChemicalDispenseGrams(40) As String
  Public ChemicalDispenseResult(40) As String
    '==========================================================

    '呼叫252跟302用變數
    Public ChemicalEnabled As Integer
    Public ChemicalCallOff As Integer            'We fill in this value - prepare commands
    Public ChemicalTank As Integer               'We fill in this value - prepare commands
    Public ChemicalState As Integer              'This value is filled in by host / auto dispenser
    Public ChemicalProducts As String            'This value is filled in by host / auto dispenser
    Public DyeEnabled As Integer
    Public DyeCallOff As Integer                 'We fill in this value - prepare commands
    Public DyeTank As Integer                    'We fill in this value - prepare commands
    Public DyeState As Integer                   'This value is filled in by host / auto dispenser
    Public DyeProducts As String                 'This value is filled in by host / auto dispenser
    Public Call252AddDye As Boolean
    Public Wait252Scheduled As Boolean
    Public CallFor302D As Boolean
    Public WaitFor302D As Boolean
    Public RunCallLA252 As Boolean
    Public LA252Ready As Boolean
    Public RunCallLA302 As Boolean
    Public LA302Ready As Boolean
    Public UpdatePowderDispenseResult As Boolean

    '======
    Public 粉體步驟 As Integer
    Public 粉體藥桶 As Integer

    '跟SPC溝通用的變數
    Public CCallOff As String
    Public CTank As String
    Public CState As String
    Public CEnabled As String
    Public DCallOff As String
    Public DTank As String
    Public DState As String
    Public DEnabled As String
    Public 粉體呼叫 As String

    Public 工單 As String
    Public 重染 As Integer

    '自動備藥訊息
    Public DyeStepDispensing(12) As Boolean
    Public ChemicalStepDispensing(12) As Boolean
    Public DyeStepReady(12) As Boolean
    Public ChemicalStepReady(12) As Boolean

    '==========================================================

    '給mimic用的變數
    Public Mimic_MainTemp As Integer
    Public Mimic_BTankLevel As Integer
    Public Mimic_CTankLevel As Integer
    Public Mimic_MainLevel As Integer
    Public Mimic_TempControl As Integer
    Public Mimic_BDosing As Boolean
    Public Mimic_BDrain As Boolean
    Public Mimic_Blevel As Double
    Public Mimic_Bsetep As Double
    Public TemperatureControlAnalog As Short
    '-------------------------------------------------
    Private WaitHeatTime, WaitHacTime, 回流桶時間 As New Timer
    Private 回流動作 As Integer
    Private 回流動作Flag As Boolean
    '-------------------------------------------------
  Public ShowFillTotalValve, ShowFill1, ShowFill2 As Integer
  Public ShowFillTotalValve2, ShowFill3, ShowFill4 As Integer
  Private ShowFillFlag As Boolean
  Private ShowFillFlag2 As Boolean
  Public 總水量累計 As Integer
  Public 工單累計 As Integer
  '===============================================================
  Public FabricCycleInput1Times As Integer
  Public FabricCycleInput2Times As Integer
  Public FabricCycle1FirstInput As Boolean
  Public FabricCycle2FirstInput As Boolean
  Public FabricCycleInput1Was As Boolean
  Public FabricCycleInput2Was As Boolean
  Public FabricCycleTime1 As Integer
  Public FabricCycleTime2 As Integer
  Public FabricCycleTimer1 As New TimerUp
  Public FabricCycleTimer2 As New TimerUp

  Public FabricCycleInput3Times As Integer
  Public FabricCycleInput4Times As Integer
  Public FabricCycle3FirstInput As Boolean
  Public FabricCycle4FirstInput As Boolean
  Public FabricCycleInput3Was As Boolean
  Public FabricCycleInput4Was As Boolean
  Public FabricCycleTime3 As Integer
  Public FabricCycleTime4 As Integer
  Public FabricCycleTimer3 As New TimerUp
  Public FabricCycleTimer4 As New TimerUp

  Public FabricCycleInput5Times As Integer
  Public FabricCycleInput6Times As Integer
  Public FabricCycle5FirstInput As Boolean
  Public FabricCycle6FirstInput As Boolean
  Public FabricCycleInput5Was As Boolean
  Public FabricCycleInput6Was As Boolean
  Public FabricCycleTime5 As Integer
  Public FabricCycleTime6 As Integer
  Public FabricCycleTimer5 As New TimerUp
  Public FabricCycleTimer6 As New TimerUp

  Public FabricCycleInput7Times As Integer
  Public FabricCycleInput8Times As Integer
  Public FabricCycle7FirstInput As Boolean
  Public FabricCycle8FirstInput As Boolean
  Public FabricCycleInput7Was As Boolean
  Public FabricCycleInput8Was As Boolean
  Public FabricCycleTime7 As Integer
  Public FabricCycleTime8 As Integer
  Public FabricCycleTimer7 As New TimerUp
  Public FabricCycleTimer8 As New TimerUp
  Public ChangeLanguage As String
  '*************************************************************************
  Public PressureDifferent As Short
  Public EaioRealValue1 As Short
  Public EaioRealValue2 As Short
  Public EaioRealValue3 As Short
  Public EaioRealValue4 As Short
  Public EaioRealValue5 As Short
  Public EaioRealValue6 As Short
  Public EaioRealValue7 As Short
  Public EaioRealValue8 As Short
  Public EaioRealValue9 As Short
  Public EaioRealValue10 As Short
  Public EaioRealValue11 As Short
  Public RealValueTime As New Timer

    Public Sub New(ByVal parent As ACParent)
        Me.Parent = parent
        Select Case parent.CultureName
            Case "zh-TW" : Language = LanguageValue.ZhTw
            Case "zh-CN" : Language = LanguageValue.ZhCn
            Case Else : Language = LanguageValue.English
        End Select
    End Sub

    Public Sub StartUp() Implements ACControlCode.StartUp
        If True Then  ' Set to True to start in debug mode
            ' Parent.Mode = Mode.Debug
            Translations.Load()
            St.Load()
            '**************************************************
            Dim readText As String = My.Computer.FileSystem.ReadAllText("Setup.ini")
            Dim objStreamReader As StreamReader
            Dim strLine As String
            Dim i As Integer = 0

            'Pass the file path and the file name to the StreamReader constructor.
            objStreamReader = New StreamReader("Setup.ini")

            'Read the first line of text.
            strLine = objStreamReader.ReadLine

            'Continue to read until you reach the end of the file.
            Do While Not strLine Is Nothing
                i = i + 1

                If i = 1 Then
                    SPCServerName = strLine
                ElseIf i = 2 Then
                    SPCUserName = strLine
                ElseIf i = 3 Then
                    SPCPassword = strLine
                End If
                'Write the line to the Console window.
                Console.WriteLine(strLine)
                'Read the next line.
                strLine = objStreamReader.ReadLine
            Loop
            '**************************************************
        End If
    End Sub

    Public Sub ShutDown() Implements ACControlCode.ShutDown
    End Sub

    Public Sub Run() Implements ACControlCode.Run

    '=================================================測試用.....底
    '計算布速
    If IO.MainPumpFB Then
      If IO.FabricCycleInput1 And Not FabricCycleInput1Was Then
        FabricCycleTime1 = Maximum(FabricCycleTimer1.TimeElapsed, 999)
        FabricCycleTimer1.Start()
        FabricCycleInput1Was = Not FabricCycleInput1Was
      ElseIf FabricCycleTimer1.TimeElapsed > Parameters.MaximumFabricCycleTime Then
        FabricCycleTime1 = Parameters.MaximumFabricCycleTime
        FabricCycleTimer1.Start()
      End If
      FabricCycleInput1Was = IO.FabricCycleInput1
      If IO.FabricCycleInput2 And Not FabricCycleInput2Was Then
        FabricCycleTime2 = Maximum(FabricCycleTimer2.TimeElapsed, 999)
        FabricCycleTimer2.Start()
        FabricCycleInput2Was = Not FabricCycleInput2Was
      ElseIf FabricCycleTimer2.TimeElapsed > Parameters.MaximumFabricCycleTime Then
        FabricCycleTime2 = Parameters.MaximumFabricCycleTime
        FabricCycleTimer2.Start()
      End If
      FabricCycleInput2Was = IO.FabricCycleInput2


      If IO.FabricCycleInput3 And Not FabricCycleInput3Was Then
        FabricCycleTime3 = Maximum(FabricCycleTimer3.TimeElapsed, 999)
        FabricCycleTimer3.Start()
        FabricCycleInput3Was = Not FabricCycleInput3Was
      ElseIf FabricCycleTimer3.TimeElapsed > Parameters.MaximumFabricCycleTime Then
        FabricCycleTime3 = Parameters.MaximumFabricCycleTime
        FabricCycleTimer3.Start()
      End If
      FabricCycleInput3Was = IO.FabricCycleInput3


      If IO.FabricCycleInput4 And Not FabricCycleInput4Was Then
        FabricCycleTime4 = Maximum(FabricCycleTimer4.TimeElapsed, 999)
        FabricCycleTimer4.Start()
        FabricCycleInput4Was = Not FabricCycleInput4Was
      ElseIf FabricCycleTimer4.TimeElapsed > Parameters.MaximumFabricCycleTime Then
        FabricCycleTime4 = Parameters.MaximumFabricCycleTime
        FabricCycleTimer4.Start()
      End If
      FabricCycleInput4Was = IO.FabricCycleInput4


      If IO.FabricCycleInput5 And Not FabricCycleInput5Was Then
        FabricCycleTime5 = Maximum(FabricCycleTimer5.TimeElapsed, 999)
        FabricCycleTimer5.Start()
        FabricCycleInput5Was = Not FabricCycleInput5Was
      ElseIf FabricCycleTimer5.TimeElapsed > Parameters.MaximumFabricCycleTime Then
        FabricCycleTime5 = Parameters.MaximumFabricCycleTime
        FabricCycleTimer5.Start()
      End If
      FabricCycleInput5Was = IO.FabricCycleInput5


      If IO.FabricCycleInput6 And Not FabricCycleInput6Was Then
        FabricCycleTime6 = Maximum(FabricCycleTimer6.TimeElapsed, 999)
        FabricCycleTimer6.Start()
        FabricCycleInput6Was = Not FabricCycleInput6Was
      ElseIf FabricCycleTimer6.TimeElapsed > Parameters.MaximumFabricCycleTime Then
        FabricCycleTime6 = Parameters.MaximumFabricCycleTime
        FabricCycleTimer6.Start()
      End If
      FabricCycleInput6Was = IO.FabricCycleInput6


      If IO.FabricCycleInput7 And Not FabricCycleInput7Was Then
        FabricCycleTime7 = Maximum(FabricCycleTimer7.TimeElapsed, 999)
        FabricCycleTimer7.Start()
        FabricCycleInput7Was = Not FabricCycleInput7Was
      ElseIf FabricCycleTimer7.TimeElapsed > Parameters.MaximumFabricCycleTime Then
        FabricCycleTime7 = Parameters.MaximumFabricCycleTime
        FabricCycleTimer7.Start()
      End If
      FabricCycleInput7Was = IO.FabricCycleInput7


      If IO.FabricCycleInput8 And Not FabricCycleInput8Was Then
        FabricCycleTime8 = Maximum(FabricCycleTimer8.TimeElapsed, 999)
        FabricCycleTimer8.Start()
        FabricCycleInput8Was = Not FabricCycleInput8Was
      ElseIf FabricCycleTimer8.TimeElapsed > Parameters.MaximumFabricCycleTime Then
        FabricCycleTime8 = Parameters.MaximumFabricCycleTime
        FabricCycleTimer8.Start()
      End If
      FabricCycleInput8Was = IO.FabricCycleInput8


    Else
      FabricCycleTime1 = 0
      FabricCycleTime2 = 0
      FabricCycleTime3 = 0
      FabricCycleTime4 = 0
      FabricCycleTime5 = 0
      FabricCycleTime6 = 0
      FabricCycleTime7 = 0
      FabricCycleTime8 = 0
    End If

        '釋放資源給其他程式，避免站用100%CPU造成當機，且讓每次迴圈延遲10ms
        Application.DoEvents()
        System.Threading.Thread.Sleep(50)

        'machine idle time stuff
        If Not FirstScanDone Then ProgramStoppedTimer.Start()

        'Program running state changes
        Static WasProgramRunning As Boolean
        If Parent.IsProgramRunning Then            'A Program is running
            Static ProgramRunTimer As New TimerUp  ' program run timer
            CycleTime = ProgramRunTimer.TimeElapsed
            If Not WasProgramRunning Then     'A Program has just started
                ProgramStoppedTime = ProgramStoppedTimer.TimeElapsed
                ProgramStoppedTimer.Pause()
                ProgramRunTimer.Start()
            End If
        Else
            If WasProgramRunning Then         'A program has just finished
                ProgramStoppedTimer.Start()
                TemperatureControl.Cancel()
            End If
            CycleTime = 0                     'No Program is running
            '  TankBReady = False
            '  TankCReady = False
        End If
        WasProgramRunning = Parent.IsProgramRunning

        Dim halt = Parent.IsPaused  ' Or IO_EStop_PB Or (Not ReadInputs_Succeeded)
        Dim NHalt = Not halt
        If TempControlFlag = True Then TemperatureControl.Run(IO.MainTemperature)
        TemperatureControl.CheckErrorsAndMakeAlarms(Me)
        Setpoint = TemperatureControl.TempSetpoint  ' keep a copy to show on graph

        If TempControlFlag = False Then TemperatureControl.Cancel()
        If TemperatureControlFlag = False Then
            TemperatureControl.Cancel()
            HeatNow = False
            CoolNow = False
        End If

        If Not Command01.IsOn And CoolNow Then
            TemperatureControl.Cancel()
            TemperatureControlFlag = False
            CoolNow = False
        End If


        If halt Or (Not IO.SystemAuto) Then TemperatureControl.pidPause()
        Parent.PressForwardBackward(IO.RemoteDown, IO.RemoteUp)
        'set up a flasher
        slowflash.Flash(FlashFlag, 800)

        'set up a flasher2
        slowflash.Flash(FlashFlag2, 1600)


        slowflash.Flash(FlashFlag3, 5000)
        'run pressure out control when cooling to open up the pressure out when temp becomes ok
        'run the pressure control 
        'set the pressureouttemp to the parameter on boot

        If Not FirstScanDone Or (Parameters.SetPressureOutTemp * 10 <> PressureOutTemp) Then
            'pressure out temp
            If Parameters.SetPressureOutTemp > 0 Then
                PressureOutTemp = Parameters.SetPressureOutTemp * 10
                PressureInTemp = Parameters.SetPressureOutTemp * 10
            Else
                PressureOutTemp = 850
                PressureInTemp = 850
            End If
            If PressureOutTemp >= 1000 Then PressureOutTemp = 1000
            If PressureInTemp >= 1000 Then PressureInTemp = 1000
        End If

        If HeatNow And IO.MainTemperature > (PressureInTemp + 20) Then
            PressureIn = True
        ElseIf Not HeatNow Or Not NHalt Then
            PressureIn = False
        End If

        PressureOut.Run()

        'manual pump pushbuttons
        'If IO.MainPumpOnPB Then
        '  PumpOn = True
        'End If
        'If IO.MainPumpOffPB Then
        '  PumpOn = False
        'End If

        '升溫時開熱交換器排水閥，熱交換器排水延遲時間到達則改開排冷凝水閥
        Static HxDrainDelay As New DelayTimer

        ' Make run and halt(=pause) push-buttons work
        Parent.PressButtons(IO.RemoteRun, IO.RemoteHalt, False, False, False)

        Alarms.HighTempNoFill = NHalt And (IO.ColdFill Or IO.HotFill) And IO.MainTemperature > (Parameters.SetSafetyTemp * 10)
        Alarms.HighTempNoAdd = NHalt And (IO.BTankAddition Or IO.CTankAddition Or IO.Dosing Or IO.Addition) And IO.MainTemperature > (Parameters.SetAddSafetyTemp * 10)
        Alarms.HighTempNoDrain = NHalt And IO.Drain And IO.MainTemperature > (Parameters.SetSafetyTemp * 10)
        Alarms.TerminalError = RemoteDisplayError
        Alarms.PlcError = IO.PlcFault And Parent.Mode <> Mode.Debug  ' no plc error in debug mode
        '  Alarms.Pt1Open = (io.MainTemperature > Parameters.pt1Highlimit)
    '  Alarms.Pt1Short = (io.MainTemperature < Parameters.pt1lowlimit)
        Alarms.InsufficientSteam = MainTempStopChange And HeatNow
        Alarms.CoolingNotEnough = MainTempStopChange And CoolNow
        Alarms.MainPumpError = NHalt And (HeatNow Or CoolNow) And Not IO.MainPumpFB

        'Static delay2 As New DelayTimer
        ' Alarms.MainPumpOverload = delay2.Run(IO.MainPumpOverload, 3)            '======================
        Alarms.MainPumpOverload = IO.MainPumpOverload

        Alarms.AddMotorOverload = IO.BPumpOverload
        Alarms.ManualOperation = Not IO.SystemAuto
        Alarms.LowLevelAlarm = IO.MainPumpFB And Not IO.LowLevel

        'Alarms.MainElectricFanError = IO.FanError
        Alarms.FabricStop = IO.Entanglement1
        Alarms.STankNotReady = Command57.IsWaitingForPrepare Or Command67.IsWaitingForPrepare
        '溫度異常警報
        Alarms.Pt1Open = IO.MainTemperature < 50
        Alarms.Pt1Short = IO.MainTemperature > 1400

        '呼叫粉體助劑
        Alarms.CallForManualOperateLA302F = WaitFor302D And Not Command42.IsOn

        'LowLevel = IO.DrainLevel Or MainTankVolume >= Parameters.SetMainTankDrainVolume
        'MiddleLevel = IO.LowLevel Or MainTankVolume >= Parameters.SetMainTankLowVolume
        'HighLevel = IO.MiddleLevel Or MainTankVolume >= Parameters.SetMainTankMidVolume
        'OverflowLevel = IO.HighLevel Or MainTankVolume >= Parameters.SetMainTankHighVolume
        LowLevel = IO.DrainLevel
        MiddleLevel = IO.LowLevel
        HighLevel = IO.MiddleLevel
        OverflowLevel = IO.HighLevel

        'IO.Parameters_RealValue1 = IO.Real1Value
        'IO.Parameters_RealValue2 = IO.Real2Value
        ''IO.Parameters_RealValue3 = IO.Real3Value
    'IO.Parameters_RealValue4 = IO.Real4Value

  


        'calculate side tank level 
        BTankLowLevel = IO.BTankLow Or IO.TankBLevel > 50
        BTankMiddleLevel = IO.BTankMiddle Or IO.TankBLevel > 500
    BTankHighLevel = IO.BTankHigh Or IO.TankBLevel > 1000

        CTankLowLevel = IO.CTankLow Or IO.TankCLevel > 50
        CTankMiddleLevel = IO.CTankMiddle Or IO.TankCLevel > 500
        CTankHighLevel = IO.CTankHigh Or IO.TankCLevel > 1000

        'tank ready pushbuttons

    If IO.BTankReady And Not btankreadywas Then
      TankBReady = Not TankBReady
    ElseIf Not BTankLowLevel Then
      TankBReady = False
    End If
    btankreadywas = IO.BTankReady



    If IO.CTankReady And Not ctankreadywas Then
      TankCReady = Not TankCReady
    ElseIf Not CTankLowLevel And Parameters.C藥缸水位種類 = 0 Then

      TankCReady = False
    ElseIf Not IO.CTankLow And Parameters.C藥缸水位種類 = 1 Then
      TankCReady = False
    End If
        ctankreadywas = IO.CTankReady

        '手動加藥按鈕


        If Not IO.SystemAuto Then
            'If IO.B1Add And Not B1AddWas Then
            '    B1Add = Not B1Add
            'End If
            'B1AddWas = IO.B1Add
            'If IO.B2Add And Not B2AddWas Then
            '    B2Add = Not B2Add
            'End If
            'B2AddWas = IO.B2Add
            'If IO.B3Add And Not B3AddWas Then
            '    B3Add = Not B3Add
            'End If
            'B3AddWas = IO.B3Add
            'If IO.B4Add And Not B4AddWas Then
            '    B4Add = Not B4Add
            'End If
            'B4AddWas = IO.B4Add
            'If IO.B5Add And Not B5AddWas Then
            '    B5Add = Not B5Add
            'End If
            'B5AddWas = IO.B5Add
            If IO.B1Add Then
                B1Add = True
            Else
                B1Add = False
            End If
            If IO.B2Add Then
                B2Add = True
            Else
                B2Add = False
            End If
            If IO.B3Add Then
                B3Add = True
            Else
                B3Add = False
            End If
            If IO.B4Add Then
                B4Add = True
            Else
                B4Add = False
            End If
            If IO.B5Add Then
                B5Add = True
            Else
                B5Add = False
            End If
        Else
            B1Add = False
            B2Add = False
            B3Add = False
            B4Add = False
            B5Add = False
        End If

        'calculate main tank level 
        '''''''Dim i As Integer
        '''''''For i = 0 To 50
        '''''''    If (Parameters.SetMainTankVolume(i) > 0 And Parameters.SetMainTankAnalogInput(i) > 0) Or i = 0 Then
        '''''''        If IO.MainTankLevel >= Parameters.SetMainTankAnalogInput(i) Then
        '''''''            MainTankVolume = Parameters.SetMainTankVolume(i)
        '''''''        End If
        '''''''    End If
        '''''''Next i


        'On/Off昇降溫閥控制
        If TemperatureControlFlag And NHalt Then
            CoolValve = Not TemperatureControlTimer.Finished And Not CoolValveOpenTimer.Finished And CoolNow And (Parameters.CoolValveType = 0)
            HeatValve = Not TemperatureControlTimer.Finished And Not HeatValveOpenTimer.Finished And HeatNow And (Parameters.HeatValveType = 0)

            If CoolNow And Parameters.CoolValveType = 0 Then
                TemperatureControlTime = 6
                CoolValveOpenTime = Math.Min(Math.Max(((IO.MainTemperature - Setpoint) \ 5), 0), 6)
                If TemperatureControlTimer.Finished Then
                    TemperatureControlTimer.TimeRemaining = (TemperatureControlTime - ((CoolValveOpenTime) \ 2))
                    If (IO.MainTemperature - Setpoint) > 0 Then
                        If CoolValveOpenTimer.Finished Then
                            CoolValveOpenTimer.TimeRemainingMs = CoolValveOpenTime * 500
                        End If
                    End If
                End If
            ElseIf HeatNow And Parameters.HeatValveType = 0 Then
                TemperatureControlTime = 6
                HeatValveOpenTime = Math.Min(Math.Max((TemperatureControl.Output * 6 \ 1000), 0), 6)
                If TemperatureControlTimer.Finished Then
                    TemperatureControlTimer.TimeRemaining = (TemperatureControlTime - ((HeatValveOpenTime) \ 2))
                    If HeatValveOpenTimer.Finished Then
                        HeatValveOpenTimer.TimeRemainingMs = HeatValveOpenTime * 500
                    End If
                End If
            End If
        End If

        If HeatNow Then
            HeatOnWas = True
        ElseIf CoolNow Then
            HeatOnWas = False
        End If
        SetPointShow = Setpoint / 10
        MainTempShow = IO.MainTemperature / 10
        '***********************PH功能*************************************************************
        '-----------------------------------------------------
        '如果在十秒內,重複叫PhControl ,將縮短初期PH檢測時間
        '但是條件是第一次PhControl最少要執行過60秒以上,才可以下次重新執行PhControl時,縮短初期檢測時間


        If PhControl.IsOn = True And PhCirculateRun.IsOn = True And PH檢測_短時間內不在檢測 = False Then
            PH再檢測 = My.Computer.Clock.TickCount
            PH檢測_短時間內不在檢測 = True
        End If

        If (My.Computer.Clock.TickCount - PH再檢測) > 60000 And PH檢測_短時間內不在檢測 = True Then
            已經確保120秒檢測完成 = True
            是否縮短檢測時間 = False
            PH再檢測時間.TimeRemaining = 0
            短時間內重新執行 = False
        End If


        If PhControl.IsOn = False And 已經確保120秒檢測完成 = True And 短時間內重新執行 = False Then
            短時間內重新執行 = True
            PH再檢測時間.TimeRemaining = 10
            是否縮短檢測時間 = True
            PH檢測_短時間內不在檢測 = False
        End If

        If 短時間內重新執行 = True And PH再檢測時間.Finished Then
            是否縮短檢測時間 = False
        End If



        '================================================================
        '-----------------------計算使用PH量---------------------------------------
        If Not IO.PhAddHacOut Then
            加酸時間.TimeRemaining = 0
        ElseIf IO.PhAddHacOut And 加酸時間.Finished Then
            加酸時間.TimeRemaining = 1
            加酸次數 = 加酸次數 + 1
        End If
        If Not PhControl.IsOn Then
            加酸次數 = 0
        End If
        '-----------------------顯示mimic---------------------------------------
        test1 = IO.PhValue / 100
        test2 = PhControl.C75Gradient / 100
        test3 = 加酸次數
        test4 = PhControl.Wait1.TimeRemaining
        test5 = PhControl.PhAddError
        test6 = PhControl.ExpectPh / 100
        test7 = PhControl.CountHacVolume
        test8 = PhCheck.Wait.TimeRemaining
        test9 = PhControl.RegisterI(2)
        test10 = CType(PhControl.TotalAddHac2, Integer)
        test11 = PhControl.CalculateTmepRange
        test12 = PhControl.Wait10Second.TimeRemaining
        test13 = PhControl.WaitAddHac.TimeRemaining
        test14 = PhControl.RegisterI(10)
        test15 = PhControl.ExpectPh2

        test19 = PhCheck.DelayWait.TimeRemaining
        test20 = PhCheck.S12 / 100
        test21 = Parameters.PhConcentration
        test22 = PhControl.PhFillLevel
        test23 = Parameters.PhPumpOutRatio
        test25 = PhControl.AddOverError
        test26 = PhControl.AddOverTimes
        test27 = PhCheckError.PhOverAddTimes
        test28 = PhCheckError.Wait.TimeRemaining
        test29 = PhCheckError.StopAddPH
        test30 = PhCheckError.StopAddPH2
        test31 = PhControl.MathNeverOpenValue
        test32 = PhControl.次數
        test32 = PhControl.次數
        test33 = PhControl.微調次數
        補酸狀態分析 = PhControl.減酸比率
        test35 = PhControl.W0微量補酸


        If Parameters.PhShowData = 0 Then
            PhShowData = "PhShowClose"
        Else
            PhShowData = "PhShowOpen"
        End If


        If Parameters.PhCirTank = 0 Then
            PhShowPic = False
        Else
            PhShowPic = True
        End If

        If PhCheck.State = PhCheck_.PhCheck.Check1 Then
            test16 = "Check1"
        ElseIf PhCheck.State = PhCheck_.PhCheck.Check2 Then
            test16 = "Check2"
        ElseIf PhCheck.State = PhCheck_.PhCheck.DelayCheck1 Then
            test16 = "DelayCheck1"
        ElseIf PhCheck.State = PhCheck_.PhCheck.DelayCheck2 Then
            test16 = "DelayCheck2"
        ElseIf PhCheck.State = PhCheck_.PhCheck.Finish Then
            test16 = "Finish"
        ElseIf PhCheck.State = PhCheck_.PhCheck.off Then
            test16 = "off"
        End If

        If PhControl.State = PhControl_.PhControl.AddHacError Then
            test24 = "AddHacError"
        ElseIf PhControl.State = PhControl_.PhControl.Alarm_TmepHigh Then
            test24 = "Alarm_TmepHigh"
        ElseIf PhControl.State = PhControl_.PhControl.AlarmPhHigh Then
            test24 = "AlarmPhHigh"
        ElseIf PhControl.State = PhControl_.PhControl.AllAddHac Then
            test24 = "AllAddHac"
        ElseIf PhControl.State = PhControl_.PhControl.CheckPhValue Then
            test24 = "CheckPhValue"
        ElseIf PhControl.State = PhControl_.PhControl.Divider Then
            test24 = "Divider"
        ElseIf PhControl.State = PhControl_.PhControl.DownloadParameter Then
            test24 = "DownloadParameter"
        ElseIf PhControl.State = PhControl_.PhControl.Finished Then
            test24 = "Finished"
        ElseIf PhControl.State = PhControl_.PhControl.MathAddHac2 Then
            test24 = "MathAddHac2"
        ElseIf PhControl.State = PhControl_.PhControl.MathAddHac3 Then
            test24 = "MathAddHac3"
        ElseIf PhControl.State = PhControl_.PhControl.MathAddHac4 Then
            test24 = "MathAddHac4"
        ElseIf PhControl.State = PhControl_.PhControl.Off Then
            test24 = "Off"
        ElseIf PhControl.State = PhControl_.PhControl.Pause Then
            test24 = "Pause"
        ElseIf PhControl.State = PhControl_.PhControl.WaitKeepTime Then
            test24 = "WaitKeepTime"
        ElseIf PhControl.State = PhControl_.PhControl.WaitTempArrival Then
            test24 = "WaitTempArrival"
        ElseIf PhControl.State = PhControl_.PhControl.WaitTempFinish Then
            test24 = "WaitTempFinish"
        End If
        '-----------------------------------------------------------------------------
        IO.PhRunning = PhControl.IsOn Or PhCirculateRun.IsOn
        '-------------------------PH測試-----------------------------------------------
        If PhControlFlag = True Then

            PhControl.Run()                           'PH控制

            PhCirculateRun.Run()                      '跑迴流桶動作

            '-----------------------顯示PH加藥量-----------------------------------
            If IO.PhAddPump And IO.PhAddHacOut And MathHacFlag = False Then
                MathHacTimes.Stop()

                MathHacTimes.Start()
                MathHacFlag = True
            End If

            If MathHacFlag = True And IO.PhAddPump = False And IO.PhAddHacOut = False Then
                UseHacThisValue = MathHacTimes.TimeElapsed
                'MathHacTimes.Stop()
                'UseHacThisValue = MathHacTimes.TimeElapsed
                MathHacFlag = False
                UseHacAllTotal = UseHacAllTotal + UseHacThisValue

                If IO.PhValue < 450 Then
                    UseHacAllTotal2 = UseHacAllTotal2 + UseHacThisValue
                End If

            End If
            '-----------------PH控制加酸時，實際低於PH450時，開始計算每次加酸量，當 實際加酸總量 > 目標加酸總量 
            If UseHacAllTotal2 > PhControl.TotalAddHac4 Then                        '20525---9005 1801 2020 404

                If (UseHacAllTotal2 - PhControl.TotalAddHac4) > 1000 Then

                    Alarms.MessageAddHacError = True

                End If

            End If
        ElseIf PhControlFlag = False Then
            MathHacTimes.Stop()
        End If
        '------------------------------------------------------------------------------
        If PhControlFlag = False Then
            PhControl.Cancel()                      '結束PH控制
            If Command78.State = Command78.S78.KeepTime Then
                PhCirculateRun.Run()                 '結束迴流桶動作
            Else
                If PhCirRun And Parameters.PhCirRuning = 1 And Parent.IsProgramRunning Then
                    PhCirculateRun.Run()                 '結束迴流桶動作
                Else
                    PhCirculateRun.Cancel()                 '結束迴流桶動作
                End If

            End If

            PhControl.Wait.TimeRemaining = 0
            PhControl.ExpectPh = 0
            UseHacAllTotal = 0 '
            UseHacAllTotal2 = 0
            Alarms.MessageAddHacError = False
            PhControl.Wait1.TimeRemaining = 0
            PhControl.CalculateTmepRange = 0
            PhControl.TotalAddHac2 = 0
        End If



        '-----------------------------------------------------------------------------
        If PhControl.CheckPhRun = True And PhControl.IsOn Then
            PhCheck.Run()
            PhCheckError.Run()
        End If
        '-----------------------------------------------------------------------------
        If PhControl.CheckPhRun = False Or Not PhControl.IsOn Then
            PhCheckError.Cancel()
            PhCheck.Cancel()
            PhCheck.DelayWait.TimeRemaining = 0
            PhCheck.Wait.TimeRemaining = 0
            PhCheck.Wait1.TimeRemaining = 0
            PhCheck.X = 0
            PhCheck.X2 = 0
            PhCheck.S12 = 0
            PhCheckError.PhOverAddTimes = 0
        End If
        '---------------------------------------------------主要是關閉加藥閥時,需要延遲關閉循環馬達,讓加酸打入染機
        If IO.PhAddHacOut = True Then
            DelayAddTime.TimeRemaining = Parameters.DelayCirculatPump       '加酸關閉,循環馬達延遲關閉時間
            DelayAddHac = True
        End If

        If IO.PhAddHacOut = False And DelayAddTime.Finished Then
            DelayAddHac = False
        End If
        '-----------------------------------------------------------------------------

        Alarms.HighTempNoAddHac = PhControl.IsOn And IO.MainPumpFB And IO.MainTemperature > (Parameters.SetAddSafetyTemp * 10) And Not PhCheckError.StopAddPH And Not PhCheckError.StopAddPH2
        'PH加酸閥
        IO.PhAddHacOut = NHalt And Alarms.MessageAddHacError = False And (PhControl.PhAddHacOut And IO.MainPumpFB And IO.MainTemperature < (Parameters.PH加酸安全溫度 * 10))       'PH加酸閥
        'PH入染機
        IO.PhInToMachine = NHalt And ( _
                                     (PhControl.PhInToMachine And IO.MainTemperature < (Parameters.PH加酸安全溫度 * 10)) Or _
                                     (PhCirculateRun.PhInToMachine And IO.MainTemperature < (Parameters.PH加酸安全溫度 * 10)) Or _
                                     DelayAddHac = True _
                                     ) _
                                  And Parameters.PhCirTank = 1 'PH入染機閥

        'PH入迴水
        IO.PhFillCirculate = ((PhCirculateRun.PhFillCirculate2 Or PhCirculateRun.PhFillCirculate3) And IO.MainPumpFB And (IO.PhCheckTemp < Parameters.PH加酸安全溫度 * 10))       'PH迴水閥

        'PH定量泵
        IO.PhAddPump = NHalt And (Alarms.MessageAddHacError = False And IO.MainPumpFB And (PhControl.PhAddPump And IO.MainTemperature < (Parameters.PH加酸安全溫度 * 10) _
                                                                       )) And Not PhCheckError.StopAddPH And Not PhCheckError.StopAddPH2     'PH定量馬達

        'PH循環泵
        IO.PhCirculatePump = ((PhCirculateRun.PHTankAddPump) Or PhWash.PhCirculatePump Or DelayAddHac = True) _
                              And IO.MainPumpFB And IO.MainTemperature < (Parameters.PH加酸安全溫度 * 10) _
                              And Parameters.PhCirTank = 1

        'PH冷卻
        IO.PhCool = NHalt And ((PhCirculateRun.IsOn) And (IO.PhCheckTemp > Parameters.PhCoolingTemp * 10))              'PH冷卻

        'PH入清水
        IO.PhWashFill = PhWash.PhWashFill Or PhWash.PhNoCirTank

        'PH排水
        IO.PhDrain = PhWash.PhDrain And Parameters.PhCirTank = 1
        IO.ConnectPhSystem = Parameters.ConnectPhSystem
        '=======================藥缸回流桶控制==========================================================

        PhToAdd = (PhCirculateRun.CtankToMachine And IO.MainTemperature < (Parameters.PH加酸安全溫度 * 10)) And Parameters.PhCirTank = 0
        PhToCPump = PhCirculateRun.CAddPump And IO.MainTemperature < (Parameters.PH加酸安全溫度 * 10) And Parameters.PhCirTank = 0
        PhToDrain = PhWash.PhNoCirTank And Parameters.PhCirTank = 0
        '-----------------------------------------------------------------------------
    '******************************************************************************************

    '注意  連接ph 或 多一組EAO 只能選一個

    If Parameters.ConnectPhSystem = 1 And Parameters.Connect2EAIO = 1 Then
      Parameters.Connect2EAIO = 0
    ElseIf Parameters.ConnectPhSystem = 0 And Parameters.Connect2EAIO = 1 Then
      IO.Connect2EAIO = Parameters.Connect2EAIO
    ElseIf Parameters.ConnectPhSystem = 0 And Parameters.Connect2EAIO = 0 Then
      IO.Connect2EAIO = Parameters.Connect2EAIO
    End If




    'digital outputs 
    ' IO.Heat = NHalt And TemperatureControl.IsHeating And IO.MainPumpFB And ((HeatNow And Parameters.HeatValveType = 1) Or HeatValve) And _
    '((IO.MainTemperature < Setpoint + 30) And TemperatureControl.Parameters_HeatCoolModeChange = 1 And IO.MainTemperature < Command01.TargetTemp + 5) Or ((HeatNow And IO.MainTemperature < Setpoint) And TemperatureControl.Parameters_HeatCoolModeChange = 0)
    IO.Heat = NHalt _
          And (TemperatureControl.IsHeating _
          And IO.MainPumpFB _
          And ((HeatNow And Parameters.HeatValveType = 1) Or HeatValve) _
          And ((IO.MainTemperature < Setpoint + 30) And TemperatureControl.Parameters_HeatCoolModeChange = 1 And IO.MainTemperature < Command01.TargetTemp + 5) _
          Or ((HeatNow And IO.MainTemperature < Setpoint) And TemperatureControl.Parameters_HeatCoolModeChange = 0)) _
           Or (NHalt And IO.SystemAuto And IO.TemperatureControlHeat > 0 And Parameters.HeatCoolControlType = 1)


    '================================熱水降溫模式
    If Parameters.CoolWaterForChange * 10 < IO.MainTemperature Then
      CoolWaterForChange = True
    Else
      CoolWaterForChange = False
    End If
    If (Not CoolNow And IO.SystemAuto) Then
      CoolWaterForChange = False
    End If



    '==================================
    IO.Cool = (( _
              (NHalt And IO.MainTemperature > Setpoint And (IO.MainPumpFB And TemperatureControl.IsCooling And ((CoolNow And Parameters.CoolValveType = 1) Or CoolValve)) Or Command11.IsCoolFill Or Command12.IsCoolFill Or (Command13.IsCoolFill And Not IO.HighLevel)) Or _
              (NHalt And IO.SystemAuto And IO.TemperatureControlCool > 0 And Parameters.HeatCoolControlType = 1) _
              ) And CoolWaterForChange = False) Or _
              (IO.ManualCoolRinse And Not IO.SystemAuto) Or _
              (IO.ManualCooling And Not IO.SystemAuto)

    IO.CoolForHotWater = (( _
          (NHalt And IO.MainTemperature > Setpoint And (IO.MainPumpFB And TemperatureControl.IsCooling And ((CoolNow And Parameters.CoolValveType = 1) Or CoolValve)) Or Command11.IsCoolFill Or Command12.IsCoolFill Or (Command13.IsCoolFill And Not IO.HighLevel)) _
          ) And CoolWaterForChange = True)



    IO.CondenserDrain = NHalt And HxDrainDelay.Run(HeatOnWas, Parameters.CondensationDelayTime) And Not IO.Cool
    IO.HxDrain = Not (IO.CondenserDrain Or IO.CoolDrain Or IO.Cool Or Command11.IsOn Or Command12.IsOn Or _
                      Command13.IsOn)

    IO.CoolDrain = NHalt And (CoolNow Or (IO.ManualCooling And Not IO.SystemAuto)) And Not IO.CoolWash
    IO.PressureIn = NHalt And HeatNow And Not IO.PressureSw And PressureIn And _
                        IO.MainTemperature < 1200
    '  IO.PressureOut = (PressureOut.IsDepressurising Or PressureOut.IsDepressurised) And Not PressureIn And Not HeatNow
    IO.PressureOut = ((PressureOut.IsDepressurising Or PressureOut.IsDepressurised) And Not PressureIn)

    If Parameters.HeatNowNotPressure = 1 And HeatNow Then
      IO.PressureOut = False
    End If

    IO.Overflow = (NHalt And (Command10.IsDrain Or Command11.IsDrainingToLowLevel Or Command12.IsRinsing Or Command13.IsCoolFill Or _
                                 Command14.IsOverflowDrain)) Or (IO.ManualCoolRinse And Not IO.SystemAuto)
    IO.ColdFill = NHalt And (Command03.IsFillCold Or Command04.IsFillCold Or Command10.IsFillCold Or Command11.IsFillCold Or _
                             Command12.IsFillCold Or Command17.IsFillCold Or Command19.IsFillCold)
    IO.CoolWash = ((NHalt And (Command13.IsCoolFill)) Or (Not IO.SystemAuto And IO.ManualCoolRinse)) And Not IO.HighLevel
    IO.HotFill = NHalt And (Command03.IsFillHot Or Command04.IsFillHot Or Command11.IsFillHot Or _
                            Command12.IsFillHot Or Command17.IsFillHot Or Command19.IsFillHot)
    IO.Drain = NHalt And (Command10.IsDrain Or Command11.IsDrainingToLowLevel Or Command11.IsDrainingEmpty Or _
                          Command14.IsColdDrain Or Command32.IsDrain)
    IO.HotDrain = NHalt And (Command32.IsHotDrain Or Command14.IsHotDrain)
    IO.Fill3 = NHalt And (Command11.IsCoolFill Or Command10.IsFill3 Or Command12.IsCoolFill Or Command04.IsFill3 Or Command03.IsFill3 Or Command19.IsFill3)
    IO.PowerDrain = NHalt And Command32.IsPowerDrain
    IO.PumpOn = PumpStartRequest
    IO.PumpOff = PumpStopRequest
    IO.CounterReset = ShowFillFlag = True
    IO.LevelClean = NHalt And Command14.Is清洗水位尺
    '-----------------------------------------------------------------------------計數1歸零及記錄
    If ShowFillFlag = False Then
      If IO.進水量 > 3000 Then
        ShowFillTotalValve = ShowFill1 + IO.進水量
        ShowFill2 = IO.進水量
        ShowFillFlag = True
      Else
        ShowFillTotalValve = ShowFill1 + IO.進水量
      End If
    Else
      If IO.進水量 > 3000 Then
        ShowFill2 = IO.進水量
        ShowFillTotalValve = ShowFill1 + IO.進水量
      ElseIf IO.進水量 < 2000 Then
        ShowFill1 = ShowFill1 + ShowFill2
        ShowFillTotalValve = ShowFill1 + IO.進水量
        ShowFillFlag = False
      End If
    End If
    '-----------------------------------------------------------------------------工單紀錄
    If Parent.IsProgramRunning And ShowFillFlag2 = False Then
      ShowFillTotalValve2 = 0
      ShowFill3 = ShowFillTotalValve
      ShowFillFlag2 = True

    ElseIf Parent.IsProgramRunning And ShowFillFlag2 = True Then
      ShowFillTotalValve2 = ShowFillTotalValve - ShowFill3
    Else
      ShowFillTotalValve2 = 0
      ShowFill3 = 0
      ShowFillFlag2 = False
    End If
    工單累計 = ShowFillTotalValve2 * Parameters.VolumePerCount
    總水量累計 = ShowFillTotalValve * Parameters.VolumePerCount



    '========================================

    If Parameters.HeatCoolControlType = 0 Then
      If (HeatNow And Not Command01.IsOn) Or CoolNow Or Command13.IsCoolFill Or (Not IO.SystemAuto And IO.ManualCoolRinse) Then
        IO.TemperatureControlHeat = TemperatureControlAnalog
      ElseIf HeatNow And Command01.IsOn And Command01.Gradient <> 0 Then
        IO.TemperatureControlHeat = TemperatureControlAnalog
      ElseIf HeatNow And Command01.IsOn And Command01.Gradient = 0 Then
        If IO.MainTemperature >= Command01.TargetTemp Then
          IO.TemperatureControlHeat = 0
        Else
          IO.TemperatureControlHeat = TemperatureControlAnalog
        End If

      Else
        IO.TemperatureControlHeat = 0
      End If
      IO.TemperatureControlCool = 0

      If Not IO.SystemAuto Then
        IO.TemperatureControlHeat = 0
        IO.TemperatureControlCool = 0
      End If
      If CoolWaterForChange = True Then
        IO.TemperatureControlHeat = 0
        IO.TemperatureControlCool = 0
      End If
    Else
      If HeatNow And Command01.IsOn And Command01.Gradient <> 0 Then
        IO.TemperatureControlHeat = TemperatureControlAnalog
      ElseIf HeatNow And Command01.IsOn And Command01.Gradient = 0 Then
        If IO.MainTemperature >= Command01.TargetTemp Then
          'IO.TemperatureControlHeat = 0
          IO.TemperatureControlHeat = TemperatureControlAnalog
        Else
          IO.TemperatureControlHeat = TemperatureControlAnalog
        End If
      End If
      If Not HeatNow Then
        IO.TemperatureControlHeat = 0
      End If
      If CoolNow Or Command13.IsCoolFill Or (Not IO.SystemAuto And IO.ManualCoolRinse) Then
        IO.TemperatureControlCool = TemperatureControlAnalog
      Else
        IO.TemperatureControlCool = 0
      End If
      If Not IO.SystemAuto Then
        IO.TemperatureControlHeat = 0
        IO.TemperatureControlCool = 0
      End If
      If CoolWaterForChange = True Then
        IO.TemperatureControlHeat = 0
        IO.TemperatureControlCool = 0
      End If
      If Not CoolNow Then
        IO.TemperatureControlCool = 0
      End If
    End If

    '========================================

    'BC缸動作
    IO.Dosing = NHalt And BDosingOn Or CDosingOn
    IO.Addition = NHalt And (BAllinOn Or CAllinOn) Or PhToAdd Or Command90.Is大加藥
    IO.BTankDrain = NHalt And (Command51.IsDraining Or Command61.IsDraining Or Command57.IsDraining Or Command67.IsDraining)
    IO.BTankAddPump = (Parameters.BC缸數量跟馬達 = 0 And BPumpOn) Or (Parameters.BC缸數量跟馬達 = 2 And (BPumpOn)) Or _
                          (Parameters.BC缸數量跟馬達 = 1 And (BPumpOn Or CPumpOn)) Or PhToCPump Or Command90.Is加藥馬達
    IO.BTankHeat = NHalt And IO.SystemAuto And BTankHeatStartRequest And IO.BTankTemperature < SetBTankTemperature
    IO.BTankMixCir2 = NHalt And (Command64.IsMixingForTime Or Command54.IsMixingForTime Or TankBMix.IsMixingForTime Or Command57.IsCirculating Or Command67.IsCirculating Or Command51.IsMixing Or Command61.IsMixing Or Command57.IsRinsing Or Command67.IsRinsing)
    IO.BTankColdFill = NHalt And ((Command54.IsFillingFresh Or Command64.IsFillingFresh Or _
                                      Command51.IsRinsing Or Command57.IsRinsing Or Command61.IsRinsing Or Command67.IsRinsing) Or ManualDose.IsWashtank Or Command90.Is藥缸入水) And Not BTankHighLevel
    IO.BTankCirculate2 = NHalt And ((Command54.IsFillingCirc Or Command64.IsFillingCirc Or Command51.IsFillingCirc Or Command61.IsFillingCirc)) And Not BTankHighLevel



    IO.BTankAddition = BAllinOn Or BInjOn Or IO.BTankMixCir2 Or PhToCPump Or Command90.Is大加藥


    IO.BTankOkLamp = ((Command54.IsSlow And FlashFlag) Or (Command64.IsSlow And FlashFlag) Or _
                         (Command51.IsWaitingForPrepare And FlashFlag) Or (Command61.IsWaitingForPrepare And FlashFlag) Or TankBReady) And BTankLowLevel

    IO.CTankDrain = NHalt And (Command52.IsDraining Or Command58.IsDraining Or Command62.IsDraining Or Command68.IsDraining) Or PhToDrain
    IO.CTankHeat = CTankHeatStartRequest And IO.CTankTemperature < SetCTankTemperature
    IO.CTankMixCir2 = NHalt And (Command65.IsMixingForTime Or Command55.IsMixingForTime Or TankCMix.IsMixingForTime Or Command58.IsCirculating Or Command68.IsCirculating Or Command52.IsMixing Or Command62.IsMixing Or Command58.IsRinsing Or Command68.IsRinsing)
    IO.CTankColdFill = NHalt And (Command55.IsFillingFresh Or Command65.IsFillingFresh Or _
                                      Command52.IsRinsing Or Command58.IsRinsing Or Command62.IsRinsing Or Command68.IsRinsing)
    IO.CTankCirculate2 = NHalt And (Command55.IsFillingCirc) Or (Command65.IsFillingCirc Or Command52.IsFillingCirc Or Command62.IsFillingCirc)
    IO.CTankAddition = CAllinOn Or CInjOn Or IO.CTankMixCir2
    'IO.BCTankToATank = NHalt And (Command58.IsTransfer Or Command59.IsTransfer)
    IO.CTankOkLamp = ((Command55.IsSlow And FlashFlag) Or (Command65.IsSlow And FlashFlag) Or _
                      (Command52.IsWaitingForPrepare And FlashFlag) Or (Command62.IsWaitingForPrepare And FlashFlag) Or _
                          TankCReady) And CTankLowLevel
    IO.CTankPump = (Parameters.BC缸數量跟馬達 = 2 And (CPumpOn))
    'B TANK outputs +++++++++++++++++++++++++++++++
    BDosingOn = (NHalt And (Command57.IsDosing Or Command67.IsDosing) Or (ManualDose.Tank = 5 And ManualDose.IsDosing))
    BAllinOn = (NHalt And (Command51.IsTransfer Or Command57.IsTransfer Or Command61.IsTransfer Or Command67.IsTransfer Or Command51.IsDosing Or Command61.IsDosing))
    BCirMixOn = NHalt And (Command64.IsMixingForTime Or Command54.IsMixingForTime Or TankBMix.IsMixingForTime Or Command57.IsCirculating Or Command67.IsCirculating Or Command51.IsMixing Or Command61.IsMixing)
    BInjOn = BDosingOn Or BAllinOn Or Command57.IsInject Or Command67.IsInject Or (BCirMixOn And Parameters.BTankMixType = 0)
    BPumpOn = (NHalt And (((Command64.IsMixingForTime Or Command54.IsMixingForTime Or TankBMix.IsMixingForTime) And Parameters.BTankMixType = 0) Or _
                              Command51.IsTransfer Or Command51.IsMixing Or Command61.IsTransfer Or Command61.IsMixing Or _
                              Command57.IsTransferPump Or Command67.IsTransferPump) Or (ManualDose.Tank = 5 And ManualDose.IsTransferPump))

    'C TANK outputs +++++++++++++++++++++++++++++++
    CDosingOn = (NHalt And Command58.IsDosing Or Command68.IsDosing Or Command52.IsTransfer Or Command62.IsTransfer Or (ManualDose.Tank = 4 And ManualDose.IsDosing))
    CAllinOn = (NHalt And (Command52.IsTransfer Or Command58.IsTransfer Or Command62.IsTransfer Or Command68.IsTransfer))
    CCirMixOn = NHalt And (Command65.IsMixingForTime Or Command55.IsMixingForTime Or TankCMix.IsMixingForTime Or Command52.IsMixing Or Command62.IsMixing Or Command58.IsCirculating Or Command68.IsCirculating)
    CInjOn = CDosingOn Or CAllinOn Or Command58.IsInject Or Command68.IsInject Or (CCirMixOn And Parameters.CTankMixType = 0)
    CPumpOn = (NHalt And (((Command65.IsMixingForTime Or Command55.IsMixingForTime Or TankCMix.IsMixingForTime) And Parameters.CTankMixType = 0) Or _
                  Command52.IsTransfer Or Command52.IsMixing Or Command62.IsTransfer Or Command62.IsMixing Or Command58.IsTransferPump Or Command68.IsTransferPump) Or (ManualDose.Tank = 4 And ManualDose.IsDosing))


    加藥馬達速度 = CType(IO.BDosingOutput / 10, Integer)
    C加藥馬達速度 = CType(IO.CDosingOutput / 10, Integer)
    '停止取樣警報
    Static StopAlarmTimer As New Timer
    If Command20.IsCalling And IO.CallAck And StopAlarmTimer.Finished Then
      StopAlarmTimer.TimeRemaining = Parameters.SetSampleAlarmStopTime * 60
    End If


    Static delay4 As New DelayTimer
    IO.ErrorLamp = delay4.Run(Alarms.MainPumpError Or IO.Entanglement1, 5) Or _
                   Command05.IsCalling Or (Command20.IsCalling And StopAlarmTimer.Finished) Or Command16.IsCalling Or Command77.IsAlarm Or Command15.IsCalling Or _
                  Command51.IsWaitingForPrepare Or Command52.IsWaitingForPrepare Or Alarms.STankNotReady Or SideTankNotReady Or Command64.IsSlow Or Command65.IsSlow Or Command54.IsSlow Or Command55.IsSlow Or Command44.IsCall Or Command42.IsCall


    IO.CallLamp = Command05.IsCalling Or Command20.IsCalling Or Command16.IsCalling Or Command77.IsAlarm Or Command15.IsCalling Or Command64.IsSlow Or Command65.IsSlow Or Command54.IsSlow Or Command55.IsSlow Or Command44.IsCall Or Command42.IsCall


    If (NHalt And ((TemperatureControl.IsCooling And Command01.Gradient = 0 And IO.MainPumpFB And TempControlFlag And IO.MainTemperature > Setpoint) Or Command13.IsCoolFill)) Or (Not IO.SystemAuto And IO.ManualCoolRinse) Or (Not IO.SystemAuto And IO.ManualCooling) Then
      TemperatureControlAnalog = 1000
    ElseIf NHalt And (TemperatureControl.IsHeating Or TemperatureControl.IsCooling) And IO.MainPumpFB And TempControlFlag Then
      TemperatureControlAnalog = CType(TemperatureControl.Output, Short)
    Else
      TemperatureControlAnalog = 0
    End If

    'pump speed
    If PumpOn Or PumpStartRequest Then
      IO.PumpSpeedControl = CType(PumpSpeed * 10, Short)
      IO.RollerSpeedControl = CType(RollerSpeed * 10, Short)
      IO.PlaitingSpeedControl = CType(PlaitingSpeed * 10, Short)
    Else
      IO.PumpSpeedControl = 0
      IO.RollerSpeedControl = CType(RollerSpeed * 10, Short)
      IO.PlaitingSpeedControl = CType(PlaitingSpeed * 10, Short)
    End If
    'b dosing pump output
    'if 0 inverter on dosing pump run at full speed. if 1 just an on off pump

    '--------------------------------------------------------------------
    '比例式 = 0 ,單藥缸單馬達 =0
    '54,64=B備藥 , 55,65 C備藥 , 57,67=B加藥 , 58,68 C加藥
    If Parameters.Dosing種類 = 0 And Parameters.BC缸數量跟馬達 = 0 Then
      If NHalt And (Command57.IsCirculating Or Command67.IsCirculating Or Command90.Is加藥馬達) Then
        IO.BDosingOutput = 1000

      ElseIf NHalt And Command57.IsOn Then
        IO.BDosingOutput = CShort(Command57.DoseOutput)
      ElseIf NHalt And Command67.IsOn Then
        IO.BDosingOutput = CShort(Command67.DoseOutput)
      ElseIf NHalt And Command58.IsOn And Not Parameters.BC缸數量跟馬達 = 2 Then
        IO.BDosingOutput = CShort(Command58.DoseOutput)
      ElseIf NHalt And Command68.IsOn And Not Parameters.BC缸數量跟馬達 = 2 Then
        IO.BDosingOutput = CShort(Command68.DoseOutput)
      ElseIf ManualDose.IsOn Then
        IO.BDosingOutput = CShort(MinMax(ManualDose.DoseOutput, 0, 1000))
      Else
        IO.BDosingOutput = 0
      End If
    Else
      If Parameters.BC缸數量跟馬達 = 0 Then
        IO.BDosingOutput = 0
      End If
    End If
    '--------------------------------------------------------------------
    '比例式 = 0 ,雙藥缸單馬達 =1
    '54,64=B備藥 , 55,65 C備藥 , 57,67=B加藥 , 58,68 C加藥
    If Parameters.Dosing種類 = 0 And Parameters.BC缸數量跟馬達 = 1 Then
      If NHalt And (Command57.IsCirculating Or Command67.IsCirculating Or Command90.Is加藥馬達) Then
        IO.BDosingOutput = 1000

      ElseIf NHalt And Command57.IsOn Then
        IO.BDosingOutput = CShort(Command57.DoseOutput)
      ElseIf NHalt And Command67.IsOn Then
        IO.BDosingOutput = CShort(Command67.DoseOutput)
      ElseIf NHalt And Command58.IsOn And Not Parameters.BC缸數量跟馬達 = 2 Then
        IO.BDosingOutput = CShort(Command58.DoseOutput)
      ElseIf NHalt And Command68.IsOn And Not Parameters.BC缸數量跟馬達 = 2 Then
        IO.BDosingOutput = CShort(Command68.DoseOutput)
      ElseIf ManualDose.IsOn Then
        IO.BDosingOutput = CShort(MinMax(ManualDose.DoseOutput, 0, 1000))
      Else
        IO.BDosingOutput = 0
      End If
    Else
      If Parameters.BC缸數量跟馬達 = 1 Then
        IO.BDosingOutput = 0
      End If
    End If
    '--------------------------------------------------------------------
    '比例式 = 0 ,雙藥缸雙馬達 =1
    '54,64=B備藥 , 55,65 C備藥 , 57,67=B加藥 , 58,68 C加藥

    If Parameters.Dosing種類 = 0 And Parameters.BC缸數量跟馬達 = 2 Then
      If NHalt And (Command57.IsCirculating Or Command67.IsCirculating Or Command90.Is加藥馬達) Then
        IO.BDosingOutput = 1000

      ElseIf NHalt And Command57.IsOn Then
        IO.BDosingOutput = CShort(Command57.DoseOutput)
      ElseIf NHalt And Command67.IsOn Then
        IO.BDosingOutput = CShort(Command67.DoseOutput)

      ElseIf NHalt And Command58.IsOn Then
        IO.BDosingOutput = CShort(Command58.DoseOutput)
      ElseIf NHalt And Command68.IsOn Then
        IO.BDosingOutput = CShort(Command68.DoseOutput)
      ElseIf ManualDose.IsOn Then
        IO.BDosingOutput = CShort(MinMax(ManualDose.DoseOutput, 0, 1000))
      Else
        IO.BDosingOutput = 0
      End If
    Else
      If Parameters.BC缸數量跟馬達 = 2 Then
        IO.BDosingOutput = 0
      End If
    End If


    'c dosing pump output
    'if 0 inverter on dosing pump run at full speed. if 1 just an on off pump

    '參數轉換至IO

    IO.DrainDelayToPlc = Parameters.DrainDelay * 10
    IO.SetSafetyTempToPlc = Parameters.SetSafetyTemp * 10
    IO.SetAddSafetyTempToPlc = Parameters.SetAddSafetyTemp * 10
    IO.AddFinishDelayToPlc = Parameters.AddTransferTimeBeforeRinse * 10
    IO.TankDrainDelayToPlc = Parameters.AddTransferDrainTime * 10
    IO.HXDrainDelay = Parameters.CondensationDelayTime
    '模擬B缸水位下載給PLC
    IO.AnalogyBLow = IO.Parameters_MinValue1 + 50
    IO.AnalogyBHigh = IO.Parameters_MaxValue1
    IO.AnalogyBMid = CType(((IO.Parameters_MaxValue1 - IO.Parameters_MinValue1) / 2) + IO.Parameters_MinValue1, Integer)
    '模擬C缸水位下載給PLC
    IO.AnalogyCLow = IO.Parameters_MinValue2 + 50
    IO.AnalogyCHigh = IO.Parameters_MaxValue2
    IO.AnalogyCMid = CType(((IO.Parameters_MaxValue2 - IO.Parameters_MinValue2) / 2) + IO.Parameters_MinValue2, Integer)
    If Parameters.HeatCoolControlType = 0 Then
      IO.ManualCoolMode = Parameters.HeatCoolControlType

    Else
      IO.ManualCoolMode = 1
    End If
    '變數轉換到mimic
    Mimic_MainTemp = IO.MainTemperature \ 10
    Mimic_BTankLevel = IO.TankBLevel \ 10
    If IO.HighLevel Then
      Mimic_MainLevel = 100
    ElseIf IO.MiddleLevel Then
      Mimic_MainLevel = 50
    ElseIf IO.LowLevel Then
      Mimic_MainLevel = 20
    End If
    Mimic_TempControl = TemperatureControlAnalog \ 10
    Mimic_BDosing = IO.Dosing And IO.BTankAddPump
    Mimic_BDrain = IO.BTankDrain And Not IO.BTankAddPump
    Mimic_Blevel = CType(IO.TankBLevel / 10, Double)
    If ManualDose.IsOn Then
      Mimic_Bsetep = CType(ManualDose.SetPoint / 10, Double)
    ElseIf Command57.IsOn Then
      Mimic_Bsetep = CType(Command57.SetPoint / 10, Double)
    ElseIf Command67.IsOn Then
      Mimic_Bsetep = CType(Command67.SetPoint / 10, Double)
    End If


    'DownLoad Parameter to LB60B
    FirstScanDone = True
    PressureDifferent = IO.NozzlePressure - IO.MainTankPressure



    '藥缸功能執行時，啟動溫度控制
    ' If (Command54.IsOn Or Command57.IsOn Or Command64.IsOn Or Command67.IsOn) And Not Command16.IsOn And TempControlFlag Then
    ' If IO.MainTemperature > TemperatureControl.TempFinalTemp Then
    ' CoolNow = True
    ' HeatNow = False
    ' Else
    ' CoolNow = False
    ' HeatNow = True
    ' End If
    ' End If

    '溫度控制時，檢查溫度是否有變化
    If Command01.IsOn And TempControlFlag And CheckMainTempTimer.Finished Then
      CheckMainTempTimer.TimeRemaining = 10
      If (HeatNow And (Setpoint - IO.MainTemperature) > 30 And Command01.Gradient > 0) Or _
      (HeatNow And (Setpoint - IO.MainTemperature) > 30 And Command01.IsHolding) Then
        MainTempStopChange = True
      ElseIf (CoolNow And (IO.MainTemperature - Setpoint) > 30 And Command01.Gradient > 0) Or _
      (CoolNow And (IO.MainTemperature - Setpoint) > 30 And Command01.IsHolding) Then
        MainTempStopChange = True
      Else
        MainTempStopChange = False
      End If

    ElseIf Not Command01.IsOn Then
      MainTempStopChange = False
    End If
    ManualDose.Run()
    TankBMix.Run()
    TankCMix.Run()
    CallLA252.Run()
    CallLA302.Run()
    '============================================================================================
    Dim dyelot = Parent.Job
    If Not dyelot Is Nothing Then
      SQL顯示狀態 = "SQL 開始"
      Dim f = Parent.Job.IndexOf("@")
      If f <> -1 Then
        工單 = dyelot.Substring(0, f)
        Integer.TryParse(dyelot.Substring(f + 1), 重染)
      Else
        工單 = dyelot
        重染 = 0
      End If

      If (工單.Length > 2 And SPCConnectTimer.Finished And Not SPCConnectError And Parameters.ConnectSPCEnable = 1) Or Parameters.ConnectSPCTest = 1 Then
        SQL顯示狀態 = "SQL 執行"
        ConnectBDC()
        DispenseState()

        SPCConnectTimer.TimeRemaining = 5
      Else
        If SPCConnectError Or SPCConnectTimer.Finished Then
          SQL顯示狀態 = "SQL 停止"
        End If

      End If

      If (工單.Length < 3 Or 工單 = System.Environment.MachineName) And Parameters.EnableLoadProgramOnLocal = 0 And Parent.IsProgramRunning And NotAllowLoadProgramAlarmTimer.Finished Then
        StopProgram = True
        NotAllowLoadProgramAlarmTimer.TimeRemaining = 5
      Else
        StopProgram = False
      End If


    End If

    Parent.PressButtons(IO.RemoteRun, IO.RemoteHalt, StopProgram, False, False)
    '============================================================================================


    '=================================================測試用.......起
    If Parent.Mode = Mode.Debug Then

      ' IO.MainPumpFB = True
      'IO.DrainLevel = True
      ' IO.LowLevel = True
      IO.PhMixTankLowLevel = True
      ' IO.SystemAuto = True
      '====================================
      If IO.PhValue = 0 Then
        IO.PhValue = 800
      End If
      '===================================
      If IO.MainTemperature = 0 Then
        IO.MainTemperature = 250
      End If
      '===================================
      'If Not IO.Heat And WaitHeatTime.Finished Then
      '  WaitHeatTime.TimeRemaining = 0
      'ElseIf IO.Heat And WaitHeatTime.Finished Then
      '  WaitHeatTime.TimeRemaining = 1
      '  If IO.Heat And IO.TemperatureControlHeat > 600 Then
      '    IO.MainTemperature = IO.MainTemperature + CType(8, Short)
      '  ElseIf IO.Heat And IO.TemperatureControlHeat > 300 Then
      '    IO.MainTemperature = IO.MainTemperature + CType(5, Short)
      '  Else

      '  End If

      'End If
      '''===================================加酸後,PH的變化
      'If Not IO.PhAddHacOut And WaitHacTime.Finished Then
      '    WaitHacTime.TimeRemaining = 0
      'ElseIf IO.PhAddHacOut And WaitHacTime.Finished Then
      '    WaitHacTime.TimeRemaining = 1

      '    IO.PhValue = IO.PhValue - CType(19, Short)
      'End If
      '==================================PH回流桶動作
      'If (IO.PhFillCirculate Or IO.PhWashFill) And 回流動作Flag = False Then
      '    回流動作 = 1
      '    回流動作Flag = True
      'ElseIf Not IO.PhFillCirculate And Not IO.PhWashFill And Not IO.PhCirculatePump Then
      '    回流動作Flag = False
      '    '回流動作 = 0
      'End If

      'Select Case 回流動作
      '    Case 0

      '    Case 1
      '        回流桶時間.TimeRemaining = 3
      '        回流動作 = 2
      '    Case 2
      '        If 回流桶時間.Finished Then
      '            IO.PhMixTankLowLevel = True
      '            回流桶時間.TimeRemaining = 3
      '            回流動作 = 3
      '        End If

      '    Case 3
      '        If IO.PhCirculatePump Then
      '            回流桶時間.TimeRemaining = 3
      '            回流動作 = 4
      '        End If
      '        If 回流桶時間.Finished Then
      '            IO.PhMixTankHighLevel = True
      '            回流動作 = 5
      '        End If

      '    Case 4
      '        If 回流桶時間.Finished Then
      '            IO.PhMixTankLowLevel = False
      '            IO.PhMixTankHighLevel = False
      '            回流動作 = 1
      '        End If

      '    Case 5
      '        If IO.PhCirculatePump Then
      '            回流桶時間.TimeRemaining = 3
      '            回流動作 = 6
      '        End If

      '    Case 6
      '        If 回流桶時間.Finished Then
      '            IO.PhMixTankHighLevel = False
      '            回流桶時間.TimeRemaining = 3
      '            回流動作 = 4
      '        End If

      'End Select


    End If
    '=================================================測試用.....底
    If Parameters.水位模擬測試 = 1 Then
      IO.Parameters_RealValue3 = CType(Parameters.AI主缸實際值, Short)
    End If
    Try
      'calculate main tank level 
      Dim zz As Integer
      For zz = 0 To 50
        If (Parameters.SetMainTankVolume(zz) > 0 And Parameters.SetMainTankAnalogInput(zz) > 0) Or zz = 0 Then

          If zz >= 50 Then
            If IO.Parameters_RealValue3 >= Parameters.SetMainTankAnalogInput(zz) Then
              MainTankVolume = Parameters.SetMainTankVolume(zz)
            End If

          Else
            If IO.Parameters_RealValue3 >= Parameters.SetMainTankAnalogInput(zz) And IO.Parameters_RealValue3 < Parameters.SetMainTankAnalogInput(zz + 1) Then
              Dim hh, jj, pp, uu As Integer
              hh = Parameters.SetMainTankAnalogInput(zz + 1) - Parameters.SetMainTankAnalogInput(zz)
              jj = hh - (Parameters.SetMainTankAnalogInput(zz + 1) - IO.Parameters_RealValue3)
              pp = Parameters.SetMainTankVolume(zz + 1) - Parameters.SetMainTankVolume(zz)
              uu = (pp * jj) \ hh
              MainTankVolume = Parameters.SetMainTankVolume(zz) + uu
            ElseIf IO.Parameters_RealValue3 >= Parameters.SetMainTankAnalogInput(zz) And Parameters.SetMainTankAnalogInput(zz + 1) = 0 Then
              MainTankVolume = Parameters.SetMainTankVolume(zz)
            End If
          End If
        End If
      Next zz
    Catch ex As Exception

    End Try

    'If RealValueTime.Finished And IO.Parameters_OpenRealValue = 1 Then

    '  IO.Parameters_OpenRealValue = 0

    'ElseIf IO.Parameters_OpenRealValue = 0 Then
    '  RealValueTime.TimeRemaining = 1200
    '  IO.Parameters_RealValue1 = 0
    '  IO.Parameters_RealValue2 = 0
    '  IO.Parameters_RealValue3 = 0
    '  IO.Parameters_RealValue4 = 0
    '  IO.Parameters_RealValue5 = 0
    '  IO.Parameters_RealValue6 = 0
    '  IO.Parameters_RealValue7 = 0
    '  IO.Parameters_RealValue8 = 0
    '  IO.Parameters_RealValue9 = 0
    '  IO.Parameters_RealValue10 = 0
    '  IO.Parameters_RealValue11 = 0
    'End If


    '=====================切換語言=======================

    If ChangeLanguage <> Parent.CultureName Then
      Me.Parent = Parent
      Select Case Parent.CultureName
        Case "zh-TW" : Language = LanguageValue.ZhTw
        Case "zh-CN" : Language = LanguageValue.ZhCn
        Case Else : Language = LanguageValue.English
      End Select
      ChangeLanguage = Parent.CultureName
    End If




  End Sub

    Public Function ReadInputs(ByVal dinp() As Boolean, ByVal aninp() As Short, ByVal temp() As Short) As Boolean Implements ACControlCode.ReadInputs
        Return IO.ReadInputs(dinp, aninp, temp)
    End Function

    Public Sub WriteOutputs(ByVal dout() As Boolean, ByVal anout() As Short) Implements ACControlCode.WriteOutputs
        IO.WriteOutputs(dout, anout)
    End Sub

  <ScreenButton("MT", 1, ButtonImage.Vessel), ScreenButton("AT", 2, ButtonImage.SideVessel), ScreenButton("Info", 3, ButtonImage.SideVessel), ScreenButton("Other", 4, ButtonImage.SideVessel)> _
    Public Sub DrawScreen(ByVal screen As Integer, ByVal row() As String) Implements ACControlCode.DrawScreen
    Dim maximumRows As Integer = 24

    Select Case screen
      Case 1
        'Screen 1
        row(1) = If(Language = LanguageValue.ZhTw, "實際溫度:", "Actual Temp.     :") & IO.MainTemperature / 10 & "C"
        row(2) = If(Language = LanguageValue.ZhTw, "演算溫度:", "Calculate Temp.:") & Setpoint / 10 & "C"
        row(3) = If(Language = LanguageValue.ZhTw, "目標溫度:", "Target Temp.     :") & TemperatureControl.TempFinalTemp / 10 & "C"
        row(4) = If(Language = LanguageValue.ZhTw, "持溫時間:", "Holding Time     :") & TimerString(Command01.Wait.TimeRemaining)
        If IO.HighLevel Then
          row(5) = If(Language = LanguageValue.ZhTw, "主缸水位:高水位", "MainTankLevel  :High") & "/ " & MainTankVolume & " L"
        ElseIf IO.MiddleLevel Then
          row(5) = If(Language = LanguageValue.ZhTw, "主缸水位:中水位", "MainTankLevel  :Middle") & "/ " & MainTankVolume & " L"
        ElseIf IO.LowLevel Then
          row(5) = If(Language = LanguageValue.ZhTw, "主缸水位:低水位", "MainTankLevel  :Low") & "/ " & MainTankVolume & " L"
        Else
          row(5) = If(Language = LanguageValue.ZhTw, "主缸水位:無", "MainTankLevel  :None") & "/ " & MainTankVolume & " L"
        End If
        If IO.MainPumpFB Then
          row(6) = If(Language = LanguageValue.ZhTw, "主馬達狀態:啟動", "MainPumpStatus:On")
        Else
          row(6) = If(Language = LanguageValue.ZhTw, "主馬達狀態:停止", "MainPumpStatus:Off")
        End If
        row(7) = If(Language = LanguageValue.ZhTw, "溫控閥開度:", "TempControlOutput:") & TemperatureControlAnalog / 10 & "%"

        row(8) = If(Language = LanguageValue.ZhTw, "工單水量:", "TempControlOutput:") & ((工單累計)) & "L"
        Dim 總水量累計T As Double
        總水量累計T = 總水量累計 / 1000
        row(9) = If(Language = LanguageValue.ZhTw, "總累計水量:", "TempControlOutput:") & ((總水量累計T)) & "T"
        '                row(8) = If(Language = LanguageValue.ZhTw, "布重:", "TotalWeight:") & TotalWeight & _
        '                         If(Language = LanguageValue.ZhTw, "  浴比:", " LiquidRatio:") & LiquidRatio
        '              row(9) = If(Language = LanguageValue.ZhTw, "總浴量", "TotalVolume") & ShowTotalVolume & "L"
        '              row(10) = If(Language = LanguageValue.ZhTw, "目標水量:", "TargetVolume:") & TargetVolume & "L"
        '             row(11) = If(Language = LanguageValue.ZhTw, "實際水量:", "RealVolume:") & If(Command03.IsOn, Command03.CounterRealPoint, Command04.CounterRealPoint) & "L"
        If Command01.IsPaused Then
          row(12) = If(Language = LanguageValue.ZhTw, "        暫停", "        Pause")
        ElseIf TemperatureControl.IsHeating And Not Command01.IsHolding Then
          row(12) = If(Language = LanguageValue.ZhTw, "        升溫中", "        Heating")
        ElseIf TemperatureControl.IsCooling And Not Command01.IsHolding Then
          row(12) = If(Language = LanguageValue.ZhTw, "        降溫中", "        Cooling")
        ElseIf Command01.IsHolding Then
          row(12) = If(Language = LanguageValue.ZhTw, "        持溫中", "        Holding")
        ElseIf TemperatureControl.IsHeating And Setpoint - IO.MainTemperature > 20 Then
          row(12) = If(Language = LanguageValue.ZhTw, "        蒸氣不足", "        SteamNotEnough")
        ElseIf TemperatureControl.IsCooling And IO.MainTemperature - Setpoint > 20 Then
          row(12) = If(Language = LanguageValue.ZhTw, "        冷卻水不足", "        CoolingWaterNotEnough")
        ElseIf IO.HotFill And IO.ColdFill Then
          row(12) = If(Language = LanguageValue.ZhTw, "        進熱水+冷水", "        Fill Hot+Cold")
        ElseIf IO.ColdFill Then
          row(12) = If(Language = LanguageValue.ZhTw, "        進冷水", "        Fill Cold")
        ElseIf IO.HotFill Then
          row(12) = If(Language = LanguageValue.ZhTw, "        進熱水", "        FillHot")
        ElseIf IO.Fill3 Then
          row(12) = If(Language = LanguageValue.ZhTw, "        進軟水", "        FillHot")
        End If
        If MessageCallOperator Then
          row(13) = If(Language = LanguageValue.ZhTw, "        呼叫操作員", "        CallOperator")
        ElseIf MessageTakeSample Then
          row(13) = If(Language = LanguageValue.ZhTw, "        呼叫取樣", "        Sample")
        ElseIf MessageLoadFabric Then
          row(13) = If(Language = LanguageValue.ZhTw, "        入布", "        Load")
        ElseIf MessageUnloadFiber Then
          row(13) = If(Language = LanguageValue.ZhTw, "        出布", "        Unload")
        End If
        If Alarms.ManualOperation Then
          row(14) = If(Language = LanguageValue.ZhTw, "        系統手動中", "        Manual")
        ElseIf IO.SystemAuto Then
          row(14) = If(Language = LanguageValue.ZhTw, "        系統自動中", "        Auto")
        End If
        If MessageSystemPause Then
          row(14) = If(Language = LanguageValue.ZhTw, "        系統暫停", "        Pause")
        End If
        '-------------------------------------------------------------------------------------------------------
        If PhCirculateRun.IsOn And (Not Parent.IsPaused) And IO.MainPumpFB Then
          row(15) = If(Language = LanguageValue.ZhTw, "Ph回流檢測中", "PH Checking")
        ElseIf PhCirculateRun.IsOn And (Parent.IsPaused Or Not IO.MainPumpFB) Then
          row(15) = If(Language = LanguageValue.ZhTw, "Ph回流檢測停止", "PH Stop")
        End If


        '-------------------------------------------------------------------------------------------------------
        If PhControl.IsOn Or PhCirculateRun.IsOn Then
          If Command77.State = Command77.S77.Start Or Command77.State = Command77.S77.KeepTime Then
            row(16) = If(Language = LanguageValue.ZhTw, "pH 目標值   ", "        Pause") & Command77.PhTarget / 100
          ElseIf Command74.State = Command74.S74.Start Then
            row(16) = If(Language = LanguageValue.ZhTw, "pH 目標值   ", "        Pause") & Command74.PhTarget / 100
          ElseIf PhControl.State = PhControl_.PhControl.AllAddHac Or PhControl.State = PhControl_.PhControl.Divider Or PhControl.State = PhControl_.PhControl.AllPhWaitTime Then
            row(16) = If(Language = LanguageValue.ZhTw, "pH 計量加藥中   ", "        Pause")
          Else

            row(16) = If(Language = LanguageValue.ZhTw, "pH 預定設定值   ", "        Pause") & PhControl.ExpectPh / 100
          End If
        End If

        If PhCirculateRun.IsOn Then
          If PhControl.IsOn Then
            If PhControl.ExpectPh <= 200 Then
              row(17) = If(Language = LanguageValue.ZhTw, "pH 實際值   ", "        Pause") & IO.PhValue / 100
            Else
              Dim TestPH As Integer
              TestPH = CType((IO.PhValue + PhControl.ExpectPh) / 2, Integer)

              If IO.PhValue > PhControl.ExpectPh Then

                row(17) = If(Language = LanguageValue.ZhTw, "pH 實際值   ", "        Pause") & IO.PhValue / 100


              Else
                row(17) = If(Language = LanguageValue.ZhTw, "pH 實際值   ", "        Pause") & TestPH / 100
              End If

            End If
          Else
            row(17) = If(Language = LanguageValue.ZhTw, "pH 實際值   ", "        Pause") & IO.PhValue / 100
          End If

        Else
          row(17) = If(Language = LanguageValue.ZhTw, "pH 實際值   ", "        Pause") & IO.PhValue / 100
        End If

        '-------------------------------------------------------------------------------------------------------



        If Command80.IsOn And PhControl.開啟PH控制旗標 = True Then
          row(18) = If(Language = LanguageValue.ZhTw, "pH 演算時間   ", "        Pause") & Command80.Wait.TimeRemaining & " S"
        End If
        '-------------------------------------------------------------------------------------------------------

      Case 2
        'Screen 2 - side tank

        row(1) = If(Language = LanguageValue.ZhTw, "B 缸訊息", "B Tank Message")
        If BTankHighLevel Then
          row(2) = If(Language = LanguageValue.ZhTw, "B缸水位:", "B Tank Level:") & IO.TankBLevel / 10 & _
          If(Language = LanguageValue.ZhTw, "% -->高水位", "% -->High")
        ElseIf BTankMiddleLevel Then
          row(2) = If(Language = LanguageValue.ZhTw, "B缸水位:", "B Tank Level:") & IO.TankBLevel / 10 & _
          If(Language = LanguageValue.ZhTw, "% -->中水位", "% -->Middle")
        ElseIf BTankLowLevel Then
          row(2) = If(Language = LanguageValue.ZhTw, "B缸水位:", "B Tank Level:") & IO.TankBLevel / 10 & _
          If(Language = LanguageValue.ZhTw, "% -->低水位", "% -->Low")
        Else
          row(2) = If(Language = LanguageValue.ZhTw, "B缸水位:", "B Tank Level:") & IO.TankBLevel / 10 & _
          If(Language = LanguageValue.ZhTw, "% -->無", "% -->None")
        End If
        '加藥目標水位
        If Command57.IsOn Then
          row(3) = If(Language = LanguageValue.ZhTw, "目標水位:", "Target Level:") & Command57.SetPoint / 10 & "%"
        ElseIf Command67.IsOn Then
          row(3) = If(Language = LanguageValue.ZhTw, "目標水位:", "Target Level:") & Command67.SetPoint / 10 & "%"
        Else
          row(3) = If(Language = LanguageValue.ZhTw, "目標水位:", "Target Level:")
        End If
        '顯示藥缸溫度
        row(4) = If(Language = LanguageValue.ZhTw, "B缸溫度: ", "Tank B Temperature: ") & IO.BTankTemperature / 10 & "C"

        If IO.BTankColdFill Then '備水顯示程式還沒完成
          row(5) = If(Language = LanguageValue.ZhTw, "B缸備水中", "B Tank Filling")
        ElseIf Command57.IsWaitingForPrepare Or Command67.IsWaitingForPrepare Then
          row(5) = If(Language = LanguageValue.ZhTw, "等待B備藥OK", "Wait B Tank Ready")
        ElseIf IO.BTankCirculate2 Then
          row(5) = If(Language = LanguageValue.ZhTw, "B缸備迴水中", "B Tank Circulating")
        ElseIf IO.BTankMixCir2 Then
          row(5) = If(Language = LanguageValue.ZhTw, "B缸攪拌中", "B Tank Mixing")
        End If
        '顯示攪拌時間
        If Command54.IsOn Then
          row(6) = Command54.StateString
          '備藥OK
          '加藥中，顯示時間
        ElseIf Command57.IsOn Then
          row(6) = Command57.StateString
        End If

        row(7) = If(Language = LanguageValue.ZhTw, "C缸訊息", "C Tank Message")
        If CTankHighLevel Then
          row(8) = If(Language = LanguageValue.ZhTw, "C水位:", "C Tank Level:") & IO.TankCLevel / 10 & _
          If(Language = LanguageValue.ZhTw, "% -->高水位", "% -->High")
        ElseIf CTankMiddleLevel Then
          row(8) = If(Language = LanguageValue.ZhTw, "C缸水位:", "C Tank Level:") & IO.TankCLevel / 10 & _
          If(Language = LanguageValue.ZhTw, "% -->中水位", "% -->Middle")
        ElseIf CTankLowLevel Then
          row(8) = If(Language = LanguageValue.ZhTw, "C缸水位:", "C Tank Level:") & IO.TankCLevel / 10 & _
          If(Language = LanguageValue.ZhTw, "% -->低水位", "% -->Low")
        Else
          row(8) = If(Language = LanguageValue.ZhTw, "C缸水位:", "C Tank Level:") & IO.TankCLevel / 10 & _
          If(Language = LanguageValue.ZhTw, "% -->無", "% -->None")
        End If
        '加藥目標水位
        If Command58.IsOn Then
          row(9) = If(Language = LanguageValue.ZhTw, "目標水位:", "Target Level:") & Command58.SetPoint / 10 & "%"
        ElseIf Command68.IsOn Then
          row(9) = If(Language = LanguageValue.ZhTw, "目標水位:", "Target Level:") & Command68.SetPoint / 10 & "%"
        ElseIf ManualDose.IsOn And ManualDose.Tank = 4 Then
          row(9) = If(Language = LanguageValue.ZhTw, "目標水位:", "Target Level:") & ManualDose.SetPoint / 10 & "%"
        Else
          row(9) = If(Language = LanguageValue.ZhTw, "目標水位:", "Target Level:")
        End If
        '進水中
        If IO.CTankColdFill Then '備水顯示程式還沒完成
          row(10) = If(Language = LanguageValue.ZhTw, "C缸備水中", "C Tank Filling")
        ElseIf Command58.IsWaitingForPrepare Or Command68.IsWaitingForPrepare Then
          row(10) = If(Language = LanguageValue.ZhTw, "等待C備藥OK", "Wait C Tank Ready")
        ElseIf IO.CTankCirculate2 Then
          row(10) = If(Language = LanguageValue.ZhTw, "C缸備迴水中", "C Tank Circulating")
        ElseIf IO.CTankMixCir2 Then
          row(10) = If(Language = LanguageValue.ZhTw, "C缸攪拌中", "C Tank Mixing")
        End If
        '顯示攪拌時間
        If Command55.IsOn Then
          row(11) = Command55.StateString
        ElseIf TankCMixOn Then
          row(11) = TankCMix.StateString
          '加藥中，顯示時間
        ElseIf Command58.IsOn Then
          row(11) = Command58.StateString
        ElseIf Command52.IsOn Then
          row(11) = Command52.StateString
        ElseIf Command62.IsOn Then
          row(11) = Command62.StateString
        End If
        row(12) = "SQL連線狀況:" & (SQL連線狀況 / 1000) & "秒回應"
        row(13) = SQL顯示狀態
        row(14) = "ChemicalCallOff : " & ChemicalCallOff
        row(15) = "ChemicalTank : " & ChemicalTank
        row(16) = "DyeCallOff : " & DyeCallOff
        row(17) = "DyeTank : " & DyeTank
        'row(12) = "LA252狀態："
        'row(13) = "步驟:" & DCallOff & "桶號:" & DTank & "狀態:" & DState & "啟用:" & DEnabled
        'row(15) = "LA302狀態："
        'row(16) = "步驟:" & CCallOff & "桶號:" & CTank & "狀態:" & CState & "啟用:" & CEnabled
        'row(17) = "粉體呼叫:" & CallFor302D & "樓上呼叫軟體:" & 粉體呼叫
        'row(19) = "SQL連線狀況:" & (SQL連線狀況 / 1000) & "秒回應"
      Case 3  '配方訊息

        row(1) = "領料單號: " & Parent.Job

        If DyeState = 202 Or DyeState = 201 Then
          row(2) = "步驟" & DyeCallOff & " " & "染料輸送中: " & TimerString(CallLA252.WaitTimer.TimeRemaining)
        Else
          row(2) = ""
        End If

        If DyeStepReady(1) Or _
           DyeStepReady(2) Or _
           DyeStepReady(3) Or _
           DyeStepReady(4) Or _
           DyeStepReady(5) Or _
           DyeStepReady(6) Or _
           DyeStepReady(7) Or _
           DyeStepReady(8) Or _
           DyeStepReady(9) Or _
           DyeStepReady(10) Or _
           DyeStepReady(11) Or _
           DyeStepReady(12) Then
          row(3) = "步驟" & If(DyeStepReady(1), "1 ", "") & _
          If(DyeStepReady(2), "2 ", "") & _
          If(DyeStepReady(3), "3 ", "") & _
          If(DyeStepReady(4), "4 ", "") & _
          If(DyeStepReady(5), "5 ", "") & _
          If(DyeStepReady(6), "6 ", "") & _
          If(DyeStepReady(7), "7 ", "") & _
          If(DyeStepReady(8), "8 ", "") & _
          If(DyeStepReady(9), "9 ", "") & _
          If(DyeStepReady(10), "10 ", "") & _
          If(DyeStepReady(11), "11 ", "") & _
          If(DyeStepReady(12), "12 ", "") & _
          "染料計量完成"
        Else
          row(3) = ""
        End If

        '顯示LA-302F狀態

        If ChemicalState = 201 Then
          row(4) = "步驟" & ChemicalCallOff & " " & "助劑呼叫中"
        ElseIf ChemicalState = 202 Then
          row(4) = "步驟" & ChemicalCallOff & " " & "助劑輸送中: " & TimerString(CallLA302.WaitTimer.TimeRemaining)
        Else
          row(4) = ""
        End If

        If ChemicalStepReady(1) Or _
           ChemicalStepReady(2) Or _
           ChemicalStepReady(3) Or _
           ChemicalStepReady(4) Or _
           ChemicalStepReady(5) Or _
           ChemicalStepReady(6) Or _
           ChemicalStepReady(7) Or _
           ChemicalStepReady(8) Or _
           ChemicalStepReady(9) Or _
           ChemicalStepReady(10) Or _
           ChemicalStepReady(11) Or _
           ChemicalStepReady(12) Then
          row(3) = "步驟" & If(ChemicalStepReady(1), "1 ", "") & _
          If(ChemicalStepReady(2), "2 ", "") & _
          If(ChemicalStepReady(3), "3 ", "") & _
          If(ChemicalStepReady(4), "4 ", "") & _
          If(ChemicalStepReady(5), "5 ", "") & _
          If(ChemicalStepReady(6), "6 ", "") & _
          If(ChemicalStepReady(7), "7 ", "") & _
          If(ChemicalStepReady(8), "8 ", "") & _
          If(ChemicalStepReady(9), "9 ", "") & _
          If(ChemicalStepReady(10), "10 ", "") & _
          If(ChemicalStepReady(11), "11 ", "") & _
          If(ChemicalStepReady(12), "12 ", "") & _
          "助劑計量完成"
        Else
          row(5) = ""
        End If
        Dim 數量 As Integer = 0
        For i As Integer = 0 To 50
          If ProductCode(i) <> "" Then
            數量 = 數量 + 1
          End If
        Next


        For i As Integer = 0 To 數量 - 1
          If ProductCode(i) IsNot Nothing Then
            If DispenseResult(i) = "301" Then
              row(i + 6) = StepNumber1(i) & ": " & ProductCode(i) & ": " & Grams(i) & "/" & DispenseGrams(i) & " 正常"
            ElseIf DispenseResult(i) = "302" Then
              row(i + 6) = StepNumber1(i) & ": " & ProductCode(i) & ": " & Grams(i) & "/" & DispenseGrams(i) & " 手動"
            ElseIf DispenseResult(i) = "309" Then
              row(i + 6) = StepNumber1(i) & ": " & ProductCode(i) & ": " & Grams(i) & "/" & DispenseGrams(i) & " 異常"
            Else
              row(i + 6) = StepNumber1(i) & ": " & ProductCode(i) & ": " & Grams(i) & "/" & DispenseGrams(i)
            End If
          Else
            row(i + 6) = ""
          End If
          If i = 17 Then
            i = i
          End If
        Next
        If 數量 = 19 Then
          數量 = 數量
        End If

      Case 4
        'Screen 4 - Display Aninp Calibrate real Value

        row(1) = If(Language = LanguageValue.ZhTw, "布速1: ", "CycleSpeed1: ") & FabricCycleTime1 & "sec" & _
        If(Language = LanguageValue.ZhTw, "計算時間: ", "CycleTime1: ") & FabricCycleTimer1.TimeElapsed.ToString & "sec"
        row(2) = If(Language = LanguageValue.ZhTw, "布速2: ", "CycleSpeed2: ") & FabricCycleTime2 & "sec" & _
        If(Language = LanguageValue.ZhTw, "計算時間: ", "CycleTime2: ") & FabricCycleTimer2.TimeElapsed.ToString & "sec"
        row(3) = If(Language = LanguageValue.ZhTw, "布速3: ", "CycleSpeed3: ") & FabricCycleTime3 & "sec" & _
        If(Language = LanguageValue.ZhTw, "計算時間: ", "CycleTime3: ") & FabricCycleTimer3.TimeElapsed.ToString & "sec"
        row(4) = If(Language = LanguageValue.ZhTw, "布速4: ", "CycleSpeed4: ") & FabricCycleTime4 & "sec" & _
        If(Language = LanguageValue.ZhTw, "計算時間: ", "CycleTime4: ") & FabricCycleTimer4.TimeElapsed.ToString & "sec"
        row(5) = If(Language = LanguageValue.ZhTw, "布速5: ", "CycleSpeed1: ") & FabricCycleTime5 & "sec" & _
        If(Language = LanguageValue.ZhTw, "計算時間: ", "CycleTime1: ") & FabricCycleTimer5.TimeElapsed.ToString & "sec"
        row(6) = If(Language = LanguageValue.ZhTw, "布速6: ", "CycleSpeed2: ") & FabricCycleTime6 & "sec" & _
        If(Language = LanguageValue.ZhTw, "計算時間: ", "CycleTime2: ") & FabricCycleTimer6.TimeElapsed.ToString & "sec"
        row(7) = If(Language = LanguageValue.ZhTw, "布速7: ", "CycleSpeed3: ") & FabricCycleTime7 & "sec" & _
        If(Language = LanguageValue.ZhTw, "計算時間: ", "CycleTime3: ") & FabricCycleTimer7.TimeElapsed.ToString & "sec"
        row(8) = If(Language = LanguageValue.ZhTw, "布速8: ", "CycleSpeed4: ") & FabricCycleTime8 & "sec" & _
        If(Language = LanguageValue.ZhTw, "計算時間: ", "CycleTime4: ") & FabricCycleTimer8.TimeElapsed.ToString & "sec"
        row(9) = If(Language = LanguageValue.ZhTw, "主馬達轉速: ", "Main Pump") & IO.MainPumpSpeed & "RPM"
        row(10) = If(Language = LanguageValue.ZhTw, "帶布輪1轉速: ", "Main Pump") & IO.ReelSpeed1 & "RPM"
        row(11) = If(Language = LanguageValue.ZhTw, "帶布輪2轉速: ", "Main Pump") & IO.ReelSpeed2 & "RPM"
        row(12) = If(Language = LanguageValue.ZhTw, "帶布輪3轉速: ", "Main Pump") & IO.ReelSpeed3 & "RPM"
        row(13) = If(Language = LanguageValue.ZhTw, "帶布輪4轉速: ", "Main Pump") & IO.ReelSpeed4 & "RPM"
        row(14) = "LA302狀態："
        If CState = "101" Then
          row(15) = "步驟:" & CCallOff & "桶號:" & CTank
          row(16) = "狀態: 等待中" & CState & "/啟用:" & CEnabled
        ElseIf CState = "201" Then
          row(15) = "步驟:" & CCallOff & "桶號:" & CTank
          row(16) = "狀態: 呼叫助劑中" & CState & "/啟用:" & CEnabled
        ElseIf CState = "202" Then
          row(15) = "步驟:" & CCallOff & "桶號:" & CTank
          row(16) = "狀態: 輸送助劑中" & CState & "/啟用:" & CEnabled
        ElseIf CState = "301" Then
          row(15) = "步驟:" & CCallOff & "桶號:" & CTank
          row(16) = "狀態: 完成" & CState & "/啟用:" & CEnabled
        Else
          row(15) = "步驟:" & CCallOff & "桶號:" & CTank
          row(16) = "狀態: 異常" & CState & "/啟用:" & CEnabled
        End If

        row(17) = "LA252狀態："
        If DState = "101" Then
          row(18) = "步驟:" & DCallOff & "桶號:" & DTank
          row(19) = "狀態: 等待中" & DState & "/啟用:" & DEnabled
        ElseIf DState = "201" Then
          row(18) = "步驟:" & DCallOff & "桶號:" & DTank
          row(19) = "狀態: 呼叫染料中" & DState & "/啟用:" & DEnabled
        ElseIf DState = "202" Then
          row(18) = "步驟:" & DCallOff & "桶號:" & DTank
          row(19) = "狀態: 樓上化料中" & DState & "/啟用:" & DEnabled
        ElseIf DState = "205" Then
          row(18) = "步驟:" & DCallOff & "桶號:" & DTank
          row(19) = "狀態: 樓上化料中" & DState & "/啟用:" & DEnabled
        ElseIf DState = "207" Then
          row(18) = "步驟:" & DCallOff & "桶號:" & DTank
          row(19) = "狀態: 輸送化料中" & DState & "/啟用:" & DEnabled
        ElseIf DState = "301" Then
          row(18) = "步驟:" & DCallOff & "桶號:" & DTank
          row(19) = "狀態: 完成" & DState & "/啟用:" & DEnabled
        Else
          row(18) = "步驟:" & DCallOff & "桶號:" & DTank
          row(19) = "狀態: 異常" & DState & "/啟用:" & DEnabled
        End If
    End Select
  End Sub

    Public Sub ProgramStart() Implements ACControlCode.ProgramStart

        'Reset dispense variables
        ChemicalCallOff = 0
        ChemicalState = 0
        ChemicalTank = 0
        ChemicalProducts = ""
        DyeCallOff = 0
        DyeState = 0
        DyeTank = 0
        DyeProducts = ""
    Dim dyelot = Parent.Job
    Dim f = Parent.Job.IndexOf("@")
    If f <> -1 Then
      工單 = dyelot.Substring(0, f)
      Integer.TryParse(dyelot.Substring(f + 1), 重染)
    Else
      工單 = dyelot
      重染 = 0
    End If

    Try
      Dim ReadDyelotData As String
      ReadDyelotData = "Select Cast((JulianDay(EndTime)-JulianDay(StartTime))*24*60 As Integer) As Minutes, ColorName From Dyelots" &
        " Where dyelot = '" & 工單 & "' and redye = '" & 重染 & "'"
      Dim dt As System.Data.DataTable = Parent.DbGetDataTable(ReadDyelotData)
      DyelotRemainMinutes = CInt(dt.Rows(0).Item("Minutes"))
      ColorName = dt.Rows(0).Item("ColorName").ToString
    Catch ex As Exception
    End Try


  End Sub

  Public Sub ProgramStop() Implements ACControlCode.ProgramStop
    Command05.Cancel() : Command06.Cancel() : Command07.Cancel() : Command08.Cancel()
    Command09.Cancel() : Command11.Cancel() : Command12.Cancel() : Command13.Cancel()
    Command14.Cancel() : Command15.Cancel() : Command17.Cancel() : Command18.Cancel() : Command39.Cancel() : Command40.Cancel()
    Command19.Cancel() : Command20.Cancel() : Command21.Cancel() : Command22.Cancel()
    Command32.Cancel() : Command40.Cancel() : Command51.Cancel() : Command52.Cancel()
    Command54.Cancel() : Command55.Cancel() : Command57.Cancel() : Command58.Cancel()
    Command61.Cancel() : Command62.Cancel() : Command64.Cancel() : Command65.Cancel()
    Command61.Cancel() : Command62.Cancel() : Command64.Cancel() : Command65.Cancel()
    Command67.Cancel() : Command68.Cancel() : Command01.Cancel() : Command90.Cancel()
        Command01.Cancel() : TemperatureControl.Cancel()
        TankBMix.Cancel()
        TankCMix.Cancel()
        PhControl.Cancel()
        Command03.Cancel() : PhCirculateRun.Cancel()
        PhWash.Cancel() : Command73.Cancel() : Command74.Cancel()
        Command75.Cancel() : Command76.Cancel() : Command77.Cancel() : Command78.Cancel() : Command79.Cancel() : Command80.Cancel()
        '--------------------------------------------------------------------------------------------------------
        BTankHeatStartRequest = False
        CTankHeatStartRequest = False
        TemperatureControlFlag = False
        HeatNow = False
        CoolNow = False
        PumpStartRequest = False
        PumpStopRequest = False
        PumpOn = False
        TankBMixOn = False
        TankCMixOn = False
        TotalWeight = 0
        LiquidRatio = 0
        ShowTotalVolume = 0
        TargetVolume = 0
    '--------------------------------------------------------------------------------------------------------
    'Reset dispense variables
    ChemicalCallOff = 0
    ChemicalState = 0
    ChemicalTank = 0
    ChemicalProducts = ""
    DyeCallOff = 0
    DyeState = 0
    DyeTank = 0
    DyeProducts = ""

    RunCallLA252 = False
    RunCallLA302 = False
    CallLA252.Cancel()
    CallLA302.Cancel()
    LA252Ready = False
    LA302Ready = False
    Call252AddDye = False
    Wait252Scheduled = False
    CallFor302D = False
    WaitFor302D = False

    '清除計量狀態的陣列
    Array.Clear(DyeStepDispensing, 0, 12)
    Array.Clear(DyeStepReady, 0, 12)
    Array.Clear(ChemicalStepDispensing, 0, 12)
    Array.Clear(ChemicalStepReady, 0, 12)

    '清除配方的陣列
    Array.Clear(StepNumber1, 0, 30)
    Array.Clear(ProductCode, 0, 30)
    Array.Clear(ProductType, 0, 30)
    Array.Clear(Grams, 0, 30)
    Array.Clear(DispenseGrams, 0, 30)
    Array.Clear(DispenseResult, 0, 30)
    '清除染料的陣列
    Array.Clear(DyeStepNumber, 0, 10)
    Array.Clear(DyeCode, 0, 10)
    Array.Clear(DyeGrams, 0, 10)
    Array.Clear(DyeDispenseGrams, 0, 10)
    Array.Clear(DyeDispenseResult, 0, 10)
    '清除助劑的陣列
    Array.Clear(ChemicalStepNumber, 0, 20)
    Array.Clear(ChemicalCode, 0, 20)
    Array.Clear(ChemicalGrams, 0, 20)
    Array.Clear(ChemicalDispenseGrams, 0, 20)
    Array.Clear(ChemicalDispenseResult, 0, 20)
    Try

      cn_DispenseState.Close()
      DispenseState1()
    Catch ex As Exception

    End Try
    End Sub
  <GraphTrace(1, 1000, -1000, 3000, "Red", "%2tpH"), Translate("zh-TW", "PH值")> _
Public ReadOnly Property PHValve() As Integer
    Get
      If PhCirculateRun.IsOn Then
        If PhControl.IsOn Then
          If PhControl.ExpectPh <= 200 Then
            Return IO.PhValue
          Else
            Dim TestPH As Integer
            TestPH = CType((IO.PhValue + PhControl.ExpectPh) / 2, Integer)

            If IO.PhValue > PhControl.ExpectPh Then
              Return IO.PhValue

            Else
              Return TestPH
            End If

          End If
        Else
          Return IO.PhValue
        End If

      End If

    End Get

  End Property
    <GraphTrace(1, 1000, -1000, 3000, "Blue", "%2tpH"), Translate("zh-TW", "PH設定值")> _
Public ReadOnly Property PHSetpoint() As Integer
        Get
            If PhControl.IsOn Then
                Return PhControl.ExpectPh


            End If

        End Get
    End Property
  <GraphTrace(1, 1800, 6000, 8000, "Green", "0"), Translate("zh-TW", "主馬達轉速")> _
  Public ReadOnly Property MainPumpSpeedCurve() As Integer
    Get
      Return (IO.MainPumpSpeed)
    End Get
  End Property
  <GraphTrace(1, 1800, 6000, 8000, "Green", "0 RPM"), Translate("zh-TW", "布輪1轉速")> _
  Public ReadOnly Property Reel1PumpSpeedCurve() As Integer
    Get
      Return (IO.ReelSpeed1)
    End Get
  End Property
  <GraphTrace(1, 1800, 6000, 8000, "Green", "0 RPM"), Translate("zh-TW", "布輪2轉速")> _
  Public ReadOnly Property Reel2PumpSpeedCurve() As Integer
    Get
      Return (IO.ReelSpeed2)
    End Get
  End Property
  <GraphTrace(1, 900, 6000, 8000, "Yellow", "0 sec"), Translate("zh-TW", "布速1時間")> _
  Public ReadOnly Property Reel1TimeCurve() As Integer
    Get
      Return (FabricCycleTime1)
    End Get
  End Property
  <GraphTrace(1, 900, 6000, 8000, "Yellow", "0 sec"), Translate("zh-TW", "布速2時間")> _
  Public ReadOnly Property Reel2TimeCurve() As Integer
    Get
      Return (FabricCycleTime2)
    End Get
  End Property
  <GraphTrace(1, 900, 6000, 8000, "Yellow", "0 sec"), Translate("zh-TW", "布速3時間")> _
  Public ReadOnly Property Reel3TimeCurve() As Integer
    Get
      Return (FabricCycleTime3)
    End Get
  End Property
  <GraphTrace(1, 900, 6000, 8000, "Yellow", "0 sec"), Translate("zh-TW", "布速4時間")> _
  Public ReadOnly Property Reel4TimeCurve() As Integer
    Get
      Return (FabricCycleTime4)
    End Get
  End Property
  <GraphTrace(1, 900, 6000, 8000, "Yellow", "0 sec"), Translate("zh-TW", "布速5時間")> _
  Public ReadOnly Property Reel5TimeCurve() As Integer
    Get
      Return (FabricCycleTime5)
    End Get
  End Property
  <GraphTrace(1, 900, 6000, 8000, "Yellow", "0 sec"), Translate("zh-TW", "布速6時間")> _
  Public ReadOnly Property Reel6TimeCurve() As Integer
    Get
      Return (FabricCycleTime6)
    End Get
  End Property
  <GraphTrace(1, 900, 6000, 8000, "Yellow", "0 sec"), Translate("zh-TW", "布速7時間")> _
  Public ReadOnly Property Reel7TimeCurve() As Integer
    Get
      Return (FabricCycleTime7)
    End Get
  End Property
  <GraphTrace(1, 900, 6000, 8000, "Yellow", "0 sec"), Translate("zh-TW", "布速8時間")> _
  Public ReadOnly Property Reel8TimeCurve() As Integer
    Get
      Return (FabricCycleTime8)
    End Get
  End Property


  <GraphTrace(1, 12000, 6000, 8000, "Green", "0L"), Translate("zh-TW", "主缸水位")> _
Public ReadOnly Property MainTankLiter() As Integer
    Get
      Return MainTankVolume
    End Get
  End Property
  <GraphTrace(1, 500, 6000, 9500, "Blue", "%2t"), Translate("zh-TW", "缸壓")> _
  Public ReadOnly Property MainTankPressure() As Short
    Get
      Return IO.MainTankPressure
    End Get
  End Property
  <GraphTrace(1, 500, 6000, 9500, "Yellow", "%2t"), Translate("zh-TW", "噴壓")> _
  Public ReadOnly Property NozzlePressure() As Short
    Get
      Return IO.NozzlePressure
    End Get
  End Property
  <GraphTrace(1, 500, 6000, 9500, "Black", "%2t"), Translate("zh-TW", "壓差")> _
  Public ReadOnly Property ShowPressureDifferent() As Short
    Get
      Return PressureDifferent
    End Get
  End Property


    Public ReadOnly Property CurrentTime() As String
        Get
            Return Date.Now.ToLongTimeString()
        End Get
    End Property

    Public ReadOnly Property Status() As String
        Get
            If Parent.Signal <> "" And FlashFlag Then Return Parent.Signal
            If ManualDose.IsOn Then
                Return ManualDose.StateString
            ElseIf Command01.IsOn Then
                If PhControl.IsOn And FlashFlag2 Then
                    Return PhControl.StateString
                ElseIf Command61.IsOn And FlashFlag Then
                    Return Command61.StateString
                ElseIf Command62.IsOn And FlashFlag Then
                    Return Command62.StateString
                ElseIf Command64.IsOn And FlashFlag Then
                    Return Command64.StateString
                ElseIf Command65.IsOn And FlashFlag Then
                    Return Command65.StateString
                ElseIf Command67.IsOn And FlashFlag Then
                    Return Command67.StateString
                ElseIf Command68.IsOn And FlashFlag Then
                    Return Command68.StateString
                Else
                    Return Command01.StateString
                End If
            ElseIf (Command74.IsOn Or Command75.IsOn Or Command76.IsOn Or Command77.IsOn) And PhWash.IsOn And FlashFlag Then
                Return PhWash.StateString

            ElseIf Command73.IsOn Then
                If PhWash.IsOn And FlashFlag2 Then
                    Return PhWash.StateString
                Else
                    Return Command73.StateString
                End If
            ElseIf Command74.IsOn Then
                If PhControl.IsOn And FlashFlag2 Then
                    Return PhControl.StateString
                Else
                    Return Command74.StateString
                End If

            ElseIf Command75.IsOn Then
                Return Command75.StateString

            ElseIf Command76.IsOn Then
                If PhControl.IsOn And FlashFlag2 Then
                    Return PhControl.StateString
                Else
                    Return Command76.StateString
                End If
            ElseIf Command77.IsOn Then
                If PhControl.IsOn And FlashFlag2 Then
                    Return PhControl.StateString
                Else
                    Return Command77.StateString
                End If
            ElseIf Command78.IsOn Then
                If PhControl.IsOn And FlashFlag2 Then
                    Return PhControl.StateString
                Else
                    Return Command78.StateString
                End If
            ElseIf Command79.IsOn Then
                If PhControl.IsOn And FlashFlag2 Then
                    Return PhControl.StateString
                Else
                    Return Command79.StateString
                End If
            ElseIf Command80.IsOn Then
                If PhControl.IsOn And FlashFlag2 Then
                    Return PhControl.StateString
                Else
                    Return Command80.StateString
                End If
            ElseIf Command02.IsOn Then
                If Command61.IsOn And FlashFlag Then
                    Return Command61.StateString
                ElseIf Command62.IsOn And FlashFlag Then
                    Return Command62.StateString
                ElseIf Command64.IsOn And FlashFlag Then
                    Return Command64.StateString
                ElseIf Command65.IsOn And FlashFlag Then
                    Return Command65.StateString
                ElseIf Command67.IsOn And FlashFlag Then
                    Return Command67.StateString
                ElseIf Command68.IsOn And FlashFlag Then
                    Return Command68.StateString
                Else
                    Return Command02.StateString
                End If
            ElseIf Command03.IsOn Then
                Return Command03.StateString
            ElseIf Command39.IsOn Then
                Return Command39.StateString
            ElseIf Command04.IsOn Then
        Return Command04.StateString
      ElseIf Command10.IsOn Then
        Return Command10.StateString
            ElseIf Command11.IsOn Then
                Return Command11.StateString
            ElseIf Command12.IsOn Then
                Return Command12.StateString
            ElseIf Command13.IsOn Then
                Return Command13.StateString
            ElseIf Command14.IsOn Then
                Return Command14.StateString
            ElseIf Command17.IsOn Then
                Return Command17.StateString
            ElseIf Command18.IsOn Then
                Return Command18.StateString
            ElseIf Command19.IsOn Then
                Return Command19.StateString
            ElseIf Command20.IsOn Then
                Return Command20.StateString
            ElseIf Command32.IsOn Then
                Return Command32.StateString
            ElseIf Command51.IsOn Then
                Return Command51.StateString
            ElseIf Command52.IsOn Then
                Return Command52.StateString
            ElseIf Command90.IsOn Then
                Return Command90.StateString
            ElseIf Command54.IsOn Then
                If Command61.IsOn Or Command65.IsOn Or Command67.IsActive Then
                    If Command61.IsOn And FlashFlag Then
                        Return Command61.StateString
                    ElseIf Command65.IsOn And FlashFlag Then
                        Return Command65.StateString
                    ElseIf Command67.IsActive And FlashFlag Then
                        Return Command67.StateString
                    Else
                        Return Command54.StateString
                    End If
                Else
                    Return Command54.StateString
                End If

            ElseIf Command55.IsOn Then
                If Command64.IsOn Or Command61.IsActive Or Command67.IsActive Then
                    If Command64.IsOn And FlashFlag Then
                        Return Command64.StateString
                    ElseIf Command61.IsOn And FlashFlag Then
                        Return Command61.StateString
                    ElseIf Command67.IsActive And FlashFlag Then
                        Return Command67.StateString
                    Else
                        Return Command55.StateString
                    End If
                Else
                    Return Command55.StateString
                End If

            ElseIf Command57.IsOn Then
                If Command65.IsOn Then
                    If Command65.IsOn And FlashFlag Then
                        Return Command65.StateString
                    Else
                        Return Command57.StateString
                    End If
                Else
                    Return Command57.StateString
                End If

            ElseIf Command58.IsOn Then
                If Command64.IsOn Then
                    If Command64.IsOn And FlashFlag Then
                        Return Command64.StateString
                    Else
                        Return Command58.StateString
                    End If
                Else
                    Return Command58.StateString
                End If

            ElseIf Not Parent.IsProgramRunning Then
                Return "停滯時間: " & TimerString(ProgramStoppedTimer.TimeElapsed)
            ElseIf Command41.IsOn Then
                Return Command41.StateString
            ElseIf Command42.IsOn Then
                Return Command42.StateString
            ElseIf Command43.IsOn Then
                Return Command43.StateString
            ElseIf Command44.IsOn Then
                Return Command44.StateString
            End If
            Return ""
        End Get
    End Property
    Private Function GetProductsFromString(ByVal productString As String) As String()
        Try
            Dim products() As String
            products = productString.Split("|".ToCharArray)
            Return products

        Catch ex As Exception
            Parent.LogException(ex)
        End Try
        Return Nothing
    End Function
  '---------------------------------------------------------------------
  Public Sub ConnectBDC()
    Try
      Parameters.ConnectSPCTest = 0
      SPCConnectError = True
      cs_Recipe = "data source=" & SPCServerName & ";initial catalog=BatchDyeingCentral;user id= " & SPCUserName & ";password=" & SPCPassword & ""
      qs_Recipe = "SELECT Dyelot, ReDye, StepNumber, ProductCode, ProductType, Grams, DispenseGrams, DispenseResult FROM DyelotsBulkedRecipe WHERE (Dyelot = '" & 工單 & "') AND (ReDye = '" & 重染 & "') AND (ProductCode <> '') ORDER BY StepNumber"

      '1.SqlConnection
      cn_Recipe = New SqlClient.SqlConnection(cs_Recipe)
      cn_Recipe.Open()
      '2.SqlCommand
      cmd_Recipe = New SqlClient.SqlCommand(qs_Recipe, cn_Recipe)
      '3.SqlDataAdapter
      da_Recipe = New SqlClient.SqlDataAdapter(cmd_Recipe)
      '4.SqlCommandBuilder
      cb_Recipe = New SqlClient.SqlCommandBuilder(da_Recipe)
      '5.建立DataSet類別或DataTable類別
      ds_Recipe = New DataSet()
      '6.使用Fill方法填入DataTable
      da_Recipe.Fill(ds_Recipe, "Recipe")
      dt_Recipe = ds_Recipe.Tables("Recipe")
      If dt_Recipe.Rows.Count > 0 Then
        Dim i As Integer
        Dim j As Integer = 0
        Dim k As Integer = 0
        For i = 0 To dt_Recipe.Rows.Count - 1
          StepNumber1(i) = dt_Recipe.Rows(i).Item("StepNumber").ToString
          ProductCode(i) = dt_Recipe.Rows(i).Item("ProductCode").ToString.Trim
          ProductType(i) = dt_Recipe.Rows(i).Item("ProductType").ToString
          Grams(i) = dt_Recipe.Rows(i).Item("Grams").ToString
          DispenseGrams(i) = dt_Recipe.Rows(i).Item("DispenseGrams").ToString
          DispenseResult(i) = dt_Recipe.Rows(i).Item("DispenseResult").ToString



          If ProductType(i) = "1" Then
            DyeStepNumber(j) = StepNumber1(i)
            DyeCode(j) = ProductCode(i).Trim
            DyeGrams(j) = Grams(i)
            DyeDispenseGrams(j) = DispenseGrams(i)
            DyeDispenseResult(j) = DispenseResult(i)
            j = j + 1
          Else
            ChemicalStepNumber(k) = StepNumber1(i)
            ChemicalCode(k) = ProductCode(i)
            ChemicalGrams(k) = Grams(i)
            ChemicalDispenseGrams(k) = DispenseGrams(i)
            ChemicalDispenseResult(k) = DispenseResult(i)
            k = k + 1
          End If
        Next

      End If
      SPCConnectError = False

      cn_Recipe.Close()
      SQL成功 = SQL成功 + 1
    Catch ex As Exception
      'Ignore errors
    End Try
  End Sub

  Public Sub DispenseState()
    Try
      'ComputerName = System.Environment.MachineName
      ComputerName = System.Environment.MachineName
      cs_DispenseState = "data source=" & SPCServerName & ";initial catalog=BatchDyeingCentral;user id= " & SPCUserName & ";password=" & SPCPassword & ""
      qs_DispenseState = "SELECT Name, DispenseDyelot, DispenseReDye, ChemicalCallOff, ChemicalState, ChemicalTank, ChemicalEnabled, DyeCallOff, DyeState, DyeTank, DyeEnabled FROM Machines WHERE (Name = '" & ComputerName & "') "

      '1.SqlConnection
      cn_DispenseState = New SqlClient.SqlConnection(cs_DispenseState)
      cn_DispenseState.Open()
      '2.SqlCommand
      cmd_DispenseState = New SqlClient.SqlCommand(qs_DispenseState, cn_DispenseState)
      '3.SqlDataAdapter
      da_DispenseState = New SqlClient.SqlDataAdapter(cmd_DispenseState)
      '4.SqlCommandBuilder
      cb_DispenseState = New SqlClient.SqlCommandBuilder(da_DispenseState)
      '5.建立DataSet類別或DataTable類別
      ds_DispenseState = New DataSet()
      '6.使用Fill方法填入DataTable
      da_DispenseState.Fill(ds_DispenseState, "DispenseState")
      dt_DispenseState = ds_DispenseState.Tables("DispenseState")
      CCallOff = dt_DispenseState.Rows(0).Item("ChemicalCallOff").ToString
      CTank = dt_DispenseState.Rows(0).Item("ChemicalTank").ToString
      CState = dt_DispenseState.Rows(0).Item("ChemicalState").ToString
      CEnabled = dt_DispenseState.Rows(0).Item("ChemicalEnabled").ToString
      DCallOff = dt_DispenseState.Rows(0).Item("DyeCallOff").ToString
      DTank = dt_DispenseState.Rows(0).Item("DyeTank").ToString
      DState = dt_DispenseState.Rows(0).Item("DyeState").ToString
      DEnabled = dt_DispenseState.Rows(0).Item("DyeEnabled").ToString

      '助劑呼叫的規則


      If ProgramStopCleanDatabase Then
        cmd_DispenseState = New SqlClient.SqlCommand("UPDATE Machines SET DispenseDyelot=null, DispenseReDye=0 " & _
                                             ", ChemicalCallOff = 0, ChemicalTank=0, ChemicalState='101', DyeCallOff =0, DyeTank=0, DyeState='101'" & _
                                             " WHERE (Name = '" & ComputerName & "')", cn_DispenseState)
        cmd_DispenseState.ExecuteNonQuery()
        ProgramStopCleanDatabase = False


      Else
        If ChemicalCallOff = 0 And ChemicalTank = 0 And Not CallFor302D And ((CState = "") Or (CState = "101") Or (CState = "301") Or (CState = "302") Or (CState = "309") Or (CState = "202") Or (CState = "201") Or ChemicalState = 101) Then
          ChemicalState = 101
          cmd_DispenseState = New SqlClient.SqlCommand("UPDATE Machines SET DispenseDyelot='" & 工單 & "', DispenseReDye='" & 重染 & _
                                                       "', ChemicalCallOff = " & ChemicalCallOff & ", ChemicalTank=" & ChemicalTank & ", ChemicalState=" & ChemicalState & _
                                                       " WHERE (Name = '" & ComputerName & "')", cn_DispenseState)
          cmd_DispenseState.ExecuteNonQuery()

        ElseIf ChemicalCallOff = 0 And ChemicalTank = 0 And CState = "101" And CEnabled = "1" And CallFor302D Then
          If CallFor302D And Not UpdatePowderDispenseResult Then
            'cmd_DispenseState = New SqlClient.SqlCommand("UPDATE DyelotsBulkedRecipe SET DispenseResult=Null WHERE Dyelot='" & 工單 & "'AND ReDye='" & 重染 &
            '                                 "'AND StepNumber = " & Command46.CallOff.ToString & " AND ProductType= 3", cn_DispenseState)

            'cmd_DispenseState.ExecuteNonQuery()
            UpdatePowderDispenseResult = True
          End If
        ElseIf ChemicalCallOff <> 0 And ChemicalTank <> 0 And CState = "101" And CEnabled = "1" Then
          ChemicalState = 201
          cmd_DispenseState = New SqlClient.SqlCommand("UPDATE Machines SET DispenseDyelot='" & 工單 & "', DispenseReDye='" & 重染 & _
                                                       "', ChemicalCallOff = " & ChemicalCallOff & ", ChemicalTank=" & ChemicalTank & ", ChemicalState=" & ChemicalState & _
                                                       " WHERE (Name = '" & ComputerName & "')", cn_DispenseState)
          cmd_DispenseState.ExecuteNonQuery()

          '=============================================================================================================================
          'cmd_DispenseState = New SqlClient.SqlCommand("update DyelotsBulkedRecipe set dispenseresult = null , ladispenseresult = null where dyelot ='" & 工單 & "' and redye = '" & 重染 &
          '                                   "' and stepnumber = " & ChemicalCallOff, cn_DispenseState)
          'cmd_DispenseState.ExecuteNonQuery()

        ElseIf ChemicalCallOff <> 0 And ChemicalTank <> 0 And (CState = "202" Or CState = "102" Or (WaitFor302D)) And CEnabled = "1" Then
          If CState = "102" Then
            ChemicalState = 102
          Else
            ChemicalState = 202
          End If

          For i As Integer = 0 To 40
            If ChemicalCode(i) IsNot Nothing Then
              If ChemicalStepNumber(i) = ChemicalCallOff.ToString Then
                If ChemicalDispenseResult(i) = "309" Or ChemicalState = 309 Then
                  ChemicalState = 309
                ElseIf ChemicalDispenseResult(i) = "302" Or ChemicalState = 302 Then
                  ChemicalState = 302
                ElseIf ChemicalDispenseResult(i) = "301" Or ChemicalState = 301 Then
                  ChemicalState = 301
                End If
              End If
            End If
          Next
          If ChemicalState = 301 Or ChemicalState = 302 Or ChemicalState = 309 Then
            cmd_DispenseState = New SqlClient.SqlCommand("UPDATE Machines SET DispenseDyelot='" & 工單 & "', DispenseReDye='" & 重染 & _
                                                         "', ChemicalCallOff = " & ChemicalCallOff & ", ChemicalTank=" & ChemicalTank & ", ChemicalState=" & ChemicalState & _
                                                         " WHERE (Name = '" & ComputerName & "')", cn_DispenseState)
            cmd_DispenseState.ExecuteNonQuery()
          End If
        ElseIf ChemicalCallOff <> 0 And ChemicalTank <> 0 And CState = "301" And CEnabled = "1" Then
          ChemicalState = 301
        ElseIf ChemicalCallOff <> 0 And ChemicalTank <> 0 And CState = "302" And CEnabled = "1" Then
          ChemicalState = 302
          ' If 粉體呼叫 <> "0" Or 粉體呼叫 <> "" Then
          ' cmd_DispenseState = New SqlClient.SqlCommand("UPDATE Machines SET CheckPowderRb = '1' WHERE Name = 'NC'", cn_DispenseState)
          ' cmd_DispenseState.ExecuteNonQuery()
          ' End If

        ElseIf ChemicalCallOff <> 0 And ChemicalTank <> 0 And CState = "309" And CEnabled = "1" Then
          ChemicalState = 309
        End If
      End If

      '染料呼叫的規則
      If DyeCallOff = 0 And DyeTank = 0 And ((DState = "") Or (DState = "101") Or (DState = "301") Or (DState = "302") Or (DState = "309") Or (DState = "201") Or (DState = "202") Or (DState = "203") Or (DState = "205") Or (DState = "207")) Then
        DyeState = 101
        cmd_DispenseState = New SqlClient.SqlCommand("UPDATE Machines SET DispenseDyelot='" & 工單 & "', DispenseReDye='" & 重染 & _
                                             "', DyeCallOff = " & DyeCallOff & ", DyeTank=" & DyeTank & ", DyeState=" & DyeState & _
                                             " WHERE (Name = '" & ComputerName & "')", cn_DispenseState)
        cmd_DispenseState.ExecuteNonQuery()
      ElseIf DyeCallOff <> 0 And DyeTank <> 0 And DState = "101" And DEnabled = "1" Then
        DyeState = 201
        cmd_DispenseState = New SqlClient.SqlCommand("UPDATE Machines SET DispenseDyelot='" & 工單 & "', DispenseReDye='" & 重染 & _
                                             "', DyeCallOff = " & DyeCallOff & ", DyeTank=" & DyeTank & ", DyeState=" & DyeState & _
                                             " WHERE (Name = '" & ComputerName & "')", cn_DispenseState)
        cmd_DispenseState.ExecuteNonQuery()
      ElseIf DyeCallOff <> 0 And DyeTank <> 0 And DState = "202" And DEnabled = "1" Then
        DyeState = 202
        ' If Call252AddDye Then
        ' DyeState = 207
        ' cmd_DispenseState = New SqlClient.SqlCommand("UPDATE Machines SET DispenseDyelot='" & 工單 & "', DispenseReDye='" & 重染 & _
        '                                              "', DyeCallOff = " & DyeCallOff & ", DyeTank=" & DyeTank & ", DyeState=" & DyeState & _
        '                                              " WHERE (Name = '" & ComputerName & "')", cn_DispenseState)
        ' cmd_DispenseState.ExecuteNonQuery()
        ' End If
      ElseIf DyeCallOff <> 0 And DyeTank <> 0 And (DState = "205" Or DyeState = 205) And DEnabled = "1" And Command44.IsOn Then
        DyeState = 205
        If Call252AddDye Then
          DyeState = 207
          cmd_DispenseState = New SqlClient.SqlCommand("UPDATE Machines SET DispenseDyelot='" & 工單 & "', DispenseReDye='" & 重染 & _
                                             "', DyeCallOff = " & DyeCallOff & ", DyeTank=" & DyeTank & ", DyeState=" & DyeState & _
                                             " WHERE (Name = '" & ComputerName & "')", cn_DispenseState)
          cmd_DispenseState.ExecuteNonQuery()
        End If
      ElseIf DyeCallOff <> 0 And DyeTank <> 0 And (DState = "102" And DyeState = 207) And DEnabled = "1" And Command44.IsOn Then
        DyeState = 207
        If Call252AddDye Then
          DyeState = 207
          cmd_DispenseState = New SqlClient.SqlCommand("UPDATE Machines SET DispenseDyelot='" & 工單 & "', DispenseReDye='" & 重染 & _
                                             "', DyeCallOff = " & DyeCallOff & ", DyeTank=" & DyeTank & ", DyeState=" & DyeState & _
                                             " WHERE (Name = '" & ComputerName & "')", cn_DispenseState)
          cmd_DispenseState.ExecuteNonQuery()
        End If
      ElseIf DyeCallOff <> 0 And DyeTank <> 0 And DState = "301" And DEnabled = "1" Then
        DyeState = 301
      ElseIf DyeCallOff <> 0 And DyeTank <> 0 And DState = "302" And DEnabled = "1" Then
        DyeState = 302
      ElseIf DyeCallOff <> 0 And DyeTank <> 0 And DState = "309" And DEnabled = "1" Then
        DyeState = 309
      End If

      cn_DispenseState.Close()


      SQL連線狀況 = My.Computer.Clock.TickCount - SQL連線狀況1

      SQL連線狀況1 = My.Computer.Clock.TickCount


    Catch ex As Exception
      Dim gg As Integer
      gg = gg
    End Try
  End Sub
  Public Sub DispenseState1()
    Try
      ComputerName = System.Environment.MachineName

      cs_DispenseState1 = "data source=" & SPCServerName & ";initial catalog=BatchDyeingCentral;user id= " & SPCUserName & ";password=" & SPCPassword & ""
      qs_DispenseState1 = "SELECT Name, DispenseDyelot, DispenseReDye, ChemicalCallOff, ChemicalState, ChemicalTank, ChemicalEnabled, DyeCallOff, DyeState, DyeTank, DyeEnabled FROM Machines WHERE (Name = '" & ComputerName & "') "

      '1.SqlConnection
      cn_DispenseState1 = New SqlClient.SqlConnection(cs_DispenseState1)
      cn_DispenseState1.Open()
      '2.SqlCommand
      cmd_DispenseState1 = New SqlClient.SqlCommand(qs_DispenseState1, cn_DispenseState1)
      '3.SqlDataAdapter
      da_DispenseState1 = New SqlClient.SqlDataAdapter(cmd_DispenseState1)
      '4.SqlCommandBuilder
      cb_DispenseState1 = New SqlClient.SqlCommandBuilder(da_DispenseState1)
      '5.建立DataSet類別或DataTable類別
      ds_DispenseState1 = New DataSet()
      '6.使用Fill方法填入DataTable
      da_DispenseState1.Fill(ds_DispenseState1, "DispenseState1")
      dt_DispenseState1 = ds_DispenseState1.Tables("DispenseState1")


      CCallOff = dt_DispenseState.Rows(0).Item("ChemicalCallOff").ToString
      CTank = dt_DispenseState.Rows(0).Item("ChemicalTank").ToString
      CState = dt_DispenseState.Rows(0).Item("ChemicalState").ToString
      CEnabled = dt_DispenseState.Rows(0).Item("ChemicalEnabled").ToString
      DCallOff = dt_DispenseState.Rows(0).Item("DyeCallOff").ToString
      DTank = dt_DispenseState.Rows(0).Item("DyeTank").ToString
      DState = dt_DispenseState.Rows(0).Item("DyeState").ToString
      DEnabled = dt_DispenseState.Rows(0).Item("DyeEnabled").ToString

      '粉體呼叫 = dt_DispenseState.Rows(0).Item("CheckPowderRb").ToString





      ChemicalState = 101
      cmd_DispenseState1 = New SqlClient.SqlCommand("UPDATE Machines SET DispenseDyelot='', DispenseReDye='0', ChemicalCallOff =0, ChemicalTank=0, ChemicalState=101, DyeCallOff =0, DyeTank=0, DyeState=101 WHERE (Name = '" & ComputerName & "')", cn_DispenseState1)

      cmd_DispenseState1.ExecuteNonQuery()


      cn_DispenseState1.Close()
      CTank = "0"
      CState = "101"
      CCallOff = "0"
      DCallOff = "0"
      DTank = "0"
      DState = "101"
    Catch ex As Exception
      'Ignore errors
    End Try
  End Sub

End Class

Public Enum LanguageValue
  English
  ZhTw
  ZhCn
End Enum