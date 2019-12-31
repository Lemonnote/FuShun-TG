Public NotInheritable Class Parameters
  Inherits MarshalByRefObject

  '主缸參數
    <Translate("zh-TW", "MT:主缸排水延遲時間秒"), Category("Main Tank"), _
     TranslateCategory("zh-TW", "主缸參數"), _
     Description("When system runs main tank drain function, it will start counting delay time as main tank low level is off. The system will not stop drain until delay time up."), _
     TranslateDescription("zh-TW", "當主缸執行排水功能，若主缸低水位訊號Low，則開始計算延遲時間，延遲時間到達即結束排水")> Public DrainDelay As Integer = 30

  <Translate("zh-TW", "MT:主缸排水安全時間分"), Category("Main Tank"), _
   TranslateCategory("zh-TW", "主缸參數"), _
   Description("When system runs main tank drain function, if it runs over set time, the system will cancel function."), _
   TranslateDescription("zh-TW", "當主缸執行排水功能，若超過設定時間則強制結束")> Public SetDrainSafetyTime As Integer = 5

  <Translate("zh-TW", "MT:主缸熱交換器排水時間秒"), Category("Main Tank"), _
   TranslateCategory("zh-TW", "主缸參數"), _
  Description("主缸升溫時會先開啟熱交換器排水閥，待設定時間到達則將熱交換器排水閥關閉，開啟排冷凝水閥")> Public CondensationDelayTime As Integer = 60

  <Translate("zh-TW", "MT:主缸排壓安全溫度度"), Category("Main Tank"), _
   TranslateCategory("zh-TW", "主缸參數"), _
  Description("降溫時，當主缸溫度低於設定值，則開始進行點放排壓動作")> Public SetPressureOutTemp As Integer = 85

  <Translate("zh-TW", "MT:主缸排壓點放次數"), Category("Main Tank"), _
 TranslateCategory("zh-TW", "主缸參數"), _
Description("執行排壓時，點放次數")> Public SetPressureOutTimes As Integer = 15

  <Translate("zh-TW", "MT:升溫時，不開排壓閥"), Category("Main Tank"), _
TranslateCategory("zh-TW", "主缸參數"), _
Description("執行升溫時，不開排壓")> Public HeatNowNotPressure As Integer = 0

  <Translate("zh-TW", "MT:主缸安全溫度度"), Category("Main Tank"), _
   TranslateCategory("zh-TW", "主缸參數"), _
  Description("當主缸溫度高於設定值，則不允許進水、排水、溢流動作")> Public SetSafetyTemp As Integer = 85

  <Translate("zh-TW", "MT:主缸加藥安全溫度度"), Category("Main Tank"), _
   TranslateCategory("zh-TW", "主缸參數"), _
  Description("當主缸溫度高於設定值，則不允許加藥動作")> Public SetAddSafetyTemp As Integer = 85

  <Translate("zh-TW", "MT:帶布輪啟動延遲時間秒"), Category("Main Tank"), _
   TranslateCategory("zh-TW", "主缸參數"), _
  Description("當主馬達啟動後，即開始計算延遲時間，時間到達時即啟動帶布輪正轉")> Public RollerStartDelayTime As Integer = 10

  <Translate("zh-TW", "MT:昇溫閥種類，0數位，1類比"), Category("Main Tank"), _
   Translate("EN", "MT:Heat Valve Type, 0:OnOff, 1:Analog"), _
   TranslateCategory("zh-TW", "主缸參數")> Public HeatValveType As Integer = 0

  <Translate("zh-TW", "MT:降溫閥種類，0數位，1類比"), Category("Main Tank"), _
   Translate("EN", "MT:Cool Valve Type, 0:OnOff, 1:Analog"), _
   TranslateCategory("zh-TW", "主缸參數")> Public CoolValveType As Integer = 0

    <Translate("zh-TW", "MT:比例式升降溫控制，0一起，1分開"), Category("Main Tank"), _
 Translate("EN", "MT:Temperature Control AO Type, 0:Same, 1:Separate"), _
 TranslateCategory("zh-TW", "主缸參數")> Public HeatCoolControlType As Integer = 0

  <Translate("zh-TW", "MT:熱水降溫切換溫度"), Category("Main Tank"), _
