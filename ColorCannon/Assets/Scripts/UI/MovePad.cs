using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MovePad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Slider slider { get; private set; }

    private float defalutValue;

    private bool pointerDown;

    void    Awake()
    {
        slider = GetComponent<Slider>();

        defalutValue = 0.5f;
    }

    void OnEnable()
    {
        StartCoroutine(GamePadProcess());
    }

    IEnumerator GamePadProcess()
    {
        while(true)
        {
            if (pointerDown)
            {
                if (slider.value < defalutValue)
                    slider.value = 0;
                else if (slider.value > defalutValue)
                    slider.value = 1;

            }
            else
                slider.value = defalutValue;
            yield return null;
        }

    }

    public void OnPointerDown(PointerEventData data)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        pointerDown = false;
    }


}
