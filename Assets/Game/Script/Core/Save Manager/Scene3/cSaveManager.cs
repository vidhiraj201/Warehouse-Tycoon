using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class cSaveManager 
{
    public static void cSave(cGameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(GetPath(), FileMode.Create);
        formatter.Serialize(fs, data);
        fs.Close();
    }
    public static cGameData Load()
    {
        if (!File.Exists(GetPath()))
        {
            cGameData emptyData = new cGameData();
            cSave(emptyData);
            return emptyData;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(GetPath(), FileMode.Open);
        cGameData data = formatter.Deserialize(fs) as cGameData;
        fs.Close();
        return data;
    }
    private static string GetPath()
    {
        return Application.persistentDataPath + "/s3data.db";
    }
}
