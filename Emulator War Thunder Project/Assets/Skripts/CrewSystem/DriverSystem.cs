using UnityEngine;

public class DriverSystem : MonoBehaviour
{
    [SerializeField] private Client client;
    [SerializeField] private SelectNameSystem selectNameSystem;
    [Space]
    [SerializeField] private GearBoxObj[] gearBoxObjs;

    private PackageFactory pachageFactory;

    public void Awake()
    {
        pachageFactory = new PackageFactory(selectNameSystem, TypeCrew.Driver);
    }

    public void Gas(bool value)
    {
        PackageValueBool package = pachageFactory.GetPackageValueBool("Gas");
        package.Value = value;
        client.PostData(package);
    }
    public void Brake(bool value)
    {
        PackageValueBool package = pachageFactory.GetPackageValueBool("Brake");
        package.Value = value;
        client.PostData(package);
    }
    public void Repair(bool value)
    {
        PackageValueBool package = pachageFactory.GetPackageValueBool("Repair");
        package.Value = value;
        client.PostData(package);
    }
    public void Gear(int value)
    {
        PackageValueInt package = pachageFactory.GetPackageValueInt("Gear");
        package.ValueInt = value;
        client.PostData(package);
    } 
    public void Turn(int value)
    {
        PackageValueInt package = pachageFactory.GetPackageValueInt("Turn");
        package.ValueInt = value;
        client.PostData(package);
    }
    public void EngineActive()
    {
        if (client.IsConnected())
        {
            Package package = pachageFactory.GetPackage("Engine Active");
            client.PostData(package);
        }
    }
    public void SmoothTurn(bool valueStatus)
    {
        PackageValueBool package = pachageFactory.GetPackageValueBool("SmoothTurn");
        package.Value = valueStatus;
        client.PostData(package);
    }
    public void FirefightingActive()
    {
        if (client.IsConnected())
        {
            Package package = pachageFactory.GetPackage("Firefighting");
            client.PostData(package);
        }
    }
    public void SetGear(int gear)
    {
        foreach (var item in gearBoxObjs)
        {
            if (item.objGearlBox.activeSelf)
            {
                item.objGearlBox.SetActive(false);
            }
        }
        foreach (var item in gearBoxObjs)
        {
            if (gear == item.maxGears)
            {
                item.objGearlBox.SetActive(true);
                return;
            }
        }
    }
}
[System.Serializable]
public class GearBoxObj
{
    public GameObject objGearlBox;
    public int maxGears;
}