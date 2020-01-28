using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public GameObject go;
    public bool active;
    public Enemy(GameObject newGo, bool newBool)
    {
        go = newGo;
        active = newBool;
    }
}

public class Spawner : MonoBehaviour
{
    public GameObject spawn;
    public int amount = 1;
    public float delaySpawn = 1;
    public bool spawnsDead;

    private int getAmount;
    private float timer;
    private int spawned;
    private int enemyDead;

    public List<Enemy> enemies = new List<Enemy>();

    public void Start()
    {
        GameManager.RoundComplete += ResetRound;
        ResetRound();
        while (spawned < getAmount)
        {
            //Increment the amount spawned count.
            spawned++;
            //Create the prefab as an instance.
            GameObject instance = Instantiate(spawn, transform);
            enemies.Add(new Enemy(instance, false));
            //Removes the spawned object from the spawner object.
            instance.transform.parent = null;
            instance.SetActive(false);
        }
        ResetRound();
    }
    public void ResetRound()
    {
        spawnsDead = false;
        getAmount = amount;
        spawned = 0;
        timer = 0;
        enemyDead = 0;
    }

    void Update()
    {
        //Increase timer per frame.
        timer += Time.deltaTime;
        //Do the spawn if our timer is larger than the delay spawn we set.
        if (delaySpawn < timer)
        {
            //And we haven’t reached the spawn amount.
            if (spawned < getAmount)
            {
                //Reset our timer.
                timer = 0;
                //Set our bool to track the state of the enemy.
                enemies[spawned].active = true;
                //Set the enemy to be active.
                enemies[spawned].go.SetActive(true);
                //Get ready to set isKinematic.
                StartCoroutine(SetKinematic(spawned));
                //Increment the amount spawned count.
                spawned++;
            }

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                //If another script disabled the object but we set them active above.
                if (enemies[i].go.activeSelf == false && enemies[i].active == true)
                {
                    //Reset the spawn position and set our tracking bool that they are not active.
                    enemies[i].go.transform.position = transform.position;
                    enemies[i].active = false;
                    enemyDead++;
                }
            }

            if (enemyDead == enemies.Count)
            {
                spawnsDead = true;
            }
        }
    }

    IEnumerator SetKinematic(int id)
    {
        //We set isKinematic at the start of the next frame to avoid confusion with other commands.
        yield return null;
        enemies[id].go.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnDrawGizmos()
    {
        //Draw the wireframe mesh of what we intend to spawn in our editor.
        Gizmos.color = Color.red;
        if (spawn != null)
        {
            Gizmos.DrawWireMesh(spawn.GetComponent<MeshFilter>().sharedMesh, transform.position, spawn.transform.rotation, Vector3.one);
        }
    }
}

