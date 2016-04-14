using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class Cannon : MonoBehaviour, IChangeColor
{
    [SerializeField]
    private int speed;
    [SerializeField]
    private float fireDelay = 0.3f;
    [SerializeField]
    private short bulletCount = 5;

    [SerializeField]
    private Button FireButton;
    [SerializeField]
    private Button ChangeButton;

    private Text changeText;

    private Animator ani;

    private GameObjectPool<GameObject> bulletPool;
    private GameObject bulletPrefab;

    private Slider movePad;

    private SpriteRenderer[] sprs;

    private int colorIdx = 0;
    public int ColorIdx
    {
        get
        {
            return colorIdx;
        }
        set
        {
            if(value >= GameLogic.Instance.giveColorList.Count)
                colorIdx = 0;
            else if(value < 0)
                colorIdx = GameLogic.Instance.giveColorList.Count - 1;
            else
                colorIdx = value;
        }

    }

    private float attackEleapsedTime = 0.0f;
    private bool canAttack = false;

    void Start()
    {
        bulletPrefab    = Resources.Load<GameObject>("Prefabs/Bullet");
        

        ani             = GetComponent<Animator>();
        sprs            = transform.FindChild("Graphic").GetComponentsInChildren<SpriteRenderer>();


        bulletPool = new GameObjectPool<GameObject>(bulletCount, bulletPrefab, (GameObject obj) =>
        {
            GameObject clone = GameObject.Instantiate(bulletPrefab);
            clone.SetActive(false);
            return clone;
        });

        FireButton.onClick.AddListener(OnFireButton);
        ChangeButton.onClick.AddListener(OnChangeButton);


        ChangeColor(GameLogic.Instance.giveColorList[ColorIdx]);
        StartCoroutine(CannonProcess());
    }

    public void Reset()
    {
        movePad     =   GameObject.Find("Move").GetComponent<Slider>();
        changeText  =   ChangeButton.GetComponentInChildren<Text>();

        
        colorIdx = 0;
        ChangeColor(ColorType.Red);
        ChangeTextColor();
    }
    IEnumerator CannonProcess()
    {
        while (true)
        {
            if (Time.timeScale != 0)
            {
                WaitForAttack();
                InputProcess();
            }
            yield return null;
        }
    }


    void InputProcess()
    {
        #region Android

        if (movePad != null)
        {
            if (movePad.value < 0.5f)
                transform.Rotate(Vector3.forward * 1 * speed * Time.deltaTime * Time.timeScale);
            if (movePad.value > 0.5f)
                transform.Rotate(Vector3.forward * -1 * speed * Time.deltaTime * Time.timeScale);
        }

        #endregion

        #region Cmputer

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.forward * 1 * speed * Time.deltaTime * Time.timeScale);

        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.forward * -1 * speed * Time.deltaTime * Time.timeScale);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            ColorIdx++;
            ChangeColor(GameLogic.Instance.giveColorList[ColorIdx]);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ColorIdx++;
            ChangeColor(GameLogic.Instance.giveColorList[ColorIdx]);
        }

        if (Input.GetButton("Jump") && canAttack)
            Fire();

        #endregion
    }


    void Fire()
    {
        canAttack = false;

        SoundManager.Instance.PlayEffect(EffectSound.Fire);

        GameObject bullet = bulletPool.pop();

        bullet.transform.SetParent(transform);
        bullet.transform.localPosition = new Vector3(0, 1, 0);
        bullet.transform.rotation = Quaternion.Euler(0, 0, (transform.localRotation.eulerAngles.z));
        bullet.transform.SetParent(null);

        bullet.SetActive(true);
        ani.Play("Fire");
    }


    void WaitForAttack()
    {
        if (canAttack)
            return;

        attackEleapsedTime += Time.deltaTime;

        if (attackEleapsedTime >= fireDelay)
        {
            canAttack = true;
            attackEleapsedTime = 0;
        }
    }



    public void DestroyBullet(GameObject obj)
    {
        obj.SetActive(false);
        bulletPool.push(obj);
    }


    public void OnFireButton()
    {
        if(canAttack)
            Fire();
    }

    public void OnChangeButton()
    {
        ColorIdx++;
        ChangeColor(GameLogic.Instance.giveColorList[ColorIdx]);
        ChangeTextColor();
    }

    #region IChangeColor 멤버

    public void ChangeColor(ColorType color)
    {
        for (int i = 0; i < sprs.Length; i++)
            sprs[i].color = ColorManager.Instance.GetColor(color);
    }

    #endregion

    public void ChangeTextColor()
    {
        changeText.color = ColorManager.Instance.GetColor(GameLogic.Instance.giveColorList[ColorIdx ]);
    }
}
