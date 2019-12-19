using UnityEngine;

public class BirdScript : MonoBehaviour
{
    [Header("Bird References")]
    public Animator anim;
    [Header("Bird Configuration")]
    public bool notRandomizedBird;
    public BirdColor birdColor = BirdColor.BLACK;
   
    public bool FlyingBird = false;

    void Start()
    {
        float range = Random.Range(0f,1f);

        if(notRandomizedBird)
        {
            anim.SetFloat("offset", range);
            anim.SetBool("isGray", birdColor.Equals(BirdColor.BLACK)? false: true);
            return;
        }

        if (anim == null)
        {
            GetComponent<Animator>().SetFloat("offset", range);
            GetComponent<Animator>().SetBool("isGray", range > 0.5f ? true : false);
        }
        else
        {
            anim.SetFloat("offset", range);
            anim.SetBool("isGray", range > 0.5f? true : false);
        } 
    }

    bool isFlying = false;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(FlyingBird && !isFlying)
        {
            anim.SetBool("Fly", true);
            isFlying = true;
        }
    }

    public void DestroyBird()
    {
        Destroy(gameObject);
    }
}

public enum BirdColor
{
    BLACK, GRAY
}
