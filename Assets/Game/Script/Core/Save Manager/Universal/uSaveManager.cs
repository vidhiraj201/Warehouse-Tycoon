using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class uSaveManager 
{
    public static void uSave(uGameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(GetPath(), FileMode.Create);
        formatter.Serialize(fs, data);
        fs.Close();
    }
    public static uGameData Load()
    {
        if (!File.Exists(GetPath()))
        {
            uGameData emptyData = new uGameData();
            uSave(emptyData);
            return emptyData;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(GetPath(), FileMode.Open);
        uGameData data = formatter.Deserialize(fs) as uGameData;
        fs.Close();
        return data;
    }
    private static string GetPath()
    {
        return Application.persistentDataPath + "/udata.db";
    }
}
