using System.Collections;
using UnityEngine;

public class SleeveItemController : MonoBehaviour
{
    private bool isDragging = false;
    private Rigidbody2D rb;
    private float moveSpeed = 10;

    [SerializeField] private ParticleSystem particleSystem; // Ссылка на Particle System
    [SerializeField] private float particleFadeDuration = 10f; // Время для уменьшения частиц
    [SerializeField] private float minTorque = -100f; // Минимальная случайная сила кручения
    [SerializeField] private float maxTorque = 100f; // Максимальная случайная сила кручения

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();

        // Задать случайную силу кручения
        float randomTorque = Random.Range(minTorque, maxTorque);
        rb.AddTorque(randomTorque);

        // Запуск Particle System
        if (particleSystem != null)
        {
            particleSystem.Play();
            StartCoroutine(FadeOutParticles());
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        rb.isKinematic = false;
    }

    private void FixedUpdate()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 newPosition = Vector2.Lerp(rb.position, mousePosition, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
        }
    }

    private IEnumerator FadeOutParticles()
    {
        var emission = particleSystem.emission;
        float initialRate = emission.rateOverTime.constant;
        float elapsedTime = 0f;

        while (elapsedTime < particleFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newRate = Mathf.Lerp(initialRate, 0, elapsedTime / particleFadeDuration);
            emission.rateOverTime = newRate;
            yield return null;
        }

        emission.rateOverTime = 0;
        particleSystem.Stop();
    }
}
