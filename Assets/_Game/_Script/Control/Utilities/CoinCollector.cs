using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public List<GameObject> coins = new List<GameObject>();
    private bool ring;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform T in transform)
        {
            coins.Add(T.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (coins.Count > 0)
        {
            for (int i = 0; i <= coins.Count - 1;)
            {
                if (coins[i] == null)
                {
                    coins.Remove(coins[i]);
                }
                i++;
            }
        }
        
        if (coins.Count <= 0)
        {
            Destroy(gameObject);
        }
    }
}
