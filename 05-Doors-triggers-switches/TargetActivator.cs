using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple triggerable to activate or deactive objects it's attached to
// Useful for spawners etc.
public class TargetActivator : Triggerable
{
    public bool onceOnly = true;
    public bool deactivateOnAwake = true;

    private bool _triggered = false;

    void Awake ()
    {
        if (deactivateOnAwake)
        {
            gameObject.SetActive(false);
        }
    }

    public override void Trigger (TriggerAction action)
    {
        if (onceOnly && _triggered) { return; }

        if (action == TriggerAction.Trigger || action == TriggerAction.Activate)
        {
            gameObject.SetActive(true);
        }
        else if (action == TriggerAction.Deactivate)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        _triggered = true;
    }
}