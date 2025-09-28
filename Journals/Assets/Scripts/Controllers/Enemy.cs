using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;

    public float maxSpeed = 1f;
    public float accelerationTime = 1f;
    public float followDistance = 1f; 
    public float sideOffsetRange = 2f; 

    private Vector3 velocity;
    private float acceleration;

    private Vector3 targetOffset;
    void Start()
    {
        acceleration = maxSpeed / accelerationTime;
        RandomOffSet();
    }

    void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        if (playerTransform == null) return;

        Vector3 targetPos = playerTransform.position + targetOffset;

        if (Vector3.Distance(transform.position, targetPos) < 0.5f)
        {
            RandomOffSet();
            targetPos = playerTransform.position + targetOffset;
        }

       
        Vector3 direction = (targetPos - transform.position).normalized;

        velocity += direction * acceleration * Time.deltaTime;

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        transform.position += velocity * Time.deltaTime;
    }

    private void RandomOffSet()
    {
        float randomSide = Random.Range(-1f, 1f); 
        float randomUpDown = Random.Range(-1f, 1f); 
        targetOffset = new Vector3(randomSide, randomUpDown, 0).normalized * sideOffsetRange;
    }




}
