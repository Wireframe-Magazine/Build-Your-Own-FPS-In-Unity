using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefExample : MonoBehaviour
{
    public void PlayerPrefSave(int iAmmoToSave, float fHealthToSave)
    {
        PlayerPrefs.SetInt("currentAmmo", iAmmoToSave);
        PlayerPrefs.SetFloat("currentHealth", fHealthToSave);
    }

    public void PlayerPrefLoad(ref int iAmmoToUse, ref float fHealthToUse)
    {
        iAmmoToUse = PlayerPrefs.GetInt("currentAmmo");
        fHealthToUse = PlayerPrefs.GetFloat("currentHealth");
    }
}
