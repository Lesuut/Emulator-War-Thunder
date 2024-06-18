using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class JoyMoveSendDataController : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private GameObject objJoystickActive;
    [Space]
    [SerializeField] private CommanderSystem commanderSystem;
    [Space]
    [SerializeField] private Slider sensitivitySlider;

    private Vector2 lastPosTouch = Vector2.zero;

    private bool moveJoy = false;

    private void Start()
    {
        StartCoroutine(curUpdate());
    }
    private IEnumerator curUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.15f);
            if (objJoystickActive.activeSelf)
            {
                if (!moveJoy)
                {
                    moveJoy = true;
                    commanderSystem.MoveJoy(true);
                }

                if (lastPosTouch != new Vector2(joystick.Horizontal, joystick.Vertical))
                {
                    lastPosTouch = new Vector2(joystick.Horizontal, joystick.Vertical);
                    commanderSystem.MoveJoy(lastPosTouch * sensitivitySlider.value);
                }
            }
            else
            {
                if (moveJoy)
                {
                    commanderSystem.MoveJoy(false);
                    Debug.Log("StopMoveJoy");
                    moveJoy = false;
                }
            }
        }
    }
}