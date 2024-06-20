using Emulator_War_Thunder_Server_Forms;
using Newtonsoft.Json.Linq;
using System;
using System.Numerics;
using System.Security.Policy;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

public class CustomeVirtualInput
{
    private Action<string> console;
    private InputSimulator inputSimulator;
    private GameSettingsData gameSettingsData;

    public CustomeVirtualInput(Action<string> console)
    {
        this.console = console;
        inputSimulator = new InputSimulator();
    }

    private string currentLablePackage = "";

    private int currentGear = 0;
    private bool nowGearWork = false;

    private int moveMouseInterval = 10; // Интервал времени между обновлениями положения мыши в миллисекундах

    private Vector2 mouseMoveDirectionCommander = new Vector2(0, 0);
    private bool isMouseMovingCommander = false;
    private int moveStepCommander = 5; // Шаг перемещения мыши

    private bool isMouseMovingGunner = false;
    private Vector2 mouseMovePosGunner = new Vector2(0, 0);
    private Vector2 startMousePosGunner = new Vector2(0, 0);

    private bool loadGun = true;

    public async Task<bool> TryAction(Package package, GameSettingsData gameSettingsData)
    {
        this.gameSettingsData = gameSettingsData;
        currentLablePackage = $"{package.NameClient}({package.TypeCrew}) '{package.NamePackage}':";       
        if (package is PackageValueInt packageValueInt)
        {
            switch (package.NamePackage)
            {
                case "Gear":
                    await RunGearChanger(packageValueInt.ValueInt);
                    break;
                case "Turn":
                    Turn(packageValueInt.ValueInt);
                    break;
                case "Select Projectile":
                    await SelectProjectileActive(packageValueInt.ValueInt);
                    break;
                default:
                    break;
            }
        }
        else if(package is PackageValueBool packageValueBool)
        {
            switch (package.NamePackage)
            {
                case "Gas":
                    Gas(packageValueBool.Value);
                    break;
                case "Brake":
                    Brake(packageValueBool.Value);
                    break;
                case "SmoothTurn":
                    SmoothTurn(packageValueBool.Value);
                    break;
                case "Repair":
                    Repair(packageValueBool.Value);
                    break;
                case "Commander Move Joy Active":
                    MoveJoyActiveCommander(packageValueBool.Value);
                    break;
                case "Gunner Move Gun Active":
                    MoveGunActiveGunner(packageValueBool.Value);
                    break;
                case "Machine Gun Active":
                    MachineGun(packageValueBool.Value);
                    break;
                default:
                    break;
            }
        }
        else if (package is PackageValueVector2 packageValueVector2)
        {
            switch (package.NamePackage)
            {
                case "Commander Move Joy":
                    MoveMouseCommander(new Vector2(packageValueVector2.X, packageValueVector2.Y));
                    break;
                case "Gunner Move Mouse":
                    MoveMouseGunner(new Vector2(packageValueVector2.X, packageValueVector2.Y));
                    break;
                default:
                    break;
            }
        }
        else if (package is Package packageDef)
        {
            switch (package.NamePackage)
            {
                case "Engine Active":
                    await EngineActive();
                    break;
                case "Reconnaissance":
                    await ReconnaissanceActive();
                    break;
                case "Binoculars":
                    await BinocularsActive();
                    break;
                case "Optics":
                    await OpticsActive();
                    break;
                case "Shot":
                    await ShotActive();
                    break;
                case "Zoom":
                    await ZoomActive();
                    break;
                case "Target Distance":
                    await TargetDistanceActive();
                    break;
                case "Smoke":
                    await SmokeActive();
                    break;
                case "Firefighting":
                    await FirefightingActive();
                    break;
                case "Finish Reload":
                    FinishLoadGun();
                    break;
                default:
                    break;
            }
        }
        return true;
    }
    public void ResetGun()
    {
        loadGun = true;
    }
    private void FinishLoadGun()
    {
        console($"Gun is reloaded");
        loadGun = true;
    }
    private void Gas(bool value)
    {
        if (value)
        {
            VirtualJeyBoard.HoldKey(KEYCODE.VK_W);
        }
        else
        {
            VirtualJeyBoard.UpKey(KEYCODE.VK_W);
        }
        console($"{currentLablePackage} Gas: {value}");
    }
    private void Repair(bool value)
    {
        if (value)
        {
            VirtualJeyBoard.HoldKey(KEYCODE.VK_F);
        }
        else
        {
            VirtualJeyBoard.UpKey(KEYCODE.VK_F);
        }
        console($"{currentLablePackage} Repair: {value}");
    }

