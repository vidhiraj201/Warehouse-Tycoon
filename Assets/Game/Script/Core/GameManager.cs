using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace warehouse.Core
{
    public class GameManager : MonoBehaviour
    {
        public float maxMoney;
        public float moneyCounterSpeed;
        public TextMeshProUGUI money;
        private float currentMoney;

        [Header("Truck UI")]
        [Space(20)]
        public float maxTruckToLoad;
        public float currentTruckLoaded;
        public float tempCurrentTruckLoaded;

        public int currentLevel;

        [Space(20)]
        public TextMeshProUGUI truckCountUpdate;
        public TextMeshProUGUI nextLevelText;
        public TextMeshProUGUI currentLevelText;

        [Space(20)]
        public Slider truckCount;
        public float DSpeed;
        public float Uspeed;

        [Header("Level Manager")]
        public GameObject UpgradeButton;

        [Header("Color")]
        [Space(10)]
        public Color cointInPositive;
        public Color cointInNegative;

        [Header("Reward")]
        [Space(15)]
        public GameObject LockedSection;
        public int RewardMoney;

        [Header("Costings")]
        [Space(10)]
        public int ParkingLot = 500;
        public int Bots = 1500;
        public int ChargindStation = 3000;
        public int RefillingCost = 500;

        void Start()
        {
            Application.targetFrameRate = 60;
            currentMoney = maxMoney;
            maxTruckToLoad = MaxCustomerCount();
        }

        
        void Update()
        {
            moneyCount();
            UIUpdates();
            TruckSlider();
            upgrade();
        }

        public void moneyCount()
        {
            /*maxMoney = (int)Mathf.Clamp(maxMoney, 0, Mathf.Infinity);*/
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

            if (currentMoney < 0)
                money.color = cointInNegative;

            if (currentMoney > 0)
                money.color = cointInPositive;
        }
        public void UIUpdates()
        {
           /* if (currentLevel > 0)
            {
                currentLevelText.text = currentLevel.ToString("N0");
                nextLevelText.text = (currentLevel + 1).ToString("N0");
            }*/
            if (currentLevel >= 0)
            {
                currentLevelText.text = (currentLevel+1).ToString("N0");
                nextLevelText.text = (currentLevel + 2).ToString("N0");
            }

            truckCountUpdate.text = currentTruckLoaded.ToString("N0") + "/" + maxTruckToLoad.ToString("N0");
            
            
            truckCount.maxValue = maxTruckToLoad;
            truckCount.value = tempCurrentTruckLoaded;
        }
        public void TruckSlider()
        {
            currentTruckLoaded = (int)Mathf.Clamp(currentTruckLoaded, 0, Mathf.Infinity);

            if (tempCurrentTruckLoaded < currentTruckLoaded)
            {
                tempCurrentTruckLoaded += Uspeed * Time.deltaTime;
                if (tempCurrentTruckLoaded >= currentTruckLoaded)
                    tempCurrentTruckLoaded = currentTruckLoaded;
            }
            if (tempCurrentTruckLoaded > currentTruckLoaded)
            {
                tempCurrentTruckLoaded -= DSpeed * Time.deltaTime;
                if (tempCurrentTruckLoaded <= currentTruckLoaded)
                    tempCurrentTruckLoaded = currentTruckLoaded;
            }
            if(tempCurrentTruckLoaded == currentTruckLoaded)
                tempCurrentTruckLoaded = currentTruckLoaded;
        }

        public void upgrade()
        {
            if(currentTruckLoaded >= maxTruckToLoad)
            {
                UpgradeButton.SetActive(truckCount);
            }
            if(currentLevel == 4 || currentLevel == 9)
            {
                LockedSection.SetActive(true);
            }
            else
            {
                LockedSection.SetActive(false);
            }
        }

        int truckCountData;
        public int MaxCustomerCount()
        {
            if (currentLevel == 0)
                truckCountData = 2;

            if (currentLevel == 1)
                truckCountData = 2;

            if (currentLevel == 2)
                truckCountData = 4;

            if (currentLevel == 3)
                truckCountData = 8;
            if (currentLevel == 4)
                truckCountData = 12;

            if (currentLevel == 5)
                truckCountData = 16;

            if (currentLevel == 6)
                truckCountData = 20;

            if (currentLevel == 7)
                truckCountData = 24;

            if (currentLevel == 8)
                truckCountData = 28;

            if (currentLevel == 9)
                truckCountData = 32;

            if (currentLevel == 10)
                truckCountData = 36;

            if (currentLevel == 11)
                truckCountData = 40;

            if (currentLevel == 12)
                truckCountData = 44;

            if (currentLevel == 13)
                truckCountData = 48;

            if (currentLevel == 14)
                truckCountData = 52;

            if (currentLevel == 15)
                truckCountData = 56;

            if (currentLevel >= 16)
                truckCountData = Random.Range(56, 80);
            return truckCountData;
        }
        public void UpgradeLevel()
        {
            addReward();
            currentLevel += 1;
            currentTruckLoaded -= maxTruckToLoad;
            maxTruckToLoad = MaxCustomerCount();
            UpgradeButton.SetActive(false);
        }

        public void addReward()
        {
            maxMoney += Reward();
        }

        public int Reward()
        {
            if (currentLevel == 0)
                RewardMoney = 100;

            if (currentLevel == 1)
                RewardMoney = 0;

            if (currentLevel == 2)
                RewardMoney = 250;

            if (currentLevel == 3)
                RewardMoney = 0;

            if (currentLevel == 4)
                RewardMoney = 0;

            if (currentLevel == 5)
                RewardMoney = 1000;

            if (currentLevel == 6)
                RewardMoney = 0;

            if (currentLevel == 7)
                RewardMoney = 1500;

            if (currentLevel == 8)
                RewardMoney = 2000;

            if (currentLevel == 9)
                RewardMoney = 0;

            if (currentLevel == 10)
                RewardMoney = 3000;

            if (currentLevel == 11)
                RewardMoney = 3500;

            if (currentLevel == 12)
                RewardMoney = 5000;

            if (currentLevel == 13)
                RewardMoney = 6000;

            if (currentLevel == 14)
                RewardMoney = 6500;

            if (currentLevel == 15)
                RewardMoney = 7000;

            return RewardMoney;
        }
    }

}
