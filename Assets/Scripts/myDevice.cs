using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.LowLevel;
using System.Runtime.InteropServices;
using UnityEditor;

//입력이 수신되고 저장되는 형태를 나타내는 C# 구조를 생성
public struct myDeviceState : IInputStateTypeInfo
{
    // FourCC type codes are used to identify the memory layouts of state blocks.
    public FourCC format => new FourCC('M', 'D', 'E', 'V');

    [InputControl(name = "button", layout = "Button", bit = 4)]
    public int button;

}

[InputControlLayout(stateType = typeof(myDeviceState))]
#if UNITY_EDITOR
[InitializeOnLoad] // Make sure static constructor is called during startup.
#endif
public class myDevice : InputDevice
{
    public ButtonControl button { get; private set; }

    //생성자
    static myDevice()
    {
        InputSystem.RegisterLayout<myDevice>(
            matches : new InputDeviceMatcher()
            .WithInterface("HID")
            .WithCapability("PID", 0)
            .WithCapability("VID", 65535));
    }

    // This is only to trigger the static class constructor to automatically run
    // in the player.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeInPlayer() { Debug.Log("Startup"); }


    protected override void FinishSetup()
    //protected override void FinishSetup(InputControlSetup setup)
    {
        base.FinishSetup();
        button = GetChildControl<ButtonControl>("button");
    }

}

