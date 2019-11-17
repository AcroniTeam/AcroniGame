using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        float range = Random.Range(0f,1f);

        if(anim == null)
            GetComponent<Animator>().SetFloat("offset", range);
        else
            anim.SetFloat("offset", range);
    }
}
