using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace warehouse.Control
{
    public class controlLoadingDack : MonoBehaviour
    {
        private Core.corePickupArea corePickupArea;
        public List<GameObject> Cart = new List<GameObject>();
        public GameObject TargetTruck;
        public GameObject TargetedBot;
        public controlParkingDack controlParkingDack;
        public bool isPlayerNear;
        public bool isRobotNear;

        public Transform DroppingPosition;
        public Transform inventory;
        public Vector3 StartPosition;
        
        public int StackingArrayX;
        public int StackingArrayCount;
        public float cartUpdateSpeed = 0.1f;
        public float ObjectMovementSpeed = 35;

        public bool TradeCompleted;
        private coreAudioManager audioManager;
        private void Start()
        {
            audioManager = FindObjectOfType<coreAudioManager>();
            corePickupArea = FindObjectOfType<Core.corePickupArea>();
            x = cartUpdateSpeed;
        }
        void Update()
        {
            if(Cart.Count>0)
                ArrangeObjectInCart();

            if (!TradeCompleted)
                getAccessToBot();            
/*            if (controlParkingDack.NPCTruck != null && controlParkingDack.NPCTruck.GetComponent<Move.moveNPCTruck>().isRechedOnParkingArea)
                TargetTruck = controlParkingDack.NPCTruck;*/

            if(TargetTruck != null)
                AddObjectToLoadingDack();

           /* if(Cart.Count>0)
                RemoveAtNull();*/
        }

        [SerializeField]int i = 0;
        void RemoveAtNull()
        {
            if(i<= Cart.Count - 1)
            {
                if (Cart[i] == null)
                {
                    Cart.Remove(Cart[i]);
                    i++;
                }
            }
            if (i >= Cart.Count - 1)
                i = 0;
        }


        public void ArrangeObjectInCart()
        {
            if (Cart.Count <= 1 /*&& Cart.Count % StackingArrayX != 0 && Cart.Count % StackingArrayCount != 0*/)
            {
                Cart[Cart.Count - 1].transform.GetComponent<controlObject>().EndPosition = StartPosition;
                //audioManager.source.PlayOneShot(audioManager.Sell);
                return;
            }
            if (Cart.Count > 1 && Cart.Count % StackingArrayX != 0 && Cart.Count % StackingArrayCount != 0) 
            {
                Cart[Cart.Count - 1].GetComponent<controlObject>().EndPosition =
                    new Vector3(Cart[Cart.Count - 2].GetComponent<controlObject>().EndPosition.x + 0.95f,
                    Cart[Cart.Count - 2].GetComponent<controlObject>().EndPosition.y,
                    Cart[Cart.Count - 2].GetComponent<controlObject>().EndPosition.z);                
                return;
            }
            if (Cart.Count > 1 && Cart.Count % StackingArrayX == 0 && Cart.Count % StackingArrayCount != 0)
            {
                Cart[Cart.Count - 1].GetComponent<controlObject>().EndPosition =
                    new Vector3(StartPosition.x,
                    Cart[Cart.Count - 2].GetComponent<controlObject>().EndPosition.y,
                    Cart[Cart.Count - 2].GetComponent<controlObject>().EndPosition.z +0.95f);
                
                return;
            }
            if (Cart.Count > 1 && Cart.Count % StackingArrayX == 0 && Cart.Count % StackingArrayCount == 0)
            {
                Cart[Cart.Count - 1].GetComponent<controlObject>().EndPosition =
                    new Vector3(StartPosition.x,
                    Cart[Cart.Count - 2].GetComponent<controlObject>().EndPosition.y + 1f,
                    StartPosition.z);
                
                return;
            }
        }
        float x = 0.2f;

        public controlPlayerInventory controlPlayerInventory;
        public controlBotInvetory controlBotInvetory;
        public void AddObjectToLoadingDack()
        {
            TargetTruck.GetComponent<controlNPCTruck>().controlLoadingDack = this.GetComponent<controlLoadingDack>();
            if (!TargetTruck.GetComponent<Move.moveNPCTruck>().tradeIsOver)
            {
                if (TargetTruck.GetComponent<controlNPCTruck>().nRed <= 0 &&
            TargetTruck.GetComponent<controlNPCTruck>().nYellow <= 0 &&
            TargetTruck.GetComponent<controlNPCTruck>().nGreen <= 0 &&
            TargetTruck.GetComponent<controlNPCTruck>().nBlue <= 0 &&
            TargetTruck.GetComponent<controlNPCTruck>().nOrange <= 0 &&
            TargetTruck.GetComponent<controlNPCTruck>().nViolet <= 0
            )
                {
                    TradeCompleted = true;
//                    TargetTruck.GetComponent<controlNPCTruckPopup>().isTradeOver = true;
                    if (TargetedBot != null)
                    {
                        TargetedBot.transform.GetComponent<controlBotInvetory>().ClientNeedItem = -1;
                        TargetedBot.transform.GetComponent<Move.moveBot>().isOccupied = false;
                        TargetedBot.transform.GetComponent<Move.moveBot>().TargetToStore = null;
                        TargetedBot.transform.GetComponent<Move.moveBot>().TargetToClient = null;
                        TargetedBot = null;
                    }
                    
                }
            }
            

            if (TradeCompleted)
                StartCoroutine(HideUI(0.2f));



            if (isPlayerNear)
            {
                if (x > 0)
                    x -= Time.deltaTime;
                if (x <= 0)
                {
                    if (TargetTruck.GetComponent<controlNPCTruck>().nRed > 0)
                    {
                        controlPlayerInventory.AddObjectToLoadingCart(TargetTruck.GetComponent<controlNPCTruck>().iRed, 
                            TargetTruck.GetComponent<controlNPCTruck>().nRed,
                            Cart, inventory, this.gameObject);
                        x = cartUpdateSpeed;
                    }
                    if (TargetTruck.GetComponent<controlNPCTruck>().nYellow > 0)
                    {
                        controlPlayerInventory.AddObjectToLoadingCart(TargetTruck.GetComponent<controlNPCTruck>().iYellow, 
                            TargetTruck.GetComponent<controlNPCTruck>().nYellow,
                            Cart, inventory, this.gameObject);
                        x = cartUpdateSpeed;
                    }
                    if (TargetTruck.GetComponent<controlNPCTruck>().nGreen > 0)
                    {
                        controlPlayerInventory.AddObjectToLoadingCart(TargetTruck.GetComponent<controlNPCTruck>().iGreen,
                            TargetTruck.GetComponent<controlNPCTruck>().nGreen,
                            Cart, inventory, this.gameObject);
                        x = cartUpdateSpeed;
                    }
                    if (TargetTruck.GetComponent<controlNPCTruck>().nBlue > 0)
                    {
                        controlPlayerInventory.AddObjectToLoadingCart(TargetTruck.GetComponent<controlNPCTruck>().iBlue,
                            TargetTruck.GetComponent<controlNPCTruck>().nBlue,
                            Cart, inventory, this.gameObject);
                        x = cartUpdateSpeed;
                    }
                    if (TargetTruck.GetComponent<controlNPCTruck>().nOrange > 0)
                    {
                        controlPlayerInventory.AddObjectToLoadingCart(TargetTruck.GetComponent<controlNPCTruck>().iOrange,
                            TargetTruck.GetComponent<controlNPCTruck>().nOrange,
                            Cart, inventory, this.gameObject);
                        x = cartUpdateSpeed;
                    }
                    if (TargetTruck.GetComponent<controlNPCTruck>().nViolet > 0)
                    {
                        controlPlayerInventory.AddObjectToLoadingCart(TargetTruck.GetComponent<controlNPCTruck>().iOrange,
                            TargetTruck.GetComponent<controlNPCTruck>().nOrange,
                            Cart, inventory, this.gameObject);
                        x = cartUpdateSpeed;
                    }

                }
            }
            if (isRobotNear)
            {
                if (x > 0)
                    x -= Time.deltaTime;
                if (x <= 0)
                {
                    if (TargetTruck.GetComponent<controlNPCTruck>().nRed > 0)
                    {
                        controlBotInvetory.AddObjectToLoadingCart(TargetTruck.GetComponent<controlNPCTruck>().iRed,
                            TargetTruck.GetComponent<controlNPCTruck>().nRed,
                            Cart, inventory, this.gameObject);
                    }
                    if (TargetTruck.GetComponent<controlNPCTruck>().nYellow > 0)
                    {
                        controlBotInvetory.AddObjectToLoadingCart(TargetTruck.GetComponent<controlNPCTruck>().iYellow,
                            TargetTruck.GetComponent<controlNPCTruck>().nYellow,
                            Cart, inventory, this.gameObject);
                    }
                    if (TargetTruck.GetComponent<controlNPCTruck>().nGreen > 0)
                    {
                        controlBotInvetory.AddObjectToLoadingCart(TargetTruck.GetComponent<controlNPCTruck>().iGreen,
                            TargetTruck.GetComponent<controlNPCTruck>().nGreen,
                            Cart, inventory, this.gameObject);
                    }
                    if (TargetTruck.GetComponent<controlNPCTruck>().nBlue > 0)
                    {
                        controlBotInvetory.AddObjectToLoadingCart(TargetTruck.GetComponent<controlNPCTruck>().iBlue,
                            TargetTruck.GetComponent<controlNPCTruck>().nBlue,
                            Cart, inventory, this.gameObject);
                    }
                    if (TargetTruck.GetComponent<controlNPCTruck>().nOrange > 0)
                    {
                        controlBotInvetory.AddObjectToLoadingCart(TargetTruck.GetComponent<controlNPCTruck>().iOrange,
                            TargetTruck.GetComponent<controlNPCTruck>().nOrange,
                            Cart, inventory, this.gameObject);
                    }
                    if (TargetTruck.GetComponent<controlNPCTruck>().nViolet > 0)
                    {
                        controlBotInvetory.AddObjectToLoadingCart(TargetTruck.GetComponent<controlNPCTruck>().iOrange,
                            TargetTruck.GetComponent<controlNPCTruck>().nOrange,
                            Cart, inventory, this.gameObject);
                    }

                }
            }
        }

        public void ResetLoadingDack()
        {            
            for(int i = 0; i <= Cart.Count - 1; i++)
            {
                if(Cart[i]!=null)
                    Destroy(Cart[i]);
            }
        }

        IEnumerator HideUI(float t)
        {
            yield return new WaitForSeconds(t);
            TargetTruck.transform.GetChild(0).GetComponent<controlNPCTruckAnimation>().UI.SetActive(false);
        }

        public void reduceNumber(int ID)
        {
            if (TargetTruck.GetComponent<controlNPCTruck>().iRed == ID && TargetTruck.GetComponent<controlNPCTruck>().nRed >= 1)
            {
                TargetTruck.GetComponent<controlNPCTruck>().nRed -= 1;
            }
            if (TargetTruck.GetComponent<controlNPCTruck>().iYellow == ID && TargetTruck.GetComponent<controlNPCTruck>().nYellow >= 1)
            {
                TargetTruck.GetComponent<controlNPCTruck>().nYellow -= 1;
            }
            if (TargetTruck.GetComponent<controlNPCTruck>().iGreen == ID && TargetTruck.GetComponent<controlNPCTruck>().nGreen >= 1)
            {
                TargetTruck.GetComponent<controlNPCTruck>().nGreen -= 1;
            }
            if (TargetTruck.GetComponent<controlNPCTruck>().iBlue == ID && TargetTruck.GetComponent<controlNPCTruck>().nBlue >= 1)
            {
                TargetTruck.GetComponent<controlNPCTruck>().nBlue -= 1;
            }
            if (TargetTruck.GetComponent<controlNPCTruck>().iOrange == ID && TargetTruck.GetComponent<controlNPCTruck>().nOrange >= 1)
            {
                TargetTruck.GetComponent<controlNPCTruck>().nOrange -= 1;
            }
            if (TargetTruck.GetComponent<controlNPCTruck>().iViolet == ID && TargetTruck.GetComponent<controlNPCTruck>().nViolet >= 1)
            {
                TargetTruck.GetComponent<controlNPCTruck>().nViolet -= 1;
            }
        }

        public void getAccessToBot()
        {
            if(corePickupArea.bots.Count >= 1)
            {
                if (!corePickupArea.bots[corePickupArea.bots.Count - 1].GetComponent<Move.moveBot>().isOccupied && TargetTruck != null && TargetedBot == null)
                {
                    TargetedBot = corePickupArea.bots[corePickupArea.bots.Count - 1];
                    TargetedBot.transform.GetComponent<Move.moveBot>().isOccupied = true;
                    TargetedBot.transform.GetComponent<Move.moveBot>().TargetToClient = DroppingPosition;
                }
            }
            

            if (TargetTruck!=null && TargetedBot != null)
                setTargetStorePosition();
        }
        public void setTargetStorePosition()
        {
            TargetedBot.transform.GetComponent<Move.moveBot>().isOccupied = true;
            if (TargetTruck.GetComponent<controlNPCTruck>().nRed >= 1)
            {
                TargetedBot.transform.GetComponent<Move.moveBot>().TargetToStore = corePickupArea.Red;
                TargetedBot.transform.GetComponent<controlBotInvetory>().ClientNeedItem = 0;
            }
            else if (TargetTruck.GetComponent<controlNPCTruck>().nYellow >= 1)
            {
                TargetedBot.transform.GetComponent<Move.moveBot>().TargetToStore = corePickupArea.Yellow;
                TargetedBot.transform.GetComponent<controlBotInvetory>().ClientNeedItem = 1;
            }
            else if (TargetTruck.GetComponent<controlNPCTruck>().nGreen >= 1)
            {
                TargetedBot.transform.GetComponent<Move.moveBot>().TargetToStore = corePickupArea.Green;
                TargetedBot.transform.GetComponent<controlBotInvetory>().ClientNeedItem = 2;
            }
            else if (TargetTruck.GetComponent<controlNPCTruck>().nBlue >= 1)
            {
                TargetedBot.transform.GetComponent<Move.moveBot>().TargetToStore = corePickupArea.Blue;
                TargetedBot.transform.GetComponent<controlBotInvetory>().ClientNeedItem = 3;
            }
            else if (TargetTruck.GetComponent<controlNPCTruck>().nOrange >= 1)
            {
                TargetedBot.transform.GetComponent<Move.moveBot>().TargetToStore = corePickupArea.Orange;
                TargetedBot.transform.GetComponent<controlBotInvetory>().ClientNeedItem = 4;
            }
            else if (TargetTruck.GetComponent<controlNPCTruck>().nViolet >= 1)
            {
                TargetedBot.transform.GetComponent<Move.moveBot>().TargetToStore = corePickupArea.Violet;
                TargetedBot.transform.GetComponent<controlBotInvetory>().ClientNeedItem = 5;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && other.GetComponent<warehouse.Move.movePlayer>().direction.magnitude < 0.1f)
            {
                isPlayerNear = true;

                try
                {
                    controlPlayerInventory = other.GetComponent<controlPlayerInventory>();
                }
                catch
                {

                }

                //AddObjectToCart(other);
            }
            if (other.gameObject.CompareTag("Robot") && other.GetComponent<warehouse.Move.moveBot>().agent.velocity.magnitude < 0.1f)
            {
                isRobotNear = true;

                try
                {
                    controlBotInvetory = other.GetComponent<controlBotInvetory>();
                }
                catch
                {

                }

                //AddObjectToCart(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (isPlayerNear)
                isPlayerNear = false;

            if (controlPlayerInventory != null)
                controlPlayerInventory = null;

            if (isRobotNear)
                isRobotNear = false;

            if (controlBotInvetory != null)
                controlBotInvetory = null;
        }
    }
}
