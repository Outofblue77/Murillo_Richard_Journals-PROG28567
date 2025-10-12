using UnityEngine;
using UnityEngine.Rendering;

public class forceField : MonoBehaviour
{
    public Transform orbitingSprite;

    public float orbitSpeed = 50f;
    public float orbitDistance = 2f;

    private float angle;

    private void Start()
    {
        SetInitialPosition();
    }
    void Update()
    {
        RotateSprite();
    }

    private void SetInitialPosition()
    {
        orbitingSprite.position = transform.position + new Vector3(orbitDistance, 0f, 0f);
    }

    private void RotateSprite()
    {
        angle -= orbitSpeed * Time.deltaTime;

        float radians = angle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0f) * orbitDistance;

        orbitingSprite.position = transform.position + offset;
    }
}
