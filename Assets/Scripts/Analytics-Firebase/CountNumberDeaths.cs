using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountNumberDeaths : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FirebaseMethods.firebaseMethods.InitializeFirebase();
        FirebaseMethods.firebaseMethods.IncrementQttPlayed(SceneManager.GetActiveScene().name);

    }


}
