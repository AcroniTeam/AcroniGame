using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Experimental.UIElements;
using static UnityEngine.UI.Slider;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    string backgroundMusic_Name;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
            
        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.name = sound.name;
            sound.source.spatialBlend = sound.dimension;

            if(sound.name.StartsWith("sfx"))
                sound.source.volume = 0.5f;
        }
    }

    private void Start()
    {
        //backgroundMusic_Name = "bgm-aero_plano";
        //Play(backgroundMusic_Name);
    }

    public static AudioManager GetInstance()
    {
        return instance;
    }

    static string currentBgmName;
    public void Play(string audio_name)
    {
        Sound sound = Array.Find(sounds, s => s.name == audio_name);
        if (sound == null)
            return;

        if (audio_name == currentBgmName)
            return;

        if (audio_name.StartsWith("bgm"))
            StartCoroutine(FadeInOut(sound));
        else
            sound.source.Play();
    }

    IEnumerator FadeInOut(Sound transit_to)
    {
        Sound c_bgm = Array.Find(sounds, s => s.name == currentBgmName);
        if (c_bgm != null)
        {
            for (float i = SliderController.bgm_volume; i >= 0; i-=0.1f)
            {
                if(i < 0)
                {
                    c_bgm.source.volume = 0;
                    break;
                }

                c_bgm.source.volume = i;
                //Debug.Log("diminuindo o volume de " + c_bgm.name + " para " + c_bgm.source.volume);
                yield return null;
            }
            c_bgm.source.Stop();
        }

        transit_to.source.volume = 0;
        transit_to.source.Play();
        for (float i = 0; i <= SliderController.bgm_volume; i+=0.1f)
        {
            if (i > SliderController.bgm_volume)
            {
                transit_to.source.volume = SliderController.bgm_volume;
                break;
            }

            transit_to.source.volume = i;
            //Debug.Log("aumentando o volume de " + transit_to.name + " para "+ transit_to.source.volume);
            yield return null;
        }

        currentBgmName = transit_to.name;
    }

    public void Stop(string audio_name)
    {
        Sound sound = Array.Find(sounds, s => s.name == audio_name);
        if (sound == null)
            return;
        sound.source.Stop();
    }

    bool isMuted = false;
    public void Mute() {
        isMuted = (!isMuted)?true:false;
        foreach (Sound s in sounds)
            s.source.mute = isMuted;
    }

    public void SetBackgroundMusic(float n_volume)
    {        
        Array.Find(sounds, sound => sound.name.Equals(backgroundMusic_Name)).source.volume = n_volume;
    }

    public void SetSFXMusic(float n_volume)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name.StartsWith("sfx"))
                sound.source.volume = n_volume;
        }
    }

    public void DesacelerateSfx()
    {
        foreach(Sound sound in sounds)
        {
            if (sound.name.StartsWith("sfx"))
                sound.source.pitch = 0.5f;
        }
    }

    public void AcelerateSfx()
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name.StartsWith("sfx"))
                sound.source.pitch = 1f;
        }
    }

    public string GetCurrentBGM()
    {
        return currentBgmName;
    }

    public void SetVolumeBGM(float f)
    {
        foreach(Sound s in sounds)
        {
            if (s.name.StartsWith("bgm"))
                s.source.volume = f;
        }
    }

    public void SetVolumeSFX(float f)
    {
        foreach (Sound s in sounds)
        {
            if (s.name.StartsWith("sfx"))
                s.source.volume = f;
        }
    }
}
