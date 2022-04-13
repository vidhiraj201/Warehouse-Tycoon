using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace warehouse.Core
{
    public class coreUnlockCharger : MonoBehaviour
    {
        public string WhatIsUnlocking;
        public string Camera;
        public bool animate;
        [Space(15)]
        public int Level;
        public float delay = 1;
        public Animator CC;


        public GameObject UnlockableObject;
       

        private coreAudioManager audioManager;
        

        private Core.GameManager GameManager;

        void Start()
        {
            GameManager = FindObjectOfType<warehouse.Core.GameManager>();
            audioManager = FindObjectOfType<coreAudioManager>();
        }

        
        void Update()
        {

            if(WhatIsUnlocking == "Charging Set")
                unlockCharger();
        }


        void unlockCharger()
        {
            if (GameManager.currentLevel < Level)
            {
                UnlockableObject.SetActive(false);
            }
            else if (GameManager.currentLevel == Level && !UnlockableObject.activeSelf)
            {
                StartCoroutine(unlockCharager(delay));
            }
            else if (GameManager.currentLevel > Level && !UnlockableObject.activeSelf)
            {
                UnlockableObject.SetActive(true);
            }
        }
        IEnumerator unlockCharager(float t)
        {
            if(animate)
                CC.Play(Camera);

            yield return new WaitForSeconds(t);
            UnlockableObject.SetActive(true);
            audioManager.source.PlayOneShot(audioManager.Unlock);
        }
    }
}

