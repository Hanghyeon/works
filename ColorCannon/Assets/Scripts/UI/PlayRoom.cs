using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayRoom : MonoBehaviour, IBackScene
{

    private GameObject levelText;   
    private GameObject pausePanel;

    private Image hpImg;
    private Text  scoreText;
    private Text  comboText;

    private bool isPause = false;
    void Awake()
    {
        hpImg       =   transform.FindChild("Hp").GetComponent<Image>();
        scoreText   =   transform.FindChild("Score").GetComponent<Text>();
        comboText   =   transform.FindChild("Combo").GetComponent<Text>();
        levelText   =   transform.FindChild("LevelText").gameObject;
        pausePanel  =   transform.FindChild("PausePanel").gameObject;
        
        
        transform.FindChild("Pause").GetComponent<Button>().onClick.AddListener(OnPauseDown);
    }


    /// <summary>
    ///  시작하기전 레벨 표시
    /// </summary>
    public void PreparePlay(int level)
    {
        if (level == GameLogic.Instance.maxLV)
            levelText.GetComponent<Text>().text = "Endless...";
        else
            levelText.GetComponent<Text>().text = "Level " + level.ToString();
        levelText.SetActive(true);
    }

    public void RefreshHpBar(float hp)
    {
        hpImg.fillAmount = hp / GameLogic.Instance.maxHP;
    }

    public void RefreshScore(int score)
    {
        scoreText.text  =   score.ToString();
    }

    public void RefreshCombo(int combo)
    {
        comboText.text  =   combo.ToString() + " 콤보";
        StartCoroutine(UpdownEffect());
    }


    void OnPauseDown()
    {
        SoundManager.Instance.PlayEffect(EffectSound.Button_Click);
        pausePanel.SetActive(CheckTimeScale());
    }

    bool CheckTimeScale()
    {
        if (Time.timeScale == 1)
        {
            SoundManager.Instance.ToggleBGM(false);
            Time.timeScale = 0;
            return true;
        }
        else
        {
            SoundManager.Instance.ToggleBGM(true);
            Time.timeScale = 1;
            return false;
        }
    }


    IEnumerator UpdownEffect()
    {
        comboText.transform.localPosition += new Vector3(0,20,0);
        yield return new WaitForSeconds(0.1f);
        comboText.transform.localPosition -= new Vector3(0,20, 0);
        yield break;
    }

    #region IBackScene 멤버

    public void OnBack()
    {
        SoundManager.Instance.PlayEffect(EffectSound.Button_Click);
        pausePanel.SetActive(CheckTimeScale());
    }

    #endregion
}