Translate("EN", "MT:Temperature Control AO Type, 0:Same, 1:Separate"), _
TranslateCategory("zh-TW", "主缸參數")> Public CoolWaterForChange As Integer = 140

  <Translate("zh-TW", "MT:動力排水延遲時間秒"), Category("Main Tank"), _
     TranslateCategory("zh-TW", "主缸參數")> Public PowerDrainDelay As Integer = 5


  <Translate("zh-TW", "MT:最大循環時間"), Category("Main Tank"), _
TranslateCategory("zh-TW", "主缸參數")> Public MaximumFabricCycleTime As Integer = 999

    '   <Translate("zh-TW", "MT:流量計總類"), Category("Main Tank"), _
    'TranslateCategory("zh-TW", "主缸參數")> Public VolumeKind As Integer

  <Translate("zh-TW", "MT:Liter Per Counter"), Category("Main Tank"), _
   TranslateCategory("zh-TW", "主缸參數")> Public VolumePerCount As Integer = 10

  <Translate("zh-TW", "MT:主缸最小入水量"), Category("Main Tank"), _
   TranslateCategory("zh-TW", "主缸參數")> Public MainTankFillMinLiter As Integer

    <Translate("zh-TW", "MT:主缸最大入水量"), Category("Main Tank"), _
     TranslateCategory("zh-TW", "主缸參數")> Public MainTankFillMaxLiter As Integer

    <Translate("zh-TW", "MT:溢流水洗補水時間"), Category("Main Tank"), _
     TranslateCategory("zh-TW", "主缸參數")> Public OverflowDelayFillTime As Integer

  <Translate("zh-TW", "MT:類比水位尺清洗時間"), Category("Main Tank"), _
 TranslateCategory("zh-TW", "主缸參數")> Public LevelWash As Integer = 2
    

    <Translate("zh-TW", "MT:取樣警報停止時間"), Category("Main Tank"), _
TranslateCategory("zh-TW", "主缸參數")> Public SetSampleAlarmStopTime As Integer


  <Translate("zh", "MT:溫度補償值"), Category("Main Tank"), Translate("EN", "MT:Temp + or -"), TranslateCategory("zh", "主缸參數"), _
   TranslateDescription("zh-TW", "0-100補溫度,101-199減溫度")> Public TempAdd As Integer
    '藥缸參數

    <Translate("zh-TW", "ST:加藥馬達係數"), Category("Side Tank"), _
 TranslateCategory("zh-TW", "藥缸參數")> Public DosingPumpSpeed As Integer = 10

  <Translate("zh-TW", "ST:B缸呼叫確認0是1否"), Category("Side Tank"), _
TranslateCategory("zh-TW", "藥缸參數"), _
Description("After B tank dispense finish, if set 1 then system will go to next step without press confirm button."), _
TranslateDescription("zh-TW", "當B缸計量完成，如果參數設定1，則系統不需等待確認訊號即進行下一步驟")> Public BTankCallAck As Integer

  <Translate("zh-TW", "ST:加藥缸0單缸單馬達1雙缸單馬達2雙缸雙馬達"), Category("Side Tank"), _
   TranslateCategory("zh-TW", "藥缸參數"), _
  Description("設定雙藥缸加藥馬達數量，設定0是單缸單馬達，設定1雙缸單馬達，設定2雙缸雙馬達")> Public BC缸數量跟馬達 As Integer = 0

  <Translate("zh-TW", "ST:Dosing種類0比例1氣動"), Category("Side Tank"), _
   TranslateCategory("zh-TW", "藥缸參數")> Public Dosing種類 As Integer = 0

  <Translate("zh-TW", "ST:B藥缸清洗次數"), Category("Side Tank"), _
TranslateCategory("zh-TW", "藥缸參數"), _
Description("設定藥缸清洗次數")> Public B藥缸清洗次數 As Integer = 1

  <Translate("zh-TW", "ST:C藥缸清洗次數"), Category("Side Tank"), _
TranslateCategory("zh-TW", "藥缸參數"), _
Description("設定藥缸清洗次數")> Public C藥缸清洗次數 As Integer = 1


  <Translate("zh-TW", "ST:加藥完是否排水"), Category("Side Tank"), _
TranslateCategory("zh-TW", "藥缸參數"), _
Description("0 =排水 1=不排水")> Public 加藥完是否排水 As Integer = 1

  <Translate("zh-TW", "ST:C藥缸水位種類"), Category("Side Tank"), _
