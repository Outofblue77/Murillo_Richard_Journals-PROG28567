using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;
    public Transform bombsTransform;

    public int numberOfTrailBombs;
    public float spacingbombTrailSpacing;
    public float warpAmount;

    //Player Movement
    public float maxSpeed;
    public float accelerationTime, decelerationTime;
    private Vector3 velocity;
    private float acceleration, deceleration;

    //Radar
    public int numberOfPointsForRadar = 6;
    public float radarRadius = 2f;
    public List<Transform> enemies;

    //Powerups
    public GameObject powerUpsPrefab;
    public int numberOfPowerUps = 0;
    public float distanceRadius = 1f;


    private void Start() 
    {
        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            SpawnBombAtOffset(new Vector3(0, spacingbombTrailSpacing));
        }

        if (Input.GetKeyUp(KeyCode.N))
        {
            CornerBombs();
        }

        if (Input.GetKeyUp(KeyCode.M))
        {
            WarpPlayerToTarget();
        }
        
        //Spawning Powers
        if (Input.GetKeyUp(KeyCode.P)) 
        {
            SpawningPowers();
        }

        PlayerMovement();

        Radar();
    }

    private void SpawnBombAtOffset(Vector3 inOffset) //Added private
    {
        int bombSpawned = 0;
        Vector3 offset = inOffset;
        while (bombSpawned < numberOfTrailBombs)
        {
            Instantiate(bombPrefab, transform.position + inOffset * -1, Quaternion.identity);
            //Remember that transformpoition refers to whatever object this is attached
            bombSpawned++;
            inOffset = inOffset + offset;
        }
    }

    private void CornerBombs()
    {
        Vector3 randomOffset = new Vector3 ();
        randomOffset.x = (Random.Range(0, 2)*2-1);
        randomOffset.y = (Random.Range(0, 2)*2-1);
        Instantiate(bombPrefab, transform.position + randomOffset, Quaternion.identity);
    }

    private void WarpPlayerToTarget ()
    {
        Vector3 currentPosition = transform.position; //capture current position
        Vector3 targetPosition = enemyTransform.position; //capture enemy position

        transform.position = Vector3.Lerp (currentPosition, targetPosition, warpAmount);
        //warp using lerp with it being able to control from unity

    }

    private void PlayerMovement ()
    {
        Vector2 playerInput = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerInput += Vector2.left;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerInput += Vector2.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            playerInput += Vector2.down;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerInput += Vector2.right;
        }

        if (playerInput.magnitude > 0)
        {
            velocity += (Vector3)playerInput.normalized * acceleration * Time.deltaTime;

            if (velocity.magnitude > maxSpeed)
            {
                velocity = velocity.normalized * maxSpeed;
            }
        }
        else
        {
            Vector3 changeInVelocity = velocity.normalized * deceleration * Time.deltaTime;
            if (changeInVelocity.magnitude > velocity.magnitude)
            {
                velocity = Vector3.zero;
            }
            else 
            {
                velocity -= changeInVelocity;
            }
        }

        transform.position += velocity * Time.deltaTime;

    }

    private void Radar ()
    {
        if (numberOfPointsForRadar < 3) return;
        //Making sure its a circle and not just a line. 

        bool enemyInRange = false;

        if (enemies != null)
        {
            foreach (Transform enemy in enemies)
            {
                if (enemy && Vector3.Distance(transform.position, enemy.position) <= radarRadius)
                {
                    enemyInRange = true;
                    break;
                }
            }
        }

        Color color = enemyInRange ? Color.red : Color.green;

        Vector3 prevPoint = transform.position + new Vector3(radarRadius, 0f, 0f);

        for (int i = 1; i <= numberOfPointsForRadar; i++)
        {
            float angle = (i / (float)numberOfPointsForRadar) * Mathf.PI * 2f;
            Vector3 nextPoint = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radarRadius;

            Debug.DrawLine(prevPoint, nextPoint, color);
            prevPoint = nextPoint;
        }

    }

    private void SpawningPowers ()
    {
        for (int i = 0; i < numberOfPowerUps; i++)
        {
            // Create positions for each one by deviding it then multiplying it by circle
            float angle = (i / (float)numberOfPowerUps) * Mathf.PI * 2f;

            Vector3 spawnPos = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * distanceRadius;

            Instantiate(powerUpsPrefab, spawnPos, Quaternion.identity);
        }
    }
}
