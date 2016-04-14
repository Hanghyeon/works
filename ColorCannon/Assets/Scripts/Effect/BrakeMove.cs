using UnityEngine;
using System.Collections;

public class BrakeMove : MonoBehaviour
{

    [SerializeField]
    private Vector2 startPos;
    [SerializeField]
    private Vector2 middlePos;
    [SerializeField]
    private Vector2 endPos;

    [SerializeField]
    private float brakeTime;
    [SerializeField]
    private float rushTime;


    void OnEnable()
    {
        transform.localPosition = startPos;

        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        StartCoroutine(Brake());
        yield return new WaitForSeconds(brakeTime);
        StartCoroutine(Rush());
        yield return new WaitForSeconds(rushTime);
        gameObject.SetActive(false);
    }

    IEnumerator Brake()
    {
        float currTime = 0;
        while (currTime < brakeTime)
        {
            currTime += Time.deltaTime;
            transform.localPosition = EasingUtil.EasingVector2(EasingUtil.easeOutExpo, startPos, middlePos, currTime / brakeTime);
            yield return null;
        }
    }

    IEnumerator Rush()
    {
        float currTime = 0;
        while (currTime < rushTime)
        {
            currTime += Time.deltaTime;
            transform.localPosition = EasingUtil.EasingVector2(EasingUtil.easeInExpo, middlePos, endPos, currTime / rushTime);
            yield return null;
        }
    }
}
