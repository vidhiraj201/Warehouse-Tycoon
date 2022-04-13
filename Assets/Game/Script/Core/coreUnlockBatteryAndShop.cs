using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coreUnlockBatteryAndShop : MonoBehaviour
{
    public string Camera;
    [Space(15)]
    public int Level;
    public float delay = 1;
    public Animator CC;

    public GameObject UnlockableObject;
    public GameObject LockableObject;

    public warehouse.Control.controlPickup controlPickup;   
    public warehouse.Core.coreRobotSpwanner RobotSpwanner_1;
    public warehouse.Core.coreRobotSpwanner RobotSpwanner_2;
    private coreAudioManager audioManager;


    private warehouse.Core.GameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = FindObjectOfType<warehouse.Core.GameManager>();
        audioManager = FindObjectOfType<coreAudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.currentLevel > Level && !UnlockableObject.activeSelf && controlPickup.isLocked)
        {
            UnlockableObject.SetActive(true);
            LockableObject.SetActive(false);
            RobotSpwanner_1.isLocked = false;
            RobotSpwanner_2.isLocked = false;
            controlPickup.isLocked = false;
        }
        if (GameManager.currentLevel == Level && !UnlockableObject.activeSelf && controlPickup.isLocked)
        {
            StartCoroutine(UnlockShop(delay + 1.5f));
            StartCoroutine(UnlockBattery(delay));
        }
        if (GameManager.currentLevel >= Level && UnlockableObject.activeSelf)
            controlPickup.isLocked = false;
    }
    IEnumerator UnlockShop(float t)
    {
        CC.Play(Camera);
        yield return new WaitForSeconds(t);
        audioManager.source.PlayOneShot(audioManager.Unlock);
        RobotSpwanner_1.isLocked = false;
        RobotSpwanner_2.isLocked = false;
       
    }
    IEnumerator UnlockBattery(float t)
    {
        yield return new WaitForSeconds(t);
        UnlockableObject.SetActive(true);
        LockableObject.SetActive(false);
        if(controlPickup.isLocked)
            audioManager.source.PlayOneShot(audioManager.Unlock);
        controlPickup.isLocked = false;
    }
}
