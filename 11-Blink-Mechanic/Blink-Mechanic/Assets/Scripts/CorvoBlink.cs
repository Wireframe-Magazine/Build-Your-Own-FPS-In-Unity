using UnityEngine;

public class CorvoBlink : MonoBehaviour
{
    public GameObject particlePrefab;
    GameObject particleFX;
    Vector3 destination;
    bool FXVisible = false;

    private void Start()
    {
        particleFX = Instantiate(particlePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Draw debug lines to aid visualisation.
                if (hit.normal.y > 0.5f)
                {
                    Debug.DrawLine(transform.position, hit.point, Color.green);
                    destination = hit.point;
                    FXVisible = true;
                }
                else
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    destination = transform.position;
                    FXVisible = false;
                }
            }
            else
            {
                destination = transform.position;
                FXVisible = false;
            }
        }
        if (Input.GetMouseButtonUp(0) && transform.position != destination)
        {
            destination.y += 0.5f;
            transform.parent.position = destination;
            FXVisible = false;
        }
        if (FXVisible)
        {
            particleFX.transform.position = destination;
            particleFX.SetActive(true);
        }
        else
        {
            particleFX.SetActive(false);
        }
    }
}
