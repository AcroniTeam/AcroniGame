using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static int indextoBuild = 1;

    private void Start()
    {
        StartCoroutine(LoadAsync());
    }

    public void StartAudio()
    {
        AudioManager.GetInstance().Play("bgm-loading_screen");
    }

    public void CallAsync()
    {
        asy.allowSceneActivation = true;
    }

    AsyncOperation asy;
     public IEnumerator LoadAsync() {

        asy = SceneManager.LoadSceneAsync(indextoBuild);
        asy.allowSceneActivation = false;

        yield return null;
    }
}