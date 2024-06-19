using UnityEngine;

public class CommanderSystem : MonoBehaviour
{
    [SerializeField] private Client client;
    [SerializeField] private SelectNameSystem selectNameSystem;

    private PackageFactory pachageFactory;

    public void Awake()
    {
        pachageFactory = new PackageFactory(selectNameSystem, TypeCrew.Commander);
    }
    public void MoveJoy(Vector2 joyValue)
    {
        PackageValueVector2 package = pachageFactory.GetPackageValueVector2("Commander Move Joy");
        package.X = joyValue.x;
        package.Y = joyValue.y;
        client.PostData(package);
    }
    public void ReconnaissanceActive()
    {
        if (client.IsConnected())
        {
            Package package = pachageFactory.GetPackage("Reconnaissance");
            client.PostData(package);
        }
    }
    public void BinocularsActive()
    {
        if (client.IsConnected())
        {
            Package package = pachageFactory.GetPackage("Binoculars");
            client.PostData(package);
        }
    }
    public void MoveJoy(bool value)
    {
        if (client.IsConnected())
        {
            PackageValueBool package = pachageFactory.GetPackageValueBool("Commander Move Joy Active");
            package.Value = value;
            client.PostData(package);
        }
    }
    public void TargetDistanceActive()
    {
        if (client.IsConnected())
        {
            Package package = pachageFactory.GetPackage("Target Distance");
            client.PostData(package);
        }
    }
    public void SmokeActive()
    {
        if (client.IsConnected())
        {
            Package package = pachageFactory.GetPackage("Smoke");
            client.PostData(package);
        }
    }
    public void MachineGunActive(bool value)
    {
        PackageValueBool package = pachageFactory.GetPackageValueBool("Machine Gun Active");
        package.Value = value;
        client.PostData(package);
    }
}