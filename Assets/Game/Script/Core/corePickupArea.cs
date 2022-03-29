using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace warehouse.Core
{
    public class corePickupArea : MonoBehaviour
    {

        public List<GameObject> bots = new List<GameObject>();
        public List<GameObject> RobotCount = new List<GameObject>();
        public TextMeshProUGUI BotCount;

        [Space(40)]
        public Transform Red;
        public Transform Yellow;
        public Transform Green;
        public Transform Blue;
        public Transform Orange;
        public Transform Violet;



        void Start()
        {

        }


        void Update()
        {
            BotCount.text = RobotCount.Count.ToString("N0");
        }
    }
}

