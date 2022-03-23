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

        [Space(40)]
        [Header("Truck UI")]
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
        public void UIUpdates()
        {
            if (currentLevel >= 1)
            {
                currentLevelText.text = currentLevel.ToString("N0");
                nextLevelText.text = (currentLevel + 1).ToString("N0");
            }
            if (currentLevel == 0)
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
        }

        int truckCountData;
        public int MaxCustomerCount()
        {
            if (currentLevel == 0)
                truckCountData = 2;
            if (currentLevel == 1)
                truckCountData = 4;
            if (currentLevel == 2)
                truckCountData = 6;
            if (currentLevel == 3)
                truckCountData = 8;
            if (currentLevel == 4)
                truckCountData = 7;
            if (currentLevel == 5)
                truckCountData = 3;
            if (currentLevel == 6)
                truckCountData = 8;
            if (currentLevel == 7)
                truckCountData = 9;
            if (currentLevel == 8)
                truckCountData = 10;
            if (currentLevel == 9)
                truckCountData = 11;
            if (currentLevel == 10)
                truckCountData = 12;
            if (currentLevel >= 11)
                truckCountData = Random.Range(7, 15);

            return truckCountData;
        }
        public void UpgradeLevel()
        {
            currentLevel += 1;
            currentTruckLoaded -= maxTruckToLoad;
            maxTruckToLoad = MaxCustomerCount();
            UpgradeButton.SetActive(false);
        }
    }

}
