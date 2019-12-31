Public NotInheritable Class IO
    Inherits MarshalByRefObject

  <IO(IOType.Dinp, 1), Translate("zh-TW", "保留X0")> Public SavePointX0 As Boolean
    <IO(IOType.Dinp, 2), Translate("zh-TW", "系統自動")> Public SystemAuto As Boolean
    <IO(IOType.Dinp, 3), Translate("zh-TW", "呼叫確認")> Public CallAck As Boolean
    <IO(IOType.Dinp, 4), Translate("zh-TW", "壓力訊號")> Public PressureSw As Boolean
    <IO(IOType.Dinp, 5), Translate("zh-TW", "主馬達訊號")> Public MainPumpFB As Boolean
    <IO(IOType.Dinp, 6), Translate("zh-TW", "纏車1")> Public Entanglement1 As Boolean
    <IO(IOType.Dinp, 7), Translate("zh-TW", "排水水位")> Public DrainLevel As Boolean
    <IO(IOType.Dinp, 8), Translate("zh-TW", "低水位")> Public LowLevel As Boolean
    <IO(IOType.Dinp, 9), Translate("zh-TW", "中水位")> Public MiddleLevel As Boolean
    <IO(IOType.Dinp, 10), Translate("zh-TW", "高水位")> Public HighLevel As Boolean
  <IO(IOType.Dinp, 11), Translate("zh-TW", "B手動排水")> Public BManualDrain As Boolean
  <IO(IOType.Dinp, 12), Translate("zh-TW", "C手動排水")> Public CManualDrain As Boolean
    <IO(IOType.Dinp, 13), Translate("zh-TW", "B低水位")> Public BTankLow As Boolean
    <IO(IOType.Dinp, 14), Translate("zh-TW", "B備藥完成")> Public BTankReady As Boolean

  <IO(IOType.Dinp, 15), Translate("zh-TW", "C低水位")> Public CTankLow As Boolean
  <IO(IOType.Dinp, 16), Translate("zh-TW", "C備藥完成")> Public CTankReady As Boolean

  <IO(IOType.Dinp, 17), Translate("zh-TW", "布輪訊號1")> Public FabricCycleInput1 As Boolean
  <IO(IOType.Dinp, 18), Translate("zh-TW", "布輪訊號2")> Public FabricCycleInput2 As Boolean
  <IO(IOType.Dinp, 19), Translate("zh-TW", "布輪訊號3")> Public FabricCycleInput3 As Boolean
  <IO(IOType.Dinp, 20), Translate("zh-TW", "布輪訊號4")> Public FabricCycleInput4 As Boolean
  <IO(IOType.Dinp, 21), Translate("zh-TW", "布輪訊號5")> Public FabricCycleInput5 As Boolean
  <IO(IOType.Dinp, 22), Translate("zh-TW", "布輪訊號6")> Public FabricCycleInput6 As Boolean
  <IO(IOType.Dinp, 23), Translate("zh-TW", "布輪訊號7")> Public FabricCycleInput7 As Boolean
  <IO(IOType.Dinp, 24), Translate("zh-TW", "布輪訊號8")> Public FabricCycleInput8 As Boolean

  <IO(IOType.Dinp, 25), Translate("zh-TW", "手動10分鐘加藥")> Public B1Add As Boolean
  <IO(IOType.Dinp, 26), Translate("zh-TW", "手動15分鐘加藥")> Public B2Add As Boolean
  <IO(IOType.Dinp, 27), Translate("zh-TW", "手動20分鐘加藥")> Public B3Add As Boolean
  <IO(IOType.Dinp, 28), Translate("zh-TW", "手動30分鐘加藥")> Public B4Add As Boolean
  <IO(IOType.Dinp, 29), Translate("zh-TW", "手動40分鐘加藥")> Public B5Add As Boolean


  <IO(IOType.Dinp, 30), Translate("zh-TW", "手動動力排水")> Public PowerDrainSW As Boolean
  <IO(IOType.Dinp, 31), Translate("zh-TW", "執行確認")> Public RemoteRun As Boolean
  <IO(IOType.Dinp, 32), Translate("zh-TW", "停止週期")> Public RemoteHalt As Boolean
  <IO(IOType.Dinp, 33), Translate("zh-TW", "往上")> Public RemoteUp As Boolean
  <IO(IOType.Dinp, 34), Translate("zh-TW", "往下")> Public RemoteDown As Boolean
  <IO(IOType.Dinp, 35), Translate("zh-TW", "手動降溫水洗")> Public ManualCoolRinse As Boolean
  <IO(IOType.Dinp, 36), Translate("zh-TW", "手動降溫")> Public ManualCooling As Boolean
  '***********************PH功能*************************************************************
  <IO(IOType.Dinp, 37), Translate("zh-TW", "B中水位")> Public BTankMiddle As Boolean ' 
  <IO(IOType.Dinp, 38), Translate("zh-TW", "B高水位")> Public BTankHigh As Boolean '   

  <IO(IOType.Dinp, 39), Translate("zh-TW", "C高水位")> Public CTankHigh As Boolean ' 
  <IO(IOType.Dinp, 40), Translate("zh-TW", "C中水位")> Public CTankMiddle As Boolean ' 

    <IO(IOType.Dinp, 73), Translate("zh-TW", "PH自動")> Public PhAuto As Boolean
    <IO(IOType.Dinp, 74), Translate("zh-TW", "PH呼叫確認")> Public PhCallAck As Boolean
    <IO(IOType.Dinp, 75), Translate("zh-TW", "PH循環桶高水位")> Public PhMixTankHighLevel As Boolean
    <IO(IOType.Dinp, 76), Translate("zh-TW", "PH循環桶低水位")> Public PhMixTankLowLevel As Boolean
    <IO(IOType.Dinp, 77), Translate("zh-TW", "PH手動檢測")> Public PhManulCheck As Boolean
    <IO(IOType.Dinp, 78), Translate("zh-TW", "PH手動清洗")> Public PhManulWash As Boolean
  '******************************************************************************************
  Public PulseFB As Boolean '    <IO(IOType.Dinp, 1), Translate("zh-TW", "流量計脈衝訊號")>
  Public ManualDoseBC_Switch As Boolean  '    <IO(IOType.Dinp, 21), Translate("zh-TW", "手動B/C缸選擇")> 
  Public MainPumpOverload As Boolean '  <IO(IOType.Dinp, 19), Translate("zh-TW", "主馬達過載")>
  Public BPumpOverload As Boolean '  <IO(IOType.Dinp, 20), Translate("zh-TW", "B馬達過載")>

  <IO(IOType.Aninp, 1, , , "%t%%"), _
   GraphTrace(-100, 1200, 3300, 5500, "Red", "%t%"), _
   GraphLabel("B缸水位0%", 0), GraphLabel("B缸水位100%", 1000), _
   Translate("zh-TW", "藥缸B水位")> Public TankBLevel As Short

  <IO(IOType.Aninp, 2, , , "%t%%"), _
   GraphTrace(-100, 1200, 4000, 5500, "Green", "%t%"), _
   Translate("zh-TW", "藥缸C水位")> Public TankCLevel As Short

  <IO(IOType.Aninp, 3, , , "0 L"), Translate("zh-TW", "主缸水位")> Public MainTankLevel As Short
  <IO(IOType.Aninp, 4, , , "0 PRM"), Translate("zh-TW", "主馬達轉速")> Public MainPumpSpeed As Short
  <IO(IOType.Aninp, 5, , , "0 PRM"), Translate("zh-TW", "帶布輪轉速1")> Public ReelSpeed1 As Short
  <IO(IOType.Aninp, 6, , , "0 PRM"), Translate("zh-TW", "帶布輪轉速2")> Public ReelSpeed2 As Short

    '***********************PH功能*************************************************************
  <IO(IOType.Aninp, 7, , , "%2tpH"), GraphTrace(1, 1000, -1000, 3000, "silver", "%2tpH"), _
   GraphLabel("pH 4", 400), _
     GraphLabel("pH 7", 700), GraphLabel("pH 10", 1000), _
     Translate("zh-TW", "測試PH值")> Public PhValue As Short
  <IO(IOType.Aninp, 8, , , "%2t"), Translate("zh-TW", "缸壓")> Public MainTankPressure As Short
  <IO(IOType.Aninp, 9, , , "%2t"), Translate("zh-TW", "噴壓")> Public NozzlePressure As Short
  <IO(IOType.Aninp, 10, , , "0 PRM"), Translate("zh-TW", "帶布輪轉速3")> Public ReelSpeed3 As Short
  <IO(IOType.Aninp, 11, , , "0 PRM"), Translate("zh-TW", "帶布輪轉速4")> Public ReelSpeed4 As Short
 
    <IO(IOType.Temp, 1, , , "%tC"), _
     GraphTrace(0, 1500, 6000, 9500, "Red"), _
     GraphLabel("0C", 0), GraphLabel("50C", 500), GraphLabel("100C", 1000), GraphLabel("150C", 1500), _
     Translate("zh-TW", "主缸溫度")> Public MainTemperature As Short
    <IO(IOType.Temp, 2, , , "%tC"), Translate("zh-TW", "B缸溫度")> Public BTankTemperature As Short
    <IO(IOType.Temp, 3, , , "%tC"), Translate("zh-TW", "C缸溫度")> Public CTankTemperature As Short
    '***********************PH功能*************************************************************
    <IO(IOType.Temp, 4, , , "%tC"), Translate("zh-TW", "PH檢測筒溫度")> Public PhCheckTemp As Short
    '******************************************************************************************

    <IO(IOType.Counter, 1)> Public HSCounter1 As Integer
    <IO(IOType.Counter, 2)> Public HSCounter2 As Integer

    <IO(IOType.Anout, 1), Translate("zh-TW", "比例式昇溫")> Public TemperatureControlHeat As Short  '400
    <IO(IOType.Anout, 2), Translate("zh-TW", "主泵變頻器")> Public PumpSpeedControl As Short  '401
    <IO(IOType.Anout, 3), Translate("zh-TW", "B變頻控制")> Public BDosingOutput As Short  '402
    <IO(IOType.Anout, 4), Translate("zh-TW", "比例式降溫")> Public TemperatureControlCool As Short  '403
  <IO(IOType.Anout, 5), Translate("zh-TW", "保留")> Public CDosingOutput As Short  '404
    <IO(IOType.Anout, 6), Translate("zh-TW", "主缸排水延遲時間")> Public DrainDelayToPlc As Integer  '405
    <IO(IOType.Anout, 7), Translate("zh-TW", "主缸安全溫度")> Public SetSafetyTempToPlc As Integer  '406
    <IO(IOType.Anout, 8), Translate("zh-TW", "熱交換器排水延遲時間")> Public HXDrainDelay As Integer  '407
    <IO(IOType.Anout, 9), Translate("zh-TW", "加藥安全溫度")> Public SetAddSafetyTempToPlc As Integer  '408
    <IO(IOType.Anout, 10), Translate("zh-TW", "藥缸加藥延遲時間")> Public AddFinishDelayToPlc As Integer  '409
  <IO(IOType.Anout, 11), Translate("zh-TW", "藥缸排水延遲時間")> Public TankDrainDelayToPlc As Integer  '410
  <IO(IOType.Anout, 12), Translate("zh-TW", "擺布控制")> Public PlaitingSpeedControl As Short  '411
  <IO(IOType.Anout, 14), Translate("zh-TW", "是否連結2組EAIO")> Public Connect2EAIO As Integer         'D413
    <IO(IOType.Anout, 15), Translate("zh-TW", "是否連結pH系統")> Public ConnectPhSystem As Integer         'D414
    <IO(IOType.Anout, 16), Translate("zh-TW", "取值B低水位")> Public AnalogyBLow As Integer        '415
    <IO(IOType.Anout, 17), Translate("zh-TW", "取值B中水位")> Public AnalogyBMid As Integer        '416
  <IO(IOType.Anout, 18), Translate("zh-TW", "取值B高水位")> Public AnalogyBHigh As Integer        '417
  <IO(IOType.Anout, 19), Translate("zh-TW", "取值C低水位")> Public AnalogyCLow As Integer        '418
  <IO(IOType.Anout, 20), Translate("zh-TW", "取值C中水位")> Public AnalogyCMid As Integer        '419
  <IO(IOType.Anout, 21), Translate("zh-TW", "取值C高水位")> Public AnalogyCHigh As Integer        '420
  <IO(IOType.Anout, 22), Translate("zh-TW", "手動降溫開點")> Public ManualCoolMode As Integer        '421
  Public RollerSpeedControl As Short  '404<IO(IOType.Anout, 5), Translate("zh-TW", "布輪控制")>

    <IO(IOType.Dout, 1), Translate("zh-TW", "昇溫")> Public Heat As Boolean
    <IO(IOType.Dout, 2), Translate("zh-TW", "降溫")> Public Cool As Boolean
    <IO(IOType.Dout, 3), Translate("zh-TW", "熱交換器排水")> Public HxDrain As Boolean
    <IO(IOType.Dout, 4), Translate("zh-TW", "排冷凝水")> Public CondenserDrain As Boolean
    <IO(IOType.Dout, 5), Translate("zh-TW", "冷卻排水")> Public CoolDrain As Boolean
    <IO(IOType.Dout, 6), Translate("zh-TW", "加壓")> Public PressureIn As Boolean
    <IO(IOType.Dout, 7), Translate("zh-TW", "排壓")> Public PressureOut As Boolean
    <IO(IOType.Dout, 8), Translate("zh-TW", "溢流")> Public Overflow As Boolean
    <IO(IOType.Dout, 9), Translate("zh-TW", "進冷水")> Public ColdFill As Boolean
    <IO(IOType.Dout, 10), Translate("zh-TW", "進熱水")> Public HotFill As Boolean
    <IO(IOType.Dout, 11), Translate("zh-TW", "排熱水")> Public HotDrain As Boolean
    <IO(IOType.Dout, 12), Translate("zh-TW", "排水")> Public Drain As Boolean
    <IO(IOType.Dout, 13), Translate("zh-TW", "主馬達啟動")> Public PumpOn As Boolean
    <IO(IOType.Dout, 14), Translate("zh-TW", "主馬達停止")> Public PumpOff As Boolean
    <IO(IOType.Dout, 15), Translate("zh-TW", "B進冷水")> Public BTankColdFill As Boolean
    <IO(IOType.Dout, 16), Translate("zh-TW", "B進迴水")> Public BTankCirculate2 As Boolean
    <IO(IOType.Dout, 17), Translate("zh-TW", "B排水")> Public BTankDrain As Boolean
    <IO(IOType.Dout, 18), Translate("zh-TW", "B加藥")> Public BTankAddition As Boolean
    <IO(IOType.Dout, 19), Translate("zh-TW", "B加藥馬達")> Public BTankAddPump As Boolean
    <IO(IOType.Dout, 20), Translate("zh-TW", "B循環攪拌")> Public BTankMixCir2 As Boolean
    <IO(IOType.Dout, 21), Translate("zh-TW", "異常燈")> Public ErrorLamp As Boolean
    <IO(IOType.Dout, 22), Translate("zh-TW", "呼叫燈")> Public CallLamp As Boolean
    <IO(IOType.Dout, 23), Translate("zh-TW", "C進冷水")> Public CTankColdFill As Boolean
    <IO(IOType.Dout, 24), Translate("zh-TW", "C進迴水")> Public CTankCirculate2 As Boolean
    <IO(IOType.Dout, 25), Translate("zh-TW", "C排水")> Public CTankDrain As Boolean
    <IO(IOType.Dout, 26), Translate("zh-TW", "C加藥")> Public CTankAddition As Boolean
    <IO(IOType.Dout, 27), Translate("zh-TW", "C循環攪拌")> Public CTankMixCir2 As Boolean
    <IO(IOType.Dout, 28), Translate("zh-TW", "總加藥")> Public Addition As Boolean
    <IO(IOType.Dout, 29), Translate("zh-TW", "B備藥完成燈")> Public BTankOkLamp As Boolean
    <IO(IOType.Dout, 30), Translate("zh-TW", "C備藥完成燈")> Public CTankOkLamp As Boolean
    <IO(IOType.Dout, 31), Translate("zh-TW", "動力排水")> Public PowerDrain As Boolean
    <IO(IOType.Dout, 32), Translate("zh-TW", "計量加藥")> Public Dosing As Boolean
  <IO(IOType.Dout, 33), Translate("zh-TW", "降溫水洗閥")> Public CoolWash As Boolean
  <IO(IOType.Dout, 34), Translate("zh-TW", "進水3閥")> Public Fill3 As Boolean
  <IO(IOType.Dout, 35), Translate("zh-TW", "C加藥泵")> Public CTankPump As Boolean
  <IO(IOType.Dout, 36), Translate("zh-TW", "多段水位尺清洗閥")> Public LevelClean As Boolean
  <IO(IOType.Dout, 37), Translate("zh-TW", "降溫熱水")> Public CoolForHotWater As Boolean
  Public BTankHeat As Boolean '    <IO(IOType.Dout, 35), Translate("zh-TW", "B缸加熱")>
  Public CTankHeat As Boolean '    <IO(IOType.Dout, 36), Translate("zh-TW", "C缸加熱")>
  Public FindSeam As Boolean ' <IO(IOType.Dout, 37), Translate("zh-TW", "找布頭")>

    '***********************PH功能*************************************************************
    '-----------------------------------------------------------------------------------------------------
    <IO(IOType.Dout, 65), Translate("zh-TW", "PH自動燈")> Public PhAutoLamp As Boolean
    <IO(IOType.Dout, 66), Translate("zh-TW", "PH入迴水")> Public PhFillCirculate As Boolean
    <IO(IOType.Dout, 67), Translate("zh-TW", "PH入染機")> Public PhInToMachine As Boolean
    <IO(IOType.Dout, 68), Translate("zh-TW", "PH入清水")> Public PhWashFill As Boolean
    <IO(IOType.Dout, 69), Translate("zh-TW", "PH冷卻")> Public PhCool As Boolean
    <IO(IOType.Dout, 70), Translate("zh-TW", "PH排水")> Public PhDrain As Boolean
    <IO(IOType.Dout, 71), Translate("zh-TW", "PH加酸閥")> Public PhAddHacOut As Boolean
    <IO(IOType.Dout, 72), Translate("zh-TW", "PH循環泵")> Public PhCirculatePump As Boolean
    <IO(IOType.Dout, 73), Translate("zh-TW", "PH定量泵")> Public PhAddPump As Boolean
    <IO(IOType.Dout, 74), Translate("zh-TW", "PH警報器")> Public PhAlarm As Boolean
    <IO(IOType.Dout, 75), Translate("zh-TW", "PH呼叫黃燈")> Public PhAckLamp As Boolean
    <IO(IOType.Dout, 76), Translate("zh-TW", "PH高溫燈")> Public PhHitTempLamp As Boolean
    <IO(IOType.Dout, 77), Translate("zh-TW", "運轉綠燈")> Public PhRunLamp As Boolean
    <IO(IOType.Dout, 78), Translate("zh-TW", "異常紅燈")> Public PhErrorLamp As Boolean
    <IO(IOType.Dout, 79), Translate("zh-TW", "PH執行中")> Public PhRunning As Boolean
    '******************************************************************************************
    <IO(IOType.Dout, 126), Translate("zh-TW", "輸出測試")> Public TestDO As Boolean
  <IO(IOType.Dout, 127), Translate("zh-TW", "流量計重置")> Public CounterReset As Boolean

  ' <Translate("zh-TW", "A0 讀取實際值 設定為1"), Category("A0讀取實際值"), DefaultValue(1000)> Public Parameters_OpenRealValue As Short

  <Translate("zh-TW", "AI1 (<<實際值>>)"), Category("AI1校正參數-B缸水位"), DefaultValue(1000)> Public Parameters_RealValue1 As Short
  <Translate("zh-TW", "AI1 (取低值)"), Category("AI1校正參數-B缸水位"), DefaultValue(1000)> Public Parameters_MinValue1 As Short
  <Translate("zh-TW", "AI1 (取高值)"), Category("AI1校正參數-B缸水位"), DefaultValue(1000)> Public Parameters_MaxValue1 As Short = 1000
  <Translate("zh-TW", "AI1 (設低值)"), Category("AI1校正參數-B缸水位"), DefaultValue(1000)> Public Parameters_SetMinValue1 As Short
  <Translate("zh-TW", "AI1 (設高值)"), Category("AI1校正參數-B缸水位"), DefaultValue(1000)> Public Parameters_SetMaxValue1 As Short = 1000




  <Translate("zh-TW", "AI2 (<<實際值>>)"), Category("AI2校正參數-C缸水位"), DefaultValue(1000)> Public Parameters_RealValue2 As Short
  <Translate("zh-TW", "AI2 (取低值)"), Category("AI2校正參數-C缸水位"), DefaultValue(1000)> Public Parameters_MinValue2 As Short
  <Translate("zh-TW", "AI2 (取高值)"), Category("AI2校正參數-C缸水位"), DefaultValue(1000)> Public Parameters_MaxValue2 As Short = 1000
  <Translate("zh-TW", "AI2 (設低值)"), Category("AI2校正參數-C缸水位"), DefaultValue(1000)> Public Parameters_SetMinValue2 As Short
  <Translate("zh-TW", "AI2 (設高值)"), Category("AI2校正參數-C缸水位"), DefaultValue(1000)> Public Parameters_SetMaxValue2 As Short = 1000

  <Translate("zh-TW", "AI3 (<<實際值>>)"), Category("AI3校正參數-無段式水位"), DefaultValue(1000)> Public Parameters_RealValue3 As Short
  <Translate("zh-TW", "AI3 (取低值)"), Category("AI3校正參數-無段式水位"), DefaultValue(1000)> Public Parameters_MinValue3 As Short
  <Translate("zh-TW", "AI3 (取高值)"), Category("AI3校正參數-無段式水位"), DefaultValue(1000)> Public Parameters_MaxValue3 As Short = 1000
  <Translate("zh-TW", "AI3 (設低值)"), Category("AI3校正參數-無段式水位"), DefaultValue(1000)> Public Parameters_SetMinValue3 As Short
  <Translate("zh-TW", "AI3 (設高值)"), Category("AI3校正參數-無段式水位"), DefaultValue(1000)> Public Parameters_SetMaxValue3 As Short = 1000

  <Translate("zh-TW", "AI4 (<<實際值>>)"), Category("AI4校正參數-主馬達轉速"), DefaultValue(1000)> Public Parameters_RealValue4 As Short
  <Translate("zh-TW", "AI4 (取低值)"), Category("AI4校正參數-主馬達轉速"), DefaultValue(1000)> Public Parameters_MinValue4 As Short
  <Translate("zh-TW", "AI4 (取高值)"), Category("AI4校正參數-主馬達轉速"), DefaultValue(1000)> Public Parameters_MaxValue4 As Short = 0
  <Translate("zh-TW", "AI4 (設低值)"), Category("AI4校正參數-主馬達轉速"), DefaultValue(1000)> Public Parameters_SetMinValue4 As Short
  <Translate("zh-TW", "AI4 (設高值)"), Category("AI4校正參數-主馬達轉速"), DefaultValue(1000)> Public Parameters_SetMaxValue4 As Short = 0

  <Translate("zh-TW", "AI5 (<<實際值>>)"), Category("AI5校正參數-帶布輪1轉速"), DefaultValue(1000)> Public Parameters_RealValue5 As Short
  <Translate("zh-TW", "AI5 (取低值)"), Category("AI5校正參數-帶布輪1轉速"), DefaultValue(1000)> Public Parameters_MinValue5 As Short = 0
  <Translate("zh-TW", "AI5 (取高值)"), Category("AI5校正參數-帶布輪1轉速"), DefaultValue(1000)> Public Parameters_MaxValue5 As Short = 0
  <Translate("zh-TW", "AI5 (設低值)"), Category("AI5校正參數-帶布輪1轉速"), DefaultValue(1000)> Public Parameters_SetMinValue5 As Short = 0
  <Translate("zh-TW", "AI5 (設高值)"), Category("AI5校正參數-帶布輪1轉速"), DefaultValue(1000)> Public Parameters_SetMaxValue5 As Short = 0

  <Translate("zh-TW", "AI6 (<<實際值>>)"), Category("AI6校正參數-帶布輪2轉速"), DefaultValue(1000)> Public Parameters_RealValue6 As Short
  <Translate("zh-TW", "AI6 (取低值)"), Category("AI6校正參數-帶布輪2轉速"), DefaultValue(1000)> Public Parameters_MinValue6 As Short = 0
  <Translate("zh-TW", "AI6 (取高值)"), Category("AI6校正參數-帶布輪2轉速"), DefaultValue(1000)> Public Parameters_MaxValue6 As Short = 0
  <Translate("zh-TW", "AI6 (設低值)"), Category("AI6校正參數-帶布輪2轉速"), DefaultValue(1000)> Public Parameters_SetMinValue6 As Short = 0
  <Translate("zh-TW", "AI6 (設高值)"), Category("AI6校正參數-帶布輪2轉速"), DefaultValue(1000)> Public Parameters_SetMaxValue6 As Short = 0

  <Translate("zh-TW", "AI7 (<<實際值>>)"), Category("AI7校正參數(pH值)"), DefaultValue(1000)> Public Parameters_RealValue7 As Short
  <Translate("zh-TW", "AI7 (取低值)"), Category("AI7校正參數(pH值)"), DefaultValue(1000)> Public Parameters_MinValue7 As Short = 0
  <Translate("zh-TW", "AI7 (取高值)"), Category("AI7校正參數(pH值)"), DefaultValue(1000)> Public Parameters_MaxValue7 As Short = 0
  <Translate("zh-TW", "AI7 (設低值)"), Category("AI7校正參數(pH值)"), DefaultValue(1000)> Public Parameters_SetMinValue7 As Short = 400
  <Translate("zh-TW", "AI7 (設高值)"), Category("AI7校正參數(pH值)"), DefaultValue(1000)> Public Parameters_SetMaxValue7 As Short = 700

  <Translate("zh-TW", "AI8 (<<實際值>>)"), Category("AI8校正參數-缸內壓力"), DefaultValue(1000)> Public Parameters_RealValue8 As Short
  <Translate("zh-TW", "AI8 (取低值)"), Category("AI8校正參數-缸內壓力"), DefaultValue(1000)> Public Parameters_MinValue8 As Short = 0
  <Translate("zh-TW", "AI8 (取高值)"), Category("AI8校正參數-缸內壓力"), DefaultValue(1000)> Public Parameters_MaxValue8 As Short = 0
  <Translate("zh-TW", "AI8 (設低值)"), Category("AI8校正參數-缸內壓力"), DefaultValue(1000)> Public Parameters_SetMinValue8 As Short = 0
  <Translate("zh-TW", "AI8 (設高值)"), Category("AI8校正參數-缸內壓力"), DefaultValue(1000)> Public Parameters_SetMaxValue8 As Short = 0

  <Translate("zh-TW", "AI9 (<<實際值>>)"), Category("AI9校正參數-噴壓"), DefaultValue(1000)> Public Parameters_RealValue9 As Short
  <Translate("zh-TW", "AI9 (取低值)"), Category("AI9校正參數-噴壓"), DefaultValue(1000)> Public Parameters_MinValue9 As Short = 0
  <Translate("zh-TW", "AI9 (取高值)"), Category("AI9校正參數-噴壓"), DefaultValue(1000)> Public Parameters_MaxValue9 As Short = 0
  <Translate("zh-TW", "AI9 (設低值)"), Category("AI9校正參數-噴壓"), DefaultValue(1000)> Public Parameters_SetMinValue9 As Short = 0
  <Translate("zh-TW", "AI9 (設高值)"), Category("AI9校正參數-噴壓"), DefaultValue(1000)> Public Parameters_SetMaxValue9 As Short = 0

  <Translate("zh-TW", "AIA10 (<<實際值>>)"), Category("AI10校正參數-帶布輪3轉速"), DefaultValue(1000)> Public Parameters_RealValue10 As Short
  <Translate("zh-TW", "AIA10 (取低值)"), Category("AI10校正參數-帶布輪3轉速"), DefaultValue(1000)> Public Parameters_MinValue10 As Short = 0
  <Translate("zh-TW", "AIA10 (取高值)"), Category("AI10校正參數-帶布輪3轉速"), DefaultValue(1000)> Public Parameters_MaxValue10 As Short = 0
  <Translate("zh-TW", "AIA10 (設低值)"), Category("AI10校正參數-帶布輪3轉速"), DefaultValue(1000)> Public Parameters_SetMinValue10 As Short = 0
  <Translate("zh-TW", "AIA10 (設高值)"), Category("AI10校正參數-帶布輪3轉速"), DefaultValue(1000)> Public Parameters_SetMaxValue10 As Short = 0

  <Translate("zh-TW", "AIA11 (<<實際值>>)"), Category("AI11校正參數-帶布輪4轉速"), DefaultValue(1000)> Public Parameters_RealValue11 As Short
  <Translate("zh-TW", "AIA11 (取低值)"), Category("AI11校正參數-帶布輪4轉速"), DefaultValue(1000)> Public Parameters_MinValue11 As Short = 0
  <Translate("zh-TW", "AIA11 (取高值)"), Category("AI11校正參數-帶布輪4轉速"), DefaultValue(1000)> Public Parameters_MaxValue11 As Short = 0
  <Translate("zh-TW", "AIA11 (設低值)"), Category("AI11校正參數-帶布輪4轉速"), DefaultValue(1000)> Public Parameters_SetMinValue11 As Short = 0
  <Translate("zh-TW", "AIA11 (設高值)"), Category("AI11校正參數-帶布輪4轉速"), DefaultValue(1000)> Public Parameters_SetMaxValue11 As Short = 0




    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Function ReadInputs(ByVal dinp() As Boolean, ByVal aninp() As Short, ByVal temp() As Short) As Boolean
        CheckForSerialPortParametersChanged()
        If Plc.Read(1, "WM400", dinp) = Ports.LA60B.Result.OK Then
            ReadInputs = True
            PlcTimeout.TimeRemaining = 1 ' must have a problem for at least 1 second before triggering the alarm
        End If
    Static ai(18) As UShort

    If Plc.Read(1, "D200", ai) = Ports.LA60B.Result.OK Then
      If ControlCode.Parameters.TempAdd = 0 Then

      ElseIf ControlCode.Parameters.TempAdd > 100 Then
        ai(1) = ai(1) - CType(ControlCode.Parameters.TempAdd - 100, UShort)

      ElseIf ControlCode.Parameters.TempAdd > 0 Then
        ai(1) = ai(1) + CType(ControlCode.Parameters.TempAdd, UShort)
      End If
      ' Copy temperatures directly
      ai(10) = ai(14)
      For i = 1 To temp.Length - 1
        temp(i) = CType(ai(i), Short)
      Next i
      temp(4) = CType(ai(13), Short)
      ' Copy aninps  0mA=0, 20mA=4095, so 0% = 4mA = 819 and 100% = 20mA = 4095
      For i = 1 To 4
        'aninp(i) = CType(((ai(3 + i) - 819) * 1000) \ (4095 - 819), Short)
        aninp(i) = CType(ai(3 + i), Short)
      Next i

      '  If Parameters_OpenRealValue = 1 Then
      Parameters_RealValue1 = CType(ai(4), Short)
      Parameters_RealValue2 = CType(ai(5), Short)
      Parameters_RealValue3 = CType(ai(6), Short)
      Parameters_RealValue4 = CType(ai(7), Short)
      Parameters_RealValue5 = CType(ai(8), Short)
      Parameters_RealValue6 = CType(ai(9), Short)
      Parameters_RealValue7 = CType(ai(10), Short)
      Parameters_RealValue8 = CType(ai(15), Short)
      Parameters_RealValue9 = CType(ai(16), Short)
      Parameters_RealValue10 = CType(ai(17), Short)
      Parameters_RealValue11 = CType(ai(18), Short)
      ' End If

      ControlCode.EaioRealValue1 = CType(ai(4), Short)
      ControlCode.EaioRealValue2 = CType(ai(5), Short)
      ControlCode.EaioRealValue3 = CType(ai(6), Short)
      ControlCode.EaioRealValue4 = CType(ai(7), Short)
      ControlCode.EaioRealValue5 = CType(ai(8), Short)
      ControlCode.EaioRealValue6 = CType(ai(9), Short)
      ControlCode.EaioRealValue7 = CType(ai(10), Short)
      ControlCode.EaioRealValue8 = CType(ai(15), Short)
      ControlCode.EaioRealValue9 = CType(ai(16), Short)
      ControlCode.EaioRealValue10 = CType(ai(17), Short)
      ControlCode.EaioRealValue11 = CType(ai(18), Short)

      '------------------------------------------------------------------------
      Dim value1, value2, value3, value4, value5, value6 As Double
      Dim Parameter_Max, Parameter_Min, Parameter_SetMax, Parameter_SetMin As Short



      For i = 0 To 10
        Select Case i
          Case 0
            Parameter_Min = Parameters_MinValue1
            Parameter_Max = Parameters_MaxValue1
            Parameter_SetMin = Parameters_SetMinValue1
            Parameter_SetMax = Parameters_SetMaxValue1

            value1 = Parameter_Max - Parameter_Min
            value2 = (ai(4)) - Parameter_Min
            value3 = value2 / value1
            value4 = Parameter_SetMax - Parameter_SetMin
            value5 = value4 * value3
            value6 = value5 + Parameter_SetMin
            aninp(1) = CType(value6, Short)


          Case 1
            Parameter_Min = Parameters_MinValue2
            Parameter_Max = Parameters_MaxValue2
            Parameter_SetMin = Parameters_SetMinValue2
            Parameter_SetMax = Parameters_SetMaxValue2

            value1 = Parameter_Max - Parameter_Min
            value2 = (ai(5)) - Parameter_Min
            value3 = value2 / value1
            value4 = Parameter_SetMax - Parameter_SetMin
            value5 = value4 * value3
            value6 = value5 + Parameter_SetMin
            aninp(2) = CType(value6, Short)


          Case 2
            Parameter_Min = Parameters_MinValue3
            Parameter_Max = Parameters_MaxValue3
            Parameter_SetMin = Parameters_SetMinValue3
            Parameter_SetMax = Parameters_SetMaxValue3

            value1 = Parameter_Max - Parameter_Min
            value2 = (ai(6)) - Parameter_Min
            value3 = value2 / value1
            value4 = Parameter_SetMax - Parameter_SetMin
            value5 = value4 * value3
            value6 = value5 + Parameter_SetMin
            aninp(3) = CType(value6, Short)


          Case 3
            Parameter_Min = Parameters_MinValue4
            Parameter_Max = Parameters_MaxValue4
            Parameter_SetMin = Parameters_SetMinValue4
            Parameter_SetMax = Parameters_SetMaxValue4

            value1 = Parameter_Max - Parameter_Min
            value2 = (ai(7)) - Parameter_Min
            value3 = value2 / value1
            value4 = Parameter_SetMax - Parameter_SetMin
            value5 = value4 * value3
            value6 = value5 + Parameter_SetMin
            aninp(4) = CType(value6, Short)
          Case 4
            Parameter_Min = Parameters_MinValue5
            Parameter_Max = Parameters_MaxValue5
            Parameter_SetMin = Parameters_SetMinValue5
            Parameter_SetMax = Parameters_SetMaxValue5

            value1 = Parameter_Max - Parameter_Min
            value2 = (ai(8)) - Parameter_Min
            value3 = value2 / value1
            value4 = Parameter_SetMax - Parameter_SetMin
            value5 = value4 * value3
            value6 = value5 + Parameter_SetMin
            aninp(5) = CType(value6, Short)
          Case 5
            Parameter_Min = Parameters_MinValue6
            Parameter_Max = Parameters_MaxValue6
            Parameter_SetMin = Parameters_SetMinValue6
            Parameter_SetMax = Parameters_SetMaxValue6

            value1 = Parameter_Max - Parameter_Min
            value2 = (ai(9)) - Parameter_Min
            value3 = value2 / value1
            value4 = Parameter_SetMax - Parameter_SetMin
            value5 = value4 * value3
            value6 = value5 + Parameter_SetMin
            aninp(6) = CType(value6, Short)
          Case 6
            Parameter_Min = Parameters_MinValue7
            Parameter_Max = Parameters_MaxValue7
            Parameter_SetMin = Parameters_SetMinValue7
            Parameter_SetMax = Parameters_SetMaxValue7

            value1 = Parameter_Max - Parameter_Min
            value2 = (ai(10)) - Parameter_Min
            value3 = value2 / value1
            value4 = Parameter_SetMax - Parameter_SetMin
            value5 = value4 * value3
            value6 = value5 + Parameter_SetMin
            aninp(7) = CType(value6, Short)
          Case 7
            Parameter_Min = Parameters_MinValue8
            Parameter_Max = Parameters_MaxValue8
            Parameter_SetMin = Parameters_SetMinValue8
            Parameter_SetMax = Parameters_SetMaxValue8

            value1 = Parameter_Max - Parameter_Min
            value2 = (ai(15)) - Parameter_Min
            value3 = value2 / value1
            value4 = Parameter_SetMax - Parameter_SetMin
            value5 = value4 * value3
            value6 = value5 + Parameter_SetMin
            aninp(8) = CType(value6, Short)
          Case 8
            Parameter_Min = Parameters_MinValue9
            Parameter_Max = Parameters_MaxValue9
            Parameter_SetMin = Parameters_SetMinValue9
            Parameter_SetMax = Parameters_SetMaxValue9

            value1 = Parameter_Max - Parameter_Min
            value2 = (ai(16)) - Parameter_Min
            value3 = value2 / value1
            value4 = Parameter_SetMax - Parameter_SetMin
            value5 = value4 * value3
            value6 = value5 + Parameter_SetMin
            aninp(9) = CType(value6, Short)
          Case 9
            Parameter_Min = Parameters_MinValue10
            Parameter_Max = Parameters_MaxValue10
            Parameter_SetMin = Parameters_SetMinValue10
            Parameter_SetMax = Parameters_SetMaxValue10

            value1 = Parameter_Max - Parameter_Min
            value2 = (ai(17)) - Parameter_Min
            value3 = value2 / value1
            value4 = Parameter_SetMax - Parameter_SetMin
            value5 = value4 * value3
            value6 = value5 + Parameter_SetMin
            aninp(10) = CType(value6, Short)
          Case 10
            Parameter_Min = Parameters_MinValue11
            Parameter_Max = Parameters_MaxValue11
            Parameter_SetMin = Parameters_SetMinValue11
            Parameter_SetMax = Parameters_SetMaxValue11

            value1 = Parameter_Max - Parameter_Min
            value2 = (ai(18)) - Parameter_Min
            value3 = value2 / value1
            value4 = Parameter_SetMax - Parameter_SetMin
            value5 = value4 * value3
            value6 = value5 + Parameter_SetMin
            aninp(11) = CType(value6, Short)
        End Select
      Next

      HSCounter1 = ai(11)
      HSCounter2 = ai(12)
    End If
    End Function

    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Sub WriteOutputs(ByVal dout() As Boolean, ByVal anout() As Short)
        Static watchdogDout(128) As Boolean
        Array.Copy(dout, watchdogDout, dout.Length)
        watchdogDout(128) = (Date.UtcNow.Millisecond < 500)  ' alternate the last output to keep the plc happy
        ' M = Internal Relays, W = access as words
        Plc.Write(1, "WM272", watchdogDout, Ports.WriteMode.Optimised)

        ' Rescale: 100.0% = 255
        For i = 1 To 5
            anout(i) = CType((anout(i) * 255) \ 1000, Short)
        Next i
        For i = 6 To 15
            anout(i) = CType(anout(i), Short)
        Next i
        Plc.Write(1, "D400", anout, Ports.WriteMode.Optimised)
    End Sub

    Private ReadOnly ControlCode As ControlCode
    <EditorBrowsable(EditorBrowsableState.Advanced)> Public Plc As Ports.LA60B
    Public Sub New(ByVal controlCode As ControlCode)
        Me.ControlCode = controlCode
    End Sub

    Private Sub CheckForSerialPortParametersChanged()
        If LastComNumber <> ControlCode.Parameters.ComNumber OrElse LastComBaudRate <> ControlCode.Parameters.ComBaudRate Then
            ReOpenSerialPort()
        End If
    End Sub
    Private LastComNumber, LastComBaudRate As Integer
    Private Sub ReOpenSerialPort()
        If Plc IsNot Nothing Then DirectCast(Plc, IDisposable).Dispose() : Plc = Nothing
        LastComNumber = ControlCode.Parameters.ComNumber
        LastComBaudRate = ControlCode.Parameters.ComBaudRate
        Plc = New Ports.LA60B(New Ports.SerialPort("COM" & LastComNumber.ToString, LastComBaudRate, _
                                                   System.IO.Ports.Parity.Even, 7, System.IO.Ports.StopBits.One))
    End Sub

    Private PlcTimeout As New Timer ' if no communications for 1 second, then make the fault below true
    Friend ReadOnly Property PlcFault() As Boolean
        Get
            Return PlcTimeout.Finished
        End Get
    End Property
End Class

Partial Public Class ControlCode
  Public ReadOnly IO As New IO(Me)
End Class
