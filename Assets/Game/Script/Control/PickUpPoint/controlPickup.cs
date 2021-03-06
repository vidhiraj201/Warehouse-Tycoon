using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace warehouse.Control
{
    public class controlPickup : MonoBehaviour
    {
        public List<GameObject> Cart = new List<GameObject>();
        public List<GameObject> Objects = new List<GameObject>();
        public Transform inventory;
        public Vector3 StartPosition;
        public int MaxCapacity;
        public int currentCapacity;
        public int ID;
        public bool isPlayerNear;
        public bool isLocked;
        public bool isBought;
        [Header("Refealing UI")]
        [Space(20)]
        public GameObject UI;

        private Core.coreManager coreA1;
        private Core.GameManager gm;
        private Move.movePlayer movePlayer;

        float x = 0.2f;
        private void Start()
        {
            if(UI != null)
                UI.SetActive(false);
            currentCapacity = MaxCapacity;
            movePlayer = FindObjectOfType<Move.movePlayer>();
            coreA1 = FindObjectOfType<Core.coreManager>();
            gm = FindObjectOfType<Core.GameManager>();
        }
        void Update()
        {
            addRemoveFromList();
            if (Cart.Count <= MaxCapacity && currentCapacity > 0 && !isLocked && !isPlayerNear && !isBought)
            {
                if (x > 0)
                    x -= Time.deltaTime;
                if (x <= 0)
                {
                    SpwanObjects();
                    currentCapacity -= 1;
                    x = 0.05f;
                }
            }
            if(Cart.Count <= MaxCapacity && currentCapacity > 0 && !isLocked && isBought)
            {
                if (x > 0)
                    x -= Time.deltaTime;
                if (x <= 0)
                {
                    SpwanObjects();
                    currentCapacity -= 1;
                    x = 0.05f;
                }
            }

            foreach(Transform T in inventory)
            {
                if (!Cart.Contains(T.gameObject))
                    Cart.Add(T.gameObject);
            }
            UIUpdate();
        }

        void UIUpdate()
        {
            if(currentCapacity <=0 && Cart.Count <= 0 && isPlayerNear && UI !=null)
            {
                UI.SetActive(true);
            }
        }
        public void Refil()
        {
            gm.maxMoney -= gm.RefillingCost;
            gm.RefillingCost += 100;
            currentCapacity = MaxCapacity;
            isBought = true;
            UI.SetActive(false);
        }
        void addRemoveFromList()
        {
            if (!isLocked)
            {
                if (ID == 0) coreA1.iRed = true;
                if (ID == 1) coreA1.iYellow = true;
                if (ID == 2) coreA1.iGreen = true;
                if (ID == 3) coreA1.iBlue = true;
                if (ID == 4) coreA1.iOrange = true;
                if (ID == 5) coreA1.iViolet = true;

            }
        }

        void SpwanObjects()
        {
            int x = 0;
            if (Cart.Count == 0)
            {
                GameObject obj = Instantiate(Objects[x], inventory);
                obj.transform.localPosition = StartPosition;
                return;
            }
            if (Cart.Count > 0 && Cart.Count % 7 != 0 && Cart.Count % 49 != 0)
            {
                GameObject obj = Instantiate(Objects[x], inventory);
                obj.transform.localPosition =
                    new Vector3(Cart[Cart.Count - 1].transform.localPosition.x - 1,
                    Cart[Cart.Count - 1].transform.localPosition.y,
                    Cart[Cart.Count - 1].transform.localPosition.z);
                return;
            }
            if (Cart.Count > 1 && Cart.Count % 7 == 0 && Cart.Count % 49 != 0)
            {
                GameObject obj = Instantiate(Objects[x], inventory);
                obj.transform.localPosition =
                    new Vector3(StartPosition.x,
                    Cart[Cart.Count - 1].transform.localPosition.y,
                    Cart[Cart.Count - 1].transform.localPosition.z -1);
                return;
            }
            if (Cart.Count > 1 && Cart.Count % 7 == 0 && Cart.Count % 49 == 0)
            {
                GameObject obj = Instantiate(Objects[x], inventory);
                obj.transform.localPosition =
                    new Vector3(StartPosition.x,
                    Cart[Cart.Count - 1].transform.localPosition.y + 1,
                    StartPosition.z);
                return;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<Move.movePlayer>().direction.magnitude<0.1f)
            {
                isPlayerNear = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isPlayerNear = false;
                if (isBought)
                    isBought = false;
            }
        }
    }
}
