using UnityEngine;

public class SendDamage : MonoBehaviour
{

    void OnCollisionStay(Collision other)
    {
        //We compare the tag in the other object to the tag name we set earlier.
        if (other.transform.CompareTag("Player"))
        {
            //If the above matches, then send a message to the other object.
            //This will also pass a value of 1 for our damage.
            other.transform.SendMessage("ApplyDamage", 1);
        }
    }
}
