using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class coreRobotSpwanner : MonoBehaviour
{
    public GameObject HireUI;
    public GameObject Robot;
    public Transform Inventory;
    public TextMeshProUGUI Amount;

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
        Amount.text = gameManager.Bots.ToString("N0");
    }
    public void Hire()
    {
        if(gameManager.maxMoney >= gameManager.Bots)
        {
            Instantiate(Robot, Inventory.position, Quaternion.identity, Inventory);
            gameManager.maxMoney -= gameManager.Bots;
            gameManager.Bots += 300;
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
