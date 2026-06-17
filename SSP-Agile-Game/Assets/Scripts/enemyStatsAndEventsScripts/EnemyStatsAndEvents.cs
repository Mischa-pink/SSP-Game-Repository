using Mono.Cecil.Cil;
using UnityEngine;

public class EnemyStatsAndEvents : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;
    

    void Start()
    {
        
         currentHealth = maxHealth;
        
    }
   

    public void isHit(int damage)
    {
        Debug.Log("enemy hit, dealt " + damage + " damage");

        if (currentHealth >= 0)
        {
            currentHealth -= damage;


        }
        else
        {
            Debug.Log("meow, enemy died");
            
            Destroy(this.gameObject);
           
        }

    }


}
