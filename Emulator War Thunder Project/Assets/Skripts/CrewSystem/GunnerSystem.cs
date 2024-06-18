using UnityEngine;

public class GunnerSystem : MonoBehaviour
{
    [SerializeField] private Client client;
    [SerializeField] private SelectNameSystem selectNameSystem;

    private PackageFactory pachageFactory;

    public void Awake()
    {
        pachageFactory = new PackageFactory(selectNameSystem, TypeCrew.Gunner);
    }

    public void OpticsActive()
    {
        if (client.IsConnected())
        {
            Package package = pachageFactory.GetPackage("Optics");
            client.PostData(package);
        }
    }
    public void ShotActive()
    {
        if (client.IsConnected())
        {
            Package package = pachageFactory.GetPackage("Shot");
            client.PostData(package);
        }
    }
    public void MoveJoy(Vector2 joyValue)
    {
        PackageValueVector2 package = pachageFactory.GetPackageValueVector2("Gunner Move Mouse");
        package.X = joyValue.x;
        package.Y = joyValue.y;
        client.PostData(package);
    }
    public void MoveGunActive(bool value)
    {
        PackageValueBool package = pachageFactory.GetPackageValueBool("Gunner Move Gun Active");
        package.Value = value;
        client.PostData(package);
    }
    public void ZoomActive()
    {
        if (client.IsConnected())
        {
            Package package = pachageFactory.GetPackage("Zoom");
            client.PostData(package);
        }
    }
}