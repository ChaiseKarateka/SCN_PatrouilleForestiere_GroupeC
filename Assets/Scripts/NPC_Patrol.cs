using UnityEngine;
using System.Collections;

public class NPCPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    public float rotationSpeed = 5f;
    public Animator animator;
    public LayerMask groundLayer;
    public float groundOffset = 0.1f;
    public GameObject canPrefab;
    public float minDropTime = 20f;
    public float maxDropTime = 30f;

    private bool canDrop = true;
    private int currentWaypoint = 0;
    private int patroldirection = 1;

    void Start()
    {
        StartCoroutine(DropCanRoutine());
    }

    void Update()
    {
        if (waypoints.Length == 0)
            return;


        Transform target = waypoints[currentWaypoint];


        // Direction vers le prochain point
        Vector3 direction = target.position - transform.position;
        direction.y = 0;


        // Rotation vers le point
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rotation,
                rotationSpeed * Time.deltaTime
            );
        }


        // Déplacement
        transform.position += direction.normalized * speed * Time.deltaTime;
        StickToGround();


        // Animation marche
        animator.SetBool("walking", true);


        // Arrivé au point
        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            currentWaypoint+= this.patroldirection;

            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = waypoints.Length - 2;
                patroldirection = -1;
            }


            // Arrivé au premier point
            if (currentWaypoint < 0)
            {
                currentWaypoint = 1;
                patroldirection = 1;
            }
                
        }
    }

    public void StopPatrol()
    {
        animator.SetBool("walking", false);
        enabled = false;
    }


    public void ResumePatrol()
    {
        enabled = true;
    }

    void StickToGround()
    {
        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 3f, groundLayer))
        {
            Vector3 pos = transform.position;

            pos.y = hit.point.y + groundOffset;

            transform.position = pos;
        }
    }


    System.Collections.IEnumerator DropCanRoutine()
    {
        while (canDrop)
        {
            float wait = Random.Range(minDropTime, maxDropTime);

            yield return new WaitForSeconds(wait);


            if (canDrop)
            {
                DropCan();
            }
        }
    }

    void DropCan()
    {
        Vector3 spawnPos = transform.position + transform.forward * 0.5f;

        Instantiate(
            canPrefab,
            spawnPos,
            Quaternion.identity
        );
    }

    public void StopDropping()
    {
        canDrop = false;
    }
}