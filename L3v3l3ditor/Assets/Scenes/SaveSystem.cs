// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.Runtime.Serialization.Formatters.Binary;

// public static class SaveSystem 
// {
//     public static void SavePlayer (Data Data)
//     {
//         BinaryFormatter formatter = new BinaryFormatter();
//         string path = Application.persistentDataPath + "/player.L3v3l3ditor";
//         FileStream stream = new FileStream(path, FileMode.Create);

//         PlayerData data = new PlayerData(Data);
        
//         formatter.Serialize(stream, data);
//         stream.Close();
//     }
    
//     public static PlayerData LoadPlayer ()
//     {
//         string path = Application.persistentDataPath + "/player.L3v3l3ditor";
//         if (File.Exists(path))
//         {
//             BinaryFormatter formatter = new BinaryFormatter();
//             FileStream stream = new FileStream(path, FileMode.Open);

//            //  PlayerData data = formatter.Deserialize(stream) as PlayerData;
//             stream.Close();

//             return data;
//         } else
//         {
//             Debug.LogError("Save File Not Found In" + path);
//             return null;
//         }
//     }
// }
