Imports System.Runtime.InteropServices

Public Class Mimic
  Public ControlCode As ControlCode
  Public ActualLine_X1(300) As Integer
  Public ActualLine_Y1(300) As Integer
  Public ActualLine_X2(300) As Integer
  Public ActualLine_Y2(300) As Integer
  Public ActualLine_Xwas As Integer
  Public ActualLine_Ywas As Integer
  Public UpdateActualLine As Boolean
  Public ActualLineNo As Integer
  Public 溫度(300) As Integer
  Public 取樣時間 As Integer
  Public 運行時間 As Integer
  Public FirstScan As Boolean
  Public StartRecordTemperature As Boolean


  Private Sub Mimic_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub

  Private Sub PhShowData_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PhShowData.TextChanged
    If PhShowData.Text = "PhShowOpen" Then
      PH_DATA.Visible = True
    Else : PhShowData.Text = "PhShowClose"
      PH_DATA.Visible = False
    End If
  End Sub

  Private Sub PhShowPic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PhShowPic.Click

  End Sub

  Private Sub PhShowPic_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PhShowPic.TextChanged

    IO_PhDrain.Visible = True
    IO_PhInToMachine.Visible = True
    IO_PhCirculatePump.Visible = True
    IO_PhMixTankLowLevel.Visible = True
    IO_PhMixTankHighLevel.Visible = True


  End Sub

  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    PH_DATA.Visible = Not PH_DATA.Visible
  End Sub

  Shared Function SplitMemo(value As String) As String()
    If String.IsNullOrEmpty(value) Then Return {}

    ' Constants
    Dim cr_ = Convert.ToChar(13), lf_ = Convert.ToChar(10), crAndLf_ = {cr_, lf_}

    ' Assemble into a collection
    With New List(Of String)
      Dim startIndex As Integer, valueLength = value.Length
      Do While startIndex < valueLength
        ' Better, faster
        Dim find = value.IndexOfAny(crAndLf_, startIndex, valueLength - startIndex)
        If find = -1 Then .Add(value.Substring(startIndex)) : Exit Do
        Dim ss = value.Substring(startIndex, find - startIndex)
        .Add(ss)
        startIndex = find + 1
        If startIndex = valueLength Then Exit Do
        If value.Chars(startIndex) = lf_ Then startIndex += 1
      Loop
      ' And return it as a simple string array
      Return .ToArray
    End With
  End Function
  Shared Function GetStepsFromPrefixedSteps(prefixedSteps As String) As String()
    Dim ret = New List(Of String)
    For Each line In SplitMemo(prefixedSteps)  ' split into lines
      Dim sep = Convert.ToChar(255)  ' the 'y'
      Dim ofs = 0
      For j = 1 To 5
        Dim f = line.IndexOf(sep, ofs)
        If f = -1 Then ofs = -1 : Exit For ' something bad - return nothing
        ofs = f + 1
      Next j
      If ofs <> -1 Then ret.Add(line.Substring(ofs))
    Next line
    Return ret.ToArray
  End Function
  Private Function GetParamFromString(ByVal StepString As String) As String()
    Try
      Dim param() As String
      param = StepString.Split(",".ToCharArray)
      Return param
    Catch ex As Exception
    End Try
    Return Nothing
  End Function

  Public StepsData() As String
  Public SetpointProfileFinish As Boolean

  Private Sub Mimic_Timer1_Tick(sender As Object, e As EventArgs)
    With ControlCode
      Dim g As Graphics = Me.CreateGraphics
      Dim eP As New Pen(Color.Blue, 1)
      Dim eP1 As New Pen(Color.DarkGray, 1)
      Dim eP2 As New Pen(Color.Red, 1)
      Dim bp As New Bitmap(300, 150) '定义位图,并进行赋值
      Dim i As Integer
      Dim Line_X1 As Integer
      Dim Line_Y1 As Integer
      Dim Line_X2 As Integer
      Dim Line_Y2 As Integer
      Dim StepNumber As Integer
      Dim TempWas As Integer
      Dim Line_X_was As Integer
      Dim Line_Y_Was As Integer
      Dim Gradient As Integer


      If Not .Parent.IsProgramRunning Then
        PictureBox1.Image = bp
        g = Graphics.FromImage(PictureBox1.Image)
        SetpointProfileFinish = False
        Timer2.Interval = 1000
        Timer2.Enabled = False
        ActualLineNo = 0
      End If
      If .Parent.IsProgramRunning Then
        PictureBox1.Image = bp
        g = Graphics.FromImage(PictureBox1.Image)
        Label_Dyelot.Text = .Parent.Job
        取樣時間 = CInt((300 / 運行時間))
        If 取樣時間 > 0 And Not Timer2.Enabled Then
          Timer2.Interval = 100
          Timer2.Enabled = True
        End If

        StepsData = GetStepsFromPrefixedSteps(.Parent.PrefixedSteps)
        Dim k As Integer
        Dim TemperatureWas As Integer
        運行時間 = 0
        For k = 0 To StepsData.Length - 1
          Dim param() As String
          param = GetParamFromString(StepsData(k))
          If param(0) = "01" Then
            Dim 斜率 As Integer
            斜率 = CInt(param(2)) * 10 + CInt(param(3))
            If 斜率 = 0 Then
              斜率 = 40
            End If
            If TemperatureWas = 0 Then
              運行時間 = (Math.Abs(CInt(param(1)) * 10 - 300) \ 斜率) + CInt(param(4)) + 運行時間
            Else
              運行時間 = (Math.Abs(CInt(param(1)) * 10 - TemperatureWas) \ 斜率) + CInt(param(4)) + 運行時間
            End If
            TemperatureWas = CInt(param(1)) * 10
          End If
        Next
        If StepsData.Length > 0 Then
          g.DrawLine(eP1, 0, 130, 300, 130)
          g.DrawLine(eP1, 0, 90, 300, 90)
          g.DrawLine(eP1, 0, 60, 300, 60)
          g.DrawLine(eP1, 0, 20, 300, 20)
          Dim RunHour As Integer
          For RunHour = 1 To 運行時間 \ 60
            Dim X_Hour As Integer
            X_Hour = (300 * RunHour * 60) \ 運行時間
            g.DrawLine(eP1, X_Hour, 0, X_Hour, 150)
          Next


          For i = 0 To StepsData.Length - 1
            Dim param() As String
            param = GetParamFromString(StepsData(i))
            If param(0) = "01" Then
              '斜率0則用50取代
              If param(2) = "0" And param(3) = "0" Then
                Gradient = 40
              Else
                Gradient = CInt(param(2)) * 10 + CInt(param(3))
              End If
              If StepNumber = 0 Then
                '第一條線從30度開始畫
                Line_X1 = 1
                Line_Y1 = 150 - 30
                Line_X2 = ((CInt(param(1)) - 30) * 10 * 300) \ (Gradient * 運行時間)
                Line_Y2 = 150 - CInt(param(1))
                g.DrawLine(eP, Line_X1, Line_Y1, Line_X2, Line_Y2)
                TempWas = CInt(param(1))
                '繪製持溫線
                If param.Length >= 5 Then
                  If CInt(param(4)) > 0 Then
                    Line_X1 = Line_X2
                    Line_Y1 = 150 - CInt(param(1))
                    Line_X2 = Line_X1 + (CInt(param(4)) * 300) \ 運行時間
                    Line_Y2 = 150 - CInt(param(1))
                    g.DrawLine(eP, Line_X1, Line_Y1, Line_X2, Line_Y2)
                  End If
                End If
                Line_X_was = Line_X2
                Line_Y_Was = Line_Y2
                StepNumber = StepNumber + 1
              Else
                Line_X1 = Line_X_was
                Line_Y1 = Line_Y_Was
                '第二條線的開始溫度為前一個線的結束溫度，目前步驟的目標溫度跟前一個溫度比較，檢查是升溫還是降溫
                If CInt(param(1)) >= TempWas Then
                  Line_X2 = ((CInt(param(1)) - TempWas) * 10 * 300) \ (Gradient * 運行時間) + Line_X1
                Else
                  Line_X2 = ((TempWas - CInt(param(1))) * 10 * 300) \ (Gradient * 運行時間) + Line_X1
                End If
                Line_Y2 = 150 - CInt(param(1))
                g.DrawLine(eP, Line_X1, Line_Y1, Line_X2, Line_Y2)
                TempWas = CInt(param(1))
                '繪製持溫線
                If param.Length >= 5 Then
                  If CInt(param(4)) > 0 Then
                    Line_X1 = Line_X2
                    Line_Y1 = 150 - CInt(param(1))
                    Line_X2 = Line_X1 + (CInt(param(4)) * 300) \ 運行時間
                    Line_Y2 = 150 - CInt(param(1))
                    g.DrawLine(eP, Line_X1, Line_Y1, Line_X2, Line_Y2)
                  End If
                End If
                Line_X_was = Line_X2
                Line_Y_Was = Line_Y2
                StepNumber = StepNumber
              End If
            End If
            If i >= StepsData.Length - 1 And Not SetpointProfileFinish Then
              SetpointProfileFinish = True
            End If
          Next
          Dim j As Integer
          If 溫度(0) > 0 Then
            For j = 0 To 300
              g.DrawLine(eP2, ActualLine_X1(j), ActualLine_Y1(j), ActualLine_X2(j), ActualLine_Y2(j))
            Next
          End If
        End If
      End If
      g.Dispose()

      If Timer2.Enabled Then
        Timer2Count = Timer2Count + 1
        Label26.Text = Timer2Count.ToString
      Else
        Timer2Count = 0
        Label26.Text = Timer2Count.ToString
      End If
      If UpdateActualLine And .Setpoint > 0 Then
        溫度(ActualLineNo) = CInt(MainTempShow.Text)
        Label27.Text = Timer2Count.ToString + "," + ActualLine_X1.ToString + "," + ActualLine_Y1.ToString + "," + ActualLine_X2.ToString + "," + ActualLine_Y2.ToString + "," + 取樣時間.ToString
        Timer2Count = 0
        If ActualLineNo = 0 Then
          Timer2.Interval = 取樣時間 * 60000
          ActualLine_X1(0) = 0
          ActualLine_Y1(0) = 150 - 溫度(0)
          ActualLine_X2(0) = 0
          ActualLine_Y2(0) = 150 - 溫度(0)
          ActualLine_Xwas = 0
          ActualLine_Ywas = ActualLine_Y2(0)
        Else
          ActualLine_X1(ActualLineNo) = ActualLine_Xwas
          ActualLine_Y1(ActualLineNo) = ActualLine_Ywas
          ActualLine_X2(ActualLineNo) = ActualLine_X1(ActualLineNo) + 1
          ActualLine_Y2(ActualLineNo) = 150 - 溫度(ActualLineNo)
          ActualLine_Xwas = ActualLine_X2(ActualLineNo)
          ActualLine_Ywas = ActualLine_Y2(ActualLineNo)
        End If
        ActualLineNo = ActualLineNo + 1
        UpdateActualLine = False
      End If


    End With
  End Sub
  Public Timer2Count As Integer

  Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
    UpdateActualLine = True

  End Sub


  '  Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
  '    'MessageBox.Show(Me.Name)
  '    For i = 0 To Me.Parent.Parent.Parent.Parent.Controls.Count - 1
  '      Dim oo As Object
  '      oo = Me.Parent.Parent.Parent.Parent.Controls(i)
  '      'MessageBox.Show(Me.Parent.Parent.Parent.Parent.Controls(i).Name)
  '      If Me.Parent.Parent.Parent.Parent.Controls(i).GetType.Name = "MainView838" Then
  '        For j = 0 To Me.Parent.Parent.Parent.Parent.Controls(i).Controls.Count - 1
  '          If Me.Parent.Parent.Parent.Parent.Controls(i).Controls(j).GetType.Name = "ParameterEdit" Then
  '            Dim ooo As Object
  '            ooo = Me.Parent.Parent.Parent.Parent.Controls(i)
  '          End If
  '          'MessageBox.Show(Me.Parent.Parent.Parent.Parent.Controls(i).Controls(j).Name)
  '        Next
  '      End If
  '    Next

  '  End Sub

  '  Private bb As Button = Nothing
  '  <DllImport("user32.dll")> _
  'Shared Function IsWindowVisible( _
  ' ByVal hwnd As IntPtr _
  ' ) As Boolean
  '  End Function

  '  Private Sub Timer1_Tick_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
  '    For i = 0 To Me.Parent.Parent.Parent.Parent.Controls.Count - 1
  '      Dim oo As Object
  '      oo = Me.Parent.Parent.Parent.Parent.Controls(i)
  '      'MessageBox.Show(Me.Parent.Parent.Parent.Parent.Controls(i).Name)
  '      If Me.Parent.Parent.Parent.Parent.Controls(i).GetType.Name = "MainView838" Then
  '        For j = 0 To Me.Parent.Parent.Parent.Parent.Controls(i).Controls.Count - 1
  '          If Me.Parent.Parent.Parent.Parent.Controls(i).Controls(j).GetType.Name = "ParameterEdit" Then
  '            'Dim cc As ParameterEdit

  '            If bb Is Nothing Then
  '              bb = New Button
  '              bb.Location = New Point(20, 20)
  '              bb.Size = New Size(40, 40)
  '              bb.Parent = Me.Parent.Parent.Parent.Parent.Controls(i).Controls(j)
  '              bb.Text = "BB"
  '              bb.Visible = True
  '              bb.BringToFront()
  '            Else
  '              If IsWindowVisible(Me.Parent.Parent.Parent.Parent.Controls(i).Controls(j).Handle) = True Then
  '                bb.Text = "BBC"
  '              End If
  '            End If
  '            Dim ooo As Object
  '            ooo = Me.Parent.Parent.Parent.Parent.Controls(i).Name
  '            'ooo = Me.Parent.Parent.Parent.Controls(i)
  '          End If
  '          'MessageBox.Show(Me.Parent.Parent.Parent.Parent.Controls(i).Controls(j).Name)
  '        Next
  '      End If
  '    Next
  '  End Sub
End Class
