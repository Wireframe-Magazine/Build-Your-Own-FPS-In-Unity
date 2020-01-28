using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Putting this before our class makes sure that we can't fire this code without our PlayerPrefExample code attached.
[RequireComponent(typeof(BinarySaveLoad))]
public class ExampleBinarySaveLoad : MonoBehaviour
{
    BinarySaveLoad ourSaveScript;
    BinarySaveLoad.PlayerData ourPlayerData;

    // Start is called before the first frame update
    void Start()
    {
        // Find our Save Game Script
        ourSaveScript = GetComponent<BinarySaveLoad>();

        // Fire the function to get our data vales.
        ourPlayerData = ourSaveScript.BinaryLoad();

        //Write our values to the log, so we can see if they were correctly loaded.
        Debug.Log("Our current Ammo = " + ourPlayerData.iCurrentAmmo + ", and our health value = " + ourPlayerData.fCurrentHealth);

        PickedUpAmmo(10);
        PickedUpHealthPack(5.0f);
    }

    void PickedUpAmmo(int iAmountOfAmmoPickedUp)
    {
        ourPlayerData.iCurrentAmmo += iAmountOfAmmoPickedUp;
    }

    void PickedUpHealthPack(float fHealthToAdd)
    {
        ourPlayerData.fCurrentHealth += fHealthToAdd;
    }

    private void OnApplicationQuit()
    {
        // Find our Save Game Script
        ourSaveScript = gameObject.GetComponent<BinarySaveLoad>();

        //Write our values to the log, so we can see if they were correctly loaded.
        Debug.Log("We are saving the ammo value of = " + ourPlayerData.iCurrentAmmo + " and the health value of = " + ourPlayerData.fCurrentHealth);

        // Fire the function using our ammo and health values
        ourSaveScript.BinarySave(ourPlayerData);
    }
}
