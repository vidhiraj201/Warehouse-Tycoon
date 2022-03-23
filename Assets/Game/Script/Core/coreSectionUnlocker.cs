using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace warehouse.Core
{
    public class coreSectionUnlocker : MonoBehaviour
    {
        public Animator cam;

        public GameObject Ground, PickupArea, ParkingDeck, wallSet;
        public TextMeshProUGUI LevelUnlockText;
        public string LevelUnlockPrintText;
        public int UnlockLevelVal;
        public float DelayInUnlock;
        public bool isUnlocked;
        private GameManager GameManager;
        void Start()
        {
            GameManager = FindObjectOfType<GameManager>();
        }

        void Update()
        {
            LevelUnlockText.text = LevelUnlockPrintText;
            if (GameManager.currentLevel >= UnlockLevelVal && !isUnlocked)
            {
                StartCoroutine(UnlockLevel(DelayInUnlock));
            }
            if (isUnlocked)
            {
                Ground.SetActive(true);
                PickupArea.SetActive(true);
                ParkingDeck.SetActive(true);
                wallSet.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
        IEnumerator UnlockLevel(float t)
        {
            cam.Play("T1");
            yield return new WaitForSeconds(t);
            Ground.SetActive(true);
            PickupArea.SetActive(true);
            ParkingDeck.SetActive(true);
            wallSet.SetActive(true);
            this.gameObject.SetActive(false);
        }        
    }
}

