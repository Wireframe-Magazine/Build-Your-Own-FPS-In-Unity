using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 10;
    public bool respawn;
    public float delaySpawn = 30;


    void OnCollisionEnter(Collision other)
    {
        //We compare the tag in the other object to the tag name we set earlier.
        if (other.transform.CompareTag("Player"))
        {
            //We disable the mesh renderer to make it look like it's been picked up.
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            //We disable the collider once it's picked up.
            gameObject.GetComponent<Collider>().enabled = false;
            other.transform.SendMessage("ApplyHeal", healthAmount);
            //If we choose to we can make it respawn after X seconds.
            if (respawn)
            {
                Invoke("Respawn", delaySpawn);
            }
        }
    }

    void Respawn()
    {
        //We make the pickup visible again.
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        //The collider is enabled so we can pick it up again.
        gameObject.GetComponent<Collider>().enabled = true;
    }
}
