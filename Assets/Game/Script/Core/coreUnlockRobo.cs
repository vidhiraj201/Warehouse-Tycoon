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
        private coreAudioManager audioManager;
        

        private Core.GameManager GameManager;

        void Start()
        {
            GameManager = FindObjectOfType<warehouse.Core.GameManager>();
            audioManager = FindObjectOfType<coreAudioManager>();
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
            if (GameManager.currentLevel >= Level && !UnlockableObject.activeSelf && controlPickup.isLocked)
            {
                UnlockableObject.SetActive(true);
                LockableObject.SetActive(false);
                audioManager.source.PlayOneShot(audioManager.Unlock);
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
                audioManager.source.PlayOneShot(audioManager.Unlock);
            }        
        }
        void unlockRoboSpwanner()
        {
            if (GameManager.currentLevel >= Level && !UnlockableObject.activeSelf && RobotSpwanner.isLocked)
            {
                UnlockableObject.SetActive(true);
                LockableObject.SetActive(false);
                audioManager.source.PlayOneShot(audioManager.Unlock);
                RobotSpwanner.isLocked = false;
            }
            if (GameManager.currentLevel >= Level && UnlockableObject.activeSelf)
                RobotSpwanner.isLocked = false;
        }
    }
}

