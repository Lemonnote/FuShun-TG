<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Mimic
    Inherits System.Windows.Forms.UserControl

    'UserControl1 overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim Mimic_Timer1 As System.Windows.Forms.Timer
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Mimic))
    Me.Label8 = New System.Windows.Forms.Label()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.Label11 = New System.Windows.Forms.Label()
    Me.Mimic_TempControl = New System.Windows.Forms.TextBox()
    Me.PhShowPic = New System.Windows.Forms.Label()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.SetPointShow = New System.Windows.Forms.Label()
    Me.Label16 = New System.Windows.Forms.Label()
    Me.Label15 = New System.Windows.Forms.Label()
    Me.MainTempShow = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.PH_DATA = New System.Windows.Forms.GroupBox()
    Me.補酸狀態分析 = New System.Windows.Forms.Label()
    Me.test33 = New System.Windows.Forms.Label()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label20 = New System.Windows.Forms.Label()
    Me.Label19 = New System.Windows.Forms.Label()
    Me.Label18 = New System.Windows.Forms.Label()
    Me.Label17 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.test10 = New System.Windows.Forms.Label()
    Me.test23 = New System.Windows.Forms.Label()
    Me.test22 = New System.Windows.Forms.Label()
    Me.test4 = New System.Windows.Forms.Label()
    Me.test21 = New System.Windows.Forms.Label()
    Me.test3 = New System.Windows.Forms.Label()
    Me.test32 = New System.Windows.Forms.Label()
    Me.test31 = New System.Windows.Forms.Label()
    Me.test6 = New System.Windows.Forms.Label()
    Me.test35 = New System.Windows.Forms.Label()
    Me.test2 = New System.Windows.Forms.Label()
    Me.test1 = New System.Windows.Forms.Label()
    Me.test8 = New System.Windows.Forms.Label()
    Me.test19 = New System.Windows.Forms.Label()
    Me.test13 = New System.Windows.Forms.Label()
    Me.test12 = New System.Windows.Forms.Label()
    Me.test11 = New System.Windows.Forms.Label()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.Label13 = New System.Windows.Forms.Label()
    Me.Label21 = New System.Windows.Forms.Label()
    Me.Label22 = New System.Windows.Forms.Label()
    Me.Label23 = New System.Windows.Forms.Label()
    Me.Label24 = New System.Windows.Forms.Label()
    Me.test16 = New System.Windows.Forms.Label()
    Me.test24 = New System.Windows.Forms.Label()
    Me.PhShowData = New System.Windows.Forms.Label()
    Me.Button1 = New System.Windows.Forms.Button()
    Me.加藥馬達速度 = New System.Windows.Forms.Label()
    Me.Mimic_Blevel = New System.Windows.Forms.Label()
    Me.Mimic_Bsetep = New System.Windows.Forms.Label()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Parent_Job = New TG838.MimicControls.Label()
        Me.Label_Dyelot = New TG838.MimicControls.Label()
        Me.Label27 = New TG838.MimicControls.Label()
        Me.Parent_PrefixedSteps = New TG838.MimicControls.Label()
        Me.Label25 = New TG838.MimicControls.Label()
        Me.Parent_IsProgramRunning = New TG838.MimicControls.Label()
        Me.IO_PhAddPump = New TG838.MimicControls.Lamp()
        Me.IO_PhCirculatePump = New TG838.MimicControls.Lamp()
        Me.IO_MainPumpFB = New TG838.MimicControls.Lamp()
        Me.IO_CTankPump = New TG838.MimicControls.Lamp()
        Me.IO_BTankAddPump = New TG838.MimicControls.Lamp()
        Me.IO_HighLevel = New TG838.MimicControls.Lamp()
        Me.IO_MiddleLevel = New TG838.MimicControls.Lamp()
        Me.IO_DrainLevel = New TG838.MimicControls.Lamp()
        Me.IO_LowLevel = New TG838.MimicControls.Lamp()
        Me.IO_PhMixTankLowLevel = New TG838.MimicControls.Lamp()
        Me.IO_PhMixTankHighLevel = New TG838.MimicControls.Lamp()
        Me.IO_PhAddHacOut = New TG838.MimicControls.Valve()
        Me.IO_PhCool = New TG838.MimicControls.Valve()
        Me.IO_PhWashFill = New TG838.MimicControls.Valve()
        Me.IO_CondenserDrain = New TG838.MimicControls.Valve()
        Me.IO_HxDrain = New TG838.MimicControls.Valve()
        Me.IO_Dosing = New TG838.MimicControls.Valve()
        Me.IO_Addition = New TG838.MimicControls.Valve()
        Me.IO_CoolDrain = New TG838.MimicControls.Valve()
        Me.IO_ColdFill = New TG838.MimicControls.Valve()
        Me.IO_HotFill = New TG838.MimicControls.Valve()
        Me.IO_BTankColdFill = New TG838.MimicControls.Valve()
        Me.IO_PhFillCirculate = New TG838.MimicControls.Valve()
        Me.IO_PhDrain = New TG838.MimicControls.Valve()
        Me.IO_PhInToMachine = New TG838.MimicControls.Valve()
        Me.IO_HotDrain = New TG838.MimicControls.Valve()
        Me.IO_Overflow = New TG838.MimicControls.Valve()
        Me.IO_LevelClean = New TG838.MimicControls.Valve()
        Me.IO_Drain = New TG838.MimicControls.Valve()
        Me.IO_CTankAddition = New TG838.MimicControls.Valve()
        Me.IO_CTankDrain = New TG838.MimicControls.Valve()
        Me.IO_BTankAddition = New TG838.MimicControls.Valve()
        Me.IO_BTankDrain = New TG838.MimicControls.Valve()
        Me.IO_BTankCirculate2 = New TG838.MimicControls.Valve()
        Me.IO_CTankColdFill = New TG838.MimicControls.Valve()
        Me.IO_CTankCirculate2 = New TG838.MimicControls.Valve()
        Me.IO_CoolForHotWater = New TG838.MimicControls.Valve()
        Me.IO_Cool = New TG838.MimicControls.Valve()
        Me.IO_Heat = New TG838.MimicControls.Valve()
        Me.IO_CTankMixCir2 = New TG838.MimicControls.Valve()
        Me.IO_BTankMixCir2 = New TG838.MimicControls.Valve()
        Me.IO_PressureOut = New TG838.MimicControls.Valve()
        Me.IO_PressureIn = New TG838.MimicControls.Valve()
        Me.IO_Entanglement1 = New TG838.MimicControls.Lamp()
        Me.IO_ErrorLamp = New TG838.MimicControls.Lamp()
        Me.IO_CallLamp = New TG838.MimicControls.Lamp()
        Me.IO_SystemAuto = New TG838.MimicControls.Lamp()
        Me.Mimic_MainLevel = New TG838.MimicControls.LevelBar()
        Me.Mimic_BTankLevel = New TG838.MimicControls.LevelBar()
        Me.Mimic_CTankLevel = New TG838.MimicControls.LevelBar()
        Me.Label26 = New TG838.MimicControls.Label()
        Mimic_Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.PH_DATA.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Mimic_Timer1
        '
        Mimic_Timer1.Enabled = True
        Mimic_Timer1.Interval = 1000
        AddHandler Mimic_Timer1.Tick, AddressOf Me.Mimic_Timer1_Tick
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Window
        Me.Label8.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label8.Location = New System.Drawing.Point(17, 11)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(42, 21)
        Me.Label8.TabIndex = 47
        Me.Label8.Text = "自動"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Window
        Me.Label9.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label9.Location = New System.Drawing.Point(17, 38)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(42, 21)
        Me.Label9.TabIndex = 49
        Me.Label9.Text = "呼叫"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Window
        Me.Label10.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label10.Location = New System.Drawing.Point(17, 62)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(42, 21)
        Me.Label10.TabIndex = 51
        Me.Label10.Text = "警報"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Window
        Me.Label11.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label11.Location = New System.Drawing.Point(17, 88)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(42, 21)
        Me.Label11.TabIndex = 53
        Me.Label11.Text = "纏車"
        '
        'Mimic_TempControl
        '
        Me.Mimic_TempControl.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Mimic_TempControl.ForeColor = System.Drawing.Color.Navy
        Me.Mimic_TempControl.Location = New System.Drawing.Point(503, 7)
        Me.Mimic_TempControl.Multiline = True
        Me.Mimic_TempControl.Name = "Mimic_TempControl"
        Me.Mimic_TempControl.Size = New System.Drawing.Size(42, 23)
        Me.Mimic_TempControl.TabIndex = 75
        '
        'PhShowPic
        '
        Me.PhShowPic.AutoSize = True
        Me.PhShowPic.Location = New System.Drawing.Point(15, 407)
        Me.PhShowPic.Name = "PhShowPic"
        Me.PhShowPic.Size = New System.Drawing.Size(29, 12)
        Me.PhShowPic.TabIndex = 80
        Me.PhShowPic.Text = "0000"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.SetPointShow)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.MainTempShow)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(568, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(210, 89)
        Me.GroupBox1.TabIndex = 81
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Temperature"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label12.Font = New System.Drawing.Font("微軟正黑體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(110, 24)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(64, 17)
        Me.Label12.TabIndex = 62
        Me.Label12.Text = "SetTemp."
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'SetPointShow
        '
        Me.SetPointShow.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.SetPointShow.Font = New System.Drawing.Font("微軟正黑體", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.SetPointShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.SetPointShow.Location = New System.Drawing.Point(100, 44)
        Me.SetPointShow.Name = "SetPointShow"
        Me.SetPointShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SetPointShow.Size = New System.Drawing.Size(84, 30)
        Me.SetPointShow.TabIndex = 62
        Me.SetPointShow.Text = "130.9"
        Me.SetPointShow.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label16.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(183, 51)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(21, 21)
        Me.Label16.TabIndex = 62
        Me.Label16.Text = "C"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label15.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label15.Location = New System.Drawing.Point(79, 51)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(21, 21)
        Me.Label15.TabIndex = 62
        Me.Label15.Text = "C"
        '
        'MainTempShow
        '
        Me.MainTempShow.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.MainTempShow.Font = New System.Drawing.Font("微軟正黑體", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.MainTempShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.MainTempShow.Location = New System.Drawing.Point(3, 43)
        Me.MainTempShow.Name = "MainTempShow"
        Me.MainTempShow.Size = New System.Drawing.Size(81, 30)
        Me.MainTempShow.TabIndex = 62
        Me.MainTempShow.Text = "130.5"
        Me.MainTempShow.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label1.Font = New System.Drawing.Font("微軟正黑體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(10, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 17)
        Me.Label1.TabIndex = 62
        Me.Label1.Text = "MainTemp."
        '
        'PH_DATA
        '
        Me.PH_DATA.BackColor = System.Drawing.Color.Transparent
        Me.PH_DATA.Controls.Add(Me.補酸狀態分析)
        Me.PH_DATA.Controls.Add(Me.test33)
        Me.PH_DATA.Controls.Add(Me.Label6)
        Me.PH_DATA.Controls.Add(Me.Label4)
        Me.PH_DATA.Controls.Add(Me.Label20)
        Me.PH_DATA.Controls.Add(Me.Label19)
        Me.PH_DATA.Controls.Add(Me.Label18)
        Me.PH_DATA.Controls.Add(Me.Label17)
        Me.PH_DATA.Controls.Add(Me.Label2)
        Me.PH_DATA.Controls.Add(Me.Label7)
        Me.PH_DATA.Controls.Add(Me.Label5)
        Me.PH_DATA.Controls.Add(Me.Label3)
        Me.PH_DATA.Controls.Add(Me.test10)
        Me.PH_DATA.Controls.Add(Me.test23)
        Me.PH_DATA.Controls.Add(Me.test22)
        Me.PH_DATA.Controls.Add(Me.test4)
        Me.PH_DATA.Controls.Add(Me.test21)
        Me.PH_DATA.Controls.Add(Me.test3)
        Me.PH_DATA.Controls.Add(Me.test32)
        Me.PH_DATA.Controls.Add(Me.test31)
        Me.PH_DATA.Controls.Add(Me.test6)
        Me.PH_DATA.Controls.Add(Me.test35)
        Me.PH_DATA.Controls.Add(Me.test2)
        Me.PH_DATA.Controls.Add(Me.test1)
        Me.PH_DATA.Controls.Add(Me.test8)
        Me.PH_DATA.Controls.Add(Me.test19)
        Me.PH_DATA.Controls.Add(Me.test13)
        Me.PH_DATA.Controls.Add(Me.test12)
        Me.PH_DATA.Controls.Add(Me.test11)
        Me.PH_DATA.Controls.Add(Me.Label14)
        Me.PH_DATA.Controls.Add(Me.Label13)
        Me.PH_DATA.Controls.Add(Me.Label21)
        Me.PH_DATA.Controls.Add(Me.Label22)
        Me.PH_DATA.Controls.Add(Me.Label23)
        Me.PH_DATA.Controls.Add(Me.Label24)
        Me.PH_DATA.Controls.Add(Me.test16)
        Me.PH_DATA.Controls.Add(Me.test24)
        Me.PH_DATA.Location = New System.Drawing.Point(665, 98)
        Me.PH_DATA.Name = "PH_DATA"
        Me.PH_DATA.Size = New System.Drawing.Size(122, 385)
        Me.PH_DATA.TabIndex = 85
        Me.PH_DATA.TabStop = False
        Me.PH_DATA.Text = "PH"
        '
        '補酸狀態分析
        '
        Me.補酸狀態分析.AutoSize = True
        Me.補酸狀態分析.Location = New System.Drawing.Point(88, 214)
        Me.補酸狀態分析.Name = "補酸狀態分析"
        Me.補酸狀態分析.Size = New System.Drawing.Size(11, 12)
        Me.補酸狀態分析.TabIndex = 86
        Me.補酸狀態分析.Text = "+"
        '
        'test33
        '
        Me.test33.AutoSize = True
        Me.test33.Location = New System.Drawing.Point(66, 214)
        Me.test33.Name = "test33"
        Me.test33.Size = New System.Drawing.Size(11, 12)
        Me.test33.TabIndex = 86
        Me.test33.Text = "+"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 217)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 12)
        Me.Label6.TabIndex = 86
        Me.Label6.Text = "W1次數"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 238)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 12)
        Me.Label4.TabIndex = 86
        Me.Label4.Text = "W0次數"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(9, 360)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(53, 12)
        Me.Label20.TabIndex = 85
        Me.Label20.Text = "加酸總量"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(9, 340)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(67, 12)
        Me.Label19.TabIndex = 85
        Me.Label19.Text = "PH泵加酸比"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(9, 320)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(65, 12)
        Me.Label18.TabIndex = 85
        Me.Label18.Text = "主缸進水量"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(9, 300)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(62, 12)
        Me.Label17.TabIndex = 85
        Me.Label17.Text = "HAC酸度%"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 280)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 85
        Me.Label2.Text = "總計量時間"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 260)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(55, 12)
        Me.Label7.TabIndex = 85
        Me.Label7.Text = "PH總酸量"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 179)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 12)
        Me.Label5.TabIndex = 85
        Me.Label5.Text = "PH演算值"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 196)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 12)
        Me.Label3.TabIndex = 85
        Me.Label3.Text = "PH到達值"
        '
        'test10
        '
        Me.test10.AutoSize = True
        Me.test10.Location = New System.Drawing.Point(92, 360)
        Me.test10.Name = "test10"
        Me.test10.Size = New System.Drawing.Size(29, 12)
        Me.test10.TabIndex = 85
        Me.test10.Text = "0000"
        '
        'test23
        '
        Me.test23.AutoSize = True
        Me.test23.Location = New System.Drawing.Point(92, 340)
        Me.test23.Name = "test23"
        Me.test23.Size = New System.Drawing.Size(29, 12)
        Me.test23.TabIndex = 85
        Me.test23.Text = "0000"
        '
        'test22
        '
        Me.test22.AutoSize = True
        Me.test22.Location = New System.Drawing.Point(92, 320)
        Me.test22.Name = "test22"
        Me.test22.Size = New System.Drawing.Size(29, 12)
        Me.test22.TabIndex = 85
        Me.test22.Text = "0000"
        '
        'test4
        '
        Me.test4.AutoSize = True
        Me.test4.Location = New System.Drawing.Point(92, 280)
        Me.test4.Name = "test4"
        Me.test4.Size = New System.Drawing.Size(29, 12)
        Me.test4.TabIndex = 85
        Me.test4.Text = "0000"
        '
        'test21
        '
        Me.test21.AutoSize = True
        Me.test21.Location = New System.Drawing.Point(92, 300)
        Me.test21.Name = "test21"
        Me.test21.Size = New System.Drawing.Size(29, 12)
        Me.test21.TabIndex = 85
        Me.test21.Text = "0000"
        '
        'test3
        '
        Me.test3.AutoSize = True
        Me.test3.Location = New System.Drawing.Point(92, 260)
        Me.test3.Name = "test3"
        Me.test3.Size = New System.Drawing.Size(29, 12)
        Me.test3.TabIndex = 85
        Me.test3.Text = "0000"
        '
        'test32
        '
        Me.test32.AutoSize = True
        Me.test32.Location = New System.Drawing.Point(73, 239)
        Me.test32.Name = "test32"
        Me.test32.Size = New System.Drawing.Size(11, 12)
        Me.test32.TabIndex = 85
        Me.test32.Text = "0"
        '
        'test31
        '
        Me.test31.AutoSize = True
        Me.test31.Location = New System.Drawing.Point(59, 239)
        Me.test31.Name = "test31"
        Me.test31.Size = New System.Drawing.Size(11, 12)
        Me.test31.TabIndex = 85
        Me.test31.Text = "0"
        '
        'test6
        '
        Me.test6.AutoSize = True
        Me.test6.Location = New System.Drawing.Point(92, 179)
        Me.test6.Name = "test6"
        Me.test6.Size = New System.Drawing.Size(29, 12)
        Me.test6.TabIndex = 85
        Me.test6.Text = "0000"
        '
        'test35
        '
        Me.test35.AutoSize = True
        Me.test35.Location = New System.Drawing.Point(85, 239)
        Me.test35.Name = "test35"
        Me.test35.Size = New System.Drawing.Size(29, 12)
        Me.test35.TabIndex = 85
        Me.test35.Text = "0000"
        '
        'test2
        '
        Me.test2.AutoSize = True
        Me.test2.Location = New System.Drawing.Point(92, 196)
        Me.test2.Name = "test2"
        Me.test2.Size = New System.Drawing.Size(29, 12)
        Me.test2.TabIndex = 85
        Me.test2.Text = "0000"
        '
        'test1
        '
        Me.test1.AutoSize = True
        Me.test1.Location = New System.Drawing.Point(92, 160)
        Me.test1.Name = "test1"
        Me.test1.Size = New System.Drawing.Size(29, 12)
        Me.test1.TabIndex = 85
        Me.test1.Text = "0000"
        '
        'test8
        '
        Me.test8.AutoSize = True
        Me.test8.Location = New System.Drawing.Point(92, 140)
        Me.test8.Name = "test8"
        Me.test8.Size = New System.Drawing.Size(29, 12)
        Me.test8.TabIndex = 85
        Me.test8.Text = "0000"
        '
        'test19
        '
        Me.test19.AutoSize = True
        Me.test19.Location = New System.Drawing.Point(92, 120)
        Me.test19.Name = "test19"
        Me.test19.Size = New System.Drawing.Size(29, 12)
        Me.test19.TabIndex = 85
        Me.test19.Text = "0000"
        '
        'test13
        '
        Me.test13.AutoSize = True
        Me.test13.Location = New System.Drawing.Point(92, 100)
        Me.test13.Name = "test13"
        Me.test13.Size = New System.Drawing.Size(29, 12)
        Me.test13.TabIndex = 85
        Me.test13.Text = "0000"
        '
        'test12
        '
        Me.test12.AutoSize = True
        Me.test12.Location = New System.Drawing.Point(92, 80)
        Me.test12.Name = "test12"
        Me.test12.Size = New System.Drawing.Size(29, 12)
        Me.test12.TabIndex = 85
        Me.test12.Text = "0000"
        '
        'test11
        '
        Me.test11.AutoSize = True
        Me.test11.Location = New System.Drawing.Point(92, 60)
        Me.test11.Name = "test11"
        Me.test11.Size = New System.Drawing.Size(29, 12)
        Me.test11.TabIndex = 85
        Me.test11.Text = "0000"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(9, 60)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(77, 12)
        Me.Label14.TabIndex = 85
        Me.Label14.Text = "升溫所需時間"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(9, 80)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(77, 12)
        Me.Label13.TabIndex = 85
        Me.Label13.Text = "計量標準60秒"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(9, 100)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(65, 12)
        Me.Label21.TabIndex = 85
        Me.Label21.Text = "開HAC時間"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(9, 140)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(67, 12)
        Me.Label22.TabIndex = 85
        Me.Label22.Text = "PH檢樣時間"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(9, 120)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(67, 12)
        Me.Label23.TabIndex = 85
        Me.Label23.Text = "PH初延遲時"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(9, 160)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(55, 12)
        Me.Label24.TabIndex = 85
        Me.Label24.Text = "PH實際值"
        '
        'test16
        '
        Me.test16.AutoSize = True
        Me.test16.Location = New System.Drawing.Point(31, 39)
        Me.test16.Name = "test16"
        Me.test16.Size = New System.Drawing.Size(43, 12)
        Me.test16.TabIndex = 85
        Me.test16.Text = "PH狀態"
        '
        'test24
        '
        Me.test24.AutoSize = True
        Me.test24.Location = New System.Drawing.Point(31, 18)
        Me.test24.Name = "test24"
        Me.test24.Size = New System.Drawing.Size(43, 12)
        Me.test24.TabIndex = 85
        Me.test24.Text = "PH狀態"
        '
        'PhShowData
        '
        Me.PhShowData.AutoSize = True
        Me.PhShowData.Location = New System.Drawing.Point(667, 486)
        Me.PhShowData.Name = "PhShowData"
        Me.PhShowData.Size = New System.Drawing.Size(69, 12)
        Me.PhShowData.TabIndex = 86
        Me.PhShowData.Text = "PhShowClose"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(629, 373)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(23, 23)
        Me.Button1.TabIndex = 88
        Me.Button1.UseVisualStyleBackColor = True
        '
        '加藥馬達速度
        '
        Me.加藥馬達速度.AutoSize = True
        Me.加藥馬達速度.Location = New System.Drawing.Point(302, 277)
        Me.加藥馬達速度.Name = "加藥馬達速度"
        Me.加藥馬達速度.Size = New System.Drawing.Size(11, 12)
        Me.加藥馬達速度.TabIndex = 85
        Me.加藥馬達速度.Text = "0"
        '
        'Mimic_Blevel
        '
        Me.Mimic_Blevel.AutoSize = True
        Me.Mimic_Blevel.Location = New System.Drawing.Point(186, 218)
        Me.Mimic_Blevel.Name = "Mimic_Blevel"
        Me.Mimic_Blevel.Size = New System.Drawing.Size(11, 12)
        Me.Mimic_Blevel.TabIndex = 85
        Me.Mimic_Blevel.Text = "0"
        '
        'Mimic_Bsetep
        '
        Me.Mimic_Bsetep.AutoSize = True
        Me.Mimic_Bsetep.Location = New System.Drawing.Point(186, 233)
        Me.Mimic_Bsetep.Name = "Mimic_Bsetep"
        Me.Mimic_Bsetep.Size = New System.Drawing.Size(11, 12)
        Me.Mimic_Bsetep.TabIndex = 85
        Me.Mimic_Bsetep.Text = "0"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Gainsboro
        Me.PictureBox1.Location = New System.Drawing.Point(11, 7)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(300, 150)
        Me.PictureBox1.TabIndex = 285
        Me.PictureBox1.TabStop = False
        '
        'Timer2
        '
        Me.Timer2.Interval = 1000
        '
        'Parent_Job
        '
        Me.Parent_Job.ForeColor = System.Drawing.Color.Black
        Me.Parent_Job.Location = New System.Drawing.Point(943, 463)
        Me.Parent_Job.Name = "Parent_Job"
        Me.Parent_Job.Size = New System.Drawing.Size(43, 12)
        Me.Parent_Job.TabIndex = 291
        Me.Parent_Job.Text = "Label26"
        '
        'Label_Dyelot
        '
        Me.Label_Dyelot.ForeColor = System.Drawing.Color.Black
        Me.Label_Dyelot.Location = New System.Drawing.Point(866, 473)
        Me.Label_Dyelot.Name = "Label_Dyelot"
        Me.Label_Dyelot.Size = New System.Drawing.Size(43, 12)
        Me.Label_Dyelot.TabIndex = 290
        Me.Label_Dyelot.Text = "Label26"
        '
        'Label27
        '
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(866, 442)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(43, 12)
        Me.Label27.TabIndex = 289
        Me.Label27.Text = "Label27"
        '
        'Parent_PrefixedSteps
        '
        Me.Parent_PrefixedSteps.ForeColor = System.Drawing.Color.Black
        Me.Parent_PrefixedSteps.Location = New System.Drawing.Point(866, 399)
        Me.Parent_PrefixedSteps.Name = "Parent_PrefixedSteps"
        Me.Parent_PrefixedSteps.Size = New System.Drawing.Size(43, 12)
        Me.Parent_PrefixedSteps.TabIndex = 288
        Me.Parent_PrefixedSteps.Text = "Label26"
        '
        'Label25
        '
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(599, 243)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(43, 12)
        Me.Label25.TabIndex = 287
        Me.Label25.Text = "Label25"
        '
        'Parent_IsProgramRunning
        '
        Me.Parent_IsProgramRunning.ForeColor = System.Drawing.Color.Black
        Me.Parent_IsProgramRunning.Location = New System.Drawing.Point(866, 358)
        Me.Parent_IsProgramRunning.Name = "Parent_IsProgramRunning"
        Me.Parent_IsProgramRunning.Size = New System.Drawing.Size(43, 12)
        Me.Parent_IsProgramRunning.TabIndex = 286
        Me.Parent_IsProgramRunning.Text = "Label25"
        '
        'IO_PhAddPump
        '
        Me.IO_PhAddPump.Location = New System.Drawing.Point(106, 449)
        Me.IO_PhAddPump.Name = "IO_PhAddPump"
        Me.IO_PhAddPump.Size = New System.Drawing.Size(20, 20)
        Me.IO_PhAddPump.TabIndex = 77
        '
        'IO_PhCirculatePump
        '
        Me.IO_PhCirculatePump.Location = New System.Drawing.Point(270, 466)
        Me.IO_PhCirculatePump.Name = "IO_PhCirculatePump"
        Me.IO_PhCirculatePump.Size = New System.Drawing.Size(20, 20)
        Me.IO_PhCirculatePump.TabIndex = 77
        '
        'IO_MainPumpFB
        '
        Me.IO_MainPumpFB.Location = New System.Drawing.Point(441, 223)
        Me.IO_MainPumpFB.Name = "IO_MainPumpFB"
        Me.IO_MainPumpFB.Size = New System.Drawing.Size(20, 20)
        Me.IO_MainPumpFB.TabIndex = 77
        '
        'IO_CTankPump
        '
        Me.IO_CTankPump.Location = New System.Drawing.Point(83, 332)
        Me.IO_CTankPump.Name = "IO_CTankPump"
        Me.IO_CTankPump.Size = New System.Drawing.Size(20, 20)
        Me.IO_CTankPump.TabIndex = 77
        '
        'IO_BTankAddPump
        '
        Me.IO_BTankAddPump.Location = New System.Drawing.Point(210, 332)
        Me.IO_BTankAddPump.Name = "IO_BTankAddPump"
        Me.IO_BTankAddPump.Size = New System.Drawing.Size(20, 20)
        Me.IO_BTankAddPump.TabIndex = 77
        '
        'IO_HighLevel
        '
        Me.IO_HighLevel.Location = New System.Drawing.Point(301, 122)
        Me.IO_HighLevel.Name = "IO_HighLevel"
        Me.IO_HighLevel.Size = New System.Drawing.Size(16, 16)
        Me.IO_HighLevel.TabIndex = 77
        '
        'IO_MiddleLevel
        '
        Me.IO_MiddleLevel.Location = New System.Drawing.Point(300, 141)
        Me.IO_MiddleLevel.Name = "IO_MiddleLevel"
        Me.IO_MiddleLevel.Size = New System.Drawing.Size(16, 16)
        Me.IO_MiddleLevel.TabIndex = 77
        '
        'IO_DrainLevel
        '
        Me.IO_DrainLevel.Location = New System.Drawing.Point(300, 177)
        Me.IO_DrainLevel.Name = "IO_DrainLevel"
        Me.IO_DrainLevel.Size = New System.Drawing.Size(16, 16)
        Me.IO_DrainLevel.TabIndex = 77
        '
        'IO_LowLevel
        '
        Me.IO_LowLevel.Location = New System.Drawing.Point(300, 159)
        Me.IO_LowLevel.Name = "IO_LowLevel"
        Me.IO_LowLevel.Size = New System.Drawing.Size(16, 16)
        Me.IO_LowLevel.TabIndex = 77
        '
        'IO_PhMixTankLowLevel
        '
        Me.IO_PhMixTankLowLevel.Location = New System.Drawing.Point(182, 430)
        Me.IO_PhMixTankLowLevel.Name = "IO_PhMixTankLowLevel"
        Me.IO_PhMixTankLowLevel.Size = New System.Drawing.Size(20, 20)
        Me.IO_PhMixTankLowLevel.TabIndex = 77
        '
        'IO_PhMixTankHighLevel
        '
        Me.IO_PhMixTankHighLevel.Location = New System.Drawing.Point(182, 409)
        Me.IO_PhMixTankHighLevel.Name = "IO_PhMixTankHighLevel"
        Me.IO_PhMixTankHighLevel.Size = New System.Drawing.Size(20, 20)
        Me.IO_PhMixTankHighLevel.TabIndex = 77
        '
        'IO_PhAddHacOut
        '
        Me.IO_PhAddHacOut.Location = New System.Drawing.Point(124, 403)
        Me.IO_PhAddHacOut.Name = "IO_PhAddHacOut"
        Me.IO_PhAddHacOut.Size = New System.Drawing.Size(20, 20)
        Me.IO_PhAddHacOut.TabIndex = 76
        '
        'IO_PhCool
        '
        Me.IO_PhCool.Location = New System.Drawing.Point(375, 463)
        Me.IO_PhCool.Name = "IO_PhCool"
        Me.IO_PhCool.Size = New System.Drawing.Size(20, 20)
        Me.IO_PhCool.TabIndex = 76
        '
        'IO_PhWashFill
        '
        Me.IO_PhWashFill.Location = New System.Drawing.Point(489, 426)
        Me.IO_PhWashFill.Name = "IO_PhWashFill"
        Me.IO_PhWashFill.Size = New System.Drawing.Size(20, 20)
        Me.IO_PhWashFill.TabIndex = 76
        '
        'IO_CondenserDrain
        '
        Me.IO_CondenserDrain.Location = New System.Drawing.Point(513, 337)
        Me.IO_CondenserDrain.Name = "IO_CondenserDrain"
        Me.IO_CondenserDrain.Size = New System.Drawing.Size(20, 20)
        Me.IO_CondenserDrain.TabIndex = 76
        '
        'IO_HxDrain
        '
        Me.IO_HxDrain.Location = New System.Drawing.Point(513, 304)
        Me.IO_HxDrain.Name = "IO_HxDrain"
        Me.IO_HxDrain.Size = New System.Drawing.Size(20, 20)
        Me.IO_HxDrain.TabIndex = 76
        '
        'IO_Dosing
        '
        Me.IO_Dosing.Location = New System.Drawing.Point(300, 298)
        Me.IO_Dosing.Name = "IO_Dosing"
        Me.IO_Dosing.Size = New System.Drawing.Size(20, 20)
        Me.IO_Dosing.TabIndex = 76
        '
        'IO_Addition
        '
        Me.IO_Addition.Location = New System.Drawing.Point(300, 332)
        Me.IO_Addition.Name = "IO_Addition"
        Me.IO_Addition.Size = New System.Drawing.Size(20, 20)
        Me.IO_Addition.TabIndex = 76
        '
        'IO_CoolDrain
        '
        Me.IO_CoolDrain.Location = New System.Drawing.Point(513, 270)
        Me.IO_CoolDrain.Name = "IO_CoolDrain"
        Me.IO_CoolDrain.Size = New System.Drawing.Size(20, 20)
        Me.IO_CoolDrain.TabIndex = 76
        '
        'IO_ColdFill
        '
        Me.IO_ColdFill.Location = New System.Drawing.Point(639, 244)
        Me.IO_ColdFill.Name = "IO_ColdFill"
        Me.IO_ColdFill.Size = New System.Drawing.Size(20, 20)
        Me.IO_ColdFill.TabIndex = 76
        '
        'IO_HotFill
        '
        Me.IO_HotFill.Location = New System.Drawing.Point(639, 209)
        Me.IO_HotFill.Name = "IO_HotFill"
        Me.IO_HotFill.Size = New System.Drawing.Size(20, 20)
        Me.IO_HotFill.TabIndex = 76
        '
        'IO_BTankColdFill
        '
        Me.IO_BTankColdFill.Location = New System.Drawing.Point(193, 152)
        Me.IO_BTankColdFill.Name = "IO_BTankColdFill"
        Me.IO_BTankColdFill.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.IO_BTankColdFill.Size = New System.Drawing.Size(20, 20)
        Me.IO_BTankColdFill.TabIndex = 76
        '
        'IO_PhFillCirculate
        '
        Me.IO_PhFillCirculate.Location = New System.Drawing.Point(466, 337)
        Me.IO_PhFillCirculate.Name = "IO_PhFillCirculate"
        Me.IO_PhFillCirculate.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.IO_PhFillCirculate.Size = New System.Drawing.Size(20, 20)
        Me.IO_PhFillCirculate.TabIndex = 76
        '
        'IO_PhDrain
        '
        Me.IO_PhDrain.Location = New System.Drawing.Point(419, 377)
        Me.IO_PhDrain.Name = "IO_PhDrain"
        Me.IO_PhDrain.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.IO_PhDrain.Size = New System.Drawing.Size(20, 20)
        Me.IO_PhDrain.TabIndex = 76
        '
        'IO_PhInToMachine
        '
        Me.IO_PhInToMachine.Location = New System.Drawing.Point(400, 270)
        Me.IO_PhInToMachine.Name = "IO_PhInToMachine"
        Me.IO_PhInToMachine.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.IO_PhInToMachine.Size = New System.Drawing.Size(20, 20)
        Me.IO_PhInToMachine.TabIndex = 76
        '
        'IO_HotDrain
        '
        Me.IO_HotDrain.Location = New System.Drawing.Point(357, 244)
        Me.IO_HotDrain.Name = "IO_HotDrain"
        Me.IO_HotDrain.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.IO_HotDrain.Size = New System.Drawing.Size(20, 20)
        Me.IO_HotDrain.TabIndex = 76
        '
        'IO_Overflow
        '
        Me.IO_Overflow.Location = New System.Drawing.Point(261, 170)
        Me.IO_Overflow.Name = "IO_Overflow"
        Me.IO_Overflow.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.IO_Overflow.Size = New System.Drawing.Size(20, 20)
        Me.IO_Overflow.TabIndex = 76
        '
        'IO_LevelClean
        '
        Me.IO_LevelClean.Location = New System.Drawing.Point(352, 193)
        Me.IO_LevelClean.Name = "IO_LevelClean"
        Me.IO_LevelClean.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.IO_LevelClean.Size = New System.Drawing.Size(20, 20)
        Me.IO_LevelClean.TabIndex = 76
        '
        'IO_Drain
        '
        Me.IO_Drain.Location = New System.Drawing.Point(331, 245)
        Me.IO_Drain.Name = "IO_Drain"
        Me.IO_Drain.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.IO_Drain.Size = New System.Drawing.Size(20, 20)
        Me.IO_Drain.TabIndex = 76
        '
        'IO_CTankAddition
        '
        Me.IO_CTankAddition.Location = New System.Drawing.Point(38, 298)
        Me.IO_CTankAddition.Name = "IO_CTankAddition"
        Me.IO_CTankAddition.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.IO_CTankAddition.Size = New System.Drawing.Size(20, 20)
        Me.IO_CTankAddition.TabIndex = 76
        '
        'IO_CTankDrain
        '
        Me.IO_CTankDrain.Location = New System.Drawing.Point(3, 294)
        Me.IO_CTankDrain.Name = "IO_CTankDrain"
        Me.IO_CTankDrain.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.IO_CTankDrain.Size = New System.Drawing.Size(20, 20)
        Me.IO_CTankDrain.TabIndex = 76
        '
        'IO_BTankAddition
        '
        Me.IO_BTankAddition.Location = New System.Drawing.Point(164, 293)
        Me.IO_BTankAddition.Name = "IO_BTankAddition"
        Me.IO_BTankAddition.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.IO_BTankAddition.Size = New System.Drawing.Size(20, 20)
        Me.IO_BTankAddition.TabIndex = 76
        '
        'IO_BTankDrain
        '
        Me.IO_BTankDrain.Location = New System.Drawing.Point(130, 289)
        Me.IO_BTankDrain.Name = "IO_BTankDrain"
        Me.IO_BTankDrain.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.IO_BTankDrain.Size = New System.Drawing.Size(20, 20)
        Me.IO_BTankDrain.TabIndex = 76
        '
        'IO_BTankCirculate2
        '
        Me.IO_BTankCirculate2.Location = New System.Drawing.Point(167, 152)
        Me.IO_BTankCirculate2.Name = "IO_BTankCirculate2"
        Me.IO_BTankCirculate2.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.IO_BTankCirculate2.Size = New System.Drawing.Size(20, 20)
        Me.IO_BTankCirculate2.TabIndex = 76
        '
        'IO_CTankColdFill
        '
        Me.IO_CTankColdFill.Location = New System.Drawing.Point(67, 153)
        Me.IO_CTankColdFill.Name = "IO_CTankColdFill"
        Me.IO_CTankColdFill.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.IO_CTankColdFill.Size = New System.Drawing.Size(20, 20)
        Me.IO_CTankColdFill.TabIndex = 76
        '
        'IO_CTankCirculate2
        '
        Me.IO_CTankCirculate2.Location = New System.Drawing.Point(40, 152)
        Me.IO_CTankCirculate2.Name = "IO_CTankCirculate2"
        Me.IO_CTankCirculate2.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.IO_CTankCirculate2.Size = New System.Drawing.Size(20, 20)
        Me.IO_CTankCirculate2.TabIndex = 76
        '
        'IO_CoolForHotWater
        '
        Me.IO_CoolForHotWater.Location = New System.Drawing.Point(456, 98)
        Me.IO_CoolForHotWater.Name = "IO_CoolForHotWater"
        Me.IO_CoolForHotWater.Size = New System.Drawing.Size(20, 20)
        Me.IO_CoolForHotWater.TabIndex = 76
        '
        'IO_Cool
        '
        Me.IO_Cool.Location = New System.Drawing.Point(456, 75)
        Me.IO_Cool.Name = "IO_Cool"
        Me.IO_Cool.Size = New System.Drawing.Size(20, 20)
        Me.IO_Cool.TabIndex = 76
        '
        'IO_Heat
        '
        Me.IO_Heat.Location = New System.Drawing.Point(456, 32)
        Me.IO_Heat.Name = "IO_Heat"
        Me.IO_Heat.Size = New System.Drawing.Size(20, 20)
        Me.IO_Heat.TabIndex = 76
        '
        'IO_CTankMixCir2
        '
        Me.IO_CTankMixCir2.Location = New System.Drawing.Point(85, 247)
        Me.IO_CTankMixCir2.Name = "IO_CTankMixCir2"
        Me.IO_CTankMixCir2.Size = New System.Drawing.Size(20, 20)
        Me.IO_CTankMixCir2.TabIndex = 76
        '
        'IO_BTankMixCir2
        '
        Me.IO_BTankMixCir2.Location = New System.Drawing.Point(210, 247)
        Me.IO_BTankMixCir2.Name = "IO_BTankMixCir2"
        Me.IO_BTankMixCir2.Size = New System.Drawing.Size(20, 20)
        Me.IO_BTankMixCir2.TabIndex = 76
        '
        'IO_PressureOut
        '
        Me.IO_PressureOut.Location = New System.Drawing.Point(300, 50)
        Me.IO_PressureOut.Name = "IO_PressureOut"
        Me.IO_PressureOut.Size = New System.Drawing.Size(20, 20)
        Me.IO_PressureOut.TabIndex = 76
        '
        'IO_PressureIn
        '
        Me.IO_PressureIn.Location = New System.Drawing.Point(301, 14)
        Me.IO_PressureIn.Name = "IO_PressureIn"
        Me.IO_PressureIn.Size = New System.Drawing.Size(20, 20)
        Me.IO_PressureIn.TabIndex = 76
        '
        'IO_Entanglement1
        '
        Me.IO_Entanglement1.Location = New System.Drawing.Point(65, 88)
        Me.IO_Entanglement1.Name = "IO_Entanglement1"
        Me.IO_Entanglement1.Size = New System.Drawing.Size(20, 20)
        Me.IO_Entanglement1.TabIndex = 54
        '
        'IO_ErrorLamp
        '
        Me.IO_ErrorLamp.Location = New System.Drawing.Point(65, 62)
        Me.IO_ErrorLamp.Name = "IO_ErrorLamp"
        Me.IO_ErrorLamp.Size = New System.Drawing.Size(20, 20)
        Me.IO_ErrorLamp.TabIndex = 52
        '
        'IO_CallLamp
        '
        Me.IO_CallLamp.Location = New System.Drawing.Point(65, 36)
        Me.IO_CallLamp.Name = "IO_CallLamp"
        Me.IO_CallLamp.Size = New System.Drawing.Size(20, 20)
        Me.IO_CallLamp.TabIndex = 50
        '
        'IO_SystemAuto
        '
        Me.IO_SystemAuto.Location = New System.Drawing.Point(65, 11)
        Me.IO_SystemAuto.Name = "IO_SystemAuto"
        Me.IO_SystemAuto.Size = New System.Drawing.Size(20, 20)
        Me.IO_SystemAuto.TabIndex = 48
        '
        'Mimic_MainLevel
        '
        Me.Mimic_MainLevel.ForeColor = System.Drawing.Color.Black
        Me.Mimic_MainLevel.Format = Nothing
        Me.Mimic_MainLevel.Location = New System.Drawing.Point(341, 152)
        Me.Mimic_MainLevel.Name = "Mimic_MainLevel"
        Me.Mimic_MainLevel.Size = New System.Drawing.Size(10, 55)
        Me.Mimic_MainLevel.TabIndex = 6
        '
        'Mimic_BTankLevel
        '
        Me.Mimic_BTankLevel.ForeColor = System.Drawing.Color.Black
        Me.Mimic_BTankLevel.Format = Nothing
        Me.Mimic_BTankLevel.Location = New System.Drawing.Point(135, 215)
        Me.Mimic_BTankLevel.Name = "Mimic_BTankLevel"
        Me.Mimic_BTankLevel.Size = New System.Drawing.Size(9, 50)
        Me.Mimic_BTankLevel.TabIndex = 4
        '
        'Mimic_CTankLevel
        '
        Me.Mimic_CTankLevel.ForeColor = System.Drawing.Color.Black
        Me.Mimic_CTankLevel.Format = Nothing
        Me.Mimic_CTankLevel.Location = New System.Drawing.Point(11, 214)
        Me.Mimic_CTankLevel.Name = "Mimic_CTankLevel"
        Me.Mimic_CTankLevel.Size = New System.Drawing.Size(9, 50)
        Me.Mimic_CTankLevel.TabIndex = 4
        '
        'Label26
        '
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(375, 72)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(43, 12)
        Me.Label26.TabIndex = 292
        Me.Label26.Text = "Label26"
        '
        'Mimic
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.Parent_Job)
        Me.Controls.Add(Me.Label_Dyelot)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.Parent_PrefixedSteps)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.Parent_IsProgramRunning)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.PhShowData)
        Me.Controls.Add(Me.PH_DATA)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.PhShowPic)
        Me.Controls.Add(Me.IO_PhAddPump)
        Me.Controls.Add(Me.IO_PhCirculatePump)
        Me.Controls.Add(Me.IO_MainPumpFB)
        Me.Controls.Add(Me.IO_CTankPump)
        Me.Controls.Add(Me.IO_BTankAddPump)
        Me.Controls.Add(Me.IO_HighLevel)
        Me.Controls.Add(Me.IO_MiddleLevel)
        Me.Controls.Add(Me.IO_DrainLevel)
        Me.Controls.Add(Me.IO_LowLevel)
        Me.Controls.Add(Me.IO_PhMixTankLowLevel)
        Me.Controls.Add(Me.IO_PhMixTankHighLevel)
        Me.Controls.Add(Me.IO_PhAddHacOut)
        Me.Controls.Add(Me.IO_PhCool)
        Me.Controls.Add(Me.IO_PhWashFill)
        Me.Controls.Add(Me.IO_CondenserDrain)
        Me.Controls.Add(Me.IO_HxDrain)
        Me.Controls.Add(Me.IO_Dosing)
        Me.Controls.Add(Me.IO_Addition)
        Me.Controls.Add(Me.Mimic_Bsetep)
        Me.Controls.Add(Me.Mimic_Blevel)
        Me.Controls.Add(Me.加藥馬達速度)
        Me.Controls.Add(Me.IO_CoolDrain)
        Me.Controls.Add(Me.IO_ColdFill)
        Me.Controls.Add(Me.IO_HotFill)
        Me.Controls.Add(Me.IO_BTankColdFill)
        Me.Controls.Add(Me.IO_PhFillCirculate)
        Me.Controls.Add(Me.IO_PhDrain)
        Me.Controls.Add(Me.IO_PhInToMachine)
        Me.Controls.Add(Me.IO_HotDrain)
        Me.Controls.Add(Me.IO_Overflow)
        Me.Controls.Add(Me.IO_LevelClean)
        Me.Controls.Add(Me.IO_Drain)
        Me.Controls.Add(Me.IO_CTankAddition)
        Me.Controls.Add(Me.IO_CTankDrain)
        Me.Controls.Add(Me.IO_BTankAddition)
        Me.Controls.Add(Me.IO_BTankDrain)
        Me.Controls.Add(Me.IO_BTankCirculate2)
        Me.Controls.Add(Me.IO_CTankColdFill)
        Me.Controls.Add(Me.IO_CTankCirculate2)
        Me.Controls.Add(Me.IO_CoolForHotWater)
        Me.Controls.Add(Me.IO_Cool)
        Me.Controls.Add(Me.IO_Heat)
        Me.Controls.Add(Me.IO_CTankMixCir2)
        Me.Controls.Add(Me.IO_BTankMixCir2)
        Me.Controls.Add(Me.IO_PressureOut)
        Me.Controls.Add(Me.IO_PressureIn)
        Me.Controls.Add(Me.Mimic_TempControl)
        Me.Controls.Add(Me.IO_Entanglement1)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.IO_ErrorLamp)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.IO_CallLamp)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.IO_SystemAuto)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Mimic_MainLevel)
        Me.Controls.Add(Me.Mimic_BTankLevel)
        Me.Controls.Add(Me.Mimic_CTankLevel)
        Me.Name = "Mimic"
        Me.Size = New System.Drawing.Size(790, 500)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.PH_DATA.ResumeLayout(False)
        Me.PH_DATA.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Mimic_CTankLevel As TG838.MimicControls.LevelBar
  Friend WithEvents Mimic_MainLevel As TG838.MimicControls.LevelBar
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents IO_SystemAuto As TG838.MimicControls.Lamp
  Friend WithEvents Label9 As System.Windows.Forms.Label
  Friend WithEvents IO_CallLamp As TG838.MimicControls.Lamp
  Friend WithEvents Label10 As System.Windows.Forms.Label
  Friend WithEvents IO_ErrorLamp As TG838.MimicControls.Lamp
  Friend WithEvents IO_Entanglement1 As TG838.MimicControls.Lamp
  Friend WithEvents Label11 As System.Windows.Forms.Label
  Friend WithEvents Mimic_TempControl As System.Windows.Forms.TextBox
  Friend WithEvents Mimic_BTankLevel As TG838.MimicControls.LevelBar
  Friend WithEvents IO_PressureIn As TG838.MimicControls.Valve
  Friend WithEvents IO_PressureOut As TG838.MimicControls.Valve
  Friend WithEvents IO_Heat As TG838.MimicControls.Valve
  Friend WithEvents IO_Cool As TG838.MimicControls.Valve
  Friend WithEvents IO_HotFill As TG838.MimicControls.Valve
  Friend WithEvents IO_ColdFill As TG838.MimicControls.Valve
  Friend WithEvents IO_CoolDrain As TG838.MimicControls.Valve
  Friend WithEvents IO_HxDrain As TG838.MimicControls.Valve
  Friend WithEvents IO_CondenserDrain As TG838.MimicControls.Valve
  Friend WithEvents IO_CTankCirculate2 As TG838.MimicControls.Valve
  Friend WithEvents IO_CTankColdFill As TG838.MimicControls.Valve
  Friend WithEvents IO_BTankCirculate2 As TG838.MimicControls.Valve
  Friend WithEvents IO_BTankColdFill As TG838.MimicControls.Valve
  Friend WithEvents IO_BTankDrain As TG838.MimicControls.Valve
  Friend WithEvents IO_BTankAddition As TG838.MimicControls.Valve
  Friend WithEvents IO_CTankDrain As TG838.MimicControls.Valve
  Friend WithEvents IO_CTankAddition As TG838.MimicControls.Valve
  Friend WithEvents IO_Addition As TG838.MimicControls.Valve
  Friend WithEvents IO_Drain As TG838.MimicControls.Valve
  Friend WithEvents IO_HotDrain As TG838.MimicControls.Valve
  Friend WithEvents IO_PhInToMachine As TG838.MimicControls.Valve
  Friend WithEvents IO_PhDrain As TG838.MimicControls.Valve
  Friend WithEvents IO_PhWashFill As TG838.MimicControls.Valve
  Friend WithEvents IO_PhCool As TG838.MimicControls.Valve
  Friend WithEvents IO_PhFillCirculate As TG838.MimicControls.Valve
  Friend WithEvents IO_PhAddHacOut As TG838.MimicControls.Valve
  Friend WithEvents IO_PhMixTankHighLevel As TG838.MimicControls.Lamp
  Friend WithEvents IO_PhMixTankLowLevel As TG838.MimicControls.Lamp
  Friend WithEvents IO_PhCirculatePump As TG838.MimicControls.Lamp
  Friend WithEvents IO_PhAddPump As TG838.MimicControls.Lamp
  Friend WithEvents IO_BTankAddPump As TG838.MimicControls.Lamp
  Friend WithEvents IO_MainPumpFB As TG838.MimicControls.Lamp
  Friend WithEvents PhShowPic As System.Windows.Forms.Label
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents Label12 As System.Windows.Forms.Label
  Friend WithEvents SetPointShow As System.Windows.Forms.Label
  Friend WithEvents Label16 As System.Windows.Forms.Label
  Friend WithEvents Label15 As System.Windows.Forms.Label
  Friend WithEvents MainTempShow As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents PH_DATA As System.Windows.Forms.GroupBox
  Friend WithEvents Label20 As System.Windows.Forms.Label
  Friend WithEvents Label19 As System.Windows.Forms.Label
  Friend WithEvents Label18 As System.Windows.Forms.Label
  Friend WithEvents Label17 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Label7 As System.Windows.Forms.Label
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents test10 As System.Windows.Forms.Label
  Friend WithEvents test23 As System.Windows.Forms.Label
  Friend WithEvents test22 As System.Windows.Forms.Label
  Friend WithEvents test4 As System.Windows.Forms.Label
  Friend WithEvents test21 As System.Windows.Forms.Label
  Friend WithEvents test3 As System.Windows.Forms.Label
  Friend WithEvents test6 As System.Windows.Forms.Label
  Friend WithEvents test2 As System.Windows.Forms.Label
  Friend WithEvents test1 As System.Windows.Forms.Label
  Friend WithEvents test8 As System.Windows.Forms.Label
  Friend WithEvents test19 As System.Windows.Forms.Label
  Friend WithEvents test13 As System.Windows.Forms.Label
  Friend WithEvents test12 As System.Windows.Forms.Label
  Friend WithEvents test11 As System.Windows.Forms.Label
  Friend WithEvents Label14 As System.Windows.Forms.Label
  Friend WithEvents Label13 As System.Windows.Forms.Label
  Friend WithEvents Label21 As System.Windows.Forms.Label
  Friend WithEvents Label22 As System.Windows.Forms.Label
  Friend WithEvents Label23 As System.Windows.Forms.Label
  Friend WithEvents Label24 As System.Windows.Forms.Label
  Friend WithEvents test16 As System.Windows.Forms.Label
  Friend WithEvents test24 As System.Windows.Forms.Label
  Friend WithEvents PhShowData As System.Windows.Forms.Label
  Friend WithEvents IO_Overflow As TG838.MimicControls.Valve
  Friend WithEvents IO_BTankMixCir2 As TG838.MimicControls.Valve
  Friend WithEvents IO_CTankMixCir2 As TG838.MimicControls.Valve
  Friend WithEvents IO_Dosing As TG838.MimicControls.Valve
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents test31 As System.Windows.Forms.Label
  Friend WithEvents test32 As System.Windows.Forms.Label
  Friend WithEvents test33 As System.Windows.Forms.Label
  Friend WithEvents 補酸狀態分析 As System.Windows.Forms.Label
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents test35 As System.Windows.Forms.Label
  Friend WithEvents 加藥馬達速度 As System.Windows.Forms.Label
  Friend WithEvents Mimic_Blevel As System.Windows.Forms.Label
  Friend WithEvents Mimic_Bsetep As System.Windows.Forms.Label
  Friend WithEvents IO_CTankPump As TG838.MimicControls.Lamp
  Friend WithEvents IO_LevelClean As TG838.MimicControls.Valve
  Friend WithEvents IO_LowLevel As TG838.MimicControls.Lamp
  Friend WithEvents IO_MiddleLevel As TG838.MimicControls.Lamp
  Friend WithEvents IO_HighLevel As TG838.MimicControls.Lamp
  Friend WithEvents IO_DrainLevel As TG838.MimicControls.Lamp
  Friend WithEvents IO_CoolForHotWater As TG838.MimicControls.Valve
  Friend WithEvents PictureBox1 As PictureBox
  Friend WithEvents Parent_IsProgramRunning As MimicControls.Label
  Friend WithEvents Label25 As MimicControls.Label
  Friend WithEvents Parent_PrefixedSteps As MimicControls.Label
  Friend WithEvents Label27 As MimicControls.Label
  Friend WithEvents Label_Dyelot As MimicControls.Label
  Friend WithEvents Parent_Job As MimicControls.Label
  Friend WithEvents Timer2 As Windows.Forms.Timer
    Friend WithEvents Label26 As MimicControls.Label
End Class
