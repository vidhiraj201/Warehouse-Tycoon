using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace warehouse.Move
{
    public class moveNPCTruck : MonoBehaviour
    {
        private Core.coreA1 coreA1;
        public GameObject TargetObject;

        public NavMeshAgent NPCAgent;
        public Animator Anime;
        public float Rotate;

        public Transform target;
        public Transform end;

        private float turnSmoothVelocity;
        public float rotationSmooth = 0.01f;

        [SerializeField] private float DistanceBetweenTargetAndTruck;
        public bool isRechedStation = false;
        public bool isRechedOnParkingArea = false;
        public bool tradeIsOver = false;
        public bool isDown = false;

        [Header("Move")]
        private bool isMoveTrue;
        void Start()
        {
            NPCAgent = GetComponent<NavMeshAgent>();
            coreA1 = FindObjectOfType<Core.coreA1>();
            
        }
        
        void Update()
        {
            move();
            if(TargetObject !=null)
                Checking();

            if (TargetObject == null)
                ActionForMove();
        }
        void move()
        {
            if(NPCAgent.velocity.magnitude > 0.1f && !tradeIsOver && !isRechedStation)
            {
                float targetAngle = Mathf.Atan2(NPCAgent.velocity.x, NPCAgent.velocity.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmooth);
                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
            if (isMoveTrue)
            {
                NPCAgent.SetDestination(target.position);
            }
            if (tradeIsOver)
            {
                StartCoroutine(AnimateLeaving(1f));
            }
        }
        void Checking()
        {
            if(target!=null)
                DistanceBetweenTargetAndTruck = Vector3.Distance(transform.position, target.position);
            if(DistanceBetweenTargetAndTruck <= 10f)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, Rotate, 0), 2 * Time.deltaTime);

            if (DistanceBetweenTargetAndTruck <= 0.2f && !isRechedStation && !tradeIsOver) 
            {
                isRechedStation = true;
                StartCoroutine(AnimateParking(0.25f));               
            }
        }

        IEnumerator AnimateParking(float t)
        {
            yield return new WaitForSeconds(t);
            Anime.Play("Place to Loading Dack");
            
        }
        IEnumerator AnimateLeaving(float t)
        {
            yield return new WaitForSeconds(t);
            Anime.Play("Go to End Position");

        }
        void ActionForMove()
        {
            if (!isDown)
            {
                if (coreA1.controlParkingDacksUp.Count > 0)
                {
                    try
                    {
                        TargetObject = coreA1.controlParkingDacksUp[0];
                        TargetObject.GetComponent<Control.controlParkingDack>().isOccupied = true;
                        TargetObject.GetComponent<Control.controlParkingDack>().NPCTruck = this.gameObject;
                        target = TargetObject.transform.GetChild(0);
                        end = TargetObject.transform.GetChild(1);
                        isMoveTrue = true;
                    }
                    catch
                    {
                        ActionForMove();
                    }
                }
            }
            if (isDown)
            {
                if (coreA1.controlParkingDacksDown.Count > 0)
                {
                    try
                    {
                        TargetObject = coreA1.controlParkingDacksDown[0];
                        TargetObject.GetComponent<Control.controlParkingDack>().isOccupied = true;
                        TargetObject.GetComponent<Control.controlParkingDack>().NPCTruck = this.gameObject;
                        target = TargetObject.transform.GetChild(0);
                        end = TargetObject.transform.GetChild(1);
                        isMoveTrue = true;
                    }
                    catch
                    {
                        ActionForMove();
                    }
                }
            }

        }
    }
}

