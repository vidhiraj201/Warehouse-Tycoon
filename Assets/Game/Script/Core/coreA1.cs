using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace warehouse.Core
{
    public class coreA1 : MonoBehaviour
    {
        public List<GameObject> ParkingDacks = new List<GameObject>();
        public List<GameObject> controlParkingDacksUp = new List<GameObject>();
        public List<GameObject> controlParkingDacksDown = new List<GameObject>();

        public TextMeshProUGUI numberOfUnlockedLoadingDacks;
        public bool iRed;
        public bool iYellow;
        public bool iGreen;
        public bool iBlue;
        public bool iOrange;
        public bool iViolet;

        void Start()
        {

        }

        
        void Update()
        {
            numberOfUnlockedLoadingDacks.text = (ParkingDacks.Count).ToString("N0");
        }
    }
}

