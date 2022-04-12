using UnityEngine;

public class uSLData : MonoBehaviour
{
    public  warehouse.Core.GameManager GameManager;
    public warehouse.Core.coreSectionUnlocker sectionUnlocker1;
    public warehouse.Core.coreSectionUnlocker sectionUnlocker2;
    public uGameData data;
    private void Awake()
    {
        data = uSaveManager.Load();
        uLoadGame();
    }
    public void uSaveGame()
    {
        data.level = GameManager.currentLevel;
        data.cash = (int)GameManager.maxMoney;
        data.truckServed = (int)GameManager.currentTruckLoaded;
        data.parkingLot = GameManager.ParkingLot;
        data.Bots = GameManager.Bots;
        data.refillingCost = GameManager.RefillingCost;
        data.chargingStation = GameManager.ChargingStation;
        data.isSection1 = sectionUnlocker1.isUnlocked;
        data.isSection2 = sectionUnlocker2.isUnlocked;
        uSaveManager.uSave(data);
    }
    public void uLoadGame()
    {
        GameManager.currentLevel = data.level;
        GameManager.maxMoney = data.cash;
        GameManager.currentTruckLoaded = data.truckServed;
        GameManager.ParkingLot = data.parkingLot;
        GameManager.Bots = data.Bots;
        GameManager.RefillingCost = data.refillingCost;
        GameManager.ChargingStation = data.chargingStation;
        sectionUnlocker1.isUnlocked = data.isSection1;
        sectionUnlocker2.isUnlocked = data.isSection2;

    }
}
