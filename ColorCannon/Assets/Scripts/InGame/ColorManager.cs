using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorManager : Singleton<ColorManager>
{

    public Color GetColor(ColorType color)
    {
        switch (color)
        {
            case ColorType.Red:
                return Color.red;
            case ColorType.Orange:
                return new Color(1, 0.55f, 0, 1);
            case ColorType.Yellow:
                return Color.yellow;
            case ColorType.Green:
                return Color.green;
            case ColorType.Blue:
                return Color.blue;
            case ColorType.Purple:
                return new Color(0.54f, 0.03f, 1, 1);
            case ColorType.Black:
                return Color.black;
            default:
                return Color.black;
        }
    }

    public bool IsMixedColor(ColorType color)
    {
        if(color == ColorType.Orange || color == ColorType.Purple
            || color == ColorType.Green || color == ColorType.Black)
            return true;

        return false;
    }

    public ColorType CheckMixColor(ColorType mixedColor, ColorType disassemblyColor)
    {

        if (IsMixedColor(mixedColor))
        {
            return (ColorType)(mixedColor - disassemblyColor);
        }
        
        return mixedColor;
    }

    public bool IsColorCollision(ColorType bulletColor, ColorType enemyColor)
    {
        // 색상이 같을때
        if(bulletColor == enemyColor)
            return true;

        // 색이 혼합색상일때
        if (IsMixedColor(enemyColor))
        {
            int temp = (int)enemyColor & (int)bulletColor;
            Debug.Log(temp);
            if (temp > 0)
                return true;
        }
        return false;
    }

}
