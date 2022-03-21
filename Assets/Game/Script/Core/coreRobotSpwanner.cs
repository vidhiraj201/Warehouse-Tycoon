using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coreRobotSpwanner : MonoBehaviour
{
    public GameObject HireUI;
    public GameObject Robot;
    public int Amount;
    public Transform Inventory;

    private warehouse.Core.GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        HireUI.SetActive(false);
        gameManager = FindObjectOfType<warehouse.Core.GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hire()
    {
        if(gameManager.maxMoney >= Amount)
        {
            Instantiate(Robot, Inventory.position, Quaternion.identity, Inventory);
            gameManager.maxMoney -= Amount;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HireUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(HireUI.activeSelf)
            HireUI.SetActive(false);
    }
}
