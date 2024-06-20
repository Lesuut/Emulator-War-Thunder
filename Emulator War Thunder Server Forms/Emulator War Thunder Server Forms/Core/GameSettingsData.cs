public class GameSettingsData
{
    public GameSettingsData(
        TextBox textBoxMaxGear, 
        TextBox textBoxMinGear, 
        Label lableCurrentGear, 
        Action<Package> broadcastMessage, 
        PackageFactory packageFactory, 
        Action mainGunShot,
        CheckBox checkBoxReloadGun,
        CheckBox sightingReticleActiveCheckBox)
    {
        if (textBoxMaxGear != null && int.TryParse(textBoxMaxGear.Text, out int maxGearValue))
        {
            maxGear = maxGearValue;
        }
        else
        {
            maxGear = 2; 
        }

        if (textBoxMinGear != null && int.TryParse(textBoxMinGear.Text, out int minGearValue))
        {
            minGear = minGearValue;
        }
        else
        {
            minGear = 1; 
        }

        this.lableCurrentGear = lableCurrentGear;
        this.broadcastMessage = broadcastMessage;
        this.packageFactory = packageFactory;
        this.mainGunShot = mainGunShot;
        this.checkBoxReloadGun = checkBoxReloadGun;
        this.sightingReticleActiveCheckBox = sightingReticleActiveCheckBox;
    }

    public int maxGear { get; set; }
    public int minGear { get; set; }

    public Label lableCurrentGear { get; set; }
    public Action<Package> broadcastMessage { get; set; }
    public PackageFactory packageFactory { get; set; }
    public Action mainGunShot { get; set; }
    public CheckBox checkBoxReloadGun { get; set; }
    public CheckBox sightingReticleActiveCheckBox { get; set; }
}
