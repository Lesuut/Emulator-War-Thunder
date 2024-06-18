using UnityEngine;

public class ClutchPedalActivity : MonoBehaviour
{
    private bool ClutchValue = false;
    public void SetClutchValue(bool value)
    {
        ClutchValue = value;
    }
    public bool GetActive()
    {
        return ClutchValue;
    }
}