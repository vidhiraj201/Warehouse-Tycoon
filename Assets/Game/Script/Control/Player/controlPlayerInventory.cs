using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace warehouse.Control
{
    public class controlPlayerInventory : MonoBehaviour
    {
        public List<GameObject> Cart = new List<GameObject>();
        [SerializeField] private Vector3 StartPosition;
        public Transform cartTransform;
        public bool DetachObject;

        public int MaxLimit;
        public int CurrentLimit;
        public float cartUpdateSpeed;
        private Transform EndPosition;
        void Start()
        {
            CurrentLimit = MaxLimit;
        }

        
        void Update()
        {
          
        }
        void clear()
        {
            for(int i = 0; i <= Cart.Count - 1; i++)
            {
                if (Cart[i] == null)
                {
                    Cart.Remove(Cart[i]);
                }
            }
        }

        public void cartManagement()
        {
            if (Cart.Count > 0)
            {
                for(int i=0; i <= Cart.Count - 1; i++)
                {
                    Cart[i].transform.localPosition = new Vector3(0, Cart[i].transform.localPosition.y, 0);
                }
            }
        }
        public void ArrangeObjectInCart()
        {
            if (Cart.Count == 1)
            {
                Cart[0].transform.GetComponent<controlObject>().EndPosition = StartPosition;
                Cart[0].transform.GetComponent<controlObject>().isMove = true;
                Cart[Cart.Count - 1].transform.GetComponent<controlObject>().isReched = true;
                return;
            }
            if (Cart.Count > 1)
            {
                Cart[Cart.Count - 1].GetComponent<controlObject>().EndPosition =
                    new Vector3(Cart[Cart.Count - 2].GetComponent<controlObject>().EndPosition.x,
                    Cart[Cart.Count - 2].GetComponent<controlObject>().EndPosition.y + 1f,
                    Cart[Cart.Count - 2].GetComponent<controlObject>().EndPosition.z);
                Cart[Cart.Count - 1].transform.GetComponent<controlObject>().isMove = true;
                Cart[Cart.Count - 1].transform.GetComponent<controlObject>().isReched = true;
                return;
            }
        }

        float x = 0;
        public void AddObjectToCart(Collider col)
        {
            if (CurrentLimit > 0)
            {
                if(x > 0)
                    x -= Time.deltaTime;
                if (x <= 0)
                {
                    try
                    {
                        if (col.GetComponent<controlPickup>())
                        {
                            controlPickup c = col.GetComponent<controlPickup>();
                            if (c.Cart.Count > 0)
                            {
                                Cart.Add(c.Cart[c.Cart.Count - 1]);
                                Cart[Cart.Count - 1].transform.parent = cartTransform;
                                c.Cart.Remove(c.Cart[c.Cart.Count - 1]);
                                CurrentLimit -= Cart[Cart.Count - 1].GetComponent<controlObject>().objectHeight;
                                ArrangeObjectInCart();                                
                                x = cartUpdateSpeed;
                            }

                        }
                        else
                        {
                            print("Error !" + transform.name);
                        }
                    }
                    catch
                    {
                        print("Error !" + transform.name);
                    }
                }                
            }
        }

        public void AddObjectToLoadingCart(float ID, int NumberOfObject, List<GameObject> c, Transform inventory, GameObject G)
        {
            if (Cart.Count > 0 && NumberOfObject > 0)
            {
                for(int i=0; i<= Cart.Count - 1;)
                {                    
                    if (i >= Cart.Count - 1 && Cart[i].GetComponent<controlObject>().objectID != ID) return;
                    if (Cart[i].GetComponent<controlObject>().objectID != ID) i++;
                    if(Cart[i].GetComponent<controlObject>().objectID == ID)
                    {
                        G.GetComponent<controlLoadingDack>().reduceNumber(Cart[i].GetComponent<controlObject>().objectID);    
                        cartManagement();
                        CurrentLimit += Cart[i].GetComponent<controlObject>().objectHeight;
                        c.Add(Cart[i]);
                        Cart.Remove(Cart[i]);
                        G.GetComponent<controlLoadingDack>().ArrangeObjectInCart();                        
                        c[c.Count - 1].transform.GetComponent<controlObject>().isReched = false;                        
                        c[c.Count - 1].transform.parent = inventory;
                        c[c.Count - 1].transform.GetComponent<controlObject>().isMove = true;
                        c[c.Count - 1].transform.GetComponent<controlObject>().movementSpeed = G.GetComponent<controlLoadingDack>().ObjectMovementSpeed;             
                        clear();
                        break;
                    }
                }
            }
        }



        float xB = 0.5f;
        public void GiveBatteryToRobot(float ID, Collider collider, float BatteryStatus, float max)
        {
            if (xB > 0)
                xB -= Time.deltaTime;
            if (xB <= 0)
            {
                if (Cart.Count > 0 && BatteryStatus < (max/**75*0.01f*/))
                {

                    for (int i = 0; i <= Cart.Count - 1;)
                    {

                        if (i >= Cart.Count - 1 && Cart[i].GetComponent<controlObject>().objectID != ID) return;
                        if (Cart[i].GetComponent<controlObject>().objectID != ID) i++;
                        if (Cart[i].GetComponent<controlObject>().objectID == ID)
                        {

                            cartManagement();
                            CurrentLimit += Cart[i].GetComponent<controlObject>().objectHeight;
                            Cart[i].transform.GetComponent<controlObject>().EndPosition = Vector3.zero;
                            Cart[i].transform.parent = collider.transform;
                            Cart[i].transform.GetComponent<controlObject>().isMove = true;
                            //Cart[i].transform.GetComponent<controlObject>().isDestroy = true;
                            Cart.Remove(Cart[i]);

                            /* G.GetComponent<controlLoadingDack>().ArrangeObjectInCart();
                             c[c.Count - 1].transform.GetComponent<controlObject>().isReched = false;
                             c[c.Count - 1].transform.parent = inventory;
                             c[c.Count - 1].transform.GetComponent<controlObject>().isMove = true;
                             c[c.Count - 1].transform.GetComponent<controlObject>().movementSpeed = G.GetComponent<controlLoadingDack>().ObjectMovementSpeed;*/
                            clear();
                            xB = 1.5f;
                            break;
                        }
                    }
                }
            }

           
        }

        float xp = 0.125f;
        public void RemoveObject(Collider other)
        {
            if (xp > 0)
                xp -= Time.deltaTime;
            if(xp<=0)
            {
                if (Cart.Count > 0)
                {
                    CurrentLimit += Cart[Cart.Count - 1].GetComponent<controlObject>().objectHeight;
                    Cart[Cart.Count - 1].transform.parent = other.transform;
                    Cart[Cart.Count - 1].GetComponent<controlObject>().EndPosition = Vector3.zero;
                    Cart[Cart.Count - 1].GetComponent<controlObject>().isMove = true;                   
                    Cart.Remove(Cart[Cart.Count - 1]);
                    clear();
                    xp = 0.125f;
                }
            }
          
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Pickup") && GetComponent<warehouse.Move.movePlayer>().direction.magnitude<0.1f && !other.gameObject.GetComponent<controlPickup>().isBought)
            {
                
                AddObjectToCart(other);
                
            }
            if (other.gameObject.CompareTag("Dustbin") && GetComponent<warehouse.Move.movePlayer>().direction.magnitude < 0.1f)
            {
                RemoveObject(other);
                
            }
            if (other.gameObject.CompareTag("Robot"))
            {

                GiveBatteryToRobot(other.GetComponent<controlBotInvetory>().BatteryID, other, other.GetComponent<Move.moveBot>().currentCharge, other.GetComponent<Move.moveBot>().maxCharge);
            }
        }
        /*private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Robot"))
            {
                print("Trigger");
                GiveBatteryToRobot(other.GetComponent<controlBotInvetory>().BatteryID, other, other.GetComponent<controlBotInvetory>().CurrentLimit);
            }
        }*/
    }
}
