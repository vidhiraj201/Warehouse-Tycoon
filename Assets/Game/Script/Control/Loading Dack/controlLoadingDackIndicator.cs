using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace warehouse.Control{
    public class controlLoadingDackIndicator : MonoBehaviour
    {
        public GameObject indicator;
        private controlLoadingDack controlLoading;
        void Start()
        {
            controlLoading = GetComponent<controlLoadingDack>();
            indicator.SetActive(false);
        }

        
        void Update()
        {
            checkIndicator();
        }
        void checkIndicator()
        {
            if(controlLoading.TradeCompleted && !controlLoading.isPlayerNear)
            {
                indicator.SetActive(true);
            }

            if (controlLoading.TradeCompleted && controlLoading.isPlayerNear)
            {
                indicator.SetActive(false);
            }

            if (!controlLoading.TradeCompleted)
            {
                indicator.SetActive(false);
            }
        }
    }
}

