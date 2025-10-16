using UnityEngine;
using UnityEngine.Rendering;

public class forceField : MonoBehaviour
{
    public Transform orbitingShield;
    public Transform enemy;
    private SpriteRenderer shieldRenderer;

    public float orbitSpeed = 50f;
    public float orbitDistance = 2f;
    private float angle;

    public float hitDistance = 0.5f;

    public Color idleColor = Color.blue;
    public Color hitColor = Color.red;
      
    private void Start()
    {
        shieldRenderer = orbitingShield.GetComponent<SpriteRenderer>();
        
        orbitingShield.position = transform.position + Vector3.right * orbitDistance;
        orbitingShield.rotation = Quaternion.Euler(0f, 0f, 90f);
        shieldRenderer.color = idleColor;
    }

    private void Update()
    {
        HandleOrbit();
        HandleColor();
    }
    private void HandleOrbit()
    {
        angle -= orbitSpeed * Time.deltaTime;
        float radians = angle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0f) * orbitDistance;
        orbitingShield.position = transform.position + offset;

        float rotationAngle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg + 90f;
        orbitingShield.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
    }

    private void HandleColor()
    {
        float dist = Vector3.Distance(orbitingShield.position, enemy.position);
        shieldRenderer.color = (dist <= hitDistance) ? hitColor : idleColor;
    }
}