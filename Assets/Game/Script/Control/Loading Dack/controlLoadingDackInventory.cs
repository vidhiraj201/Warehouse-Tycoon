using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace warehouse.Control
{
    public class controlLoadingDackInventory : MonoBehaviour
    {
        Control.controlNPCTruck controlNPC;
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            getControlOfTruck();
        }
        void getControlOfTruck()
        {
            if (GetComponent<controlLoadingDack>().TargetTruck != null)
                controlNPC = GetComponent<controlLoadingDack>().TargetTruck.GetComponent<controlNPCTruck>();
        }
    }

}
