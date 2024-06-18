using UnityEngine;

public class InputPacketHandler : MonoBehaviour
{
    [SerializeField] private DriverSystem driverSystem;

    public void ProcessPackage(Package package)
    {
        if (package is PackageValueInt packageValueInt)
        {
            switch (package.NamePackage)
            {
                case "Suitable Gear Box":
                    driverSystem.SetGear(packageValueInt.ValueInt);
                    break;
                default:
                    break;
            }
        }
    }
}