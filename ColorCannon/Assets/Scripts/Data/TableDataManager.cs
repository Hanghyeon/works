using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class TableDataManager : Singleton<TableDataManager>
{
    public List<TableStage> stages { get; private set; }

    void Awake()
    {
        stages = new List<TableStage>();
    }

    public void LoadStage()
    {
        TextAsset data = Resources.Load<TextAsset>("TextFile/ColorCannon_level");

        JsonData tables = JsonMapper.ToObject(data.text);

        for (int i = 0; i < tables.Count; i++)
        {
            stages.Add(new TableStage(tables[i]));
        }

        Debug.Log("Loaded Stages");
        
    }
}
