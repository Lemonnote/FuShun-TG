<Command("Parallel C Prepare", "Type|0-1| Qty|0-100| Mix|0-999|", , , "('4/60)+2", CommandType.ParallelCommand), _
TranslateCommand("zh-TW", "平行C備藥", "水源|0-1| 水量|0-100|  攪拌|0-999|"), _
Description("0=CIRCULATE 1=COLD Qty=0-100%, 0=No Heat 1=Heat, Mix=0-999Seconds "), _
TranslateDescription("zh-TW", "0=備回水 1=備清水, Qty=0-100%, 0=不加熱 , 攪拌=0-999秒")> _
Public NotInheritable Class Command65
  Inherits MarshalByRefObject
  Implements ACCommand

  Public Enum S65
    Off
    CheckReady
    WaitNoAddButtons
    FillToLowLevel
    CheckReady1
    DispenseWaitReady
    DispenseWaitResponse
    Slow
    FillQty
    Ready
    MixForTime
    MixForTime1
    WaitMixer
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
      .Command55.Cancel()
      .TankCMix.Cancel()
      Time = Maximum(param(3), 999)
      Type = MinMax(param(1), 0, 1)
      Qty = MinMax(param(2) * 10, 0, 1000)
      'HeatingSet = MinMax(param(3), 0, 99)
      .SetCTankTemperature = HeatingSet * 10
      State = S65.CheckReady
    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
    With ControlCode
      Select Case State
        Case S65.Off
          StateString = ""

        Case S65.CheckReady
          If .Parent.IsPaused Then
            StateWas = State
            State = S65.Pause
            WaitTimer.Pause()
          End If
          If .IO.CTankReady = True Then
            State = S65.Slow
          Else
            State = S65.WaitNoAddButtons
          End If

        Case S65.WaitNoAddButtons
          StateString = If(.Language = LanguageValue.ZhTw, "等待藥缸", "Tank C Interlocked")
          If .Parent.IsPaused Then
            StateWas = State
            State = S65.Pause
            WaitTimer.Pause()
          End If
          If .Command51.IsOn Or .Command52.IsOn Or .Command55.IsOn Or .Command57.IsOn Then Exit Select
          State = S65.FillToLowLevel
          .MessageSTankFilling = True

        Case S65.FillToLowLevel
          If .Parameters.C藥缸水位種類 = 1 Then
            StateString = If(.Language = LanguageValue.ZhTw, "C藥缸進水至 ", "Filling Tank C to ") & "Low"
          Else
            StateString = If(.Language = LanguageValue.ZhTw, "C藥缸進水至 ", "Filling Tank C to ") & Qty / 10 & "%"
          End If

          If .Parent.IsPaused Or Not .IO.SystemAuto Then
            StateWas = State
            State = S65.Pause
            WaitTimer.Pause()
          End If
          If .IO.TankCLevel > Qty Or (.Parameters.C藥缸水位種類 = 1 And .IO.CTankLow) Then
            .MessageSTankFilling = False
            .MessageSTankPrepare = True
            WaitReadyTimer.TimeRemaining = 5
            State = S65.Slow
            If CallOff > 0 And .Parameters.ChemicalEnable = 1 Then
              .ChemicalCallOff = 0   'Starts the handshake with the host / auto dispenser
              .ChemicalTank = 1
              State = S65.DispenseWaitReady
            End If
          End If

        Case S65.DispenseWaitReady
          StateString = If(.Language = LanguageValue.ZhTw, "助劑備藥中", "Prepare Tank C")
          'TODO  Add timeout code to switch to manual if no response
          If .ChemicalState = EDispenseState.Ready Then
            'Dispenser is ready so set CallOff number and wait for result
            .ChemicalCallOff = CallOff
            .ChemicalTank = 1
            State = S65.DispenseWaitResponse
          End If
          'Switch to manual if enable parameter is changed or calloff is reset
          If .Parameters.ChemicalEnable <> 1 Then State = S65.Slow
          If CallOff = 0 Then State = S65.Slow

        Case S65.DispenseWaitResponse
          Select Case .ChemicalState
            Case EDispenseState.Complete
              'Everything completed ok so set ready flag and carry on
              .ChemicalCallOff = 0
              State = S65.Slow

            Case EDispenseState.Manual
              'Manual dispenses required so call the operator
              .ChemicalCallOff = 0
              State = S65.Slow

            Case EDispenseState.Error
              'Dispense error call the operator
              .ChemicalCallOff = 0
              State = S65.Slow
          End Select
          'Switch to manual if enable parameter is changed or calloff is reset
          If .Parameters.ChemicalEnable <> 1 Then State = S65.Slow
          If CallOff = 0 Then State = S65.Slow

        Case S65.Slow
          StateString = If(.Language = LanguageValue.ZhTw, "備藥完成，請按備藥OK", "Prepare Tank C")
          If .Parent.IsPaused Then
            StateWas = State
            State = S65.Pause
            WaitTimer.Pause()
          End If
          If WaitReadyTimer.Finished Then
            .SideTankNotReady = False
          End If
          If .TankCReady Or (.Parameters.SideTankPrepareConfirm = 1) Then
            .SideTankNotReady = False

            State = S65.FillQty

          End If

        Case S65.FillQty
          If .Parameters.C藥缸水位種類 = 1 Then
            StateString = If(.Language = LanguageValue.ZhTw, "C藥缸進水至 ", "Filling Tank C to ") & "Hight"
          Else
            StateString = If(.Language = LanguageValue.ZhTw, "C藥缸進水至 ", "Filling Tank C to ") & Qty / 10 & "%"
          End If
          If .Parent.IsPaused Then
            StateWas = State
            State = S65.Pause
            WaitTimer.Pause()
          End If
          If .Parent.IsPaused Or Not .IO.SystemAuto Then
            StateWas = State
            State = S65.Pause
            WaitTimer.Pause()
          End If
          If (.IO.TankCLevel > Qty And .Parameters.C藥缸水位種類 = 0) Or (.Parameters.C藥缸水位種類 = 1 And .IO.CTankHigh) Then
            State = S65.MixForTime
          End If

        Case S65.MixForTime
          StateString = Translations.Translate("Tank C heating")
          If HeatingSet > 1 Then
            .CTankHeatStartRequest = True
          Else
            .CTankHeatStartRequest = False
          End If
          State = S65.WaitMixer

        Case S65.WaitMixer
          WaitTimer.TimeRemaining = Time
          StateString = Translations.Translate("Wait Tank B Mixing")
          If .TankBMix.IsMixingForTime And (.Parameters.CTankMixType = 0 And .Parameters.BTankMixType = 0) Then Exit Select
          State = S65.MixForTime1

        Case S65.MixForTime1
          StateString = Translations.Translate("Tank C mixing for") & " " & TimerString(WaitTimer.TimeRemaining)
          If .Parent.IsPaused Or Not .IO.SystemAuto Then
            StateWas = State
            State = S65.Pause
            WaitTimer.Pause()
          End If
          If WaitTimer.Finished And .IO.CTankTemperature >= .SetCTankTemperature Then
            State = S65.Ready
          End If

        Case S65.Ready
          .TankCReady = True
          .CTankHeatStartRequest = False
          State = S65.Off


        Case S65.Ready
          .TankCMixOn = True
          State = S65.Off

        Case S65.Pause
          StateString = If(.Language = LanguageValue.ZhTw, "暫停", "Paused") & " " & TimerString(WaitTimer.TimeRemaining)
          If .Parent.CurrentStep <> .Parent.ChangingStep Then
            State = S65.Off
            WaitTimer.Cancel()
          End If
          If Not .Parent.IsPaused And .IO.SystemAuto Then
            State = StateWas
            StateWas = S65.Off
            WaitTimer.Restart()
            .SetCTankTemperature = HeatingSet * 10
          End If

      End Select
    End With
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    State = S65.Off
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
      Return State <> S65.Off
    End Get
  End Property
  Public ReadOnly Property IsTankInterlocked() As Boolean
    Get
      Return (State = S65.WaitNoAddButtons)
    End Get
  End Property
  Public ReadOnly Property IsFillingFresh() As Boolean
    Get
      Return (Type = 1) And ((State = S65.FillQty) Or (State = S65.FillToLowLevel))
    End Get
  End Property
  Public ReadOnly Property IsFillingCirc() As Boolean
    Get
      Return (Type = 0) And ((State = S65.FillQty) Or (State = S65.FillToLowLevel))
    End Get
  End Property
  Public ReadOnly Property IsSlow() As Boolean
    Get
      Return (State = S65.Slow)
    End Get
  End Property
  Public ReadOnly Property IsReady() As Boolean
    Get
      Return (State = S65.Ready)
    End Get
  End Property
  Public ReadOnly Property IsMixingForTime() As Boolean
    Get
      Return (State = S65.MixForTime) Or (State = S65.MixForTime1)
    End Get
  End Property
  <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S65
  <EditorBrowsable(EditorBrowsableState.Advanced)> Private statewas_ As S65
  Public Property State() As S65
    Get
      Return state_
    End Get
    Private Set(ByVal value As S65)
      state_ = value
    End Set
  End Property
  Public Property StateWas() As S65
    Get
      Return statewas_
    End Get
    Private Set(ByVal value As S65)
      statewas_ = value
    End Set
  End Property

#End Region

End Class

#Region " Class Instance "

Partial Public Class ControlCode
  Public ReadOnly Command65 As New Command65(Me)
End Class

#End Region
