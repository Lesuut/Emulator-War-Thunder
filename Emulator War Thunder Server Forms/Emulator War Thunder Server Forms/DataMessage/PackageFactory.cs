public class PackageFactory
{
    private TypeCrew typeCrew;
    private string localName;

    public PackageFactory(string localName, TypeCrew typeCrew)
    {
        this.typeCrew = typeCrew;
        this.localName = localName;
    }
    public PackageValueFloat GetPackageValueFlaot(string namePackage)
    {
        return new PackageValueFloat(localName, namePackage, typeCrew);
    }
    public PackageValueInt GetPackageValueInt(string namePackage)
    {
        return new PackageValueInt(localName, namePackage, typeCrew);
    }
    public PackageValueBool GetPackageValueBool(string namePackage)
    {
        return new PackageValueBool(localName, namePackage, typeCrew);
    }
    public Package GetPackage(string namePackage)
    {
        return new Package(localName, namePackage, typeCrew);
    }
    public PackageValueVector2 GetPackageValueVector2(string namePackage)
    {
        return new PackageValueVector2(localName, namePackage, typeCrew);
    }
    public PackageValueString GetPackageValueString(string namePackage)
    {
        return new PackageValueString(localName, namePackage, typeCrew);
    }
}