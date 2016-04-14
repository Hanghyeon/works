using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class TableStage
{
    public int level { get; private set; }

    public int needScore { get; private set; }

    public List<ColorType> giveColor { get; private set; }

    public List<ColorType> addSpawnColor { get; private set; }

    public TableStage(JsonData table)
    {
        giveColor       =   new List<ColorType>();
        addSpawnColor   =   new List<ColorType>();


        level = int.Parse(table["Level"].ToString());
        needScore = int.Parse(table["NeedScore"].ToString());

        for (int i = 0; i < table["GiveColor"].Count; i++)
        {
            giveColor.Add((ColorType)Enum.Parse(typeof(ColorType),table["GiveColor"][i].ToString()));
        }

        for (int i = 0; i < table["AddSpawnColor"].Count; i++)
        {
            addSpawnColor.Add((ColorType)Enum.Parse(typeof(ColorType),table["AddSpawnColor"][i].ToString()));
        }
    }
}
