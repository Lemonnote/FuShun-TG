<Command("Parallel B Prepare", "Type|0-1| Qty|0-100| Mix|0-999|", , , "('4/60)+2", CommandType.ParallelCommand), _
TranslateCommand("zh-TW", "平行B備藥", "水源|0-1| 水量|0-100| 攪拌|0-999|"), _
Description("0=CIRCULATE 1=COLD Qty=0-100%, 0=No Heat, Mix=0-999Seconds"), _
TranslateDescription("zh-TW", "0=備回水 1=備清水, Qty=0-100%, 0=不加熱, 攪拌=0-999秒")> _
Public NotInheritable Class Command64
  Inherits MarshalByRefObject                       'Inheritsg是繼承Windows Form的應用程式要繼承System.Windows.Forms.Form，可先參考物件導向程式設計相關書籍
  Implements ACCommand

  Public Enum S64
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
      .Command54.Cancel()

      Time = Maximum(param(3), 999)
      Type = MinMax(param(1), 0, 1)
      Qty = MinMax(param(2) * 10, 0, 1000)
      ' HeatingSet = MinMax(param(3), 0, 99)
      .SetBTankTemperature = HeatingSet * 10
      State = S64.CheckReady
    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
    With ControlCode
      Select Case State
        Case S64.Off
          StateString = ""

        Case S64.CheckReady
          If .Parent.IsPaused Then
            StateWas = State
            State = S64.Pause
            WaitTimer.Pause()
          End If
          If .IO.BTankReady = True Then
            State = S64.Slow
          Else
            State = S64.WaitNoAddButtons
          End If

        Case S64.WaitNoAddButtons
          StateString = If(.Language = LanguageValue.ZhTw, "等待藥缸", "Tank B Interlocked")
          If .Parent.IsPaused Then
            StateWas = State
            State = S64.Pause
            WaitTimer.Pause()
          End If
          If .Command51.IsOn Or .Command52.IsOn Or .Command54.IsOn Or .Command55.IsOn Or _
          .Command57.IsOn Or .Command67.IsOn Then Exit Select
          State = S64.FillToLowLevel
          .MessageSTankFilling = True

        Case S64.FillToLowLevel
          StateString = If(.Language = LanguageValue.ZhTw, "B藥缸進水 ", "Filling Tank B to ") & "20" & "%"

          If .Parent.IsPaused Or Not .IO.SystemAuto Then
            StateWas = State
            State = S64.Pause
            WaitTimer.Pause()
          End If
          If .IO.TankBLevel > 200 Then
            .MessageSTankFilling = False
            .MessageSTankPrepare = True
            WaitReadyTimer.TimeRemaining = 5
            State = S64.Slow
            If CallOff > 0 And .Parameters.DyeEnable = 1 Then
              .DyeCallOff = 0   'Starts the handshake with the host / auto dispenser
              .DyeTank = 1
              State = S64.DispenseWaitReady
            End If
          End If

        Case S64.DispenseWaitReady
          StateString = If(.Language = LanguageValue.ZhTw, "染料備藥中", "Prepare Tank B")
          'TODO  Add timeout code to switch to manual if no response
          If .DyeState = EDispenseState.Ready Then
            'Dispenser is ready so set CallOff number and wait for result
            .DyeCallOff = CallOff
            .DyeTank = 1
            State = S64.DispenseWaitResponse
          End If
          'Switch to manual if enable parameter is changed or calloff is reset
          If .Parameters.DyeEnable <> 1 Then State = S64.Slow
          If CallOff = 0 Then State = S64.Slow

        Case S64.DispenseWaitResponse
          Select Case .DyeState
            Case EDispenseState.Complete
              'Everything completed ok so set ready flag and carry on
              .DyeCallOff = 0
              State = S64.Slow

            Case EDispenseState.Manual
              'Manual dispenses required so call the operator
              .DyeCallOff = 0
              State = S64.Slow

            Case EDispenseState.Error
              'Dispense error call the operator
              .DyeCallOff = 0
              State = S64.Slow
          End Select
          'Switch to manual if enable parameter is changed or calloff is reset
          If .Parameters.DyeEnable <> 1 Then State = S64.Slow
          If CallOff = 0 Then State = S64.Slow

        Case S64.Slow
          StateString = If(.Language = LanguageValue.ZhTw, "備藥完成，請按備藥OK", "Prepare Tank C")

          If WaitReadyTimer.Finished Then
            .SideTankNotReady = False
          End If
          If .TankBReady Or (.Parameters.SideTankPrepareConfirm = 1) Then
            .SideTankNotReady = False
            If Qty > 5 Then
              State = S64.FillQty
            Else
              State = S64.MixForTime
            End If
          End If

        Case S64.FillQty
          StateString = Translations.Translate("Filling Tank B to") & " " & Qty / 10 & "%"
          If .Parent.IsPaused Then
            StateWas = State
            State = S64.Pause
            WaitTimer.Pause()
          End If
          If .Parent.IsPaused Or Not .IO.SystemAuto Then
            StateWas = State
            State = S64.Pause
            WaitTimer.Pause()
          End If
          If .IO.TankBLevel > Qty Then
            State = S64.MixForTime
          End If

        Case S64.MixForTime
          StateString = Translations.Translate("Tank B heating")
          If HeatingSet > 1 Then
            .BTankHeatStartRequest = True
          Else
            .BTankHeatStartRequest = False
          End If
          State = S64.WaitMixer

        Case S64.WaitMixer
          WaitTimer.TimeRemaining = Time
          StateString = Translations.Translate("Wait Tank C Mixing")
          If .TankCMix.IsMixingForTime And (.Parameters.CTankMixType = 0 And .Parameters.BTankMixType = 0) Then Exit Select
          State = S64.MixForTime1

        Case S64.MixForTime1
          StateString = Translations.Translate("Tank B mixing for") & " " & TimerString(WaitTimer.TimeRemaining)
          If .Parent.IsPaused Then
            StateWas = State
            State = S64.Pause
            WaitTimer.Pause()
          End If
          If WaitTimer.Finished And .IO.BTankTemperature >= .SetBTankTemperature Then
            State = S64.Ready
          End If

        Case S64.Ready
          .TankBReady = True
          .BTankHeatStartRequest = False
          State = S64.Off

        Case S64.Pause
          StateString = If(.Language = LanguageValue.ZhTw, "暫停", "Paused") & " " & TimerString(WaitTimer.TimeRemaining)
          If .Parent.CurrentStep <> .Parent.ChangingStep Then
            State = S64.Off
            WaitTimer.Cancel()
          End If

          If Not .Parent.IsPaused And .IO.SystemAuto Then
            State = StateWas
            StateWas = S64.Off
            WaitTimer.Restart()
            .SetBTankTemperature = HeatingSet * 10
          End If


      End Select

    End With
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    State = S64.Off
    WaitTimer.Cancel()
    ControlCode.BTankHeatStartRequest = False
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
      Return State <> S64.Off
    End Get
  End Property
  Public ReadOnly Property IsTankInterlocked() As Boolean
    Get
      Return (State = S64.WaitNoAddButtons)
    End Get
  End Property
  Public ReadOnly Property IsFillingFresh() As Boolean
    Get
      Return (Type = 1) And ((State = S64.FillQty) Or (State = S64.FillToLowLevel))
    End Get
  End Property
  Public ReadOnly Property IsFillingCirc() As Boolean
    Get
      Return (Type = 0) And ((State = S64.FillQty) Or (State = S64.FillToLowLevel))
    End Get
  End Property
  Public ReadOnly Property IsSlow() As Boolean
    Get
      Return (State = S64.Slow)
    End Get
  End Property
  Public ReadOnly Property IsMixingForTime() As Boolean
    Get
      Return (State = S64.MixForTime) Or (State = S64.MixForTime1)
    End Get
  End Property
  Public ReadOnly Property IsReady() As Boolean
    Get
      Return (State = S64.Ready)
    End Get
  End Property
  <EditorBrowsable(EditorBrowsableState.Advanced)> Private state_ As S64
  <EditorBrowsable(EditorBrowsableState.Advanced)> Private statewas_ As S64
  Public Property State() As S64
    Get
      Return state_
    End Get
    Private Set(ByVal value As S64)
      state_ = value
    End Set
  End Property
  Public Property StateWas() As S64
    Get
      Return statewas_
    End Get
    Private Set(ByVal value As S64)
      statewas_ = value
    End Set
  End Property
  'Property score() As Integer
  '    Get
  '        score = a           '讀取私有資料成員a的值
  '    End Get
  '
  '    Set(ByVal Value As Integer)
  '        a = Value           '設定私有資料成員a的值
  '    End Set
  'End Property

#End Region

End Class

#Region " Class Instance "

Partial Public Class ControlCode
  Public ReadOnly Command64 As New Command64(Me)
End Class

#End Region
