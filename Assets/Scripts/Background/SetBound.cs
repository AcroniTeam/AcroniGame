using UnityEngine;

public class SetBound : MonoBehaviour
{

    bool isSeted = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isSeted)
            return;

        isSeted = true;
        try
        {
            FindObjectOfType<CameraBoundsManager>().NextBound();
        }
        catch { }
    }
}
