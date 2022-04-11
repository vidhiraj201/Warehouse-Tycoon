using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace warehouse.Core
{
    public class coreUnlockRobo : MonoBehaviour
    {
        public string WhatIsUnlocking;
        [Space(15)]
        public int Level;
        public GameObject UnlockableObject;
        public GameObject LockableObject;
        
        public warehouse.Control.controlPickup controlPickup;
        public warehouse.Control.controlCharger controlCharger;
        public warehouse.Core.coreRobotSpwanner RobotSpwanner;

        

        private Core.GameManager GameManager;

        void Start()
        {
            GameManager = FindObjectOfType<warehouse.Core.GameManager>();

        }

        
        void Update()
        {
            if(controlPickup && controlPickup.isLocked)
                unlockBattery();

            if (RobotSpwanner && RobotSpwanner.isLocked)
                unlockRoboSpwanner();

            if(WhatIsUnlocking == "Charging Set")
                unlockCharger();
        }

        void unlockBattery()
        {
            if (GameManager.currentLevel >= Level && !UnlockableObject.activeSelf)
            {
                UnlockableObject.SetActive(true);
                LockableObject.SetActive(false);
                controlPickup.isLocked = false;
            }
            if (GameManager.currentLevel >= Level && UnlockableObject.activeSelf)
                controlPickup.isLocked = false;
        }

        void unlockCharger()
        {
            if (GameManager.currentLevel < Level)
            {
                UnlockableObject.SetActive(false);
            }
            else if(GameManager.currentLevel >= Level && !UnlockableObject.activeSelf)
            {
                UnlockableObject.SetActive(true);
            }        
        }
        void unlockRoboSpwanner()
        {
            if (GameManager.currentLevel >= Level && !UnlockableObject.activeSelf)
            {
                UnlockableObject.SetActive(true);
                LockableObject.SetActive(false);
                RobotSpwanner.isLocked = false;
            }
            if (GameManager.currentLevel >= Level && UnlockableObject.activeSelf)
                RobotSpwanner.isLocked = false;
        }
    }
}

