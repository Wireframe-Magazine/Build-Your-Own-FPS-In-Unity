using UnityEngine;

public class TargetDamage : MonoBehaviour
{
    //Sets default health to 100
    public int health = 100;

    void ApplyDamage(int damage)
    {
        //Checks our health is greater than 0
        if (health > 0)
        {
            //Stores the current health and subtracts the damage value
            health = health - damage;
            //Shows the health in the log.
            Debug.Log("Health: " + health);
        }
        else
        {
            //Log a message to show it's destroyed
            Debug.Log("Destroyed!");
            //Disable object so it's not visible.
            gameObject.SetActive(false);
        }
    }
}