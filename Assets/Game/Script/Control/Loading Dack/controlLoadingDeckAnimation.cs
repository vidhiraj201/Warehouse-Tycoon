using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

namespace warehouse.Control
{
    public class controlLoadingDeckAnimation : MonoBehaviour
    {
        

        private controlLoadingDack controlLoadingDack;
        public CinemachineVirtualCamera cinemachineVirtual;
        
        public Slider Progress;
        public GameObject ProgressText;
        public Animator CameraB;
        public Animator loadItem;
        public GameObject Joystick;

        public Transform CameraFocusTarget;
        public Transform RotatingObjectStack;
        public Transform Tape;
        //public Animator anime;
        public float speed;
        public float maxVal;
        public float curVal;

        public bool isProgressComplete;
        public bool isCameraActivated;
        void Start()
        {
            /*   if (controlLoadingDack.controlParkingDack.isUp)
                   cinemachineVirtual = GameObject.Find("FocusCameraUp").GetComponent<CinemachineVirtualCamera>();

               if (!controlLoadingDack.controlParkingDack.isUp)
                   cinemachineVirtual = GameObject.Find("FocusCameraDown").GetComponent<CinemachineVirtualCamera>();
   */
            CameraB = GameObject.Find("Camera Blend").GetComponent<Animator>();
            Tape.gameObject.SetActive(false);
            Progress.gameObject.SetActive(false);
            ProgressText.gameObject.SetActive(false);
            controlLoadingDack = GetComponent<controlLoadingDack>();
        }


        void Update()
        {

            Progress.transform.forward = Camera.main.transform.forward;

            if (controlLoadingDack.isPlayerNear && !isCameraActivated && controlLoadingDack.TradeCompleted )
            {                        
                cinemachineVirtual.m_Follow = CameraFocusTarget;
                Joystick = GameObject.Find("Floating Joystick");
                Joystick.GetComponent<Image>().raycastTarget = false;
                StartCoroutine(StartMiniGame(0.5f));
                StartCoroutine(startProcess(1.5f));
                isCameraActivated = true;
            }

            if (!controlLoadingDack.TradeCompleted)
            {
                if (Joystick != null)
                {
                    if (controlLoadingDack.controlParkingDack.isUp)
                        CameraB.SetTrigger("up");

                    if (!controlLoadingDack.controlParkingDack.isUp)
                        CameraB.SetTrigger("down");
                    cinemachineVirtual.m_Follow = null;
                    Joystick.GetComponent<Image>().raycastTarget = true;
                    Joystick = null;
                }
                
            }       
            if(StartPtocessing)
                progress();
            if (curVal >= maxVal && !isProgressComplete)
            {
                loadItem.Play("Invetory");
                StartCoroutine(upgradeUIDelay(2f));
                isProgressComplete = true;
            }
           
        }

        public bool StartPtocessing;
        IEnumerator startProcess(float t)
        {
            yield return new WaitForSeconds(t);
            StartPtocessing = true;
        }

        IEnumerator StartMiniGame(float t)
        {
            yield return new WaitForSeconds(t);
            if (controlLoadingDack.controlParkingDack.isUp)
                CameraB.Play("Up");
            if (!controlLoadingDack.controlParkingDack.isUp)
                CameraB.Play("Down");

            yield return new WaitForSeconds(1.5f);
            controlLoadingDack.TargetTruck.GetComponent<controlNPCTruck>().Upgrade.SetActive(true);
            Progress.gameObject.SetActive(true);
            ProgressText.gameObject.SetActive(true);
        }
        IEnumerator upgradeUIDelay(float t)
        {
            yield return new WaitForSeconds(t);
            controlLoadingDack.TargetTruck.GetComponent<controlNPCTruck>().CloseMiniGame();
            
        }
        float x;
        public void progress()
        {
            
            RotatingObjectStack.localRotation = Quaternion.Euler(RotatingObjectStack.localEulerAngles.x, x, RotatingObjectStack.localEulerAngles .z);
            curVal = x / 360;
            if (controlLoadingDack.isPlayerNear && controlLoadingDack.TradeCompleted && Input.GetMouseButton(0))
            {
                controlLoadingDack.TargetTruck.GetComponent<controlNPCTruck>().HandUI.SetActive(false);                
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
            Progress.maxValue = maxVal;
            Progress.value = curVal;
            Tape.localScale = new Vector3(Tape.localScale.x, (x * 0.009f), Tape.localScale.z);
        }

        public void ResetData()
        {
            Tape.localScale = new Vector3(Tape.localScale.x, 0, Tape.localScale.z);
            RotatingObjectStack.localRotation = Quaternion.identity;
            curVal = 0;
            x = 0;
            Progress.value = 0;
            isCameraActivated = false;
            StartPtocessing = false;
            isProgressComplete = false;
            Joystick.SetActive(true);
            Tape.gameObject.SetActive(false);
            Progress.gameObject.SetActive(false);
            ProgressText.gameObject.SetActive(false);

        }

    }


}
