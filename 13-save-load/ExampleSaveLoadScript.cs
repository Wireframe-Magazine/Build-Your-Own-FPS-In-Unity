using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Putting this before our class makes sure that we can't fire this code without our PlayerPrefExample code attached.
[RequireComponent(typeof(PlayerPrefExample))]
public class ExampleSaveLoadScript : MonoBehaviour
{
    PlayerPrefExample ourSaveScript;
    int iCurrentAmmo;
    float fCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        // Find our Save Game Script
        ourSaveScript = GetComponent<PlayerPrefExample>();

        // Fire the function using our ammo and health values
        ourSaveScript.PlayerPrefLoad(ref iCurrentAmmo, ref fCurrentHealth);

        //Write our values to the log, so we can see if they were correctly loaded.
        Debug.Log("Our current Ammo = " + iCurrentAmmo + ", and our health value = " + fCurrentHealth);
    }

    void PickedUpAmmo(int iAmountOfAmmoPickedUp)
    {
        iCurrentAmmo += iAmountOfAmmoPickedUp;
    }

    void PickedUpHealthPack(float fHealthToAdd)
    {
        fCurrentHealth += fHealthToAdd;
    }

    private void OnApplicationQuit()
    {
        // Find our Save Game Script
        ourSaveScript = gameObject.GetComponent<PlayerPrefExample>();

        //Write our values to the log, so we can see if they were correctly loaded.
        Debug.Log("We are saving the ammo value of = " + iCurrentAmmo + " and the health value of = " + fCurrentHealth);

        // Fire the function using our ammo and health values
        ourSaveScript.PlayerPrefSave(iCurrentAmmo, fCurrentHealth);
    }
}
