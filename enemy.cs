using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed;
    public float minimum, maximum;
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time*speed, maximum - minimum) + minimum, transform.position.y, transform.position.z);
    }

}
