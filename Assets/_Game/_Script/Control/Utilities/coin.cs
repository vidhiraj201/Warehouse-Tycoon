using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    public float Speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    float z;
    void Update()
    {
        z += Speed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, z, 0);
    }
}
