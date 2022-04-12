using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class aSaveManager 
{
    public static void aSave(aGameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(GetPath(), FileMode.Create);
        formatter.Serialize(fs, data);
        fs.Close();
    }
    public static aGameData Load()
    {
        if (!File.Exists(GetPath()))
        {
            aGameData emptyData = new aGameData();
            aSave(emptyData);
            return emptyData;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(GetPath(), FileMode.Open);
        aGameData data = formatter.Deserialize(fs) as aGameData;
        fs.Close();
        return data;
    }
    private static string GetPath()
    {
        return Application.persistentDataPath + "/s1data.db";
    }
}
