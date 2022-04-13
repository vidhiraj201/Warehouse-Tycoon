using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockYellowGameObject : MonoBehaviour
{
    public int Level;
    public float delay = 1;
    public string Camera;
    public Animator CC;

    private warehouse.Core.GameManager GameManager;

    public GameObject UnlockableObject;

    public warehouse.Control.controlPickup controlPickup;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = FindObjectOfType<warehouse.Core.GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.currentLevel > Level && controlPickup.isLocked)
        {
            UnlockableObject.SetActive(true);
            controlPick();
        }
        if (GameManager.currentLevel == Level && controlPickup.isLocked)
        {
            StartCoroutine(unlock(delay));
        }
        if (GameManager.currentLevel >= Level && !controlPickup.isLocked)
            controlPick();
    }
    void controlPick()
    {
        if (controlPickup != null)
            controlPickup.isLocked = false;
    }

    IEnumerator unlock(float t)
    {
        CC.Play(Camera);
        yield return new WaitForSeconds(t);
        UnlockableObject.SetActive(true);
        controlPick();
    }
}
