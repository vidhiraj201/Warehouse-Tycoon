using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float maxMoney;
    public float currentMoney;
    public float Speed;
    public float x;

    public bool isTrue;
    void Update()
    {
        if (isTrue)
        {
            if(maxMoney>= 0 && x > 0)
            {
                x -= Speed * Time.deltaTime;
                maxMoney -= Speed * Time.deltaTime;
            }            
        }
    }
}
