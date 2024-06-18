using UnityEngine;

public class SmoothTurnSwitch : MonoBehaviour
{
    [SerializeField] private DriverSystem driverSystem;
    [SerializeField] private Client client;

    private bool currentState = false;

    public void Switch()
    {
        if (client)
        {
            currentState = !currentState;
            driverSystem.SmoothTurn(currentState);
        }
    }
}