using System.Security.Cryptography;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private PlayerController playerCon;
    public float moveSpeed;
    public Rigidbody theRB;
    public float chaseRange;
    public float stopCloseRange;
    private float strafeAmount;
    public Animator anim;
    public Transform[] patrolPoints;
    private int currentPatrolPoint;
    public Transform patrolHolder;
    private bool isDead = false;
    public float patrolWaitTime = 3f;
    private float waitCounter;
    public float currentHealth = 25f;
    public float waitToDisappear = 4f;
    public Transform shootPoint;
    public EnemyProjectile enemyProjectile;
    public float timeBetweenShots;
    private float shotCounter;
    public float shotDamage;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCon = FindFirstObjectByType<PlayerController>();
        strafeAmount = Random.Range(-.75f, .75f);
        patrolHolder.SetParent(null);
        waitCounter = Random.Range(.45f, 1.25f) + patrolWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            waitToDisappear -= Time.deltaTime;
            if (waitToDisappear <= 0f)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime);
                if (transform.localScale == Vector3.zero)
                {
                    Destroy(gameObject);
                    Destroy(patrolHolder.gameObject);
                }
            }

            return;
        }

        float yStore = theRB.linearVelocity.y;

        float distance = Vector3.Distance(transform.position, playerCon.transform.position);

        if (distance < chaseRange)
        {
            transform.LookAt(new Vector3(playerCon.transform.position.x, transform.position.y, playerCon.transform.position.z));

            if (distance > stopCloseRange)
            {
                theRB.linearVelocity = (transform.forward + transform.right * strafeAmount) * moveSpeed;
                anim.SetBool("Moving", true);
            }
            else
            {
                theRB.linearVelocity = Vector3.zero;
                anim.SetBool("Moving", false);
            }

            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0f)
            {

                shootPoint.LookAt(playerCon.playerCamera.transform.position);
                shotCounter = timeBetweenShots;

                EnemyProjectile newProjectile = Instantiate(enemyProjectile, shootPoint.position, shootPoint.rotation);
                newProjectile.damageAmount = shotDamage;
                shotCounter = timeBetweenShots;

                anim.SetTrigger("Shooting");
            }

        }
        else
        {
            if (patrolPoints.Length > 0)
            {
                if (Vector3.Distance(transform.position, new Vector3(patrolPoints[currentPatrolPoint].position.x, transform.position.y, patrolPoints[currentPatrolPoint].position.z)) < .25f)
                {

                    waitCounter -= Time.deltaTime;
                    theRB.linearVelocity = Vector3.zero;
                    anim.SetBool("Moving", false);
                    if (waitCounter <= 0f)
                    {
                        currentPatrolPoint++;

                        if (currentPatrolPoint >= patrolPoints.Length)
                        {
                            currentPatrolPoint = 0;
                        }
                        waitCounter = Random.Range(.45f, 1.25f) + patrolWaitTime;
                    }
                }
                else
                {
                    transform.LookAt(new Vector3(patrolPoints[currentPatrolPoint].position.x, transform.position.y, patrolPoints[currentPatrolPoint].position.z));
                    theRB.linearVelocity = transform.forward * moveSpeed;
                    anim.SetBool("Moving", true);
                }
            }
            else
            {
                theRB.linearVelocity = Vector3.zero;
                anim.SetBool("Moving", false);
            }

        }

        theRB.linearVelocity = new Vector3(theRB.linearVelocity.x, yStore, theRB.linearVelocity.z);
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;
        if (currentHealth <= 0)
        {
            anim.SetTrigger("Dead");
            isDead = true;
            theRB.linearVelocity = Vector3.zero;
            theRB.isKinematic = true;
            GetComponent<Collider>().enabled = false;
        }
    }

}
