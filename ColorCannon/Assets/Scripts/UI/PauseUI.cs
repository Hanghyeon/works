using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseUI : MonoBehaviour

{
    void Awake()
    {
        transform.FindChild("Resume").GetComponent<Button>().onClick.AddListener(OnResumeButton);
        transform.FindChild("GoMain").GetComponent<Button>().onClick.AddListener(OnGoMainButton);
    }

    void OnResumeButton()
    {
        Time.timeScale = 1;
        SoundManager.Instance.PlayEffect(EffectSound.Button_Click);
        SoundManager.Instance.ToggleBGM(true);
        gameObject.SetActive(false);
    }

    void OnGoMainButton()
    {
        Time.timeScale = 1;
        SoundManager.Instance.PlayEffect(EffectSound.Button_Click);

        GameLogic.Instance.ForceStop();

        UIManager.Instance.Show(UIpage.MainMenu);
        UIManager.Instance.hide(UIpage.PlayRoom);

        SoundManager.Instance.UpdateTitleBGM();
        SoundManager.Instance.PlayTitleBGM();

        gameObject.SetActive(false);
    }

}
