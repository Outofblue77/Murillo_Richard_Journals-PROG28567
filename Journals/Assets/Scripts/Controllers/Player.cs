using System.Collections.Generic;
using UnityEngine;
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

    public float maxSpeed = 1f;
    public float accelerationTime = 1f;
    private Vector3 velocity;
    private float acceleration;


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

        PlayerMovement();

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
        acceleration = maxSpeed / accelerationTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            velocity += acceleration * Time.deltaTime * Vector3.left;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            velocity += acceleration * Time.deltaTime * Vector3.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            velocity += acceleration * Time.deltaTime * Vector3.down;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            velocity += acceleration * Time.deltaTime * Vector3.right;
        }

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += Time.deltaTime * velocity;

    }

    //private void DetectAstroid (float inRange, List<Transform>inAsteroid)
    //{
    //Vector3 currentPosition = transform.position;

    //Vector3 aestroidPosition = aestroidTransform;

    //Debug.DrawLine (currentPosition, aestroidPosition, Color.blue);

    //}

}
