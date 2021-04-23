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

//입력이 수신되고 저장되는 형태를 나타내는 C# 구조를 생성하고
//해당 상태를 검색하기 위해 장치에 대해 생성되어야 하는 입력 제어 인스턴스를 설명
[StructLayout(LayoutKind.Explicit, Size =8)]
public struct myDeviceState : IInputStateTypeInfo
{
    //public FourCC format => throw new System.NotImplementedException();

    // Every state format is tagged with a FourCC code that is used for type
    // checking. The characters can be anything. Choose something that allows
    // you do easily recognize memory belonging to your own device.
    public FourCC format => new FourCC('M', 'D', 'E', 'V');

    // InputControlAttributes on fields tell the input system to create controls
    // for the public fields found in the struct.
    [InputControl(name = "button", layout = "Button", bit = 1)]
    public short button;

}

//기본 클래스 중 하나에서 파생된 클래스가 필요
[InputControlLayout(displayName = "My Device", stateType = typeof(myDeviceState))]
public class myDevice : InputDevice, IInputUpdateCallbackReceiver
{
#if UNITY_EDITOR
    static myDevice()
    {
        Initialize();
    }

#endif

    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        InputSystem.RegisterLayout<myDevice>(
            matches: new InputDeviceMatcher()
                .WithInterface("myDeviceInput"));
        InputSystem.AddDevice<myDevice>();
    }
    /*
    //[ContextMenuItem("Tools/Add MyDevice")]
    public static void Initialize()
    {
        InputSystem.AddDevice<myDevice>();
    }
    */

    [InputControl]
    public ButtonControl button { get; private set; }

    protected override void FinishSetup()
    {
        base.FinishSetup();
        button = GetChildControl<ButtonControl>(path: "button");
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
        // In practice, this is where we would be reading out data from an external
        // API. Instead, here we just make up some (empty) input.
        var state = new myDeviceState();
        InputSystem.QueueStateEvent(this, state);
    }

    [RuntimeInitializeOnLoadMethod]
    private static void InitializeInPlayer() { }


}

