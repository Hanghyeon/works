using UnityEngine;
using UnityEngine.UI;
using System.Collections;


enum textColor 
{
    red,
    green,
    blue
}
public class RainbowColorText : MonoBehaviour 
{
    [SerializeField]
    private float switchTime = 1;
    private Text   targetText;

    void Awake()
    {
        targetText = GetComponent<Text>();

    }


    void OnEnable()
    {
        targetText.color = Color.red;

        StartCoroutine(ColorRoutine());
    }

    /// <summary>
    /// R : 255 ->
    /// G : 255 ->
    /// R : 0 ->
    /// B : 255 ->
    /// G : 0 ->
    /// R : 255 ->
    /// B:  0 
    /// </summary>
    /// <returns></returns>
    IEnumerator ColorRoutine()
    {
        while (true)
        {
            StartCoroutine(ChangeColor(textColor.green, 0, 1));
            yield return new WaitForSeconds(switchTime);

            StartCoroutine(ChangeColor(textColor.red, 1, 0));
            yield return new WaitForSeconds(switchTime);

            StartCoroutine(ChangeColor(textColor.blue, 0, 1));
            yield return new WaitForSeconds(switchTime);

            StartCoroutine(ChangeColor(textColor.green, 1, 0));
            yield return new WaitForSeconds(switchTime);

            StartCoroutine(ChangeColor(textColor.red, 0, 1));
            yield return new WaitForSeconds(switchTime);

            StartCoroutine(ChangeColor(textColor.blue, 1, 0));
            yield return new WaitForSeconds(switchTime);


            yield return null;
        }

    }

    IEnumerator ChangeColor(textColor colorType,float start, float end)
    {
        float currTime = 0;

        while (currTime < switchTime)
        {
            currTime += Time.deltaTime;
            AddColor(colorType,  EasingUtil.linear(start, end, currTime / switchTime));
            yield return null;
        }

        
    }

    void AddColor(textColor colorType, float add)
    {
        switch (colorType)
        {
            case textColor.red:
                targetText.color = new Color(add, targetText.color.g, targetText.color.b, targetText.color.a);
                break;
            case textColor.green:
                targetText.color = new Color(targetText.color.r, add, targetText.color.b, targetText.color.a);
                break;
            case textColor.blue:
                targetText.color = new Color(targetText.color.r, targetText.color.g, add, targetText.color.a);
                break;
            default:
                break;
        }
    }
}
