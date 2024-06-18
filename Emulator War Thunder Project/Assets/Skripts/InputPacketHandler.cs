using UnityEngine;

public class InputPacketHandler : MonoBehaviour
{
    public void ProcessPackage(Package package)
    {
        if (package is PackageValueInt)
        {
            package = (PackageValueInt)package;
            switch (package.NamePackage)
            {
                case "":

                    break;
                default:
                    break;
            }
        }
    }
}