using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class JSONSaveLoad : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerData
    {
        public string sPlayerName;
        public int iCurrentAmmo;
        public float fCurrentHealth;
        public Vector3 vPlayerLocation;
        public bool bHasPlayerDoneSomething;
    }

    public void JSONSave(PlayerData DataToUse)
    {
        var tempFormatter = new StreamWriter(Application.persistentDataPath + "/OurData.save", false);
        tempFormatter.WriteLine(JsonUtility.ToJson(DataToUse));
        tempFormatter.Close();
    }

    public PlayerData JSONLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/OurData.save"))
        {
            var tempFormatter = new StreamReader(Application.persistentDataPath + "/OurData.save");
            PlayerData tempPlayerDatatoReturn = JsonUtility.FromJson<PlayerData>(tempFormatter.ReadToEnd());
            return tempPlayerDatatoReturn;
        }

        return new PlayerData();
    }
}

