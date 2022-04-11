using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace warehouse.Control
{
    public class controlLoadingDackUnlcokBehaviour : MonoBehaviour
    {
        [Header("Plateform Color")]
        public Material Locked;
        public Material Unlocked;
        public GameObject Plateform;

        public controlParkingDack controlParkingDack;
        public controlLoadingDack controlLoadingDack;
        public TextMeshProUGUI money;
        private float maxMoneyNeedToUnlock = 100;
        public float amountReducer = 50;
        private Core.GameManager GameManager;

        //public bool isRecord;

        void Start()
        {
            GameManager = FindObjectOfType<Core.GameManager>();
        }

        public bool isReducing;
        // Update is called once per frame
        void Update()
        {
            if (controlParkingDack.isLocked && !controlLoadingDack.isPlayerNear && !isReducing)
                maxMoneyNeedToUnlock = GameManager.ParkingLot;

            money.text = "$" + maxMoneyNeedToUnlock.ToString();
            if (controlParkingDack.isLocked && maxMoneyNeedToUnlock <= 0)
            {
                controlParkingDack.isLocked = false;
                money.gameObject.SetActive(false);
                GameManager.ParkingLot += 100;
            }

            if(!controlParkingDack.isLocked)
                money.gameObject.SetActive(false);

            if (controlParkingDack.isLocked && controlLoadingDack.isPlayerNear)
                moneyChecker();

            if (controlParkingDack.isLocked)
                Plateform.GetComponent<MeshRenderer>().material = Locked;

            if (!controlParkingDack.isLocked)
                Plateform.GetComponent<MeshRenderer>().material = Unlocked;
        }

        public void moneyChecker()
        {            
            if(maxMoneyNeedToUnlock > 0 && GameManager.maxMoney> 0)
            {
                isReducing = true;
                maxMoneyNeedToUnlock -= amountReducer;
                GameManager.maxMoney -= amountReducer;
            }
        }
    }
}

