using UnityEngine;

public class LoaderSystem : MonoBehaviour
{
    [SerializeField] private Client client;
    [SerializeField] private SelectNameSystem selectNameSystem;

    private PackageFactory pachageFactory;

    public void Awake()
    {
        pachageFactory = new PackageFactory(selectNameSystem, TypeCrew.Loader);
    }
    public void SelectProjectile(int id)
    {
        PackageValueInt package = pachageFactory.GetPackageValueInt($"Select Projectile");
        package.ValueInt = id;
        client.PostData(package);
    }
    public void FinishReload()
    {
        Package package = pachageFactory.GetPackage("Finish Reload");
        client.PostData(package);
    }
}