using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class GunController : MonoBehaviour
{  
    [SerializeField] private LoaderSystem loaderSystem;
    [SerializeField] private GunAnim[] guns;
    [Space]
    [SerializeField] private Transform spawnPerent;
    [Space]
    [SerializeField] private float distForLoad;
    [Space]
    [SerializeField] private Transform[] spawnPointProjectiles;

    private Transform currentPointForLoad;
    private GunAnim currentGunAnim;

    private List<GameObject> allSpawnedItems;
    private ProjectileItemController selectProjectileItemController;

    private ProjectileInfo[] projectileInfos;

    private float reloadTime = 10;

    public bool loadStatus{ get; set; }

    private void Start()
    {
        allSpawnedItems = new List<GameObject>();
        projectileInfos = Resources.LoadAll<ProjectileInfo>("Projectiles");
        loadStatus = true;
        SetGun(0);
    }
    public void SetLoadStatus(bool newloadStatus)
    {
        loadStatus = newloadStatus;
    }
    public void SetReloadTime(float reloadTime)
    {
        this.reloadTime = reloadTime;
    }
    public void SetGun(int id)
    {
        SetActiveFalseAllGuns();
        guns[id].gameObject.SetActive(true);
        currentPointForLoad = guns[id].GetPointForReload();
        currentGunAnim = guns[id];
    }
    private bool curAddProjectilesActive = false;   
    public IEnumerator curAddProjectiles(PackageProjectile packageProjectile)
    {
        curAddProjectilesActive = true;
        foreach (var itemInfo in projectileInfos)
        {
            if ((int)itemInfo.projectileType == packageProjectile.ProjectileId)
            {
                for (int i = 0; i < packageProjectile.ProjectileCount; i++)
                {
                    SpawnProjectile(itemInfo);
                    yield return new WaitForSeconds(0.1f);
                }
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        curAddProjectilesActive = false;
    }
    public IEnumerator curDestroyAllProjectiles()
    {
        if (curAddProjectilesActive)
        {
            yield break;
        }
        for (int i = 0; i < allSpawnedItems.Count; i++)
        {
            try
            {
                var item = allSpawnedItems[i];
                if (item != null && item != null)
                {
                    Destroy(item);
                }
            }
            catch (System.Exception ex)
            {
                MyConsole.Instance.DebugLog(ex);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
    private void SpawnProjectile(ProjectileInfo projectileInfo)
    {
        Transform spawnPoint = spawnPointProjectiles[Random.Range(0, spawnPointProjectiles.Length)];
        GameObject obj = Instantiate(projectileInfo.bodyPrefab, spawnPerent);
        obj.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, obj.transform.position.z);
        ProjectileItemController newProjectileItemController = obj.AddComponent<ProjectileItemController>();
        newProjectileItemController.Init(projectileInfo, loaderSystem, this, projectileInfo.projectileType, 1 / reloadTime);
        allSpawnedItems.Add(obj);
    }
    public void SelectProjectileItemController(ProjectileItemController projectileItemController)
    {
        selectProjectileItemController = projectileItemController;
    }
    private void SetActiveFalseAllGuns()
    {
        foreach (var gun in guns)
        {
            if (gun.gameObject.activeSelf)
            {
                gun.gameObject.SetActive(false);
            }
        }
    }
    private bool needVibrateMainShotGun = false;
    private void Update()
    {
        if (needVibrateMainShotGun)
        {
            Vibration.Vibrate(250);
            needVibrateMainShotGun = false;
            if (selectProjectileItemController != null)
            {
                currentGunAnim.SetSleeve(selectProjectileItemController.projectileInfo.sleevePrefab);
            }
            currentGunAnim.Shoot();
        }
        if (selectProjectileItemController != null)
        {
            if (Vector2.Distance(currentPointForLoad.position, selectProjectileItemController.gameObject.transform.position) < distForLoad)
            {
                if (selectProjectileItemController.gameObject.activeSelf)
                {
                    loadStatus = true;
                    Vibration.Vibrate(25);
                    selectProjectileItemController.gameObject.SetActive(false);
                    loaderSystem.FinishReload();
                }
            }
        }
    }
    public void MainGunShotVibrate()
    {
        needVibrateMainShotGun = true;
    }
}