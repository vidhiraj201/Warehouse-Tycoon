using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace warehouse.Control
{
    public class controlCharger : MonoBehaviour
    {
        public Core.coreChargingPortManager PortManager;
        private Move.moveBot moveBot;
        private Core.GameManager GameManager;
        [Header("lock")]
        public GameObject Locked;
        public GameObject Unlocked;

        [Header("UIs")]
        public TextMeshProUGUI money;

        [Space(10)]
        public float maxMoneyNeedToUnlock = 100;
        public float amountReducer = 50;

        public bool isReducing;
        public bool isOccupied;
        public bool isRobotNear;
        public bool isPlayerNear;
        public bool isLocked=true;

        [Header("Charging")]
        [Space(10)]
        public float ChargingSpeed = 0.5f;
        void Start()
        {
            GameManager = FindObjectOfType<Core.GameManager>();
            
        }

        
        void Update()
        {
            if(!isLocked)
                AddorRemoveList();
            Charging();
            unlockSystem();
        }
        void Charging()
        {
            if (isRobotNear)
            {
                moveBot.currentCharge += ChargingSpeed;
            }
        }
        void AddorRemoveList()
        {
            if (!isOccupied && !PortManager.Charger.Contains(this.gameObject))
                PortManager.Charger.Add(this.gameObject);

            if (isOccupied && PortManager.Charger.Contains(this.gameObject))
                PortManager.Charger.Remove(this.gameObject);
        }
        void unlockSystem()
        {
            if (isLocked && !isPlayerNear && !isReducing)
                maxMoneyNeedToUnlock = GameManager.ChargingStation;
            money.text = "$" + maxMoneyNeedToUnlock.ToString();

            if (isLocked && maxMoneyNeedToUnlock <= 0)
            {
                isLocked = false;
                money.gameObject.SetActive(false);
                GameManager.ChargingStation += 1500;
            }

            if (!isLocked)
                money.gameObject.SetActive(false);

            if (isLocked && isPlayerNear)
                moneyChecker();

            if (isLocked)
            {
                Locked.SetActive(true);
                Unlocked.SetActive(false);
            }
            if (!isLocked)
            {
                Locked.SetActive(false);
                Unlocked.SetActive(true);
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
            if (other.gameObject.CompareTag("Robot") && other.GetComponent<Move.moveBot>().agent.velocity.magnitude < 0.1f)
            {
                isRobotNear = true;
                moveBot = other.GetComponent<Move.moveBot>();
            }

            if (other.gameObject.CompareTag("Player") && other.GetComponent<warehouse.Move.movePlayer>().direction.magnitude < 0.1f)
            {
                isPlayerNear = true;
            }
        }



        private void OnTriggerExit(Collider other)
        {
            if (isRobotNear)
            {
                isRobotNear = false;
                moveBot = null;
            }
            if (isPlayerNear)
                isPlayerNear = false;
        }
    }

}
