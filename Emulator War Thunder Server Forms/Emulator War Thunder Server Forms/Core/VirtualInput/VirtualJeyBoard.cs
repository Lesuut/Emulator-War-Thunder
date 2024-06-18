﻿using System.Runtime.InteropServices;

public enum KEYCODE
{
    VK_A = 0x41, VK_B = 0x42, VK_C = 0x43, VK_D = 0x44, VK_E = 0x45, VK_F = 0x46, VK_G = 0x47,
    VK_H = 0x48, VK_I = 0x49, VK_J = 0x4A, VK_K = 0x4B, VK_L = 0x4C, VK_M = 0x4D, VK_N = 0x4E, VK_O = 0x4F,
    VK_P = 0x50, VK_Q = 0x51, VK_R = 0x52, VK_S = 0x53, VK_T = 0x54, VK_U = 0x55, VK_V = 0x56, VK_W = 0x57,
    VK_X = 0x58, VK_Y = 0x59, VK_Z = 0x5A, VK_LSHIFT = 0xA0, VK_RSHIFT = 0xA1, VK_LCONTROL = 0xA2, VK_RCONTROL = 0xA3,

    // Top row number keys
    VK_0 = 0x30, VK_1 = 0x31, VK_2 = 0x32, VK_3 = 0x33,
    VK_4 = 0x34, VK_5 = 0x35, VK_6 = 0x36, VK_7 = 0x37,
    VK_8 = 0x38, VK_9 = 0x39,

    // Numpad keys
    VK_NUMPAD0 = 0x60, VK_NUMPAD1 = 0x61, VK_NUMPAD2 = 0x62, VK_NUMPAD3 = 0x63,
    VK_NUMPAD4 = 0x64, VK_NUMPAD5 = 0x65, VK_NUMPAD6 = 0x66, VK_NUMPAD7 = 0x67,
    VK_NUMPAD8 = 0x68, VK_NUMPAD9 = 0x69,

    // Numpad operators
    VK_MULTIPLY = 0x6A, VK_ADD = 0x6B, VK_SEPARATOR = 0x6C, VK_SUBTRACT = 0x6D,
    VK_DECIMAL = 0x6E, VK_DIVIDE = 0x6F,

    // Additional keys
    VK_NUMLOCK = 0x90, VK_SCROLL = 0x91
}

public static class VirtualJeyBoard
{
    [DllImport("user32.dll", SetLastError = true)]
    static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

    [DllImport("user32.dll")]
    private static extern uint MapVirtualKey(uint uCode, uint uMapType);

    const uint KEYEVENTF_KEYUP = 0x0002;
    const uint MAPVK_VK_TO_VSC_EX = 0x0004;

    public static void HoldKey(KEYCODE keycode)
    {
        byte scanCode = (byte)MapVirtualKey((uint)keycode, MAPVK_VK_TO_VSC_EX);
        keybd_event((byte)keycode, scanCode, 0, 0);
    }
    public static void UpKey(KEYCODE keycode)
    {
        byte scanCode = (byte)MapVirtualKey((uint)keycode, MAPVK_VK_TO_VSC_EX);
        keybd_event((byte)keycode, scanCode, KEYEVENTF_KEYUP, 0);
    }
}