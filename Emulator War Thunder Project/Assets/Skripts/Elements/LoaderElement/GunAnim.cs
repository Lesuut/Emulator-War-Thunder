using UnityEngine;
using System.Collections;

public class GunAnim : MonoBehaviour
{
    [SerializeField] private Transform gunTrans;
    [SerializeField] private float recoilDistance = 0.5f; // Насколько отодвигать орудие
    [SerializeField] private float recoilSpeed = 0.1f;    // Скорость отдачи (время движения вниз)
    [SerializeField] private float returnSpeed = 0.2f;    // Скорость возврата (время движения вверх)
    [SerializeField] private AnimationCurve recoilCurve;  // Кривая для изменения интенсивности
    [Space]
    [SerializeField] private ParticleSystem internalSmokeParticleSystem;
    [Space]
    [SerializeField] private Transform pointForRelaod;
    [Space]
    [SerializeField] private Transform perentForSpawnSleeve;

    private GameObject currentSleevePrefab;
    public void SetSleeve(GameObject sleevePrefab)
    {
        currentSleevePrefab = sleevePrefab;
    }
    public void Shoot()
    {
        StartCoroutine(RecoilCoroutine());    
    }
    public Transform GetPointForReload()
    {
        return pointForRelaod;
    }
    private bool curRecoilCoroutineActive = false;
    private IEnumerator RecoilCoroutine()
    {
        if (!curRecoilCoroutineActive)
        {
            curRecoilCoroutineActive = true;
        }
        else
        {
            yield break;
        }
        OnRecoilStart();

        Vector3 originalPosition = gunTrans.localPosition;
        Vector3 recoilPosition = originalPosition + Vector3.down * recoilDistance;

        float elapsedTime = 0;
        while (elapsedTime < recoilSpeed)
        {
            try
            {
                float curveValue = recoilCurve.Evaluate(elapsedTime / recoilSpeed);
                gunTrans.localPosition = Vector3.Lerp(originalPosition, recoilPosition, curveValue);
                elapsedTime += Time.deltaTime;
            }
            catch (System.Exception e)
            {
                MyConsole.Instance.DebugLog(e);
                throw;
            }
            yield return null;
        }
        gunTrans.localPosition = recoilPosition;

        OnRecoilReturnStart();

        elapsedTime = 0;
        while (elapsedTime < returnSpeed)
        {
            try
            {
                float curveValue = recoilCurve.Evaluate(elapsedTime / returnSpeed);
            gunTrans.localPosition = Vector3.Lerp(recoilPosition, originalPosition, curveValue);
            elapsedTime += Time.deltaTime;
            }
            catch (System.Exception e)
            {
                MyConsole.Instance.DebugLog(e);
                throw;
            }
            yield return null;
        }
        gunTrans.localPosition = originalPosition;

        OnRecoilEnd();

        curRecoilCoroutineActive = false;
    }

    private void OnRecoilStart()
    {
        
    }

    private void OnRecoilReturnStart()
    {
        try
        {
            internalSmokeParticleSystem.Play();
        }
        catch (System.Exception e)
        {
            MyConsole.Instance.DebugLog(e);
            throw;
        }
        if (currentSleevePrefab != null)
        {
            GameObject obj = Instantiate(currentSleevePrefab, perentForSpawnSleeve);
            obj.transform.position = perentForSpawnSleeve.transform.position;
        }
    }

    private void OnRecoilEnd()
    {
        try
        {
            internalSmokeParticleSystem.Stop();
        }
        catch (System.Exception e)
        {
            MyConsole.Instance.DebugLog(e);
            throw;
        }
    }
}
