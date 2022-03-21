using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace warehouse.Core
{
    public class coreNPCsSpwnner : MonoBehaviour
    {
        private coreA1 coreA1;
        public GameObject[] NPCs;

        public Transform upSpwnner;
        public Transform downSpwnner;
        public Transform Parant;
        public float SpwanTime = 1;
        float x = 0.5f;
        float y = 0.5f;

        private void Start()
        {
            coreA1 = FindObjectOfType<coreA1>();
            x = y = 0;
        }
        void Update()
        {
            spwanNPCs();
        }
        
        void spwanNPCs()
        {
            
            if (coreA1.controlParkingDacksUp.Count > 0)
            {
                if (x > 0)
                    x -= Time.deltaTime;
                if (x <= 0)
                {
                    GameObject npc = Instantiate(NPCs[0], upSpwnner.position, Quaternion.Euler(0, 180, 0), Parant);
                    x = SpwanTime;
                }
            }
            //////////////////////////////////////////////////////////////////////////////////////////
            if (coreA1.controlParkingDacksDown.Count > 0)
            {
                if (y > 0)
                    y -= Time.deltaTime;
                if (y <= 0)
                {
                    GameObject npc = Instantiate(NPCs[1], downSpwnner.position, Quaternion.Euler(0, 0, 0), Parant);
                    npc.GetComponent<Move.moveNPCTruck>().isDown = true;
                    y = SpwanTime;
                }
            }
        }
    }
}

