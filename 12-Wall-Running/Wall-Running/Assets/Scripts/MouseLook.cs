using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private GameObject player;
    private float minClamp = -45;
    private float maxClamp = 45;
    [HideInInspector]
    public Vector2 rotation;
    private Vector2 currentLookRot;
    private Vector2 rotationV = new Vector2(0, 0);
    public float lookSensitivity = 2;
    public float lookSmoothDamp = 0.1f;
    //Required if we are using the camera to freelook.
    private CharacterMovement cm;
    private bool resetRotation = false;

    void Start()
    {
        //Access the player GameObject.
        player = transform.parent.gameObject;
        cm = player.GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //Player input from the mouse
        rotation.y += Input.GetAxis("Mouse Y") * lookSensitivity;
        //Limit ability look up and down.
        rotation.y = Mathf.Clamp(rotation.y, minClamp, maxClamp);
        //Rotate the character around based on the mouse X position.
        //Unless we are not grounded or for the one frame where we set the player to match the camera.
        if (cm.Grounded)
        {
            if (resetRotation)
            {
                resetRotation = false;
                player.transform.localEulerAngles += new Vector3(0, currentLookRot.x, 0);
                currentLookRot.x = 0;
            }
            else
            {
                player.transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * lookSensitivity);
            }
        }
        else
        {
            resetRotation = true;
            //Free look in the Y rotation based on mouse.
            currentLookRot.x += Input.GetAxis("Mouse X") * lookSensitivity;
        }
        //Smooth the current Y rotation for looking up and down.
        currentLookRot.y = Mathf.SmoothDamp(currentLookRot.y, rotation.y, ref rotationV.y, lookSmoothDamp);
        //Update the camera X, Y rotation based on the values generated.
        transform.localEulerAngles = new Vector3(-currentLookRot.y, currentLookRot.x, 0);
    }
}
