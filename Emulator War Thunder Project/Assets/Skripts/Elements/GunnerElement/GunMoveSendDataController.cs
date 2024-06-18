using UnityEngine;

public class GunMoveSendDataController : MonoBehaviour
{
    [SerializeField] private SteeringWheel steeringWheelHorizontal;
    [SerializeField] private SteeringWheel steeringWheelVertical;
    [Space]
    [SerializeField] private GunnerSystem gunnerSystem;

    private bool currentActiveStatus = false;

    private void Update()
    {
        if (steeringWheelHorizontal.IsDragging() || steeringWheelVertical.IsDragging())
        {
            if (!currentActiveStatus)
            {
                currentActiveStatus = true;
                gunnerSystem.MoveGunActive(currentActiveStatus);
            }
        }
        else if (!steeringWheelHorizontal.IsDragging() && !steeringWheelVertical.IsDragging())
        {
            if (currentActiveStatus)
            {
                currentActiveStatus = false;
                gunnerSystem.MoveGunActive(currentActiveStatus);

                steeringWheelHorizontal.ResetValue();
                steeringWheelVertical.ResetValue();
            }
        }
    }
}