using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            SpawnBombAtOffset(new Vector3(0, 1));
        }

    }

    private void SpawnBombAtOffset(Vector3 inOffset) //Added private
    {
        Instantiate(bombPrefab, transform.position + inOffset, Quaternion.identity);
        //Remember that transformpoition refers to whatever object this is attached
    }

}
