using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    GameObject closestEnemy = null;
    float closestDistance = Mathf.Infinity;
    Vector2 direction;
    private Rigidbody2D rb;
    public float speed = 5f;
    public LayerMask enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance <= 5f)
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

            Debug.Log(closestEnemy);
            Debug.Log("Direction: " + direction);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // turns cordinate difference into a rotation
            transform.rotation = Quaternion.Euler(0, 0, angle); // turns the object based on rotation
            

            
            Debug.Log("velocity: " + rb.linearVelocity);
        }
        ;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.position += (Vector3)direction * 5f * Time.deltaTime; // SHOULD move the object to the target

        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == enemy)
        {

        }
    }
}
