using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float speed = 0.03f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            Vector3 vector = new Vector3(0, speed, 0);
            transform.position += vector;
        }
        if (Input.GetKey("a"))
        {
            Vector3 vector = new Vector3(-1 * speed, 0, 0);
            transform.position += vector;
        }
        if (Input.GetKey("s"))
        {
            Vector3 vector = new Vector3(0, -1 * speed, 0);
            transform.position += vector;
        }
        if (Input.GetKey("d"))
        {
            Vector3 vector = new Vector3(speed, 0, 0);
            transform.position += vector;
        }
    }
}