    private void Brake(bool value)
    {
        if (value)
        {
            VirtualJeyBoard.HoldKey(KEYCODE.VK_S);
        }
        else
        {
            VirtualJeyBoard.UpKey(KEYCODE.VK_S);
        }
        console($"{currentLablePackage} Brake: {value}");
    }
    private void MachineGun(bool value)
    {
        if (value)
        {
            VirtualJeyBoard.HoldKey(KEYCODE.VK_NUMPAD2);
        }
        else
        {
            VirtualJeyBoard.UpKey(KEYCODE.VK_NUMPAD2);
        }
        console($"{currentLablePackage} Machine Gun: {value}");
    }
    private async Task EngineActive()
    {
        console($"{currentLablePackage} Engine Active");
        VirtualJeyBoard.HoldKey(KEYCODE.VK_I);
        await Task.Delay(25);
        VirtualJeyBoard.UpKey(KEYCODE.VK_I);
    }
    private async Task BinocularsActive()
    {
        console($"{currentLablePackage} Binoculars Active");
        VirtualJeyBoard.HoldKey(KEYCODE.VK_E);
        await Task.Delay(25);
        VirtualJeyBoard.UpKey(KEYCODE.VK_E);
    }
    private async Task ReconnaissanceActive()
    {
        console($"{currentLablePackage} Reconnaissance Active");
        VirtualJeyBoard.HoldKey(KEYCODE.VK_V);
        await Task.Delay(25);
        VirtualJeyBoard.UpKey(KEYCODE.VK_V);
    }
    private async Task ShotActive()
    {
        if (gameSettingsData.checkBoxReloadGun.Checked)
        {
            if (loadGun)
            {
                console($"{currentLablePackage} Shot Active");
                gameSettingsData.mainGunShot.Invoke();
                loadGun = false;
                VirtualJeyBoard.HoldKey(KEYCODE.VK_NUMPAD7);
                await Task.Delay(25);
                VirtualJeyBoard.UpKey(KEYCODE.VK_NUMPAD7);
                gameSettingsData.broadcastMessage(gameSettingsData.packageFactory.GetPackage("Shot Main Gun"));
            }
            else
            {
                console($"{currentLablePackage} Gun not load");
            }
        }
        else
        {
            console($"{currentLablePackage} Shot Active");
            gameSettingsData.mainGunShot.Invoke();
            loadGun = false;
            VirtualJeyBoard.HoldKey(KEYCODE.VK_NUMPAD7);
            await Task.Delay(25);
            VirtualJeyBoard.UpKey(KEYCODE.VK_NUMPAD7);
            gameSettingsData.broadcastMessage(gameSettingsData.packageFactory.GetPackage("Shot Main Gun"));
        }
    }
    private async Task ZoomActive()
    {
        console($"{currentLablePackage} Zoom Active");
        VirtualJeyBoard.HoldKey(KEYCODE.VK_NUMPAD4);
        await Task.Delay(25);
        VirtualJeyBoard.UpKey(KEYCODE.VK_NUMPAD4);
    }
    private async Task TargetDistanceActive()
    {
        console($"{currentLablePackage} Target Distance Active");
        VirtualJeyBoard.HoldKey(KEYCODE.VK_NUMPAD1);
        await Task.Delay(25);
        VirtualJeyBoard.UpKey(KEYCODE.VK_NUMPAD1);
    }
    private async Task SmokeActive()
    {
        console($"{currentLablePackage} Smoke Active");
        VirtualJeyBoard.HoldKey(KEYCODE.VK_G);
        await Task.Delay(25);
        VirtualJeyBoard.UpKey(KEYCODE.VK_G);
    }
    private async Task FirefightingActive()
    {
        console($"{currentLablePackage} Firefighting Active");
        VirtualJeyBoard.HoldKey(KEYCODE.VK_6);
        await Task.Delay(25);
        VirtualJeyBoard.UpKey(KEYCODE.VK_6);
    }
    private async Task SelectProjectileActive(int idButton)
    {
        console($"{currentLablePackage} Select Projectile {idButton} Active");
        switch (idButton)
        {
            case 1:
                VirtualJeyBoard.HoldKey(KEYCODE.VK_1);
                await Task.Delay(25);
                VirtualJeyBoard.UpKey(KEYCODE.VK_1);
                break;
            case 2:
                VirtualJeyBoard.HoldKey(KEYCODE.VK_2);
                await Task.Delay(25);
                VirtualJeyBoard.UpKey(KEYCODE.VK_2);
                break;
            case 3:
                VirtualJeyBoard.HoldKey(KEYCODE.VK_3);
                await Task.Delay(25);
                VirtualJeyBoard.UpKey(KEYCODE.VK_3);
                break;
            case 4:
                VirtualJeyBoard.HoldKey(KEYCODE.VK_4);
                await Task.Delay(25);
                VirtualJeyBoard.UpKey(KEYCODE.VK_4);
                break;
            default:
                VirtualJeyBoard.HoldKey(KEYCODE.VK_1);
                await Task.Delay(25);
                VirtualJeyBoard.UpKey(KEYCODE.VK_1);
                break;
        }
        console($"{currentLablePackage} Select Projectile {idButton} Finish");
    }

