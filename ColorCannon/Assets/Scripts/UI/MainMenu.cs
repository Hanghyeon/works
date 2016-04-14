using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour, IBackScene 
{
    void Awake()
    {
        transform.FindChild("GameStart").GetComponent<Button>().onClick.AddListener(On_GoStageSelect);
        transform.FindChild("Exit").GetComponent<Button>().onClick.AddListener(On_Exit);
        transform.FindChild("AboutGame").GetComponent<Button>().onClick.AddListener(On_AboutGame);   
    }


    void On_GoStageSelect()
    {
        SoundManager.Instance.PlayEffect(EffectSound.Button_Click);
        UIManager.Instance.hide(UIpage.MainMenu);
        UIManager.Instance.Show(UIpage.StageSelect); 
    }

    void On_Exit()
    {   
        Application.Quit();
    }

    void On_AboutGame()
    {
        SoundManager.Instance.PlayEffect(EffectSound.Button_Click);
        UIManager.Instance.hide(UIpage.MainMenu);
        UIManager.Instance.Show(UIpage.AboutGame); 
    }

    #region IBackScene 멤버

    public void OnBack()
    {
        Application.Quit();
    }

    #endregion
}
