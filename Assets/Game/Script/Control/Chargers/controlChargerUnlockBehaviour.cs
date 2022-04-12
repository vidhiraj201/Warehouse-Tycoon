using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace warehouse.Control
{
    public class controlChargerUnlockBehaviour : MonoBehaviour
    {
        public GameObject Locked;
        public GameObject unLocked;
        public Collider Collider;

        [Header("Unlocking Kit")]
        [Space(10)]
        public TextMeshProUGUI Money;        
        public float maxMoneyNeedToUnlock;
        public float amountReducer = 50;
        public int UnlockLevel = 6;
        public bool unlockOverLevel;
        public bool isLocked;
        private bool isReducing;
        private bool isPlayerNear;

        private Core.GameManager GameManager;
        
        void Start()
        {
            GameManager = FindObjectOfType<Core.GameManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isLocked && !isPlayerNear && !isReducing)
                maxMoneyNeedToUnlock = GameManager.ChargingStation;
            Money.text = "$" + maxMoneyNeedToUnlock.ToString("N0");
            if(isLocked&&maxMoneyNeedToUnlock<=0)
            {
                Locked.SetActive(false);
                unLocked.SetActive(true);
                Collider.enabled = false;
                GameManager.ChargingStation += 600;

                if (unlockOverLevel)
                    unlockOverLevel = false;

                isLocked = false;
            }
            if (isLocked && isPlayerNear)
                moneyChecker();
            if(GameManager.currentLevel>=UnlockLevel && unlockOverLevel)
            {
                Locked.SetActive(false);
                unLocked.SetActive(true);
                Collider.enabled = false;
                GameManager.ChargingStation += 600;
                unlockOverLevel = false;
                isLocked = false;
            }

        }
        public void moneyChecker()
        {
            if (maxMoneyNeedToUnlock > 0 && GameManager.maxMoney > 0)
            {
                isReducing = true;
                maxMoneyNeedToUnlock -= amountReducer;
                GameManager.maxMoney -= amountReducer;
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<Move.movePlayer>().direction.magnitude < 0.1f)
                isPlayerNear = true;
        }
        private void OnTriggerExit(Collider other)
        {
            if (isPlayerNear)
                isPlayerNear = false;
        }
    }
}

