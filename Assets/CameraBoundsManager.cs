using UnityEngine;
using Cinemachine;

public class CameraBoundsManager : MonoBehaviour
{
    Cinemachine.CinemachineVirtualCamera cam;
    public PolygonCollider2D[] cameraBounds;

    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    int boundsIndex = 0;

    public void NextBound()
    {
        try
        {
            if (boundsIndex < cameraBounds.Length)
            {
                boundsIndex++;
                cam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = cameraBounds[boundsIndex];
            }
        }
        catch {}
    }
}
