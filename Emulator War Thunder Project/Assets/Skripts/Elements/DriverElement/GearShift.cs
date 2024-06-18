using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum GearType
{
    Gear_H,
    Gear_R1,
    Gear_R,
    Gear_1,
    Gear_2,
    Gear_3,
    Gear_4,
    Gear_5,
    Gear_6,
    Gear_7,
    Gear_8,
    Gear_9,
    Gear_10,
}

[Serializable]
public class GearPosition
{
    public RectTransform position;  // Позиция передачи
    public GearType gearType;
}

public class GearShift : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private RectTransform handle;  // синяя ручка
    [SerializeField] private RectTransform leftBoundary;  // левая граница
    [SerializeField] private RectTransform rightBoundary;  // правая граница
    [SerializeField] private GearPosition[] gears;  // позиции и типы передач
    [Space]
    [SerializeField] private DriverSystem driverSystem;
    [Space]
    [SerializeField] private GasPedalActivity gasPedalActivity;
    [SerializeField] private ClutchPedalActivity clutchPedalActivity;

    private RectTransform rectTransform;
    private Vector2 startDragPosition;
    private GearPosition currentGear = null;  // текущая передача

    private GearType currentGearType = GearType.Gear_H;
    private GearType currentSelectBufferGearType = GearType.Gear_H;

    private bool touchHandler = false;

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();

        // Визуализация границ и передач для отладки
        Debug.DrawLine(leftBoundary.position, rightBoundary.position, Color.green, 100f);
        foreach (var gear in gears)
        {
            Debug.DrawLine(gear.position.position - Vector3.up * 10, gear.position.position + Vector3.up * 10, Color.red, 100f);
            Debug.DrawLine(gear.position.position - Vector3.left * 10, gear.position.position + Vector3.left * 10, Color.red, 100f);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out startDragPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!gasPedalActivity.GetActive() && clutchPedalActivity.GetActive())
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);

            Vector2 handlerPosition = handle.anchoredPosition + (localPoint - startDragPosition);

            // Ограничиваем движение ручки вдоль центральной линии
            handlerPosition.x = Mathf.Clamp(handlerPosition.x, leftBoundary.anchoredPosition.x, rightBoundary.anchoredPosition.x);

            GearPosition closestGear = null;
            float closestDistance = float.MaxValue;

            foreach (var gear in gears)
            {
                float distance = Vector2.Distance(localPoint, gear.position.anchoredPosition);
                if (distance < closestDistance)
                {
                    closestGear = gear;
                    closestDistance = distance;
                }
            }

            // Если ближайшая передача находится в пределах допустимого расстояния, фиксируем ручку на ней
            if (closestGear != null && closestDistance <= 100)  // допустимое расстояние до передачи
            {
                handlerPosition = closestGear.position.anchoredPosition;
                currentGear = closestGear;

                if (currentSelectBufferGearType != currentGear.gearType)
                {
                    currentSelectBufferGearType = currentGear.gearType;
                    Vibration.Vibrate(25);
                }
            }
            else
            {
                handlerPosition.y = leftBoundary.anchoredPosition.y;
                currentGear = null;
            }

            handle.anchoredPosition = handlerPosition;
            startDragPosition = localPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Если ручка находится на передаче, оставляем её на месте
        if (currentGear != null)
        {
            handle.anchoredPosition = currentGear.position.anchoredPosition;
        }

        if (currentGearType != currentSelectBufferGearType)
        {
            currentGearType = currentSelectBufferGearType;
            driverSystem.Gear(GetValueFormGearType(currentGearType));
        }
    }
    public int GetValueFormGearType(GearType gearType)
    {
        switch (gearType)
        {
            case GearType.Gear_H:
                return 0;
            case GearType.Gear_R1:
                return -2;
            case GearType.Gear_R:
                return -1;
            case GearType.Gear_1:
                return 1;
            case GearType.Gear_2:
                return 2;
            case GearType.Gear_3:
                return 3;
            case GearType.Gear_4:
                return 4;
            case GearType.Gear_5:
                return 5;
            case GearType.Gear_6:
                return 6;
            case GearType.Gear_7:
                return 7;
            case GearType.Gear_8:
                return 8;
            case GearType.Gear_9:
                return 9;
            case GearType.Gear_10:
                return 10;
            default:
                return 0;
        }
    }
}
