using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wearhouse.Control
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
        
        public int StackingArray;
        public float cartUpdateSpeed = 0.1f;
        public float ObjectMovementSpeed = 35;

        public bool TradeCompleted;
        private void Start()
        {
            corePickupArea = FindObjectOfType<Core.corePickupArea>();
            x = cartUpdateSpeed;
        }
        void Update()
        {
            getAccessToBot();            
            if (controlParkingDack.NPCTruck != null && controlParkingDack.NPCTruck.GetComponent<Move.moveNPCTruck>().isRechedOnParkingArea)
                TargetTruck = controlParkingDack.NPCTruck;

            if(TargetTruck != null)
                AddObjectToLoadingDack();
            if (Cart.Count > 0)
            {
                RemoveAtNull();
            }
        }

        void RemoveAtNull()
        {
            for (int i = 0; i <= Cart.Count - 1; i++)
            {
                if (Cart[i] == null)
                    Cart.Remove(Cart[i]);
            }
        }


        public void ArrangeObjectInCart()
        {
            if (Cart.Count == 1 && Cart.Count % StackingArray != 0)
            {
                Cart[0].transform.GetComponent<controlObject>().EndPosition = StartPosition;

                return;
            }
            if (Cart.Count >= 2 && Cart.Count % StackingArray != 0) 
            {
                Cart[Cart.Count - 1].GetComponent<controlObject>().EndPosition =
                    new Vector3(Cart[Cart.Count - 2].GetComponent<controlObject>().EndPosition.x + 1.25f,
                    Cart[Cart.Count - 2].GetComponent<controlObject>().EndPosition.y,
                    Cart[Cart.Count - 2].GetComponent<controlObject>().EndPosition.z);

                return;
            }
            if (Cart.Count >= 2 && Cart.Count % StackingArray == 0)
            {
                Cart[Cart.Count - 1].GetComponent<controlObject>().EndPosition =
                    new Vector3(StartPosition.x,
                    Cart[Cart.Count - 2].GetComponent<controlObject>().EndPosition.y+1f,
                    Cart[Cart.Count - 2].GetComponent<controlObject>().EndPosition.z);
                
                return;
            }
        }
        float x = 0;
        public void AddObjectToCart(Collider col)
        {
            if (x > 0)
                x -= Time.deltaTime;
            if (x <= 0)
            {
                try
                {
                    if (col.GetComponent<controlPlayerInventory>())
                    {
                        controlPlayerInventory c = col.GetComponent<controlPlayerInventory>();
                        c.CurrentLimit += c.Cart[c.Cart.Count - 1].GetComponent<controlObject>().objectHeight;
                        Cart.Add(c.Cart[c.Cart.Count - 1]);
                        c.Cart.Remove(c.Cart[c.Cart.Count - 1]);                        
                        Cart[Cart.Count - 1].transform.parent = inventory;
                        ArrangeObjectInCart();
                        Cart[Cart.Count - 1].transform.GetComponent<controlObject>().isMove = true;
                        Cart[Cart.Count - 1].transform.GetComponent<controlObject>().movementSpeed = ObjectMovementSpeed;
                        x = cartUpdateSpeed;
                        return;
                    }
                    else
                    {
                        print("Error");
                    }
                }
                catch
                {

                }
            }
        }

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
                    }
                    if (TargetTruck.GetComponent<controlNPCTruck>().nYellow > 0)
                    {
                        controlPlayerInventory.AddObjectToLoadingCart(TargetTruck.GetComponent<controlNPCTruck>().iYellow, 
                            TargetTruck.GetComponent<controlNPCTruck>().nYellow,
                            Cart, inventory, this.gameObject);
                    }
                    if (TargetTruck.GetComponent<controlNPCTruck>().nGreen > 0)
                    {
                        controlPlayerInventory.AddObjectToLoadingCart(TargetTruck.GetComponent<controlNPCTruck>().iGreen,
                            TargetTruck.GetComponent<controlNPCTruck>().nGreen,
                            Cart, inventory, this.gameObject);
                    }
                    if (TargetTruck.GetComponent<controlNPCTruck>().nBlue > 0)
                    {
                        controlPlayerInventory.AddObjectToLoadingCart(TargetTruck.GetComponent<controlNPCTruck>().iBlue,
                            TargetTruck.GetComponent<controlNPCTruck>().nBlue,
                            Cart, inventory, this.gameObject);
                    }
                    if (TargetTruck.GetComponent<controlNPCTruck>().nOrange > 0)
                    {
                        controlPlayerInventory.AddObjectToLoadingCart(TargetTruck.GetComponent<controlNPCTruck>().iOrange,
                            TargetTruck.GetComponent<controlNPCTruck>().nOrange,
                            Cart, inventory, this.gameObject);
                    }
                    if (TargetTruck.GetComponent<controlNPCTruck>().nViolet > 0)
                    {
                        controlPlayerInventory.AddObjectToLoadingCart(TargetTruck.GetComponent<controlNPCTruck>().iOrange,
                            TargetTruck.GetComponent<controlNPCTruck>().nOrange,
                            Cart, inventory, this.gameObject);
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

                if(Cart[i]==null)
                    Cart.Remove(Cart[i]);
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
            if (TargetTruck !=null && TargetedBot == null)
            {
                if (corePickupArea.bots.Count > 0)
                {
                    TargetedBot = corePickupArea.bots[corePickupArea.bots.Count - 1];
                    TargetedBot.transform.GetComponent<Move.moveBot>().isOccupied = true;
                    TargetedBot.transform.GetComponent<Move.moveBot>().TargetToClient = DroppingPosition;                    
                }
                               
            }
            if(TargetedBot!=null)
                setTargetStorePosition();
        }
        public void setTargetStorePosition()
        {
            if(TargetTruck.GetComponent<controlNPCTruck>().nRed >= 1)
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
            if (other.gameObject.CompareTag("Player") && other.GetComponent<wearhouse.Move.movePlayer>().direction.magnitude < 0.1f)
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
            if (other.gameObject.CompareTag("Robot") && other.GetComponent<wearhouse.Move.moveBot>().agent.velocity.magnitude < 0.1f)
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
        }
    }
}
