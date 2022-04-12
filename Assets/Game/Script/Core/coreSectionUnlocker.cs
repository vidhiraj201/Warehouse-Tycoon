using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace warehouse.Core
{
    public class coreSectionUnlocker : MonoBehaviour
    {
        public Animator cam;

        public GameObject Ground/*, PickupArea, ParkingDeck, wallSet*/;
        public TextMeshProUGUI LevelUnlockText;
        public string LevelUnlockPrintText;
        public string Camera;
        public int UnlockLevelVal;
        public float DelayInUnlock;
        public bool isUnlocked;
        private GameManager GameManager;
        private coreAudioManager audioManager;
        void Start()
        {
            audioManager = FindObjectOfType<coreAudioManager>();
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
                this.gameObject.SetActive(false);
               /* PickupArea.SetActive(true);
                ParkingDeck.SetActive(true);
                wallSet.SetActive(true);*/
            }
        }
        IEnumerator UnlockLevel(float t)
        {
            cam.Play(Camera);
            yield return new WaitForSeconds(t);
            audioManager.source.PlayOneShot(audioManager.Upgrade);
            Ground.SetActive(true);
            this.gameObject.SetActive(false);
            FindObjectOfType<GAManager>().RoomUnlocked(this.transform);
            isUnlocked = true;
            /*PickupArea.SetActive(true);
            ParkingDeck.SetActive(true);
            wallSet.SetActive(true);*/
        }        
    }
}

