using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace warehouse.Control
{
    public class controlNPCTruckPopup : MonoBehaviour
    {
        public Canvas canvas;
        public Transform Box;
        public Transform Tape;
        public Slider Progress;
        public float speed;
        public float max;
        public float current;

        public bool isTradeOver;
        public bool isCompleteTask;
        void Start()
        {
            canvas.worldCamera = Camera.main;
            canvas.planeDistance = 7;
        }

        // Update is called once per frame
        void Update()
        {
            if(Progress.value >= Progress.maxValue && !isCompleteTask)
            {
                StartCoroutine(upgradeUIDelay(0.15f));
                isCompleteTask = true;
                //Destroy(controlNPCTruckPopup)
            }
            progress();
        }

        IEnumerator upgradeUIDelay(float t)
        {
            yield return new WaitForSeconds(t);
            GetComponent<controlNPCTruck>().CloseMiniGame();
        }

        float x;
        public void progress()
        {
            current = x / 360;
            Box.localRotation = Quaternion.Euler(Box.localEulerAngles.x, x, Box.localEulerAngles.z);
            if (isTradeOver && Input.GetMouseButton(0))
            {
                Tape.gameObject.SetActive(true);
                if (x < 360)
                {
                    x += speed;
                }
                if (x >= 360)
                {
                    x = 360;
                }
                UIUpdate();
            }
        }
        public void UIUpdate()
        {
            Progress.maxValue = max;
            Progress.value = current;
            Tape.localScale = new Vector3(Tape.localScale.x, (x / 360), Tape.localScale.z);
        }
    }
}

