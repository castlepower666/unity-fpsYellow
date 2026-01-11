using System.Security.Cryptography;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private PlayerController playerCon;

    public float moveSpeed;
    public Rigidbody theRB;

    public float chaseRange;
    public float stopCloseRange;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCon = FindFirstObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        float yStore = theRB.linearVelocity.y;

        float distance = Vector3.Distance(transform.position, playerCon.transform.position);

        if (distance < chaseRange)
        {
            transform.LookAt(new Vector3(playerCon.transform.position.x, transform.position.y, playerCon.transform.position.z));

            if (distance > stopCloseRange)
            {
                theRB.linearVelocity = transform.forward * moveSpeed;
            }
            else
            {
                theRB.linearVelocity = Vector3.zero;
            }
        }
        else
        {
            theRB.linearVelocity = Vector3.zero;
        }

        theRB.linearVelocity = new Vector3(theRB.linearVelocity.x, yStore, theRB.linearVelocity.z);

    }
}
