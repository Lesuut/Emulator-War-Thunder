using UnityEngine;

public class ProjectileItemController : MonoBehaviour
{
    private LoaderSystem loaderSystem;
    private GunController gunController;

    private bool isDragging = false;
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;

    private int currentidProjectile;

    public void Init(LoaderSystem loaderSystem, GunController gunController, ProjectileType projectileType, float moveSpeed)
    {
        this.loaderSystem = loaderSystem;
        this.gunController = gunController;
        currentidProjectile = (int)projectileType;
        this.moveSpeed = moveSpeed;
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
