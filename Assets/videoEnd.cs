using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class videoEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(endVideo()); 
    }
    
    IEnumerator endVideo()
    {
        //Debug.Log(GameManager.GetInstance().getCurrentSceneIndex());
        yield return new WaitForSeconds(164F);
        //SceneManager.LoadScene("bomb-loading_scene");
        GameManager.GetInstance().BuildNextScene();

    }
}
