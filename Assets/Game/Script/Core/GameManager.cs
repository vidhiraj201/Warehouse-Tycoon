using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace wearhouse.Core
{
    public class GameManager : MonoBehaviour
    {
        public float maxMoney;
        public float moneyCounterSpeed;
        public TextMeshProUGUI money;
        private float currentMoney;

        [Space(40)]
        public TextMeshProUGUI truckCount;
        public int NumberOfTruckTraded;

        void Start()
        {
            currentMoney = maxMoney;
        }

        
        void Update()
        {
            moneyCount();
            truckCount.text = NumberOfTruckTraded.ToString("N0");
        }

        public void moneyCount()
        {
            maxMoney = (int)Mathf.Clamp(maxMoney, 0, Mathf.Infinity);
            if (currentMoney <= maxMoney)
            {
                currentMoney += moneyCounterSpeed * Time.deltaTime;
                if (currentMoney >= maxMoney)
                    currentMoney = maxMoney;
            }
            if (currentMoney >= maxMoney)
            {
                currentMoney -= moneyCounterSpeed * Time.deltaTime;
                if (currentMoney <= maxMoney)
                    currentMoney = maxMoney;
            }
            money.text = "$" + currentMoney.ToString("N0");
        }
    }

}
