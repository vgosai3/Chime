using UnityEngine;

public class JesterTurret : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public GameObject bombProjectilePrefab;

    private float fireRate = 1.2f;
    private float _lastFireTime;

    private void Update()
    {
    }

    public void RotateTurret(float direction)
    {
        transform.Rotate(0f, direction * rotationSpeed * Time.deltaTime, 0f);
    }

    public void FireBomb()
    {
        if (Time.time - _lastFireTime >= fireRate)
        {
            GameObject bombProjectile = Instantiate(bombProjectilePrefab, transform.position, Quaternion.identity);
            Rigidbody rb = bombProjectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = transform.forward * 10f;
            }
            _lastFireTime = Time.time;
        }
    }
}
