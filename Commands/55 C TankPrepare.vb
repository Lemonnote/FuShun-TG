<Command("C Tank Prepare", "Type|0-1| Qty|0-100| Mix|0-999|", , , "('4/60)+2"), _
TranslateCommand("zh-TW", "C備藥", "水源|0-1| 水量|0-100| 攪拌|0-999|"), _
Description("0=CIRCULATE 1=COLD Qty=0-100%, Mix=0-999Seconds"), _
TranslateDescription("zh-TW", "0=備回水 1=備清水, Qty=0-100%, 攪拌=0-999秒")> _
Public NotInheritable Class Command55
  Inherits MarshalByRefObject
  Implements ACCommand

  Public Enum S55
    Off
    CheckReady
    WaitNoAddButtons
    FillToLowLevel
    CheckReady1
    DispenseWaitReady
    DispenseWaitResponse
    Slow
    FillQty
    MixForTime
    WaitMixer
    MixForTime1
    Ready
    Pause
  End Enum
  Public StateString As String

  Public Time, Type, Qty, CallOff As Integer
  Public LaSystem As Boolean
  Public HeatingSet As Integer
  Public WaitTimer As New Timer
  Public WaitReadyTimer As New Timer

  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode
      .Command01.Cancel() : .Command02.Cancel() : .Command10.Cancel() : .Command03.Cancel() : .Command04.Cancel()
      .Command05.Cancel() : .Command06.Cancel() : .Command07.Cancel() : .Command08.Cancel()
      .Command09.Cancel() : .Command11.Cancel() : .Command12.Cancel() : .Command13.Cancel()
      .Command14.Cancel() : .Command15.Cancel() : .Command17.Cancel() : .Command18.Cancel()
      .Command19.Cancel() : .Command20.Cancel() : .Command21.Cancel() : .Command22.Cancel()
      .Command32.Cancel() : .Command40.Cancel() : .Command51.Cancel() : .Command52.Cancel()
      .Command54.Cancel() : .Command55.Cancel() : .Command57.Cancel() : .Command58.Cancel()
      .Command61.Cancel() : .Command62.Cancel() : .Command64.Cancel() : .Command65.Cancel()
      .Command61.Cancel() : .Command62.Cancel() : .Command64.Cancel() : .Command65.Cancel()
      .Command67.Cancel() : .Command68.Cancel() : .Command90.Cancel()
      ' If ((.TemperatureControl.TempFinalTemp - .IO.MainTemperature) > 20 Or (.IO.MainTemperature - .TemperatureControl.TempFinalTemp) > 20) And .HeatNow Then
      .TemperatureControl.Cancel()
      .TemperatureControlFlag = False
      ' End If

      Time = Maximum(param(3), 999)
      Type = MinMax(param(1), 0, 1)
      Qty = MinMax(param(2) * 10, 0, 1000)
      ' HeatingSet = MinMax(param(3), 0, 99)
      .SetCTankTemperature = HeatingSet * 10
      State = S55.CheckReady
    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
    With ControlCode
      Select Case State
        Case S55.Off
          StateString = ""

        Case S55.CheckReady
          If .IO.CTankReady = True Then
            State = S55.Slow
          Else
            State = S55.WaitNoAddButtons
          End If

        Case S55.WaitNoAddButtons
          StateString = Translations.Translate("Tank C Interlocked")
          State = S55.FillToLowLevel
          .MessageSTankFilling = True

        Case S55.FillToLowLevel
          If .Parameters.C藥缸水位種類 = 1 Then
            StateString = Translations.Translate("Filling Tank C to") & "Low"
          Else
            StateString = Translations.Translate("Filling Tank C to") & " " & Qty / 10 & "%"
          End If

          If .Parent.IsPaused Or Not .IO.SystemAuto Then
            StateWas = State
            State = S55.Pause
            WaitTimer.Pause()
          End If

          If .IO.TankCLevel > Qty Or (.Parameters.C藥缸水位種類 = 1 And .IO.CTankLow) Then
            .MessageSTankFilling = False
            .MessageSTankPrepare = True
            WaitReadyTimer.TimeRemaining = 5
            State = S55.Slow
            If CallOff > 0 And .Parameters.ChemicalEnable = 1 Then
              .ChemicalCallOff = 0   'Starts the handshake with the host / auto dispenser
              .ChemicalTank = 1
              State = S55.DispenseWaitReady
            End If
          End If

        Case S55.DispenseWaitReady
          StateString = Translations.Translate("Prepare Tank C")
          'TODO  Add timeout code to switch to manual if no response
          If .ChemicalState = EDispenseState.Ready Then
            'Dispenser is ready so set CallOff number and wait for result
            .ChemicalCallOff = CallOff
            .ChemicalTank = 1
            State = S55.DispenseWaitResponse
          End If
          'Switch to manual if enable parameter is changed or calloff is reset
          If .Parameters.ChemicalEnable <> 1 Then State = S55.Slow
          If CallOff = 0 Then State = S55.Slow

        Case S55.DispenseWaitResponse
          Select Case .ChemicalState
            Case EDispenseState.Complete
              'Everything completed ok so set ready flag and carry on
              .ChemicalCallOff = 0
              State = S55.Slow

            Case EDispenseState.Manual
              'Manual dispenses required so call the operator
              .ChemicalCallOff = 0
              State = S55.Slow

            Case EDispenseState.Error
              'Dispense error call the operator
              .ChemicalCallOff = 0
              State = S55.Slow
          End Select
          'Switch to manual if enable parameter is changed or calloff is reset
          If .Parameters.ChemicalEnable <> 1 Then State = S55.Slow
          If CallOff = 0 Then State = S55.Slow

        Case S55.Slow
          StateString = Translations.Translate("Prepare Tank C")
          If WaitReadyTimer.Finished Then
            .SideTankNotReady = False
          End If
          If .TankCReady Or (.Parameters.SideTankPrepareConfirm = 1) Then
            .SideTankNotReady = False
            State = S55.FillQty
          End If


        Case S55.FillQty
          If .Parameters.C藥缸水位種類 = 1 Then
            StateString = Translations.Translate("Filling Tank C to") & "Hight"
          Else
            StateString = Translations.Translate("Filling Tank C to") & " " & Qty / 10 & "%"
          End If
          If .Parent.IsPaused Or Not .IO.SystemAuto Then
            StateWas = State
            State = S55.Pause
            WaitTimer.Pause()
          End If
          If (.IO.TankCLevel > Qty And .Parameters.C藥缸水位種類 = 0) Or (.Parameters.C藥缸水位種類 = 1 And .IO.CTankHigh) Then
            State = S55.MixForTime
          End If

        Case S55.MixForTime
          StateString = Translations.Translate("Tank C heating")
          If HeatingSet > 1 Then
            .CTankHeatStartRequest = True
          Else
            .CTankHeatStartRequest = False
          End If
          State = S55.WaitMixer

        Case S55.WaitMixer
          WaitTimer.TimeRemaining = Time
          StateString = Translations.Translate("Wait Tank B Mixing")
          If .TankBMix.IsMixingForTime And (.Parameters.CTankMixType = 0 And .Parameters.BTankMixType = 0) Then Exit Select
          State = S55.MixForTime1

        Case S55.MixForTime1
          StateString = Translations.Translate("Tank C mixing for") & " " & TimerString(WaitTimer.TimeRemaining)
          If .Parent.IsPaused Or Not .IO.SystemAuto Then
            StateWas = State
            State = S55.Pause
            WaitTimer.Pause()
          End If
          If WaitTimer.Finished And .IO.CTankTemperature >= .SetCTankTemperature Then
            State = S55.Ready
          End If

        Case S55.Ready
          .TankCReady = True
          .CTankHeatStartRequest = False
          State = S55.Off

        Case S55.Pause
          StateString = Translations.Translate("Paused") & " " & TimerString(WaitTimer.TimeRemaining)
          If Not .Parent.IsPaused Or .IO.SystemAuto Then
            State = StateWas
            StateWas = S55.Off
            WaitTimer.Restart()
            .SetCTankTemperature = HeatingSet * 10
          End If

      End Select
    End With
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    State = S55.Off
    WaitTimer.Cancel()
    ControlCode.CTankHeatStartRequest = False
  End Sub
  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
    Time = Maximum(param(4), 999)
    Type = MinMax(param(1), 0, 1)
    Qty = MinMax(param(2) * 10, 0, 1000)
    HeatingSet = MinMax(param(3), 0, 99)
  End Sub

