using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wearhouse.Control
{
    public class controlBotInvetory : MonoBehaviour
    {
        public GameObject[] Objects;
        public List<GameObject> Cart = new List<GameObject>();
        [SerializeField] private Vector3 StartPosition;
        public Transform UI;
        public Transform cartTransform;
        public bool GoToDustbin;
        public int BatteryID = 6;
        public int CartLimit;
        public int CurrentLimit;
        public float cartUpdateSpeed;
        
        [Header("What item you need")]
        public float ClientNeedItem;

        private Transform EndPosition;

        public bool isDustbin;
        void Start()
        {
            CurrentLimit = CartLimit;
        }


        void Update()
        {
            RemoveNullObjects();
            CartChecking();
            UI.transform.forward = -Camera.main.transform.forward;
        }
        void RemoveNullObjects()
        {
            for (int i = 0; i <= Cart.Count - 1; i++)
            {
                if (Cart[i] == null)
                    Cart.Remove(Cart[i]);
            }
        }
        void DeleteNotInUseObjects()
        {
            for (int i = 0; i <= Cart.Count - 1; i++)
            {
                if (Cart[i] != null)
                    Destroy(Cart[i]);
            }
        }


        void CartChecking() {
            if (Cart.Count > 0)
            {
                for(int i = 0; i <= Cart.Count - 1;)
                {
                    if (Cart[i].GetComponent<controlObject>().objectID == ClientNeedItem) i++;
                    if (i >= Cart.Count - 1 && Cart[i].GetComponent<controlObject>().objectID == ClientNeedItem) return;
                    if (Cart[i].GetComponent<controlObject>().objectID != ClientNeedItem)
                    {
                        if(!GoToDustbin)
                        GoToDustbin = true;
                        i++;
                    }
                }
            }
        }
        public void cartManagement()
        {
            if (Cart.Count > 0)
            {
                for (int i = 0; i <= Cart.Count - 1; i++)
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
                if (x > 0)
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
                for (int i = 0; i <= Cart.Count - 1;)
                {
                    if (i >= Cart.Count - 1 && Cart[i].GetComponent<controlObject>().objectID != ID) return;
                    if (Cart[i].GetComponent<controlObject>().objectID != ID) i++;
                    if (Cart[i].GetComponent<controlObject>().objectID == ID)
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

                        break;
                    }
                }
            }
        }
        float xp = 0.2f;
        public void RemoveObject(Collider other)
        {
            if (xp > 0)
                xp -= Time.deltaTime;
            if (xp <= 0)
            {
                if (Cart.Count > 0)
                {
                    CurrentLimit += Cart[Cart.Count - 1].GetComponent<controlObject>().objectHeight;
                    Cart[Cart.Count - 1].transform.parent = null;
                    Cart[Cart.Count - 1].GetComponent<controlObject>().EndPosition = other.transform.position;
                    Cart[Cart.Count - 1].GetComponent<controlObject>().isMove = true;
                    Cart[Cart.Count - 1].GetComponent<controlObject>().isDestroy = true;
                    Cart.Remove(Cart[Cart.Count - 1]);
                    xp = 0.2f;
                }
            }
            if (Cart.Count <= 0)
            {
                GoToDustbin = false;
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Pickup") && GetComponent<wearhouse.Move.moveBot>().agent.velocity.magnitude < 0.1f)
            {

                AddObjectToCart(other);
            }
            if (other.gameObject.CompareTag("Dustbin") && GoToDustbin)
            {
                isDustbin = true;
                RemoveObject(other);
            }
        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Battery"))
            {
                CurrentLimit += 30;
                Destroy(other.gameObject);
                print("Collied");
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Battery"))
            {
                GetComponent<Move.moveBot>().currentCharge += GetComponent<Move.moveBot>().maxCharge;
                Destroy(other.gameObject);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (isDustbin)
                isDustbin = false;
        }
    }

}
