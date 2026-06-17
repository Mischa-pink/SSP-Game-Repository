using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    GameObject closestEnemy = null;
    float closestDistance = Mathf.Infinity;

    Vector2 direction;
    private Rigidbody2D rb;
    public float speed = 5f;
    public float range = 3f;

    public LayerMask enemyLayer;

    private int damage = 15;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance <= range)
            {
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }

        rb.linearVelocity = Vector2.right * 5f;

        if (closestEnemy != null) // if a enemy within 5f exists
        {

            direction =
                (closestEnemy.transform.position - transform.position).normalized; // target enemy location from current gameobject

            //Debug.Log(closestEnemy);
            //Debug.Log("Direction: " + direction);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // turns cordinate difference into a rotation
            transform.rotation = Quaternion.Euler(0, 0, angle); // turns the object based on rotation
            

            
            //Debug.Log("velocity: " + rb.linearVelocity);
        }
        Destroy(gameObject, range);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime); // SHOULD move the object to the target

        
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("collision detected");
    //    if (collision.gameObject.layer == enemyLayer)
    //    {
    //        Debug.Log("enemy has been hit");
    //        collision.gameObject.GetComponent<EnemyStatsAndEvents>().isHit(damage);
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"Hit object: {collision.gameObject.name}");
        //Debug.Log($"Tag: {collision.gameObject.tag}");
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("enemy has been hit");
            collision.gameObject.GetComponent<EnemyStatsAndEvents>().isHit(damage);
           
        }
        
            Destroy(gameObject);
        
            
    }


}
