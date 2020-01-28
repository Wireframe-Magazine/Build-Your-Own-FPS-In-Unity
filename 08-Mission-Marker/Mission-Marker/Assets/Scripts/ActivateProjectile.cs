using UnityEngine;

public class ActivateProjectile : MonoBehaviour
{
    public GameObject projectile;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var clone = Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
            //Destroy after 2 seconds to stop clutter.
            Destroy(clone, 5.0f);
        }
    }
}
