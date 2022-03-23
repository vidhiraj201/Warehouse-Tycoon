using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float speed;
    float x;

    public bool l;
    void Update()
    {
        x += speed;
        if (!l)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, x);
        }
        if (l)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, x, transform.eulerAngles.z);
        }
       
    }
}
