<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SmallMimic
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
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.溫度曲線_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Parent_Job = New TG838.MimicControls.Label()
        Me.Parent_Is = New TG838.MimicControls.Label()
        Me.Parent_PrefixedSteps = New TG838.MimicControls.Label()
        Me.Parent_IsProgramRunning = New TG838.MimicControls.Label()
        Me.Label2 = New TG838.MimicControls.Label()
        Me.Label1 = New TG838.MimicControls.Label()
        Me.IO_MainTemperature = New TG838.MimicControls.Label()
        Me.Label_ColorNo = New TG838.MimicControls.Label()
        Me.Label_Dyelot = New TG838.MimicControls.Label()
        Me.ColorName = New TG838.MimicControls.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        '溫度曲線_Timer
        '
        Me.溫度曲線_Timer.Enabled = True
        Me.溫度曲線_Timer.Interval = 1000
        '
        'Timer2
        '
        Me.Timer2.Interval = 1000
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(5, 54)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(300, 150)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 284
        Me.PictureBox1.TabStop = False
        '
        'Parent_Job
        '
        Me.Parent_Job.ForeColor = System.Drawing.Color.Black
        Me.Parent_Job.Location = New System.Drawing.Point(393, 102)
        Me.Parent_Job.Margin = New System.Windows.Forms.Padding(2)
        Me.Parent_Job.Name = "Parent_Job"
        Me.Parent_Job.Size = New System.Drawing.Size(36, 12)
        Me.Parent_Job.TabIndex = 292
        Me.Parent_Job.Text = "Dyelot"
        Me.Parent_Job.Visible = False
        '
        'Parent_Is
        '
        Me.Parent_Is.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Parent_Is.ForeColor = System.Drawing.Color.Black
        Me.Parent_Is.Location = New System.Drawing.Point(393, 78)
        Me.Parent_Is.Margin = New System.Windows.Forms.Padding(2)
        Me.Parent_Is.Name = "Parent_Is"
        Me.Parent_Is.Size = New System.Drawing.Size(114, 20)
        Me.Parent_Is.TabIndex = 291
        Me.Parent_Is.Text = "PreFixedSteps"
        Me.Parent_Is.Visible = False
        '
        'Parent_PrefixedSteps
        '
        Me.Parent_PrefixedSteps.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Parent_PrefixedSteps.ForeColor = System.Drawing.Color.Black
        Me.Parent_PrefixedSteps.Location = New System.Drawing.Point(393, 55)
        Me.Parent_PrefixedSteps.Margin = New System.Windows.Forms.Padding(2)
        Me.Parent_PrefixedSteps.Name = "Parent_PrefixedSteps"
        Me.Parent_PrefixedSteps.Size = New System.Drawing.Size(114, 20)
        Me.Parent_PrefixedSteps.TabIndex = 290
        Me.Parent_PrefixedSteps.Text = "PreFixedSteps"
        Me.Parent_PrefixedSteps.Visible = False
        '
        'Parent_IsProgramRunning
        '
        Me.Parent_IsProgramRunning.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Parent_IsProgramRunning.ForeColor = System.Drawing.Color.Black
        Me.Parent_IsProgramRunning.Location = New System.Drawing.Point(393, 32)
        Me.Parent_IsProgramRunning.Margin = New System.Windows.Forms.Padding(2)
        Me.Parent_IsProgramRunning.Name = "Parent_IsProgramRunning"
        Me.Parent_IsProgramRunning.Size = New System.Drawing.Size(151, 20)
        Me.Parent_IsProgramRunning.TabIndex = 289
        Me.Parent_IsProgramRunning.Text = "IsProgramRunning"
        Me.Parent_IsProgramRunning.Visible = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(166, 7)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 20)
        Me.Label2.TabIndex = 288
        Me.Label2.Text = "溫度"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(166, 32)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 20)
        Me.Label1.TabIndex = 287
        Me.Label1.Text = "Timer"
        '
        'IO_MainTemperature
        '
        Me.IO_MainTemperature.ForeColor = System.Drawing.Color.Black
        Me.IO_MainTemperature.Location = New System.Drawing.Point(393, 10)
        Me.IO_MainTemperature.Margin = New System.Windows.Forms.Padding(2)
        Me.IO_MainTemperature.Name = "IO_MainTemperature"
        Me.IO_MainTemperature.Size = New System.Drawing.Size(88, 12)
        Me.IO_MainTemperature.TabIndex = 286
        Me.IO_MainTemperature.Text = "MainTemperature"
        Me.IO_MainTemperature.Visible = False
        '
        'Label_ColorNo
        '
        Me.Label_ColorNo.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label_ColorNo.ForeColor = System.Drawing.Color.Black
        Me.Label_ColorNo.Location = New System.Drawing.Point(6, 30)
        Me.Label_ColorNo.Margin = New System.Windows.Forms.Padding(2)
        Me.Label_ColorNo.Name = "Label_ColorNo"
        Me.Label_ColorNo.Size = New System.Drawing.Size(85, 20)
        Me.Label_ColorNo.TabIndex = 283
        Me.Label_ColorNo.Text = "色名: XX灰"
        '
        'Label_Dyelot
        '
        Me.Label_Dyelot.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label_Dyelot.ForeColor = System.Drawing.Color.Black
        Me.Label_Dyelot.Location = New System.Drawing.Point(6, 5)
        Me.Label_Dyelot.Margin = New System.Windows.Forms.Padding(2)
        Me.Label_Dyelot.Name = "Label_Dyelot"
        Me.Label_Dyelot.Size = New System.Drawing.Size(135, 20)
        Me.Label_Dyelot.TabIndex = 282
        Me.Label_Dyelot.Text = "工單:1234567890"
        '
        'ColorName
        '
        Me.ColorName.ForeColor = System.Drawing.Color.Black
        Me.ColorName.Location = New System.Drawing.Point(393, 118)
        Me.ColorName.Margin = New System.Windows.Forms.Padding(2)
        Me.ColorName.Name = "ColorName"
        Me.ColorName.Size = New System.Drawing.Size(59, 12)
        Me.ColorName.TabIndex = 293
        Me.ColorName.Text = "ColorName"
        Me.ColorName.Visible = False
        '
        'SmallMimic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.LightGray
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Controls.Add(Me.ColorName)
        Me.Controls.Add(Me.Parent_Job)
        Me.Controls.Add(Me.Parent_Is)
        Me.Controls.Add(Me.Parent_PrefixedSteps)
        Me.Controls.Add(Me.Parent_IsProgramRunning)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.IO_MainTemperature)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label_ColorNo)
        Me.Controls.Add(Me.Label_Dyelot)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "SmallMimic"
        Me.Size = New System.Drawing.Size(801, 242)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents 溫度曲線_Timer As System.Windows.Forms.Timer
    Friend WithEvents Label_Dyelot As MimicControls.Label
    Friend WithEvents Label_ColorNo As MimicControls.Label
    Friend WithEvents Timer2 As Windows.Forms.Timer
    Friend WithEvents IO_MainTemperature As MimicControls.Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As MimicControls.Label
    Friend WithEvents Label2 As MimicControls.Label
    Friend WithEvents Parent_IsProgramRunning As MimicControls.Label
    Friend WithEvents Parent_PrefixedSteps As MimicControls.Label
    Friend WithEvents Parent_Is As MimicControls.Label
    Friend WithEvents Parent_Job As MimicControls.Label
    Friend WithEvents ColorName As MimicControls.Label
End Class
