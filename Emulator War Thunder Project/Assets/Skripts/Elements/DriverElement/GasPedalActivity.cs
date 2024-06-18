using UnityEngine;

public class GasPedalActivity : MonoBehaviour
{
    private bool GasValue = false;
    public void SetGasValue(bool value)
    {
        GasValue = value;
    }
    public bool GetActive()
    {
        return GasValue;
    }
}