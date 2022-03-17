using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wearhouse.Control
{
    public class controlNPCTruck : MonoBehaviour
    {
        private Move.moveNPCTruck moveNPCTruck;
        private controlNPCTruckAnimation controlNPCTruckAnimation;
        private Core.coreA1 coreA1;
        [HideInInspector]public controlLoadingDack controlLoadingDack;

        public GameObject Upgrade;
        public GameObject RequestUIGameObject;

        public int maxNumberLimit = 30;
        public int minNumberLimit = 11;

        

        [Space(40)]        
        public int iRed;
        public int iYellow;
        public int iGreen;
        public int iBlue;
        public int iOrange;
        public int iViolet;

        [Space(40)]
        public int nRed;
        public int nYellow;
        public int nGreen;
        public int nBlue;
        public int nOrange;
        public int nViolet;


        void Start()
        {
            moveNPCTruck = GetComponent<Move.moveNPCTruck>();

            if (RequestUIGameObject.activeSelf)
                RequestUIGameObject.SetActive(false);

            controlNPCTruckAnimation = transform.GetChild(0).GetComponent<controlNPCTruckAnimation>();
            coreA1 = FindObjectOfType<Core.coreA1>();
            RequestGenrator();
        }

        
        void Update()
        {
            ShowUI();
            LoadMiniGame();
        }

        public void LoadMiniGame()
        {
            if(controlLoadingDack != null)
            {
                if(controlLoadingDack.TradeCompleted && controlLoadingDack.isPlayerNear && !moveNPCTruck.tradeIsOver)
                {
                    if (!Upgrade.activeSelf)
                        Upgrade.SetActive(true);
                }
            }
        }
        public void CloseMiniGame()
        {
            FindObjectOfType<Core.GameManager>().NumberOfTruckTraded += 1;
            controlLoadingDack.ResetLoadingDack();
            controlLoadingDack.TradeCompleted = false;
            moveNPCTruck.tradeIsOver = true;
            if (Upgrade.activeSelf)
            {                
                Upgrade.GetComponent<Animator>().Play("Exit");
            }
        }
        public void ShowUI()
        {
            if (controlNPCTruckAnimation.isRechedPlateform)
            {
                RequestUIGameObject.SetActive(true);
                RequestUIGameObject.transform.forward = Camera.main.transform.forward;

            }

        }

        public void RequestGenrator()
        {
            if (coreA1.iRed)
            {
                iRed = 0;
                nRed = Random.Range(minNumberLimit, maxNumberLimit);
            }
            if (coreA1.iYellow)
            {
                iYellow = 1;
                nYellow = Random.Range(minNumberLimit, maxNumberLimit);
            }
            if (coreA1.iGreen)
            {
                iGreen = 2;
                nGreen = Random.Range(minNumberLimit, maxNumberLimit);
            }
            if (coreA1.iBlue)
            {
                iBlue = 3;
                nBlue = Random.Range(minNumberLimit, maxNumberLimit);
            }
            if (coreA1.iOrange)
            {
                iOrange = 4;
                nOrange = Random.Range(minNumberLimit, maxNumberLimit);
            }
            if (coreA1.iViolet)
            {
                iViolet = 5;
                nViolet = Random.Range(minNumberLimit, maxNumberLimit);
            }
        }

    }
}

