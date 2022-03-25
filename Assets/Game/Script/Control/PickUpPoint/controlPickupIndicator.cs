using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace warehouse.Control
{
    public class controlPickupIndicator : MonoBehaviour
    {
        public GameObject indicator;

        private controlPickup controlPickup;

        void Start()
        {
            controlPickup = GetComponent<controlPickup>();
            indicator.SetActive(false);
        }

        
        void Update()
        {
            checkIndicator();
        }
        void checkIndicator()
        {
            if(controlPickup.Cart.Count<=0 && controlPickup.currentCapacity <= 0)
            {
                indicator.SetActive(true);
            }
        }
        public void ResetIndicator()
        {
            indicator.SetActive(false);
        }
    }
}

