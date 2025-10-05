using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    //public Transform planetTransform; - Idk why this is here

    public Transform orbitTarget;
    public GameObject moonPrefab;
    public float orbitRadius = 3f;
    public float orbitSpeed = 90f;

    private GameObject spawnedMoon;

    private void Start()
    {
        SpawnOrbitingMoon();
        StartCoroutine(OrbitMoon());
    }

    private void SpawnOrbitingMoon()
    {
        Vector3 startPosition = orbitTarget.position + new Vector3(orbitRadius, 0f, 0f);
        spawnedMoon = Instantiate(moonPrefab, startPosition, Quaternion.identity);
    }

    private IEnumerator OrbitMoon()
    {
        if (spawnedMoon == null || orbitTarget == null) yield break;

        float currentAngle = 0f;

        while (true)
        {
            currentAngle += orbitSpeed * Time.deltaTime;
            if (currentAngle >= 360f) currentAngle += 360f;

            float radians = currentAngle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0f) * orbitRadius;
            spawnedMoon.transform.position = orbitTarget.position + offset;

            yield return null; // wait until next frame
        }
    }
}
