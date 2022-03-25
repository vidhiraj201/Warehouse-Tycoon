using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace warehouse.Control
{
    public class controlCharger : MonoBehaviour
    {
        private Core.coreChargingPortManager PortManager;
        private Move.moveBot moveBot;
        public bool isOccupied;
        public bool isRobotNear;

        [Header("Charging")]
        [Space(10)]
        public float ChargingSpeed = 0.5f;
        void Start()
        {
            PortManager = FindObjectOfType<Core.coreChargingPortManager>();
        }

        
        void Update()
        {
            AddorRemoveList();
            Charging();
        }
        void Charging()
        {
            if (isRobotNear)
            {
                moveBot.currentCharge += ChargingSpeed;
            }
        }
        void AddorRemoveList()
        {
            if (!isOccupied && !PortManager.Charger.Contains(this.gameObject))
                PortManager.Charger.Add(this.gameObject);

            if (isOccupied && PortManager.Charger.Contains(this.gameObject))
                PortManager.Charger.Remove(this.gameObject);
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Robot") && other.GetComponent<Move.moveBot>().agent.velocity.magnitude < 0.1f)
            {
                isRobotNear = true;
                moveBot = other.GetComponent<Move.moveBot>();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (isRobotNear)
            {
                isRobotNear = false;
                moveBot = null;
            }
        }
    }

}
