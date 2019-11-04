using UnityEngine;

public class SwitchController : MonoBehaviour
{
  
    public void Enable()
    {
        enabled = true;
    }

    public void Disable()
    {
        Debug.Log("bloqueado");
        enabled = false;
    }
}
