using UnityEngine;

public class Respawner : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;

    // Start is called before the first frame update
    void Awake()
    {
        startPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        startRot = GameObject.FindGameObjectWithTag("Player").transform.rotation;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            //Just reset the position and rotation to the one we recorded when we started the game.
            other.transform.position = startPos;
            other.transform.rotation = startRot;
        }
    }
}