    private async Task RunGearChanger(int newGear)
    {
        if (nowGearWork)
        {
            return;
        }
        else
        {
            nowGearWork = true;
        }

        console($"{currentLablePackage} Start Run Gear Chane to: {newGear}");

        newGear = Math.Clamp(newGear, -gameSettingsData.minGear, gameSettingsData.maxGear);

        //console($"After Clamp newGear: {newGear}");

        while (currentGear != newGear)
        {         
            if (currentGear < newGear)
            {
                currentGear++;
                VirtualJeyBoard.HoldKey(KEYCODE.VK_NUMPAD6);
                await Task.Delay(25);
                VirtualJeyBoard.UpKey(KEYCODE.VK_NUMPAD6);

                //console($"currentGear++ now: {currentGear}");
            }
            else
            {
                currentGear--;
                VirtualJeyBoard.HoldKey(KEYCODE.VK_NUMPAD3);
                await Task.Delay(25);
                VirtualJeyBoard.UpKey(KEYCODE.VK_NUMPAD3);

                //console($"currentGear-- now: {currentGear}");
            }

            console($"{currentLablePackage} Finish GearChanger current: {currentGear}");

            gameSettingsData.lableCurrentGear.Text = $"Current Gear: {currentGear}";

            await Task.Delay(250);
        }

        gameSettingsData.lableCurrentGear.Text = $"Current Gear: {currentGear}";

        nowGearWork = false;
    }
    public void ResetGear()
    {
        currentGear = 0;
        gameSettingsData.lableCurrentGear.Text = $"Current Gear: {currentGear}";
    }
    private void Turn(int value)
    {
        if (value == -1)
        {
            console($"{currentLablePackage} Start Turn to: -1 left");
            VirtualJeyBoard.HoldKey(KEYCODE.VK_A);
        }
        else if (value == 1)
        {
            console($"{currentLablePackage} Start Turn to 1 rigt");
            VirtualJeyBoard.HoldKey(KEYCODE.VK_D);
        }
        else
        {
            VirtualJeyBoard.UpKey(KEYCODE.VK_A);
            VirtualJeyBoard.UpKey(KEYCODE.VK_D);
            console($"{currentLablePackage} Stop Turn");
        }
    }
    private void SmoothTurn(bool value)
    {
        if (value)
        {
            VirtualJeyBoard.HoldKey(KEYCODE.VK_NUMPAD5);
        }
        else
        {
            VirtualJeyBoard.UpKey(KEYCODE.VK_NUMPAD5);
        }

        console($"{currentLablePackage} SmoothTurn {value}");
    }