#Region " Standard Definitions "

  Private ReadOnly ControlCode As ControlCode
  Public Sub New(ByVal controlCode As ControlCode)
    Me.ControlCode = controlCode
  End Sub
  Friend ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
    Get
      Return State <> S55.Off
    End Get
  End Property
  Public ReadOnly Property IsTankInterlocked() As Boolean
    Get
      Return (State = S55.WaitNoAddButtons)
    End Get
  End Property
  Public ReadOnly Property IsFillingFresh() As Boolean
    Get
      Return (Type = 1) And ((State = S55.FillQty) Or (State = S55.FillToLowLevel))
    End Get
  End Property
  Public ReadOnly Property IsFillingCirc() As Boolean
    Get
      Return (Type = 0) And ((State = S55.FillQty) Or (State = S55.FillToLowLevel))
    End Get
  End Property
  Public ReadOnly Property IsSlow() As Boolean
    Get
      Return (State = S55.Slow)
    End Get
  End Property
  Public ReadOnly Property IsMixingForTime() As Boolean
    Get
      Return (State = S55.MixForTime) Or (State = S55.MixForTime1)
    End Get
  End Property
  Public ReadOnly Property IsReady() As Boolean
    Get
      Return (State = S55.Ready)
    End Get
  End Property
  <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S55
  <EditorBrowsable(EditorBrowsableState.Advanced)> Private statewas_ As S55
  Public Property State() As S55
    Get
      Return state_
    End Get
    Private Set(ByVal value As S55)
      state_ = value
    End Set
  End Property
  Public Property StateWas() As S55
    Get
      Return statewas_
    End Get
    Private Set(ByVal value As S55)
      statewas_ = value
    End Set
  End Property

#End Region

End Class

#Region " Class Instance "

Partial Public Class ControlCode
  Public ReadOnly Command55 As New Command55(Me)
End Class

#End Region
