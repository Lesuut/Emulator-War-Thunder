public class Package
{
    public string NameClient { get; set; }
    public string NamePackage { get; set; }
    public TypeCrew TypeCrew { get; set; }

    public float value;

    public Package(string nameClient, string namePackage, TypeCrew typeCrew)
    {
        NameClient = nameClient;
        NamePackage = namePackage;
        TypeCrew = typeCrew;
    }

    public string GetPackageName()
    {
        return $"{NameClient}_{TypeCrew}: {NamePackage}";
    }
}
public enum TypeCrew
{
    Commander,
    Driver,
    Gunner,
    Loader,
    Server,
}