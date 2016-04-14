using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EffectSound
{
    Fire,
    Button_Click,
    HighScore
}

public class SoundManager : Singleton<SoundManager>
{
    AudioSource bgmController;
    AudioSource effectController;

    AudioClip   titleBGM;

    Dictionary<EffectSound, AudioClip> effectClips;
    Dictionary<Difficulty, AudioClip> bgmClips;


    void Awake()
    {
        bgmController       = gameObject.AddComponent<AudioSource>();
        effectController    = gameObject.AddComponent<AudioSource>();

        GetComponent<AudioSource>().loop = true;
    }

    public void LoadSound()
    {
        // effect
        effectClips = new Dictionary<EffectSound,AudioClip>();
        effectClips.Add(EffectSound.Fire, Resources.Load<AudioClip>("Sounds/Fire"));
        effectClips.Add(EffectSound.Button_Click, Resources.Load<AudioClip>("Sounds/Button_Click"));
        // bgms
        bgmClips = new Dictionary<Difficulty,AudioClip>();
        bgmClips.Add(Difficulty.Easy,Resources.Load<AudioClip>("Sounds/BGM_easy"));
        bgmClips.Add(Difficulty.Normal, Resources.Load<AudioClip>("Sounds/BGM_normal"));
        bgmClips.Add(Difficulty.Hard, Resources.Load<AudioClip>("Sounds/BGM_hard"));
        //title
        titleBGM = Resources.Load<AudioClip>("Sounds/BGM_MainMenu");

    }

    #region BGM

    public void UpdateTitleBGM()
    {
        if (titleBGM != null)
            bgmController.clip = titleBGM;
    }

    public void PlayTitleBGM()
    {
        if(titleBGM != null)
            bgmController.Play();
    }

    public void UpdateBGMClip(Difficulty stage)
    {
        if (bgmClips[stage] != null)
        {
            bgmController.clip = bgmClips[stage];
        }
    }

    public void PlayBGM(Difficulty stage)
    {
        if(!bgmClips.ContainsKey(stage))
            return;

        if(bgmClips[stage] != null)
            bgmController.Play();
    }

    public void ToggleBGM(bool flag)
    {
        if(flag)
            bgmController.Play();
        else
            bgmController.Pause();
    }

    public void StopBGM()
    {
        bgmController.Stop();
    }

    #endregion

    public void PlayEffect(EffectSound effect)
    {
        if (effectClips[effect] != null)
        {
            effectController.PlayOneShot(effectClips[effect]);
        }
    }

    public void PlayEffectSound(EffectSound soundType)
    {
        if (effectClips[soundType] != null)
            effectController.PlayOneShot(effectClips[soundType]);
    }


}
