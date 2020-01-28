using UnityEngine;

public class BulletHit : MonoBehaviour
{

    //When we touch the collider we disable this object.
    void OnCollisionEnter()
    {
        gameObject.SetActive(false);
    }
}
