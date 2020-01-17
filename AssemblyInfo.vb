Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

'Assembly info / properties
<Assembly: ComVisibleAttribute(False)> 

'The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: GuidAttribute("c7a7269f-e825-4aa2-aa88-1460d9e4435f")>

<Assembly: AssemblyTitleAttribute("TG838 V2")>
<Assembly: AssemblyDescriptionAttribute("")> 
<Assembly: AssemblyCompanyAttribute("Adaptive Control")> 
<Assembly: AssemblyProductAttribute("TG838 V2")> 
<Assembly: AssemblyCopyrightAttribute("Copyright ©  2008")> 
<Assembly: AssemblyTrademarkAttribute("")>

<Assembly: AssemblyVersionAttribute("2.53.*")>
<Assembly: AssemblyFileVersionAttribute("2.53")>

'Version 2.53 2013-04-25 GeorgeLin
'新增small mimic，可以繪製溫度曲線圖

'Version 1.93 2013-04-25 GeorgeLin
'修改取樣通知的動作如下
'警報及找布頭輸出點動作 
'1.按第一次確認，停警報 
'2.按第二次確認，停布頭輸出點，等待2秒啟動PUMP 
'3.按第三次確認，結束後跳下一步驟
'備藥的攪拌時間改成秒
'類比進水的輸入改成五位數


'Version 1.92 2013-04-25 GeorgeLin
'修正備藥加藥時沒有全程持溫


'Version 1.91 2013-04-19 GeorgeLin
'修改溫度控制只有在進水排水時才Cancel

'Version 1.5 2012-04-12 GeorgeLin
'修正F11, F12, F13降溫水洗功能，F32動力排水功能

'Version 1.1 2011-07-30 GeorgeLin
'修改IO，DO32改成計量加藥
'ITMA展用

'Version 1.0 2011-05-30 GeorgeLin
'東庚標準838程式第一版

