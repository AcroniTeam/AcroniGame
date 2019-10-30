using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountNumberDeaths : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FirebaseMethods.firebaseMethods.InitializeFirebase();
        FirebaseMethods.firebaseMethods.IncrementQttPlayed("Fase Aérea");
    }


}
