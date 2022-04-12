
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace warehouse.Control
{
    public class controlObject : MonoBehaviour
    {
        public int objectHeight = 1;
        public int objectID = 0;
        public LayerMask layerToCheck;
        public float checkingDistance;

        [Header("Movement")]
        public Vector3 EndPosition;
        public float movementSpeed;
        public bool isMove;
        public bool isObjectTouch;
        public bool isReched = false;
        public bool isDestroy;
        public bool playAudio;
        private Rigidbody rb;
        private Transform downChecker;
        private RaycastHit hit;
        private coreAudioManager audioManager;
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            downChecker = transform.GetChild(0);
            audioManager = FindObjectOfType<coreAudioManager>();
        }


        void Update()
        {
            checkForDownObjects();
            objectMove();
            if (isDestroy)
                Destroy(this.gameObject, 0.25f);
            if (playAudio)
            {
                audioManager.source.PlayOneShot(audioManager.Sell);
                playAudio = false;
            }
        }
        void objectMove()
        {
            if (isMove)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, EndPosition, movementSpeed * Time.deltaTime);
                rb.isKinematic = true;
                rb.drag = 10;
                rb.angularDrag = 10;
                GetComponent<Collider>().isTrigger = true;
            }
            if(transform.localPosition == EndPosition)
            {
                transform.localPosition = EndPosition;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                if (isMove)
                {                    
                    GetComponent<Collider>().isTrigger = false;
                    isMove = false;
                }               
            }
            if (isReched && !isMove && isObjectTouch)
            {
                EndPosition = new Vector3(0, transform.localPosition.y, 0);
            }
            if (isReched && !isMove)
            {
                transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
            }
        }

        IEnumerator EndP(float t)
        {
            yield return new WaitForSeconds(t);
            EndPosition = new Vector3(EndPosition.x, transform.localPosition.y, EndPosition.z);

        }
        void checkForDownObjects()
        {
            if (Physics.Raycast(downChecker.position, -downChecker.up, out hit, checkingDistance/*, layerToCheck*/))
            {
                if (hit.transform.CompareTag("og"))
                {
                    isObjectTouch = true;
                }
                if (hit.transform.tag != "og")
                {
                    isObjectTouch = true;
                }
            }
            else if(!isMove)
            {
                isObjectTouch = false;
                GetComponent<Collider>().isTrigger = false;
                rb.drag = 0;
                rb.angularDrag = 0.05f;
            }

            if (isObjectTouch)
            {
                rb.isKinematic = true;
            }
            if (!isObjectTouch)
            {
                rb.isKinematic = false;
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Dustbin"))
            {
                Destroy(gameObject);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Dustbin"))
            {
                Destroy(gameObject);
            }
        }
    }
}
