using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum UIpage
{
    MainMenu,
    StageSelect,
    PlayRoom,
    Result,
    AboutGame
}

public class UIManager : Singleton<UIManager>
{
    Dictionary<UIpage, GameObject> uiObjs;
    UIpage currScene;

    void Awake()
    {
        uiObjs = new Dictionary<UIpage,GameObject>();
        uiObjs.Add(UIpage.MainMenu,transform.FindChild("MainMenu").gameObject);
        uiObjs.Add(UIpage.StageSelect, transform.FindChild("StageSelect").gameObject);
        uiObjs.Add(UIpage.PlayRoom, transform.FindChild("PlayRoom").gameObject);
        uiObjs.Add(UIpage.Result, transform.FindChild("Result").gameObject);
        uiObjs.Add(UIpage.AboutGame, transform.FindChild("AboutGame").gameObject);

        SoundManager.Instance.LoadSound();

        SoundManager.Instance.UpdateTitleBGM();
        SoundManager.Instance.PlayTitleBGM();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GetUIpage(currScene).GetComponent<IBackScene>() != null)
            {
                GetUIpage(currScene).GetComponent<IBackScene>().OnBack();
            }
        }
    }

    public GameObject GetUIpage(UIpage page)
    {
        return uiObjs[page];
    }

    public void Show(UIpage page)
    {
        uiObjs[page].SetActive(true);
        currScene = page;
    }

    public void hide(UIpage page)
    {
        uiObjs[page].SetActive(false);
    }
}
