using UnityEngine;
using UnityEngine.UI;

public class ObjectiveMarker : MonoBehaviour
{
    public Transform target;
    public Text display;
    public float distance = 3f;
    public float fadeTime = 0.3f;
    private float angle;
    private RectTransform rectTransform;
    private GameObject player;
    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        player = GameObject.FindGameObjectWithTag("MainCamera");
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentDist;
        transform.LookAt(target);
        //When we have a target, rotate our arrow to its approx direction.
        if (target != null)
        {
            Vector3 relative = player.transform.InverseTransformPoint(target.position);
            angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
            //Fixes the fact that we want to rotate the arrow clockwise and not anti-clockwise.
            angle *= -1;
        }
        //Sets the rotation for our visual arrow.
        rectTransform.transform.eulerAngles = new Vector3(0, 0, angle);
        //Find the distance from the player.
        currentDist = Vector3.Distance(player.transform.position, target.transform.position);
        //Display the distance in meters.
        if (display != null)
            display.text = (Mathf.Round(currentDist * 10f) / 10f).ToString() + " Meters";
        //If we are looking at the target and in X meters of the target.
        //We will fade off the arrow so it doesn't clutter the screen.
        if (angle <= 30 && angle >= -30 && currentDist < distance)
        {
            img.CrossFadeAlpha(0, fadeTime, false);
            if (display != null) display.CrossFadeAlpha(0, fadeTime, false);
        }
        else
        {
            img.CrossFadeAlpha(1, fadeTime, false);
            if (display != null) display.CrossFadeAlpha(1, fadeTime, false);
        }
    }
}
