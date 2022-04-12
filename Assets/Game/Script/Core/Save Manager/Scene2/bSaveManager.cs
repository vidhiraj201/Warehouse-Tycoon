using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class bSaveManager 
{
    public static void bSave(bGameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(GetPath(), FileMode.Create);
        formatter.Serialize(fs, data);
        fs.Close();
    }
    public static bGameData Load()
    {
        if (!File.Exists(GetPath()))
        {
            bGameData emptyData = new bGameData();
            bSave(emptyData);
            return emptyData;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(GetPath(), FileMode.Open);
        bGameData data = formatter.Deserialize(fs) as bGameData;
        fs.Close();
        return data;
    }
    private static string GetPath()
    {
        return Application.persistentDataPath + "/s2data.db";
    }
}
