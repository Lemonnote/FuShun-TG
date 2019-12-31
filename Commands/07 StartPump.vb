<Command("Start Pump", "Speed |1-100|% RoolSpeed |1-100|% Plaiting |1-100|"), _
 TranslateCommand("zh-TW", "起動馬達", "運轉速度 |1-100|% 布輪速度 |1-100|% 擺布速度 |1-100|%"), _
 Description("MAX=100% ,MIN=1%  MAX=100% ,MIN=1%  MAX=100% ,MIN=1%"), _
 TranslateDescription("zh-TW", "最高=100% ,最小=1%   最高=100% ,最小=1%  最高=100% ,最小=1%")> _
Public NotInheritable Class Command07
    Inherits MarshalByRefObject
    Implements ACCommand

    Public Enum S07
    Off
    WaitAuto
    WaitLowLevel
    WaitTime
    WaitMainPumpFB
    End Enum

    Public Wait As New Timer

    Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode
      .Command16.Cancel() : .Command02.Cancel() : .Command10.Cancel() : .Command03.Cancel() : .Command04.Cancel()
            .Command05.Cancel() : .Command06.Cancel() : .Command07.Cancel() : .Command08.Cancel()
            .Command09.Cancel() : .Command11.Cancel() : .Command12.Cancel() : .Command13.Cancel()
            .Command14.Cancel() : .Command15.Cancel() : .Command17.Cancel() : .Command18.Cancel() : .Command39.Cancel() : .Command40.Cancel()
            .Command19.Cancel() : .Command20.Cancel() : .Command21.Cancel() : .Command22.Cancel()
            .Command32.Cancel() : .Command40.Cancel() : .Command51.Cancel() : .Command52.Cancel()
            .Command54.Cancel() : .Command55.Cancel() : .Command57.Cancel() : .Command58.Cancel()
            .Command61.Cancel() : .Command62.Cancel() : .Command64.Cancel() : .Command65.Cancel()
            .Command61.Cancel() : .Command62.Cancel() : .Command64.Cancel() : .Command65.Cancel()
            .Command67.Cancel() : .Command68.Cancel() : .Command01.Cancel() : .Command90.Cancel()
            .TemperatureControl.Cancel()
            '--------------------------------------------------------------------------------------------------------PH用
            .PhControl.Cancel() : .PhWash.Cancel() : .PhControlFlag = False : .PhCirculateRun.Cancel() : .Command78.Cancel() : .Command80.Cancel()
            .Command73.Cancel() : .Command74.Cancel() : .Command75.Cancel() : .Command76.Cancel() : .Command77.Cancel() : .Command79.Cancel()
            '---------------------------------------------------------------------------------------------------------

      .PumpSpeed = MinMax(param(1), 1, 100)
      .RollerSpeed = MinMax(param(2), 1, 100)
      .PlaitingSpeed = MinMax(param(3), 1, 100)
      State = S07.WaitAuto
    End With
    End Function

    Public Function Run() As Boolean Implements ACCommand.Run
        With ControlCode
      Select Case State

        Case S07.WaitAuto
          If Not .IO.SystemAuto Then Exit Select
          State = S07.WaitLowLevel

        Case S07.WaitLowLevel
          If Not .IO.LowLevel Then Exit Select
          ' .IO.PumpSpeedControl = CType(.PumpSpeed * 10, Short)
          .PumpStartRequest = True
          Wait.TimeRemaining = 1
          State = S07.WaitTime

        Case S07.WaitTime
          If Not Wait.Finished Then Exit Select
          .PumpStartRequest = False
          .PumpOn = True
          State = S07.WaitMainPumpFB

        Case S07.WaitMainPumpFB
          If Not .IO.MainPumpFB Then Exit Select
          State = S07.Off
      End Select
        End With
    End Function

    Public Sub Cancel() Implements ACCommand.Cancel
        State = S07.Off
        Wait.Cancel()
  End Sub

  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged

  End Sub

#Region "Standard Definitions"
    Private ReadOnly ControlCode As ControlCode
    Public Sub New(ByVal controlCode As ControlCode)
        Me.ControlCode = controlCode
    End Sub
    Friend ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
        Get
            Return State <> S07.Off
        End Get
    End Property

    <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S07
    Public Property State() As S07
        Get
            Return state_
        End Get
        Private Set(ByVal value As S07)
            state_ = value
        End Set
    End Property
#End Region
End Class

#Region "Class Instance"
Partial Public Class ControlCode
  Public ReadOnly Command07 As New Command07(Me)
End Class
#End Region
