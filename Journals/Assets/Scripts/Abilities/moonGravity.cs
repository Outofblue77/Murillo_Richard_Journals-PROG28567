using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class moonGravity : MonoBehaviour
{
    public float gravtyForce = 2f;
    public float gravityDistance = 3f;
    public float idleTreshHold = 0.1f;

    public Transform player;

    private Vector3 lastPosition;
 


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gravity();
    }

    void gravity()
    {
        bool isIdle = Vector3.Distance(player.position, lastPosition) < idleTreshHold;
        lastPosition = player.position;

        if (isIdle && player.position.y <= gravityDistance)
        {
            player.position += Vector3.down * gravtyForce * Time.deltaTime;
        }
        Debug.DrawLine(new Vector3(-50, gravityDistance, 0), new Vector3(50, gravityDistance, 0), Color.cyan);
    }
}
