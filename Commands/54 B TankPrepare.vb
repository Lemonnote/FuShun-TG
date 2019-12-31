<Command("B Tank Prepare", "Type|0-1| Qty|0-100| Mix|0-999|", , , "('4/60)+2"), _
TranslateCommand("zh-TW", "B備藥", "水源|0-1| 水量|0-100| 攪拌|0-999|"), _
Description("0=CIRCULATE 1=COLD Qty=0-100%,  Mix=0-999Seconds"), _
TranslateDescription("zh-TW", "0=備回水 1=備清水, Qty=0-100%, 攪拌=0-999秒")> _
Public NotInheritable Class Command54
  Inherits MarshalByRefObject                       'Inheritsg是繼承Windows Form的應用程式要繼承System.Windows.Forms.Form，可先參考物件導向程式設計相關書籍
  Implements ACCommand

  Public Enum S54
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
      '   End If

      Time = Maximum(param(3), 999)
      Type = MinMax(param(1), 0, 1)
      Qty = MinMax(param(2) * 10, 0, 1000)
      'HeatingSet = MinMax(param(3), 0, 99)
      .SetBTankTemperature = HeatingSet * 10
      .Command64.Cancel()
      State = S54.CheckReady
    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
    With ControlCode
      Select Case State
        Case S54.Off
          StateString = ""

        Case S54.CheckReady
          If .IO.BTankReady = True Then
            State = S54.Slow
          Else
            State = S54.WaitNoAddButtons
          End If

        Case S54.WaitNoAddButtons
          StateString = Translations.Translate("Tank B Interlocked")
          State = S54.FillToLowLevel
          .MessageSTankFilling = True

        Case S54.FillToLowLevel
          StateString = Translations.Translate("Filling Tank B to") & " " & "20" & "%"
          If .IO.TankBLevel > 200 Then
            .MessageSTankFilling = False
            .MessageSTankPrepare = True
            WaitReadyTimer.TimeRemaining = 5
            State = S54.Slow
            If CallOff > 0 And .Parameters.DyeEnable = 1 Then
              .DyeCallOff = 0   'Starts the handshake with the host / auto dispenser
              .DyeTank = 1
              State = S54.DispenseWaitReady
            End If
          End If
          If .Parent.IsPaused Or Not .IO.SystemAuto Then
            StateWas = State
            State = S54.Pause
            WaitTimer.Pause()
          End If

        Case S54.DispenseWaitReady
          StateString = Translations.Translate("Prepare Tank B")
          'TODO  Add timeout code to switch to manual if no response
          If .DyeState = EDispenseState.Ready Then
            'Dispenser is ready so set CallOff number and wait for result
            .DyeCallOff = CallOff
            .DyeTank = 1
            State = S54.DispenseWaitResponse
          End If
          'Switch to manual if enable parameter is changed or calloff is reset
          If .Parameters.DyeEnable <> 1 Then State = S54.Slow
          If CallOff = 0 Then State = S54.Slow

        Case S54.DispenseWaitResponse
          Select Case .DyeState
            Case EDispenseState.Complete
              'Everything completed ok so set ready flag and carry on
              .DyeCallOff = 0
              State = S54.Slow

            Case EDispenseState.Manual
              'Manual dispenses required so call the operator
              .DyeCallOff = 0
              State = S54.Slow

            Case EDispenseState.Error
              'Dispense error call the operator
              .DyeCallOff = 0
              State = S54.Slow
          End Select
          'Switch to manual if enable parameter is changed or calloff is reset
          If .Parameters.DyeEnable <> 1 Then State = S54.Slow
          If CallOff = 0 Then State = S54.Slow

        Case S54.Slow
          StateString = Translations.Translate("Prepare Tank B")
          If WaitReadyTimer.Finished Then
            .SideTankNotReady = False
          End If

          If .TankBReady Or (.Parameters.SideTankPrepareConfirm = 1) Then
            .SideTankNotReady = False
            If Qty > 5 Then
              State = S54.FillQty
            Else
              State = S54.MixForTime
            End If
          End If

        Case S54.FillQty
          StateString = Translations.Translate("Filling Tank B to") & " " & Qty / 10 & "%"
          If .Parent.IsPaused Then
            StateWas = State
            State = S54.Pause
            WaitTimer.Pause()
          End If
          If .Parent.IsPaused Or Not .IO.SystemAuto Then
            StateWas = State
            State = S54.Pause
            WaitTimer.Pause()
          End If
          If .IO.TankBLevel > Qty Then
            State = S54.MixForTime
          End If

        Case S54.MixForTime
          StateString = Translations.Translate("Tank B heating")
          If HeatingSet > 1 Then
            .BTankHeatStartRequest = True
          Else
            .BTankHeatStartRequest = False
          End If
          State = S54.WaitMixer

        Case S54.WaitMixer
          WaitTimer.TimeRemaining = Time
          StateString = Translations.Translate("Wait Tank C Mixing")
          If .TankCMix.IsMixingForTime And (.Parameters.CTankMixType = 0 And .Parameters.BTankMixType = 0) Then Exit Select
          State = S54.MixForTime1

        Case S54.MixForTime1
          StateString = Translations.Translate("Tank B mixing for") & " " & TimerString(WaitTimer.TimeRemaining)
          If .Parent.IsPaused Or Not .IO.SystemAuto Then
            StateWas = State
            State = S54.Pause
            WaitTimer.Pause()
          End If
          If WaitTimer.Finished And .IO.BTankTemperature >= .SetBTankTemperature Then
            State = S54.Ready
          End If

        Case S54.Ready
          .TankBReady = True
          .BTankHeatStartRequest = False
          State = S54.Off

        Case S54.Pause
          StateString = Translations.Translate("Paused") & " " & TimerString(WaitTimer.TimeRemaining)
          If Not .Parent.IsPaused And .IO.SystemAuto Then
            State = StateWas
            StateWas = S54.Off
            WaitTimer.Restart()
            .SetBTankTemperature = HeatingSet * 10
          End If

      End Select
    End With
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    State = S54.Off
    WaitTimer.Cancel()
    ControlCode.BTankHeatStartRequest = False
  End Sub
  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
    If State = S54.MixForTime Or State = S54.MixForTime1 Then

    Else
      Time = Maximum(param(4), 999)
    End If

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
      Return State <> S54.Off
    End Get
  End Property
  Public ReadOnly Property IsTankInterlocked() As Boolean
    Get
      Return (State = S54.WaitNoAddButtons)
    End Get
  End Property
  Public ReadOnly Property IsFillingFresh() As Boolean
    Get
      Return (Type = 1) And ((State = S54.FillQty) Or (State = S54.FillToLowLevel))
    End Get
  End Property
  Public ReadOnly Property IsFillingCirc() As Boolean
    Get
      Return (Type = 0) And ((State = S54.FillQty) Or (State = S54.FillToLowLevel))
    End Get
  End Property
  Public ReadOnly Property IsSlow() As Boolean
    Get
      Return (State = S54.Slow)
    End Get
  End Property
  Public ReadOnly Property IsMixingForTime() As Boolean
    Get
      Return (State = S54.MixForTime) Or (State = S54.MixForTime1)
    End Get
  End Property
  Public ReadOnly Property IsReady() As Boolean
    Get
      Return (State = S54.Ready)
    End Get
  End Property
  <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S54
  <EditorBrowsable(EditorBrowsableState.Advanced)> Private statewas_ As S54
  Public Property State() As S54
    Get
      Return state_
    End Get
    Private Set(ByVal value As S54)
      state_ = value
    End Set
  End Property
  Public Property StateWas() As S54
    Get
      Return statewas_
    End Get
    Private Set(ByVal value As S54)
      statewas_ = value
    End Set
  End Property
  'End Property

#End Region

End Class

#Region " Class Instance "

Partial Public Class ControlCode
  Public ReadOnly Command54 As New Command54(Me)
End Class

#End Region
