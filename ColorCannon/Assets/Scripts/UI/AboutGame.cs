using UnityEngine;
using System.Collections;

public class AboutGame : MonoBehaviour, IBackScene
{

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SoundManager.Instance.PlayEffect(EffectSound.Button_Click);
            UIManager.Instance.Show(UIpage.MainMenu);
            UIManager.Instance.hide(UIpage.AboutGame);
        }
    }

    #region IBackScene 멤버

    public void OnBack()
    {
        SoundManager.Instance.PlayEffect(EffectSound.Button_Click);
        UIManager.Instance.Show(UIpage.MainMenu);
        UIManager.Instance.hide(UIpage.AboutGame);
    }

    #endregion
}
