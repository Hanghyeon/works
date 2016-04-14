using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour, IChangeColor
{
    [SerializeField]
    private float speed;

    private SpriteRenderer spr;
    private Cannon cannon;
    public ColorType myColor { get; private set;}

    void Awake()
    {
        cannon = GameObject.Find("Cannon").GetComponent<Cannon>();
        spr     =   GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        myColor= GameLogic.Instance.giveColorList[cannon.ColorIdx];
        ChangeColor(myColor);
        StartCoroutine(BulletProcess());
    }

    IEnumerator BulletProcess()
    {
        while (!IsOutScreen())
        {
            transform.Translate(0, speed * Time.smoothDeltaTime * Time.timeScale, 0);
            yield return null;
        }
        cannon.DestroyBullet(gameObject);

        yield break;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            cannon.DestroyBullet(gameObject);
        }
    }


    Vector3 view;
    bool IsOutScreen()
    {
        view = Camera.main.WorldToScreenPoint(transform.position);
        if (view.x < 0 - 50 || view.x > Screen.width + 50 ||
            view.y < 0 - 50 || view.y > Screen.height + 50)
            return true;

        return false;
    }

    #region IChangeColor 멤버

    public void ChangeColor(ColorType color)
    {
        spr.color = ColorManager.Instance.GetColor(color);
    }

    #endregion
}
