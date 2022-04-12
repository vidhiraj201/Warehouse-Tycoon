using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace warehouse.Control
{
    public class controlPickupUnlockBehaviour : MonoBehaviour
    {
        public controlPickup controlPickup;
        public TextMeshProUGUI money;
        public float maxMoneyNeedToUnlock = 100;
        public float amountReducer = 50;
        private Core.GameManager gameManager;
        private coreAudioManager audioManager;
        //public bool isRecord;
        void Start()
        {
            gameManager = FindObjectOfType<Core.GameManager>();
            audioManager = FindObjectOfType<coreAudioManager>();
        }


        void Update()
        {
            money.text = "$" + maxMoneyNeedToUnlock.ToString();
            if (maxMoneyNeedToUnlock <= 0 && controlPickup.isLocked)
            {
                controlPickup.isLocked = false;
                money.gameObject.SetActive(false);
                audioManager.source.PlayOneShot(audioManager.Unlock);
            }

            if (!controlPickup.isLocked)
            {
                maxMoneyNeedToUnlock = 0;
                money.gameObject.SetActive(false);
            }

            if (controlPickup.isLocked && controlPickup.isPlayerNear)
                moneyChecker();
        }
        void moneyChecker()
        {
            if (maxMoneyNeedToUnlock > 0 && gameManager.maxMoney > 0)
            {
                maxMoneyNeedToUnlock -= amountReducer;
                gameManager.maxMoney -= amountReducer;
            }
        }
    }
}