TranslateCategory("zh-TW", "藥缸參數"), _
Description("設定C藥缸種類 0 =類比 1=數位")> Public C藥缸水位種類 As Integer = 1

  <Translate("zh-TW", "ST:藥缸加藥延遲時間"), Category("Side Tank"), _
  TranslateCategory("zh-TW", "藥缸參數"), _
 Description("設定洗缸前加藥延遲時間")> Public AddTransferTimeBeforeRinse As Integer = 10

  <Translate("zh-TW", "ST:藥缸循環前洗缸時間"), Category("Side Tank"), _
   TranslateCategory("zh-TW", "藥缸參數"), _
  Description("設定循環前，洗缸進水的時間")> Public MixCirculateRinseTime As Integer = 5

    <Translate("zh-TW", "ST:藥缸洗缸循環時間"), Category("Side Tank"), _
     TranslateCategory("zh-TW", "藥缸參數"), _
    Description("設定洗缸完後，循環攪拌的時間")> Public AddCirculateTimeAfterRinse As Integer = 5

    <Translate("zh-TW", "ST:藥缸加熱溫度"), Category("Side Tank"), _
     TranslateCategory("zh-TW", "藥缸參數"), _
    Description("設定藥缸加熱溫度")> Public SetBTankTemp As Integer = 85

  <Translate("zh-TW", "ST:藥缸洗缸加藥時間"), Category("Side Tank"), _
   TranslateCategory("zh-TW", "藥缸參數"), _
  Description("設定洗缸完後，加藥的延遲時間")> Public AddTransferTimeAfterRinse As Integer = 5

  <Translate("zh-TW", "ST:藥缸加藥洗缸時間"), Category("Side Tank"), _
   TranslateCategory("zh-TW", "藥缸參數"), _
  Description("設定加藥完後，洗缸進水的時間")> Public AddTransferRinseTime As Integer = 5

  <Translate("zh-TW", "ST:藥缸排水延遲時間"), Category("Side Tank"), _
   TranslateCategory("zh-TW", "藥缸參數"), _
  Description("設定洗缸後排水延遲時間")> Public AddTransferDrainTime As Integer = 20

  <Translate("zh-TW", "ST:B缸攪拌類型1馬達0循環"), Category("Side Tank"), _
   TranslateCategory("zh-TW", "藥缸參數"), _
  Description("設定B藥缸攪拌類型，1是使用馬達攪拌，0是循環攪拌")> Public BTankMixType As Integer

  <Translate("zh-TW", "ST:C缸攪拌類型1馬達0循環"), Category("Side Tank"), _
   TranslateCategory("zh-TW", "藥缸參數"), _
  Description("設定C藥缸攪拌類型，1是使用馬達攪拌，0是循環攪拌")> Public CTankMixType As Integer






    <Translate("zh-TW", "ST:藥缸備藥確認1否0是"), Category("Side Tank"), _
   TranslateCategory("zh-TW", "藥缸參數"), _
      Description(""), _
  TranslateDescription("zh-TW", "設定藥缸備藥時是否要按備藥確認鈕，設定值是1時則不需按鈕確認")> Public SideTankPrepareConfirm As Integer




    <Translate("zh-TW", "連結LASPC數據庫重新連結"), Category("DispenseOverTime"), _
     TranslateCategory("zh-TW", "LASPC連結"), _
     Description("如果沒有連線,請按1會重新連線.如果有連線後會自動歸0")> Public ConnectSPCTest As Integer

    <Translate("zh-TW", "連結LASPC數據庫設置"), Category("DispenseOverTime"), _
     TranslateCategory("zh-TW", "LASPC連結"), _
     Description("如果有使用SPC請按1")> Public ConnectSPCEnable As Integer

  <Translate("zh-TW", "MT:是否已連接2組EAIO"), Category("Main Tank"), _
