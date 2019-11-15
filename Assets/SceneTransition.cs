using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadAsync());
    }

    public void CallAsync()
    {
        asy.allowSceneActivation = true;
    }

    AsyncOperation asy;
     public IEnumerator LoadAsync() {

        asy = SceneManager.LoadSceneAsync("Menu_Principal");
        asy.allowSceneActivation = false;

        yield return null;
    }
}