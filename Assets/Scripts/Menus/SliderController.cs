using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider bgm_music;
    public Slider sfx_music;

    public static float bgm_volume = 1;
    public static float sfx_volume = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        AudioManager.GetInstance().SetVolumeBGM(bgm_volume);
        AudioManager.GetInstance().SetVolumeSFX(sfx_volume);
        bgm_music.value = bgm_volume;
        sfx_music.value = sfx_volume;
    }

    public static SliderController instance;

    public void Readjust()
    {
        AudioManager.GetInstance().SetVolumeBGM(bgm_volume);
        AudioManager.GetInstance().SetVolumeSFX(sfx_volume);
        bgm_music.value = bgm_volume;
        sfx_music.value = sfx_volume;
    }

    public void Adjust_BGM(float new_bgm_volume)
    {
        bgm_volume = new_bgm_volume;
        AudioManager.GetInstance().SetVolumeBGM(new_bgm_volume);
    }

    public void Adjust_SFX(float new_sfx_volume)
    {
        sfx_volume = new_sfx_volume;
        AudioManager.GetInstance().SetVolumeSFX(new_sfx_volume);
    }

    public void GoToMainMenu()
    {
        GameManager.GetInstance().BuildScene(1);
    }
}