    private void MoveMouseCommander(Vector2 vector2Joystick)
    {
        mouseMoveDirectionCommander = vector2Joystick;
        console($"{currentLablePackage} MoveMouse Joy X:{vector2Joystick.X}Y:{vector2Joystick.Y}");
        if (!isMouseMovingCommander)
        {
            isMouseMovingCommander = true;
            Task.Run(() => MouseMoveCommander());
        }
    }
    private void MoveJoyActiveCommander(bool value)
    {
        console($"{currentLablePackage} Move Joy Active Commander: {value}");
        if (value)
        {
            VirtualJeyBoard.HoldKey(KEYCODE.VK_C);
        }
        else
        {
            VirtualJeyBoard.UpKey(KEYCODE.VK_C);
            isMouseMovingCommander = false;
            mouseMoveDirectionCommander = Vector2.Zero;
        }
    }

    private async Task MouseMoveCommander()
    {
        while (isMouseMovingCommander && !gameSettingsData.sightingReticleActiveCheckBox.Checked && MainForm.inputAccept)
        {
            if (!isMouseMovingGunner)
            {
                // Получаем текущее положение мыши
                Vector2 currentPos = VirtualMouseMove.GetCursorPosition();

                // Вычисляем новые координаты мыши
                int newX = (int)(currentPos.X + mouseMoveDirectionCommander.X * moveStepCommander);
                int newY = (int)(currentPos.Y - mouseMoveDirectionCommander.Y * moveStepCommander);

                VirtualMouseMove.MoveMouseRelativeLocal(newX - (int)currentPos.X, newY - (int)currentPos.Y);

                // Ожидаем интервал перед следующим обновлением позиции мыши
                await Task.Delay(moveMouseInterval);
            }
        }

        isMouseMovingCommander = false;
    }
    private async Task OpticsActive()
    {
        console($"{currentLablePackage} Optics Active");

        gameSettingsData.sightingReticleActiveCheckBox.Checked = !gameSettingsData.sightingReticleActiveCheckBox.Checked;

        VirtualJeyBoard.HoldKey(KEYCODE.VK_LSHIFT);
        await Task.Delay(25);
        VirtualJeyBoard.UpKey(KEYCODE.VK_LSHIFT);
    }
    private void MoveGunActiveGunner(bool value)
    {
        console($"{currentLablePackage} Move Gun Active Gunner: {value}");
        if (value)
        {
            if (!isMouseMovingGunner)
            {
                startMousePosGunner = VirtualMouseMove.GetLocalCursorPosition();
                Task.Run(() => MouseMoveGunner());

                isMouseMovingGunner = true;
            }
        }
        else
        {
            isMouseMovingGunner = false;
        }
    }
    private void MoveMouseGunner(Vector2 vector2Joystick)
    {
        mouseMovePosGunner = vector2Joystick;
        console($"{currentLablePackage} MoveMouse Joy X:{vector2Joystick.X}Y:{vector2Joystick.Y}");     
    }
    private async Task MouseMoveGunner()
    {
        console("Start async Task MouseMoveGunner");

        while (isMouseMovingGunner && MainForm.inputAccept)
        {
            if (!isMouseMovingCommander)
            {
                // Calculate the new mouse position relative to the start position and joystick input
                int TargetX = (int)(startMousePosGunner.X - mouseMovePosGunner.X);
                int TargetY = (int)(startMousePosGunner.Y + mouseMovePosGunner.Y);

                Vector2 localMousePos = VirtualMouseMove.GetLocalCursorPosition();

                Vector2 moveDir = new Vector2(0, 0);

                float distance = Vector2.Distance(localMousePos, new Vector2(TargetX, TargetY));
                float maxSpeed = 10; // Adjust the max speed as needed
                float smoothingFactor = 0.1f; // Adjust the smoothing factor as needed
                float speed = Math.Min(maxSpeed, distance * smoothingFactor);

                float deadZone = speed + 0.1f;

                if (localMousePos.X - deadZone > TargetX)
                {
                    moveDir.X = -speed;
                }
                if (localMousePos.X + deadZone < TargetX)
                {
                    moveDir.X = speed;
                }

                if (localMousePos.Y - deadZone > TargetY)
                {
                    moveDir.Y = -speed;
                }
                if (localMousePos.Y + deadZone < TargetY)
                {
                    moveDir.Y = speed;
                }

                VirtualMouseMove.MoveMouseRelativeLocal((int)moveDir.X, (int)moveDir.Y);
            }

            await Task.Delay(moveMouseInterval);
        }

        console("Stop async Task MouseMoveGunner");
    }
}