TranslateCategory("zh-TW", "主缸參數"), _
Description("是=1 否=0")> Public Connect2EAIO As Integer

 
    'PH控制動作******************************************************************************************
    Public PhShowData As Integer
    <Translate("zh-TW", "PH:是否已連接PH系統"), Category("PH Setup"), _
    TranslateCategory("zh-TW", "PH參數"), _
    Description("是=1 否=0")> Public ConnectPhSystem As Integer

    <Translate("zh-TW", "PH:是否有回流桶"), Category("PH Setup"), _
     TranslateCategory("zh-TW", "PH參數"), _
     Description("是=1 否=0")> Public PhCirTank As Integer = 1

    <Translate("zh-TW", "PH:HAC酸度%"), Category("PH Setup"), _
     TranslateCategory("zh-TW", "PH參數"), _
    Description("酸的濃度，以%計算")> Public PhConcentration As Integer = 100

    <Translate("zh-TW", "PH:調整時檢測時間(秒)"), Category("PH Setup"), _
    TranslateCategory("zh-TW", "PH參數"), _
    Description("每段時間檢測PH值")> Public PhAdjustCheckTime As Integer = 20

    <Translate("zh-TW", "PH:清洗時間(秒)"), Category("PH Setup"), _
    TranslateCategory("zh-TW", "PH參數"), _
    Description("PH清洗管路時間")> Public PhWashTime As Integer = 20

    <Translate("zh-TW", "PH:迴流桶排水延遲時間(秒)"), Category("PH Setup"), _
    TranslateCategory("zh-TW", "PH參數"), _
    Description("迴流桶排水的延遲時間")> Public CirDrainDelayTime As Integer = 20

    <Translate("zh-TW", "PH:迴流桶迴水延遲關閉時間(秒)"), Category("PH Setup"), _
     TranslateCategory("zh-TW", "PH參數"), _
     Description("迴流桶迴水至低水位時，入迴水延遲關閉時間")> Public CirFillDelayTime As Integer = 10

    <Translate("zh-TW", "PH:啟動冷卻系統溫度(度)"), Category("PH Setup"), _
    TranslateCategory("zh-TW", "PH參數"), _
    Description("PH啟動冷卻系統溫度(度)")> Public PhCoolingTemp As Integer = 40


    <Translate("zh-TW", "PH:加酸安全溫度度"), Category("PH Setup"), _
    TranslateCategory("zh-TW", "PH參數"), _
    Description("當PH溫度高於設定值，則不允許加酸動作 60C~110C")> Public PH加酸安全溫度 As Integer = 85

    <Translate("zh-TW", "PH:取樣時間(秒)"), Category("PH Setup"), _
    TranslateCategory("zh-TW", "PH參數"), _
    Description("PH取樣時間(最少60秒)")> Public PhSamplingTime As Integer = 60

    <Translate("zh-TW", "PH:取樣後，延遲等待穩定值(0-60秒)"), Category("PH Setup"), _
    TranslateCategory("zh-TW", "PH參數"), _
    Description("每PH取樣時間後，將等待設定秒確認PH值，管路越遠時間要越長")> Public DoublePhSample As Integer = 30

    <Translate("zh-TW", "PH:偏差時警報(1:是,0:不)"), Category("PH Setup"), _
    TranslateCategory("zh-TW", "PH參數"), _
    Description("PH偏差時警報(1:是,0:不)")> Public PhErrorAlarm As Integer = 0

    <Translate("zh-TW", "PH:加酸動作異常次數"), Category("PH Setup"), _
    TranslateCategory("zh-TW", "PH參數"), _
    Description("加酸時超過多少次為異常")> Public PhAddError As Integer = 50

    <Translate("zh-TW", "PH:到達範圍"), Category("PH Setup"), _
    TranslateCategory("zh-TW", "PH參數"), _
    Description("PH到達範圍,1= 0.01PH,10=0.1PH")> Public PhApproach As Integer = 2

    <Translate("zh-TW", "PH:泵加酸比(60-600)"), Category("PH Setup"), _
    TranslateCategory("zh-TW", "PH參數"), _
    Description("pH泵加酸比,1代表1分鐘=1C.C")> Public PhPumpOutRatio As Integer = 300

    <Translate("zh-TW", "PH:主缸進水量(L)"), Category("PH Setup"), _
    TranslateCategory("zh-TW", "PH參數"), _
    Description("主缸進水量(L)")> Public MainTankFillLevel As Integer = 2000

    <Translate("zh-TW", "PH:起始檢查PH時間"), Category("PH Setup"), _
    TranslateCategory("zh-TW", "PH參數"), _
    Description("一開始時，檢查PH值")> Public StartCheckPh As Integer = 10

    <Translate("zh-TW", "PH:迴水%,開大閥入染機"), Category("PH Setup"), _
    TranslateCategory("zh-TW", "PH參數"), _
    Description("迴水超過多少%，開大閥加藥")> Public CirculateOpenAdd As Integer = 10

    <Translate("zh-TW", "PH:加酸關閉,循環馬達延遲關閉時間"), Category("PH Setup"), _
     TranslateCategory("zh-TW", "PH參數"), _
     Description("加酸閥關閉時,為了讓加酸確實入染機,延遲關閉循環馬達時間")> Public DelayCirculatPump As Integer = 4


    <Translate("zh-TW", "PH:是否啟用持續回流偵測"), Category("PH Setup"), _
     TranslateCategory("zh-TW", "PH參數"), _
     Description("F73代表開始,F79洗管代表結束")> Public PhCirRuning As Integer = 0


    '其他參數 '******************************************************************************************
    <Translate("zh-TW", "通訊埠"), Category("Communication"), _
     TranslateCategory("zh-TW", "通訊設定")> Public ComNumber As Integer = 1 ' default in case no-one changes it
    <Translate("zh-TW", "連線速度"), Category("Communication"), _
     TranslateCategory("zh-TW", "通訊設定")> Public ComBaudRate As Integer = 57600 ' default
    '******************************************************************************************

    '手動加藥 多段參數++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    <Translate("zh-TW", "MD:加藥第一段時間"), Category("Manual Dosing"), _
    TranslateCategory("zh-TW", "手動加藥")> Public ManualDoseTime1 As Integer = 15

    <Translate("zh-TW", "MD:加藥第二段時間"), Category("Manual Dosing"), _
     TranslateCategory("zh-TW", "手動加藥")> Public ManualDoseTime2 As Integer = 20

    <Translate("zh-TW", "MD:加藥第三段時間"), Category("Manual Dosing"), _
     TranslateCategory("zh-TW", "手動加藥")> Public ManualDoseTime3 As Integer = 30

    <Translate("zh-TW", "MD:加藥第四段時間"), Category("Manual Dosing"), _
     TranslateCategory("zh-TW", "手動加藥")> Public ManualDoseTime4 As Integer = 40

    <Translate("zh-TW", "MD:加藥第五段時間"), Category("Manual Dosing"), _
     TranslateCategory("zh-TW", "手動加藥")> Public ManualDoseTime5 As Integer = 50

    <Translate("zh-TW", "MD:手動加藥曲線1到9"), Category("Manual Dosing"), _
     TranslateCategory("zh-TW", "手動加藥")> Public ManualDoseCurve As Integer
    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    '類比進水 參數  隱藏 +++++++++++++++++++++++++=+++++++++++++++++++++++++++++++++++++++  ((((請恢復F90主缸類比水位校正 、 F17類比進水、F18計錄水位、F19記憶入水)))))
    <Translate("zh-TW", "MT:主缸類比水位AI(唯讀)"), Category("Main Tank Calibration"), _
