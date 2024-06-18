public class PackageFactory
{
    private SelectNameSystem selectNameSystem;
    private TypeCrew typeCrew;
    public PackageFactory(SelectNameSystem selectNameSystem, TypeCrew typeCrew)
    {
        this.selectNameSystem = selectNameSystem;
        this.typeCrew = typeCrew;
    }
    public PackageValueFloat GetPackageValueFlaot(string namePackage)
    {
        return new PackageValueFloat(selectNameSystem.GetName(), namePackage, typeCrew);
    }
    public PackageValueInt GetPackageValueInt(string namePackage)
    {
        return new PackageValueInt(selectNameSystem.GetName(), namePackage, typeCrew);
    }
    public PackageValueBool GetPackageValueBool(string namePackage)
    {
        return new PackageValueBool(selectNameSystem.GetName(), namePackage, typeCrew);
    }
    public Package GetPackage(string namePackage)
    {
        return new Package(selectNameSystem.GetName(), namePackage, typeCrew);
    }
    public PackageValueVector2 GetPackageValueVector2(string namePackage)
    {
        return new PackageValueVector2(selectNameSystem.GetName(), namePackage, typeCrew);
    }
    public PackageValueString GetPackageValueString(string namePackage)
    {
        return new PackageValueString(selectNameSystem.GetName(), namePackage, typeCrew);
    }
}