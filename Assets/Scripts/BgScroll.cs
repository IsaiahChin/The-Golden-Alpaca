using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class BgScroll : MonoBehaviour
{
    //code for infinite scrolling background

    public float speed = 5f;
    public float clamp;
    public Vector3 startPos;  
  
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
      
    }

    // Update is called once per frame
    void Update() {

        //Move image for scrolling background effect
        float newPosition = Mathf.Repeat(Time.time * speed, clamp);
        transform.position = startPos + Vector3.left * newPosition;
    }
}
