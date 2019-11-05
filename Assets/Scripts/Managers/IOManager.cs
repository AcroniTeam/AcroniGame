using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class IOManager
{

    public static void SaveProgress()
    {
        string path = Application.persistentDataPath + "/player.ada";

        BinaryFormatter bnf = new BinaryFormatter();
        FileStream fs = new FileStream(path, FileMode.OpenOrCreate);

        PlayerData pd = new PlayerData(Player.getInstance());

        bnf.Serialize(fs,pd);
        fs.Close();
    }

    public static PlayerData RetriveData()
    {
        string path = Application.persistentDataPath + "/player.ada";

        if (File.Exists(path))
        {
            BinaryFormatter bnf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            PlayerData pd = bnf.Deserialize(fs) as PlayerData;
            fs.Close();

            return pd;
        }else
        {
            Debug.LogError("File in " + path + " not fount");
            return null;
        }
    }
}
