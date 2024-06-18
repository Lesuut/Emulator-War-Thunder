using System.Runtime.InteropServices;
using System;
using System.Numerics;

public class VirtualMouseMove
{
    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out Vector2 lpPoint);

    private static Vector2 localMousePos = new Vector2(0, 0);

    public static void MoveMouseRelative(int dx, int dy)
    {
        mouse_event((int)(MOUSEEVENTF.MOVE), dx, dy, 0, 0);
    }
    public static void MoveMouseRelativeLocal(int dx, int dy)
    {
        mouse_event((int)(MOUSEEVENTF.MOVE), dx, dy, 0, 0);
        localMousePos += new Vector2(dx, dy);
    }

    public static void MoveMouseAbsolute(int x, int y)
    {
        SetCursorPos(x, y);
    }

    public static Vector2 GetCursorPosition()
    {
        GetCursorPos(out Vector2 point);
        return point;
    }
    public static Vector2 GetLocalCursorPosition()
    {
        return localMousePos;
    }

    public static Vector2 GetAbsoluteCursorPosition()
    {
        Point cursorPosition = Cursor.Position;
        return new Vector2(cursorPosition.X, cursorPosition.Y);
    }

    public static void MoveMouseTo(int x, int y)
    {
        Vector2 currentPos = GetCursorPosition();
        float dx = x - currentPos.X;
        float dy = y - currentPos.Y;
        MoveMouseRelative((int)dx, (int)dy);
    }
}

public enum MOUSEEVENTF
{
    MOVE = 0x0001,
    LEFTDOWN = 0x0002,
    LEFTUP = 0x0004,
    RIGHTDOWN = 0x0008,
    RIGHTUP = 0x0010,
    MIDDLEDOWN = 0x0020,
    MIDDLEUP = 0x0040,
    XDOWN = 0x0080,
    XUP = 0x0100,
    WHEEL = 0x0800,
    HWHEEL = 0x01000,
    MOVE_NOCOALESCE = 0x2000,
    VIRTUALDESK = 0x4000,
    ABSOLUTE = 0x8000
}