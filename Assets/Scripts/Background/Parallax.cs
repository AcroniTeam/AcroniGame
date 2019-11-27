using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos, startY;
    public GameObject cam;
    
    public float parallaxEffect;
    public bool aero = false;

    //[Range(0, 1)]
    //public float velocity;
    //private float smoothing = 0.01f;

    void Start()
    {
        startpos = transform.position.x;
        startY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        if(aero)
            transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        else
            transform.position = new Vector3(startpos + dist, startY, transform.position.z);

        //transform.position = new Vector3(transform.position.x + velocity * -smoothing, transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}