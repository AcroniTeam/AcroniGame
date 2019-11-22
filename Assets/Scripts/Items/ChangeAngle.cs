using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAngle : MonoBehaviour
{
    private bool alreadyAddedForce = false;
    float timepassed = 0;
    bool canAddForce;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        
        if(Player.getInstance().GetPlayerMovement().getIsAddingForce())
        Player.getInstance().GetPlayerMovement().EnableAddForce();
    }
    void OnCollisionStay2D(Collision2D collision)
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        timepassed += Time.deltaTime;
        //Debug.Log(timepassed);
        //if (!alreadyAddedForce)
        //{
        if (timepassed > 0.03)
        {
            if (collision.otherCollider is CircleCollider2D)
                collision.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(5f, 20f), ForceMode2D.Impulse);
            else if (collision.otherCollider is BoxCollider2D)
                collision.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, 20f), ForceMode2D.Impulse);
            else if (collision.otherCollider is CapsuleCollider2D)
                collision.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-5f, 20f), ForceMode2D.Impulse);
            Player.getInstance().GetPlayerMovement().EnableAddForce();
        }
       
        //}
    }

}
