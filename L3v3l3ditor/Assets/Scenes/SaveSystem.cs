using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SavePlayer (Data Data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.L3v3l3ditor";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(Data);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    
}
