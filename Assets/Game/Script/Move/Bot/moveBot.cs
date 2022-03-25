using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

namespace warehouse.Move
{
    public class moveBot : MonoBehaviour
    {

        public NavMeshAgent agent;
        public Slider HealthSlider;
        [Header("Target Position")]
        public Transform TargetToClient;
        public Transform TargetToStore;
        public Transform TargetToCharger;

        [Header("Where to go")]
        public bool isWalkTowardClient = false;
        public bool isWalkTowardStore = false;
        public bool isWalkTowardDustbin = false;
        public bool isWalkTowardInitalPosition = false;

        [Header("Health")]
        public float maxCharge;
        public float currentCharge;
        public float DischargeSpeed = 2;
        public float rotationSmooth;
        public bool isOccupied;

       public Vector3 initPos;

        private float turnSmoothVelocity;
        private Core.corePickupArea corePickupArea;
        private Control.controlBotInvetory controlBotInvetory;
        private Core.coreChargingPortManager chargerCollection;


        void Start()
        {
            currentCharge = maxCharge;
            initPos = transform.position;
            corePickupArea = FindObjectOfType<Core.corePickupArea>();
            chargerCollection = FindObjectOfType<Core.coreChargingPortManager>();
            controlBotInvetory = GetComponent<Control.controlBotInvetory>();
            HealthSlider.maxValue = maxCharge;
        }

        [SerializeField]float BrakeCharger;
        
        void Update()
        {
            BrakeCharger = maxCharge * 25 * 0.01f;


            AddToList();
            HealthSlider.value = currentCharge;
            currentCharge = Mathf.Clamp(currentCharge, 0, maxCharge);
            //CheckForCart();
            if (currentCharge > BrakeCharger && TargetToCharger == null)
            {
                moveAndRotateTowardTarget();                
                if (isOccupied)
                    movementTowardsTarget();
                if (!isOccupied)
                    agent.SetDestination(initPos);
            }
            getCharagerLocation();

            if (currentCharge <= 0)
                agent.isStopped = true;

            if (agent.velocity.magnitude > 0.2f && currentCharge >= 0)
                currentCharge -= DischargeSpeed;
        }

        void moveAndRotateTowardTarget()
        {
            if(agent.velocity.magnitude > 0.1f)
            {
                float targetAngle = Mathf.Atan2(agent.velocity.x, agent.velocity.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmooth);
                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
        }

        void getCharagerLocation()
        {
            if(currentCharge < BrakeCharger && TargetToCharger == null && chargerCollection.Charger.Count > 0)
            {
                TargetToCharger = chargerCollection.Charger[chargerCollection.Charger.Count - 1].transform;
                TargetToCharger.GetComponent<Control.controlCharger>().isOccupied = true;
            }
            if (TargetToCharger != null && currentCharge < BrakeCharger)
            {                
                agent.SetDestination(TargetToCharger.position);
            }
            if(currentCharge >= maxCharge && TargetToCharger != null)
            {
                TargetToCharger.GetComponent<Control.controlCharger>().isOccupied = false;
                TargetToCharger = null;
            }
        }
        public void movementTowardsTarget()
        {
            if (controlBotInvetory.Cart.Count <= controlBotInvetory.CartLimit && !isWalkTowardDustbin && controlBotInvetory.ClientNeedItem != -1)
            {
                isWalkTowardStore = true;
                isWalkTowardClient = false;
                isWalkTowardInitalPosition = false;
            }
            if (controlBotInvetory.Cart.Count >= controlBotInvetory.CartLimit && !isWalkTowardDustbin && controlBotInvetory.ClientNeedItem != -1)
            {
                isWalkTowardStore = false;
                isWalkTowardClient = true;
                isWalkTowardInitalPosition = false;
            }
            if (controlBotInvetory.GoToDustbin)
            {
                isWalkTowardDustbin = true;
                isWalkTowardStore = false;
                isWalkTowardClient = false;
                isWalkTowardInitalPosition = false;
            }
            if(!controlBotInvetory.GoToDustbin)
                isWalkTowardDustbin = false;     
            
            if(controlBotInvetory.ClientNeedItem == -1 && controlBotInvetory.Cart.Count <= 0 || !isOccupied)
            {
                isWalkTowardDustbin = false;
                isWalkTowardStore = false;
                isWalkTowardClient = false;
                isWalkTowardInitalPosition = true;
            }



            if (isWalkTowardClient)
                agent.SetDestination(TargetToClient.position);

            if (isWalkTowardStore)
                agent.SetDestination(TargetToStore.position);

            if (isWalkTowardDustbin)
                agent.SetDestination(GameObject.FindGameObjectWithTag("Dustbin").transform.position);
            if (isWalkTowardInitalPosition)
                agent.SetDestination(initPos);
        }

        public void AddToList()
        {
            if (!isOccupied && !corePickupArea.bots.Contains(this.gameObject)) corePickupArea.bots.Add(this.gameObject);
            if (isOccupied && corePickupArea.bots.Contains(this.gameObject)) corePickupArea.bots.Remove(this.gameObject);
        }
    }
}

