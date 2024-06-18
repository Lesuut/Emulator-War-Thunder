using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RotationObserver : MonoBehaviour
{
    [SerializeField] private DriverSystem driverSystem;
    [SerializeField] private Client clietn;
    [SerializeField] private Slider triggerZone;
    [Space]
    [SerializeField] private GameObject objActive;

    private int currentDirection = 0;

    private void Start()
    {
        StartCoroutine(curUpdate());
    }

    private IEnumerator curUpdate()
    {
        while (true)
        {
            if (clietn.IsConnected() && objActive.activeSelf)
            {
                Vector3 acceleration = Input.acceleration.normalized;

                if (acceleration.x > triggerZone.value)
                {
                    if (currentDirection != 1)
                    {
                        currentDirection = 1;
                        driverSystem.Turn(currentDirection);
                    }
                }
                else if (acceleration.x < -triggerZone.value)
                {
                    if (currentDirection != -1)
                    {
                        currentDirection = -1;
                        driverSystem.Turn(currentDirection);
                    }
                }
                else
                {
                    if (currentDirection != 0)
                    {
                        currentDirection = 0;
                        driverSystem.Turn(currentDirection);
                    }
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
    public void ResetRotation()
    {
        currentDirection = 0;
        driverSystem.Turn(currentDirection);
    }
}