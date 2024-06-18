using UnityEngine;

public class ProjectileRndColor : MonoBehaviour
{
    [SerializeField] private float range = 0.1f; // Диапазон изменения цвета

    private void OnEnable()
    {
        SpriteRenderer spriteRenderer;
        Color originalColor = Color.white;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        if (spriteRenderer != null)
        {
            float r = Mathf.Clamp01(originalColor.r + Random.Range(-range, range));
            float g = Mathf.Clamp01(originalColor.g + Random.Range(-range, range));
            float b = Mathf.Clamp01(originalColor.b + Random.Range(-range, range));

            spriteRenderer.color = new Color(r, g, b, originalColor.a);
        }
    }
}
