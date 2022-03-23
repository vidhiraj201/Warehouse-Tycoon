using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace warehouse.Control
{
    public class controlParkingDack : MonoBehaviour
    {
        private Core.coreA1 coreA1;
        public bool isUp;
        public bool isOccupied;
        public GameObject NPCTruck;
        public controlLoadingDack controlLoadingDack;
        public controlLoadingDeckAnimation DeckAnimation;
        public bool isLocked;

        private void Start()
        {
            coreA1 = FindObjectOfType<Core.coreA1>();
        }
        void Update()
        {
            if (!isLocked)
            {
                addToList();
                removeFromList();
                if ( !coreA1.ParkingDacks.Contains(this.gameObject))
                {
                    coreA1.ParkingDacks.Add(this.gameObject);
                }
            }   
            
            if(NPCTruck != null)
            {
                if(NPCTruck.GetComponent<Move.moveNPCTruck>().isRechedOnParkingArea)
                    controlLoadingDack.TargetTruck = NPCTruck;
            }
        }

        void addToList()
        {
            if (isUp)
            {
                if (!isOccupied && !coreA1.controlParkingDacksUp.Contains(this.gameObject))
                {
                    coreA1.controlParkingDacksUp.Add(this.gameObject);
                }
            }
            if (!isUp)
            {
                if (!isOccupied && !coreA1.controlParkingDacksDown.Contains(this.gameObject))
                {
                    coreA1.controlParkingDacksDown.Add(this.gameObject);
                }
            }

            
        }
        void removeFromList()
        {
            if (isUp)
            {
                if (isOccupied && coreA1.controlParkingDacksUp.Contains(this.gameObject))
                {
                    coreA1.controlParkingDacksUp.Remove(this.gameObject);
                }
            }
            if (!isUp)
            {
                if (isOccupied && coreA1.controlParkingDacksDown.Contains(this.gameObject))
                {
                    coreA1.controlParkingDacksDown.Remove(this.gameObject);
                }
            }

        }
    }
}