TranslateCategory("zh-TW", "主缸校正參數-讀取值"), _
Description("Set analog input value for main tank level."), _
TranslateDescription("zh-TW", "設定主缸類比水位的讀取值")> Public AI主缸實際值 As Integer

    <Translate("zh-TW", "MT:水位模擬測試"), Category("Main Tank"), _
TranslateCategory("zh-TW", "類比水位校正")> Public 水位模擬測試 As Integer = 0

    <Translate("zh-TW", "MT:主缸類比水位AI值"), Category("Main Tank Calibration"), _
TranslateCategory("zh-TW", "主缸校正參數-讀取值"), _
Description("Set analog input value for main tank level."), _
TranslateDescription("zh-TW", "設定主缸類比水位的讀取值")> Public SetMainTankAnalogInput(50) As Integer

    <Translate("zh-TW", "MT:主缸類比水位設定值"), Category("Main Tank Calibration"), _
     TranslateCategory("zh-TW", "主缸校正參數-設定值"), _
     Description("Set main tank volume by liters"), _
     TranslateDescription("zh-TW", "設定主缸水位值，單位是公升")> Public SetMainTankVolume(50) As Integer



    <Translate("zh-TW", "MT:每水位相差值(L)"), Category("Main Tank"), _
TranslateCategory("zh-TW", "類比水位校正")> Public MainLevelAnalogyRangeLiter As Integer = 200
    '+++++++++++++++++++++++++=+++++++++++++++++++++++++++++++++++++++  ((((請恢復ControlCode.vb)))))
    '    <Translate("zh-TW", "MT:主缸排水水位容量(公升)"), Category("Main Tank"), _
    'TranslateCategory("zh-TW", "主缸參數")> Public SetMainTankDrainVolume As Integer = 4000

    '    <Translate("zh-TW", "MT:主缸高水位容量(公升)"), Category("Main Tank"), _
    '    TranslateCategory("zh-TW", "主缸參數")> Public SetMainTankHighVolume As Integer = 3000

    '    <Translate("zh-TW", "MT:主缸中水位容量(公升)"), Category("Main Tank"), _
    '    TranslateCategory("zh-TW", "主缸參數")> Public SetMainTankMidVolume As Integer = 2000

    '    <Translate("zh-TW", "MT:主缸低水位容量(公升)"), Category("Main Tank"), _
    '    TranslateCategory("zh-TW", "主缸參數")> Public SetMainTankLowVolume As Integer = 1000
    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


    '其他+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    ''''''<Translate("zh-TW", "主馬達保養設定時數"), Category("Maintenance"), _
    '''''' TranslateCategory("zh-TW", "維護保養參數"), _
    '''''' Description("當主馬達運行時數超過設定值，則顯示主馬達需保養的警報")> Public MainPumpMaintainTime As Integer

    ''''''<Translate("zh-TW", "主馬達運轉時數"), Category("Maintenance"), _
    '''''' TranslateCategory("zh-TW", "維護保養參數"), _
    ''''''Description("當主馬達實際運行時數，當此數值超過設定值，則顯示主馬達需保養的警報")> Public MainPumpRunTime As Integer
  '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

  <Translate("zh-TW", "AD:連接助劑自動計量系統"), Category("Auto Dispenser"), _
TranslateCategory("zh-TW", "自動計量系統"), _
  Description(""), _
TranslateDescription("zh-TW", "是否連接助劑自動計量系統，0為否，1是連接到B缸，2是連接到C缸")> Public ChemicalEnable As Integer

  <Translate("zh-TW", "AD:連接染料自動輸送系統"), Category("Auto Dispenser"), _
   TranslateCategory("zh-TW", "自動計量系統"), _
      Description(""), _
  TranslateDescription("zh-TW", "是否連接染料自動輸送系統，0為否，1是連接到B缸，2是連接到C缸")> Public DyeEnable As Integer

  <Translate("zh-TW", "AD:呼叫LA-252加藥"), Category("Auto Dispenser"), _
TranslateCategory("zh-TW", "自動計量系統"), _
Description(""), _
TranslateDescription("zh-TW", "執行等待LA-252時，是否呼叫LA-252加藥")> Public CallFor252AddDye As Integer


  <Translate("zh", "AD:助劑輸送延遲時間"), Category("Auto Dispenser"), _
TranslateCategory("zh", "自動計量系統"), _
Description(""), _
TranslateDescription("zh", "設定助劑輸送系統的延遲時間，當藥缸低水位到達時開始計算延遲時間，時間到即判定輸送完成")> Public ChemicalTransferDelayTime As Integer

  <Translate("zh", "AD:染料輸送延遲時間"), Category("Auto Dispenser"), _
TranslateCategory("zh", "自動計量系統"), _
Description(""), _
TranslateDescription("zh", "設定染料輸送系統的延遲時間，當藥缸低水位到達時開始計算延遲時間，時間到即判定輸送完成")> Public DyeTransferDelayTime As Integer

  <Translate("zh", "AD:染料輸送前檢查桶內有無水"), Category("Auto Dispenser"), _
TranslateCategory("zh", "自動計量系統"), _
Description(""), _
TranslateDescription("zh", "染料不檢查低水位=0,檢查為=1")> Public DyeCheckTank As Integer

  <Translate("zh", "AD:助劑輸送前檢查桶內有無水"), Category("Auto Dispenser"), _
TranslateCategory("zh", "自動計量系統"), _
Description(""), _
TranslateDescription("zh", "助劑不檢查低水位=0,檢查為=1")> Public ChemicalCheckTank As Integer

  <Translate("zh", "允許LA-838本機載入染程"), Category("System"), _
TranslateCategory("zh", "系統參數"), _
Description("設定是否允許操作員在本機載入染程來運行，0=不允許，1=允許")> Public EnableLoadProgramOnLocal As Integer = 1
End Class


Partial Public Class ControlCode
  Public ReadOnly Parameters As New Parameters
End Class

