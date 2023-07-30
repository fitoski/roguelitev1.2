using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem { 
    public static void SavePlayerProgress(PlayerProgress playerProgress)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.sd";

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, playerProgress);
        stream.Close();

        Debug.Log("SAVED PLAYER DATA");
    }

    public static PlayerProgress LoadPlayerProgress()
    {
        string path = Application.persistentDataPath + "/player.sd";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerProgress playerProgress =  binaryFormatter.Deserialize(stream) as PlayerProgress;
           
            stream.Close();

            Debug.Log("LOADED PLAYER DATA");
            return playerProgress;
        }
        else
        {
            return null;
        }
    }

    public static void SaveLevelProgress(CurrentLevelProgress levelProgress)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/level.sd";

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, levelProgress);
        stream.Close();

        Debug.Log("SAVED LEVEL DATA");
    }

    public static CurrentLevelProgress LoadLevelProgress()
    {
        string path = Application.persistentDataPath + "/level.sd";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CurrentLevelProgress levelProgress = binaryFormatter.Deserialize(stream) as CurrentLevelProgress;

            stream.Close();

            Debug.Log("LOADED LEVEL DATA");
            return levelProgress;
        }
        else
        {
            return null;
        }
    }

    public static void DeleteLevelProgress()
    {
        string path = Application.persistentDataPath + "/level.sd";
        if (File.Exists(path)) File.Delete(path);
    }
}
