using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace warehouse.Core
{
    public class coreRobotSpwanner : MonoBehaviour
    {
        public Transform DustbinPosition;
        public GameObject HireUI;
        public GameObject Robot;
        
        public Transform Inventory;
        public TextMeshProUGUI Amount;
        public TextMeshProUGUI BotCount;

        public coreChargingPortManager chargingCollection;
        public int HireCount;
        public bool isLocked = true;

        private warehouse.Core.GameManager gameManager;
        // Start is called before the first frame update
        void Start()
        {
            HireUI.SetActive(false);
            gameManager = FindObjectOfType<warehouse.Core.GameManager>();
            if(HireCount!=0)
                hire();
        }

        // Update is called once per frame
        void Update()
        {
            Amount.text = gameManager.Bots.ToString("N0");
            BotCount.text = FindObjectOfType<Core.corePickupArea>().RobotCount.Count.ToString("N0");
        }

        public void RemoveUI()
        {
            HireUI.SetActive(false);
        }
        public void hire()
        {
            for(int i = 0; i < HireCount; i++)
            {
                GameObject R = Instantiate(Robot, Inventory.position, Quaternion.identity, Inventory);
                R.GetComponent<warehouse.Move.moveBot>().TargetToInitPosition = Inventory;
                R.GetComponent<warehouse.Move.moveBot>().chargerCollection = chargingCollection;
                R.GetComponent<warehouse.Move.moveBot>().TargetToDustbin = DustbinPosition;
            }
        }
        public void Hire()
        {
            if (gameManager.maxMoney >= gameManager.Bots)
            {
                GameObject R = Instantiate(Robot, Inventory.position, Quaternion.identity, Inventory);
                R.GetComponent<warehouse.Move.moveBot>().TargetToInitPosition = Inventory;
                R.GetComponent<warehouse.Move.moveBot>().chargerCollection = chargingCollection;
                R.GetComponent<warehouse.Move.moveBot>().TargetToDustbin = DustbinPosition;
                gameManager.maxMoney -= gameManager.Bots;
                HireCount++;
                gameManager.Bots += 300;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (HireCount <= 1)
                    HireUI.SetActive(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (HireUI.activeSelf)
                HireUI.SetActive(false);
        }
    }
}

