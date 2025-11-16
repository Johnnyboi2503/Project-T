using UnityEngine;

public class BossController : MonoBehaviour
{
    public float speed = 6.19f;
    public float enemyRadius = 20;
    public float attackAgainTimer;
    public bool hasAttacked;

    public GameObject target;
    Rigidbody2D RB;

    private float distance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player"); // so that instantiated enemys know who to chase
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vel = new Vector2(0, 0);

        distance = Vector2.Distance(transform.position, target.transform.position);
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (distance < enemyRadius) // if the player goes inside the enemy radius the enemy will chase the player, as long as the player is still in the radius
        {
            transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }

        if (hasAttacked == true)
        {
            enemyRadius = 0;
            attackAgainTimer += Time.deltaTime;
            if (attackAgainTimer > 0.15)
            {
                enemyRadius = 20;
                hasAttacked = false;
                attackAgainTimer = 0;
            }
        }





        RB.linearVelocity = vel;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out var health) && collision.gameObject.tag.Equals("Player")) // deal damage to the player when colldiing with it
        {
            health.Damage(amount: 30);
            hasAttacked = true;
        }        
    }

    void OnDrawGizmosSelected() // shows the radius the enemy has
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyRadius);
    }
}
