using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSelectorButton : MonoBehaviour
{
   
    public void OnClick()
    {
        GameManager.GetInstance().LoadScene(System.Int32.Parse(GetComponent<TextMeshProUGUI>().text));
    }
}
