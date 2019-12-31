Partial Class ControlCode
    <Translate("zh-TW", "準備入布")> Public MessageLoadFabric As Boolean
    <Translate("zh-TW", "準備出布")> Public MessageUnloadFiber As Boolean
    <Translate("zh-TW", "呼叫操作員")> Public MessageCallOperator As Boolean
    <Translate("zh-TW", "取樣對色")> Public MessageTakeSample As Boolean
    <Translate("zh-TW", "藥缸備水中")> Public MessageSTankFilling As Boolean
    <Translate("zh-TW", "藥缸請備藥")> Public MessageSTankPrepare As Boolean
    <Translate("zh-TW", "藥缸攪拌中")> Public MessageSTankMixing As Boolean
    <Translate("zh-TW", "主缸昇溫中")> Public MessageHeatingNow As Boolean
    <Translate("zh-TW", "主缸持溫中")> Public MessageHoldingNow As Boolean
    <Translate("zh-TW", "主缸降溫中")> Public MessageCoolingNow As Boolean
    <Translate("zh-TW", "藥缸稀釋加藥中")> Public MessageSTankDiluteAddingNow As Boolean
    <Translate("zh-TW", "藥缸加藥中")> Public MessageSTankAddingNow As Boolean
    <Translate("zh-TW", "藥缸dos加藥中")> Public MessageSTankDosingNow As Boolean
    <Translate("zh-TW", "系統暫停")> Public MessageSystemPause As Boolean
    <Translate("zh-TW", "程式結束")> Public MessageProgramFinish As Boolean
End Class

Public NotInheritable Class Alarms
  Inherits MarshalByRefObject
    '---------------------------------------------------------------------------------------------
    <Translate("zh-TW", "溫度過高無法加酸")> Public HighTempNoAddHac As Boolean
    <Translate("zh-TW", "停止加酸，加酸總量超過目標量")> Public MessageAddHacError As Boolean
    '---------------------------------------------------------------------------------------------
  <Translate("zh-TW", "系統手動操作中")> Public ManualOperation As Boolean
  <Translate("zh-TW", "主排風故障")> Public MainElectricFanError As Boolean
  <Translate("zh-TW", "溫度過高無法進水")> Public HighTempNoFill As Boolean
  <Translate("zh-TW", "溫度過高無法加藥")> Public HighTempNoAdd As Boolean
  <Translate("zh-TW", "溫度過高無法排水")> Public HighTempNoDrain As Boolean
  <Translate("zh-TW", "纏車")> Public FabricStop As Boolean
  <Translate("zh-TW", "等待備藥OK")> Public STankNotReady As Boolean
  <Translate("zh-TW", "布輪加藥馬達過載")> Public AddMotorOverload As Boolean
  <Translate("zh-TW", "主馬達異常")> Public MainPumpError As Boolean
  <Translate("zh-TW", "主馬達過載")> Public MainPumpOverload As Boolean
  <Translate("zh-TW", "終端顯示器異常")> Public TerminalError As Boolean
  <Translate("zh-TW", "Plc異常")> Public PlcError As Boolean
  <Translate("zh-TW", "蒸氣不足")> Public InsufficientSteam As Boolean
  <Translate("zh-TW", "Pt1斷路")> Public Pt1Open As Boolean
  <Translate("zh-TW", "Pt1短路")> Public Pt1Short As Boolean
  <Translate("zh-TW", "A缸溫度異常")> Public ATempError As Boolean
  <Translate("zh-TW", "冷卻水不足")> Public CoolingNotEnough As Boolean
  <Translate("zh-TW", "進水完成")> Public FillFinish As Boolean
  <Translate("zh-TW", "排水完成")> Public DrainFinish As Boolean
  <Translate("zh-TW", "A缸捕水異常")> Public ATankFillError As Boolean
    <Translate("zh-TW", "主缸進水異常")> Public MainTankFillError As Boolean
    <Translate("zh-TW", "低水位警示")> Public LowLevelAlarm As Boolean
    <Translate("zh-TW", "呼叫粉體助劑，請手動操作LA-302F")> Public CallForManualOperateLA302F As Boolean
End Class

Partial Public Class ControlCode
  Public ReadOnly Alarms As New Alarms
End Class
