using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace warehouse.Control
{
    public class controlLoadingDackUnlcokBehaviour : MonoBehaviour
    {
        public controlParkingDack controlParkingDack;
        public controlLoadingDack controlLoadingDack;
        public TextMeshProUGUI money;
        public float maxMoneyNeedToUnlock = 100;
        public float amountReducer = 50;
        private Core.GameManager GameManager;

        //public bool isRecord;

        void Start()
        {
            GameManager = FindObjectOfType<Core.GameManager>();
        }

        // Update is called once per frame
        void Update()
        {
            money.text = "$" + maxMoneyNeedToUnlock.ToString();
            if (maxMoneyNeedToUnlock <= 0)
            {
                controlParkingDack.isLocked = false;
                money.gameObject.SetActive(false);
            }

            if(!controlParkingDack.isLocked)
                money.gameObject.SetActive(false);

            if (controlParkingDack.isLocked && controlLoadingDack.isPlayerNear)
                moneyChecker();
        }

        public void moneyChecker()
        {            
            if(maxMoneyNeedToUnlock > 0 && GameManager.maxMoney> 0)
            {
                maxMoneyNeedToUnlock -= amountReducer;
                GameManager.maxMoney -= amountReducer;
            }
        }
    }
}

