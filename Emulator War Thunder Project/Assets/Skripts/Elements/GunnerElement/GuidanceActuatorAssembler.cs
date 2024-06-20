using System.Collections;
using UnityEngine;

public class GuidanceActuatorAssembler : MonoBehaviour
{
    [SerializeField] private SteeringWheel steeringWheelHorizontal;
    [SerializeField] private SteeringWheel steeringWheelVertical;
    [Space]
    [SerializeField] private GunnerSystem gunnerSystem;

    private float currentX = 0;
    private float currentY = 0;

    private void Start()
    {
        StartCoroutine(curUpdate());   
    }
    private IEnumerator curUpdate()
    {
        while (true)
        {
            if (currentX != steeringWheelHorizontal.GetValue() || currentY != steeringWheelVertical.GetValue())
            {
                currentX = steeringWheelHorizontal.GetValue();
                currentY = steeringWheelVertical.GetValue();

                gunnerSystem.MoveJoy(new Vector2 (currentX, currentY));
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}