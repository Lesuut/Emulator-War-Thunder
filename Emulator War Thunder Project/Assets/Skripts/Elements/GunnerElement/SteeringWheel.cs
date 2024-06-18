using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SteeringWheel : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 centerPoint;
    [SerializeField] private float previousAngle = 0f;
    [SerializeField] private bool isDragging = false;
    [Space]
    [SerializeField] private Slider multiplySlider;

    // New fields for value and multiplier
    [SerializeField] private float value = 0f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        centerPoint = rectTransform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        previousAngle = Vector2.SignedAngle(Vector2.up, eventData.position - centerPoint);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        Vector2 currentPointerPosition = eventData.position;
        float currentAngle = Vector2.SignedAngle(Vector2.up, currentPointerPosition - centerPoint);
        float angleDelta = currentAngle - previousAngle;

        // Adjust angleDelta to account for wrapping at 180 degrees
        if (angleDelta > 180)
        {
            angleDelta -= 360;
        }
        else if (angleDelta < -180)
        {
            angleDelta += 360;
        }

        rectTransform.Rotate(0, 0, angleDelta);

        // Update the value based on the angleDelta and multiplier
        value += angleDelta * multiplySlider.value;

        previousAngle = currentAngle;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    // Optionally, provide a way to get the value
    public float GetValue()
    {
        return value;
    }
    public void ResetValue()
    {
        value = 0;
    }
    public bool IsDragging()
    {
        return isDragging;
    }
}
