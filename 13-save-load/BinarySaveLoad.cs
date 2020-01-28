using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class BinarySaveLoad : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerData
    {
        public string sPlayerName;
        public int iCurrentAmmo;
        public float fCurrentHealth;
        public bool bHasPlayerDoneSomething;
    }

    public void BinarySave(PlayerData DataToUse)
    {
        FileStream tempStreamToUse;
        BinaryFormatter tempFormatter = new BinaryFormatter();
        string sPathToUse = Application.persistentDataPath + "/BinaryData.save";

        if (File.Exists(sPathToUse))
        {
            tempStreamToUse = File.OpenWrite(sPathToUse);
        }
        else
        {
            tempStreamToUse = File.Create(sPathToUse);
        }

        tempFormatter.Serialize(tempStreamToUse, DataToUse);
        tempStreamToUse.Close();
    }

    public PlayerData BinaryLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/BinaryData.save"))
        {
            BinaryFormatter tempFormatter = new BinaryFormatter();
            FileStream tempStreamToUse = File.OpenRead(Application.persistentDataPath + "/BinaryData.save");
            PlayerData tempData = (PlayerData)tempFormatter.Deserialize(tempStreamToUse);
            tempStreamToUse.Close();
            return tempData;
        }

        return new PlayerData();
    }
}
