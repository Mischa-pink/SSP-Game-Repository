using UnityEngine;

public class BuildingLogic : MonoBehaviour
{
    public float towerRange = 5f;
    public int towerHealth;

    public float distanceTimer;
    GameObject closestEnemy = null;
    float closestDistance = Mathf.Infinity;

    public GameObject projectile;
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        distanceTimer += Time.deltaTime;
        if (distanceTimer >= 0.25f)
        {
            distanceTimer = 0;
            

           
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);

                if (distance <= towerRange)
                {
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = enemy;
                    }
                }
            }

            if (closestEnemy != null)
            {
                //Debug.Log("Closest enemy: " + closestEnemy.name);

                Vector2 direction =
                    (closestEnemy.transform.position - transform.position).normalized;

                //Debug.Log("Direction: " + direction);
                Instantiate(
                projectile,
                transform.position,
                transform.rotation
                );
            }
            closestEnemy = null;
            closestDistance = 5;
        }
    }
}
