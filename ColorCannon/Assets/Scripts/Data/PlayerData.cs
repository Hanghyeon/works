using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


/// <summary>
///  필요한 정보 : 난이도 별 최고 점수
/// </summary>
public class PlayerData : Singleton<PlayerData>
{

    public List<int> highScore { get; private set; }


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Fake Data Delete");
        }
        else// first 
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("First");

            Load();
        }
    }

    public void Checkdata()
    {

    }
    public void Save()
    {
        var bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerData.dat");

        // Save Data
        bf.Serialize(file, highScore);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerData.dat", FileMode.Open);

            //Load
            highScore = (List<int>)bf.Deserialize(file);

            file.Close();
        }
        else
        {
            highScore = new List<int>();

            for (int i = 0; i < 3; i++)
                highScore.Add(0);
        }
    }

}
