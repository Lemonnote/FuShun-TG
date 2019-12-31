Imports System.Runtime.InteropServices

Public Class Mimic
  Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)

  End Sub

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
