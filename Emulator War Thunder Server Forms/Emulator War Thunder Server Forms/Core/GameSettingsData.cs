public class GameSettingsData
{
    public GameSettingsData(TextBox textBoxMaxGear, TextBox textBoxMinGear, Label lableCurrentGear)
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
    }

    public int maxGear { get; set; }
    public int minGear { get; set; }

    public Label lableCurrentGear { get; set; }
}
