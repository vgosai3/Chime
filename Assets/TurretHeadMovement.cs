using UnityEngine;

public class TurretHeadMovement : MonoBehaviour
{
    public float speed = 1.0f;

    private Turret turretScript;

    // Start is called before the first frame update
    void Start()
    {
        turretScript = transform.parent.GetComponent<Turret>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (turretScript.Closest != null)
        {
            Vector3 targetDirection = turretScript.Closest.transform.position - transform.position;
            float singleStep = speed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}
