using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAngle : MonoBehaviour
{

    private Vector3 thisCenter;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        thisCenter = transform.GetComponent<Renderer>().bounds.center;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        //collision.otherCollider.enabled = true;
        //Debug.Log("liguei");
        if (Player.getInstance().GetPlayerMovement().getIsAddingForce())
        Player.getInstance().GetPlayerMovement().EnableAddForce();
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.otherCollider is PolygonCollider2D)

        //    collision.otherCollider.enabled = true;

    }
    float time = 0;
    void OnCollisionEnter2D(Collision2D collision)
    {
         time += Time.unscaledDeltaTime;
        //if(collision.otherCollider is PolygonCollider2D)
        //collision.otherCollider.enabled = false;
        thisCenter = transform.GetComponent<Renderer>().bounds.center;
        //Debug.Log(timepassed);
        //if (!alreadyAddedForce)
        //{
        Debug.Log(collision.rigidbody.velocity.x+" "+collision.rigidbody.velocity.y);
        Debug.Log(collision.GetContact(0).point.x - thisCenter.x + " "+collision.collider.GetType().ToString());
        if (collision.otherCollider is PolygonCollider2D)
        {
            if (Mathf.Abs(collision.rigidbody.velocity.y) > 8 && collision.gameObject.tag.Equals("Player"))
                collision.rigidbody.velocity = new Vector2(collision.rigidbody.velocity.x, collision.rigidbody.velocity.y - 8);
            else if (Mathf.Abs(collision.rigidbody.velocity.x) > 8 && collision.gameObject.tag.Equals("Player"))
                collision.rigidbody.velocity = new Vector2(collision.rigidbody.velocity.x-5, collision.rigidbody.velocity.y);
            if (collision.GetContact(0).point.x - thisCenter.x > 0.3)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(5f, 20f), ForceMode2D.Impulse);
                Debug.Log("Pro lado...direito?");
            }
            else if (collision.GetContact(0).point.x - thisCenter.x < -0.3)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-5f, 20f), ForceMode2D.Impulse);
                Debug.Log("Pro lado...esquerdo?");
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, 20f), ForceMode2D.Impulse);
                Debug.Log("Pro alto");
            }
            if (collision.gameObject.GetComponent<Rigidbody2D>().tag.Equals("Player"))
            {
                Debug.Log("Player");
                Player.getInstance().GetPlayerMovement().EnableAddForce();
            }
        }
    }
       
        //}
    }



