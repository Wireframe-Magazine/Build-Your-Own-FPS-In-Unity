using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows a single trigger to trigger with multiple trigger actions or delays
public class Relay : Triggerable
{
    public TriggerAction action = TriggerAction.Activate;
    public float triggerDelay = 1f;
    public Triggerable[] targets;

    private bool _triggered = false;

    public override void Trigger (TriggerAction action)
    {
        if (!_triggered)
        {
            StartCoroutine(TriggerDelayed());
        }
    }

    IEnumerator TriggerDelayed ()
    {
        yield return new WaitForSeconds(triggerDelay);

        foreach (Triggerable triggerable in targets)
        {
            triggerable.Trigger(action);
        }
    }

    void OnDrawGizmos ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, Vector3.one * 0.25f);
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.25f);

        if (targets != null)
        {
            foreach (Triggerable triggerable in targets)
            {
                if (triggerable != null)
                {
                    Gizmos.DrawLine(transform.position, triggerable.transform.position);
                }
            }  
        }      
    }
}
