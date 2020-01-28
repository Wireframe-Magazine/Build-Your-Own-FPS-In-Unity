using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretBehaviour : MonoBehaviour
{
    public ParticleSystem particleFX;
    public AudioClip soundFX;
    public float damageAmount = 10;
    private AudioSource audioSource;
    private GameObject target;
    private bool lookingAt;


    void Start()
    {
        StartCoroutine(Fire());
        audioSource = GetComponent<AudioSource>();
        if (soundFX && audioSource)
        {
            audioSource.clip = soundFX;
        }
        else
        {
            Debug.LogWarning("No audio source and/or effect assigned.");
            return;
        }
    }


    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Target");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }


    void Update()
    {
        target = FindClosestEnemy();
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        Vector3 targetDir;
        // Rotate the camera every frame so it keeps looking at the target
        if (target != null)
        {
            targetDir = target.transform.position - transform.position;
            float step = 2 * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(fwd, targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
            if (Physics.Raycast(transform.position, fwd, out hit))
            {
                Debug.DrawRay(transform.position, fwd * 20, Color.green);
                if (hit.collider.tag == "Target")
                {
                    lookingAt = true;
                }
                else
                {
                    lookingAt = false;
                }
            }
        }
        else
        {
            lookingAt = false;
        }
    }


    IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            //Firing effect and damage will only occur if the target can be seen.
            if (lookingAt)
            {
                //Play the particle effect.
                if (particleFX != null)
                {
                    particleFX.Play();
                }
                //Play our firing audio effect.
                if (audioSource && soundFX)
                {
                    audioSource.Play();
                }
                //Apply damage to the target via send message function.
                target.transform.SendMessage("ApplyDamage", damageAmount);
            }
            yield return null;
        }
    }
}
