using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace warehouse.Control
{
    public class controlNPCTruckAnimation : MonoBehaviour
    {
        public GameObject UI;
        public bool isRechedPlateform = false;
        public controlNPCTruck controlNPCTruck;
        public Move.moveNPCTruck moveNPCTruck;

        public Image iRed;
        public Image iYellow;
        public Image iGreen;
        public Image iBlue;
        public Image iOrange;
        public Image iViolet;


        public TextMeshProUGUI tRed;
        public TextMeshProUGUI tYellow;
        public TextMeshProUGUI tGreen;
        public TextMeshProUGUI tBlue;
        public TextMeshProUGUI tOrange;
        public TextMeshProUGUI tViolet;
        void Start()
        {
            CheckForAskingColor();
        }

        
        void Update()
        {
            tRed.text = controlNPCTruck.nRed.ToString();
            tYellow.text = controlNPCTruck.nYellow.ToString();
            tGreen.text = controlNPCTruck.nGreen.ToString();
            tBlue.text = controlNPCTruck.nBlue.ToString();
            tOrange.text = controlNPCTruck.nOrange.ToString();
            tViolet.text = controlNPCTruck.nViolet.ToString();
        }
        public void RechedPlateform()
        {
            isRechedPlateform = true;
            moveNPCTruck.isRechedOnParkingArea = true;
        }

        public void CheckForAskingColor()
        {
            if(controlNPCTruck.nRed <= 0)
            {
                iRed.gameObject.SetActive(false);
                tRed.gameObject.SetActive(false);
            }else if(controlNPCTruck.nRed > 0)
            {
                iRed.gameObject.SetActive(true);
                tRed.gameObject.SetActive(true);
            }

            if (controlNPCTruck.nYellow <= 0)
            {
                iYellow.gameObject.SetActive(false);
                tYellow.gameObject.SetActive(false);
            }
            else if (controlNPCTruck.nYellow > 0)
            {
                iYellow.gameObject.SetActive(true);
                tYellow.gameObject.SetActive(true);
            }

            if (controlNPCTruck.nGreen <= 0)
            {
                iGreen.gameObject.SetActive(false);
                tGreen.gameObject.SetActive(false);
            }
            else if (controlNPCTruck.nGreen > 0)
            {
                iGreen.gameObject.SetActive(true);
                tGreen.gameObject.SetActive(true);
            }

            if (controlNPCTruck.nViolet <= 0)
            {
                iViolet.gameObject.SetActive(false);
                tViolet.gameObject.SetActive(false);
            }
            else if (controlNPCTruck.nViolet > 0)
            {
                iViolet.gameObject.SetActive(true);
                tViolet.gameObject.SetActive(true);
            }

            if (controlNPCTruck.nOrange <= 0)
            {
                iOrange.gameObject.SetActive(false);
                tOrange.gameObject.SetActive(false);
            }
            else if (controlNPCTruck.nOrange > 0)
            {
                iOrange.gameObject.SetActive(true);
                tOrange.gameObject.SetActive(true);
            }

            if (controlNPCTruck.nBlue <= 0)
            {
                iBlue.gameObject.SetActive(false);
                tBlue.gameObject.SetActive(false);
            }
            else if (controlNPCTruck.nBlue > 0)
            {
                iBlue.gameObject.SetActive(true);
                tBlue.gameObject.SetActive(true);
            }
        }

        public void AddMoneyToBank()
        {
            FindObjectOfType<Core.GameManager>().maxMoney += 200;
        }

        public void TradeOver()
        {               
            moveNPCTruck.TargetObject.GetComponent<Control.controlParkingDack>().controlLoadingDack.TargetTruck = null;
            moveNPCTruck.TargetObject.GetComponent<Control.controlParkingDack>().isOccupied = false;
            moveNPCTruck.TargetObject.GetComponent<Control.controlParkingDack>().NPCTruck = null;

            Destroy(moveNPCTruck.gameObject);
        }
    }
}

