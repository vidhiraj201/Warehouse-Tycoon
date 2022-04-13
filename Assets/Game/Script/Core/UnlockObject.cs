using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace warehouse.Control
{
    public class UnlockObject : MonoBehaviour
    {
        public int Level;
        public float delay = 1;
        public string Camera;
        public Animator CC;

        private Core.GameManager GameManager;

        public GameObject UnlockableObject;

        public controlPickup controlPickup;
        // Start is called before the first frame update
        void Start()
        {
            GameManager = FindObjectOfType<warehouse.Core.GameManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.currentLevel >= Level && !UnlockableObject.activeSelf && controlPickup.isLocked)
            {
                StartCoroutine(unlock(delay));
            }
            if (GameManager.currentLevel >= Level && UnlockableObject.activeSelf)
                controlPick();
        }
        void controlPick()
        {
            if (controlPickup != null)
                controlPickup.isLocked = false;
        }

        IEnumerator unlock(float t)
        {
            print("Data");
            CC.Play(Camera);
            yield return new WaitForSeconds(t);
            UnlockableObject.SetActive(true);
            controlPick();
        }
    }
}

