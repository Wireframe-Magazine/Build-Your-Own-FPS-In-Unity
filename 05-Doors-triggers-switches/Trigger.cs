using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Trigger : MonoBehaviour
{
    // Targets are only active when the player is inside
    // the trigger and overrides the setting of action.
    public bool inOut = false; // doesn't work with required item or onceOnly
    public bool onceOnly = false;
    public TriggerAction action = TriggerAction.Trigger;
    public Triggerable[] targets;

    [Header("Item Required To Trigger")]
    public string requiredItemName = "";
    public bool takeItem = false;

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!string.IsNullOrEmpty(requiredItemName))
            {
                Inventory inv = other.gameObject.GetComponent<Inventory>();
                if (inv != null && inv.HasItem(requiredItemName, takeItem))
                {
                    TriggerTargets(action);
                    gameObject.SetActive(false);
                }
            }
            else
            {
                TriggerTargets(inOut ? TriggerAction.Activate : action);
                if (onceOnly)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (inOut && other.CompareTag("Player"))
        {
            TriggerTargets(TriggerAction.Deactivate);
        }
    }

    public void TriggerTargets (TriggerAction action)
    {
        if (targets != null)
        {
            foreach (Triggerable t in targets)
            {
                // Check in case a target is destroyed
                if (t != null)
                {
                    t.Trigger(action);
                }
            }
        } 
    }

    void OnDrawGizmos ()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, Vector3.one * 0.25f);
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.25f);

        if (targets != null)
        {
            foreach (Triggerable t in targets)
            {
                if (t != null)
                {
                    Gizmos.DrawLine(transform.position, t.transform.position);
                }
            }
        }        
    }
}
