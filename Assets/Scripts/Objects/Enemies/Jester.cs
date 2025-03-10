using System.Collections;
using UnityEngine;

public class Jester : Enemy
{
    public int hitsToDefeat = 3;
    private int currentHits = 0;

    public GameObject bombPrefab;
    public Transform[] positions; 
    public Transform tentPosition;

    public float throwInterval = 3f; 
    public float restTime = 2f;  
    public int roundsBeforeRest = 3; 

    private int roundsThrown = 0;
    private bool isResting = false;

    private enum State { Idle, ThrowingBombs, Resting, Hit, Defeated }
    private State currentState = State.Idle;

    protected override void _Start()
    {
        base._Start();
        _Player = GameObject.FindWithTag("Player");

        if (_Player != null)
        {
            MoveToRandomPosition();
        }
        else
        {
            Debug.LogError("Player not found in the scene!");
        }
    }

    private void MoveToRandomPosition()
    {
        int index = Random.Range(0, positions.Length);
        transform.position = positions[index].position;
        StartCoroutine(ThrowBombs());
    }

    private IEnumerator ThrowBombs()
    {
        currentState = State.ThrowingBombs;

        for (int i = 0; i < 3; i++)
        {
            ThrowBombAtPlayer();
            yield return new WaitForSeconds(throwInterval);
        }

        roundsThrown++;

        if (roundsThrown >= roundsBeforeRest)
        {
            roundsThrown = 0;
            StartCoroutine(RestAtTent());
        }
        else
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(ThrowBombs());
        }
    }

    private void ThrowBombAtPlayer()
    {
        if (_Player != null)
        {
            GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            Vector3 targetPosition = _Player.transform.position;
            Vector3 direction = (targetPosition - transform.position).normalized;
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction * 10f;
            }
        }
    }

    private IEnumerator RestAtTent()
    {
        currentState = State.Resting;
        transform.position = tentPosition.position;
        yield return new WaitForSeconds(restTime);
        MoveToRandomPosition();
    }

    public void OnHit()
    {

        currentHits++;
        if (currentHits >= hitsToDefeat)
        {
            currentState = State.Defeated;
            Defeated();
        }
        else
        {
            StartCoroutine(HandleHit());
        }
    }

    private IEnumerator HandleHit()
    {
        currentState = State.Hit;

        Vector3 startPosition = transform.position;
        Vector3 groundPosition = startPosition;
        groundPosition.y = 0f;

        float fallDuration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < fallDuration)
        {
            transform.position = Vector3.Lerp(startPosition, groundPosition, elapsedTime / fallDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = groundPosition;


        yield return new WaitForSeconds(1f);

        MoveToRandomPosition();
    }

    private void Defeated()
    {
        Kill();
    }

    protected override void Attack()
    {
    }

    protected override void Kill()
    {
        base.Kill();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBomb"))
        {
            OnHit();
            Destroy(other.gameObject);
        }
    }
}
