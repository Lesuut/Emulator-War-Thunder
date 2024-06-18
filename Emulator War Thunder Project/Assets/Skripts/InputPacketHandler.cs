using UnityEngine;

public class InputPacketHandler : MonoBehaviour
{
    [SerializeField] private DriverSystem driverSystem;
    [SerializeField] private GunController gunController;

    public void ProcessPackage(Package package)
    {
        try
        {
            if (package is PackageValueInt packageValueInt)
            {
                switch (package.NamePackage)
                {
                    case "Suitable Gear Box":
                        driverSystem.SetGear(packageValueInt.ValueInt);
                        break;
                    case "Set Gun Caliber":
                        gunController.SetGun(packageValueInt.ValueInt);
                        break;
                    default:
                        break;
                }
            }
            else if (package is PackageProjectile packageProjectile)
            {
                switch (package.NamePackage)
                {
                    case "Set Projectile":
                        StartCoroutine(gunController.curAddProjectiles(packageProjectile));
                        break;
                    default:
                        break;
                }
            }
            else if (package is PackageValueFloat packageValueFloat)
            {
                switch (package.NamePackage)
                {
                    case "Set Reload Time":
                        MyConsole.Instance.DebugLog($"Set Reload Time: {packageValueFloat.ValueFloat}");
                        gunController.SetReloadTime(packageValueFloat.ValueFloat);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (package.NamePackage)
                {
                    case "Reset Reloading":
                        StartCoroutine(gunController.curDestroyAllProjectiles());
                        gunController.SetLoadStatus(true);
                        break;
                    case "Shot Main Gun":
                        gunController.SetLoadStatus(false);
                        gunController.MainGunShotVibrate();
                        break;
                    default:
                        break;
                }
            }
        }
        catch (System.Exception ex)
        {
            MyConsole.Instance.DebugLog(ex);
            throw;
        }
    }
}