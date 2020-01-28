using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WallRunning : MonoBehaviour
{
    public AudioClip audioClip;
    private CharacterMovement cm;
    private Rigidbody rb;
    private bool isJumping;
    public bool isWall;
    private bool playAudio;
    private AudioSource audioSource;
    public float energyLimit = 3.5f;


    private void Start()
    {
        //Get attached components so we can interact with them in our script.
        cm = GetComponent<CharacterMovement>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        bool jumpPressed = Input.GetButtonDown("Jump");
        float verticalAxis = Input.GetAxis("Vertical");
        //Check if the controller is grounded.
        if (cm.Grounded)
        {
            isJumping = false;
            isWall = false;
        }
        //Has the jump button been pressed.
        if (jumpPressed)
        {
            StartCoroutine(Jumping());
        }
        //If we are pushing forward, and not grounded, and touching a wall.
        if (verticalAxis > 0 && isJumping && isWall)
        {
            StartCoroutine(Energy());
            //We constrain the Y/Z direction to defy gravity and move off the wall.
            //But we can still run forward as we ignore the X direction.
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
            //We also telegraph to the player by playing a sound effect on contact.
            if (audioClip != null && playAudio == true)
            {
                audioSource.PlayOneShot(audioClip);
                //We block more audio being played while we are on the wall.
                playAudio = false;
            }
        }
        else
        {
            StopCoroutine(Energy());
            //We need to make sure we can play audio again when touching the wall.
            playAudio = true;
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }


    void OnCollisionEnter(Collision other)
    {
        //Are we touching a wall object?
        if (other.gameObject.tag == "Walls")
        {
            isWall = true;
        }
    }


    void OnCollisionExit(Collision other)
    {
        //Did we stop touching the wall object?
        if (other.gameObject.tag != "Walls")
        {
            isWall = false;
        }
    }


    IEnumerator Jumping()
    {
        //Check for 5 frames after the jump button is pressed.
        int frameCount = 0;
        while (frameCount < 5)
        {
            frameCount++;
            //Are we airbourne in those 5 frames?
            if (!cm.Grounded)
            {
                isJumping = true;
            }
            yield return null;
        }
    }

    IEnumerator Energy()
    {
        yield return new WaitForSeconds(energyLimit);
        isWall = false;
    }
}
