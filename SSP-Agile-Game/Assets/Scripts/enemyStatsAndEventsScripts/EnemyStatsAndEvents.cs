using Mono.Cecil.Cil;
using UnityEngine;

public class EnemyStatsAndEvents : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;
    public int attackDamage = 10;

    private int amountOfKills = 0; 
    
    

    void Start()
    {
        currentHealth = maxHealth;
    }
   

    public void isHit(int damage)
    {
        //Debug.Log("enemy hit, dealt " + damage + " damage");

        if (currentHealth >= 0)
        {
            currentHealth -= damage;


        }
        //old death code
        //else
        //{
        //    //Debug.Log("meow, enemy died");
            
        //    Destroy(this.gameObject);

        //}

        if (currentHealth <= 0)
        {
            GameManager.Instance.AddKill();
            GameManager.Instance.EnemyDied();
            Destroy(gameObject);
            
        }

    }


}
