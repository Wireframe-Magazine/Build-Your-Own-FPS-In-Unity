using UnityEngine;


public class WeakSpot : MonoBehaviour
{
    private GameObject recGO;
    private BossHealth bossHealth;

    private void Start()
    {
        recGO = transform.root.gameObject;
        bossHealth = recGO.GetComponent<BossHealth>();
    }


    public void OnCollisionEnter(Collision col)
    {
        string name = gameObject.name;
        bossHealth.ReceiveCollision(ref col, ref name);
    }
}
