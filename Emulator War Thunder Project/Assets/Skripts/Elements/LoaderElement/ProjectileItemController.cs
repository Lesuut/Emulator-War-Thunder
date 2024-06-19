using UnityEngine;

public class ProjectileItemController : MonoBehaviour
{
    public ProjectileInfo projectileInfo { get; set; }

    private LoaderSystem loaderSystem;
    private GunController gunController;

    private bool isDragging = false;
    private Rigidbody2D rb;

    private float moveSpeed;

    private int currentidProjectile;

    public void Init(ProjectileInfo projectileInfo, LoaderSystem loaderSystem, GunController gunController, ProjectileType projectileType, float moveSpeed)
    {
        this.loaderSystem = loaderSystem;
        this.gunController = gunController;
        currentidProjectile = (int)projectileType;
        this.moveSpeed = moveSpeed;
        this.projectileInfo = projectileInfo;
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        isDragging = true;
        loaderSystem.SelectProjectile(currentidProjectile);

        rb.isKinematic = true;

        gunController.SelectProjectileItemController(this);
    }

    private void OnMouseUp()
    {
        isDragging = false;

        rb.isKinematic = false;
    }

    private void FixedUpdate()
    {
        if (isDragging && !gunController.loadStatus)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 newPosition = Vector2.Lerp(rb.position, mousePosition, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
        }
    }
}
