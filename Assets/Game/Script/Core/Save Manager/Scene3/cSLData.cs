using UnityEngine;

public class cSLData : MonoBehaviour
{
    public warehouse.Control.controlParkingDack controlParking1;
    public warehouse.Control.controlParkingDack controlParking2;
    public warehouse.Control.controlParkingDack controlParking3;

    public warehouse.Control.controlCharger controlCharger1;
    public warehouse.Control.controlCharger controlCharger2;
    public warehouse.Control.controlCharger controlCharger3;
    public warehouse.Control.controlCharger controlCharger4;

    public warehouse.Control.controlPickup controlPickupOrange;
    public warehouse.Control.controlPickup controlPickupViolet;
    public warehouse.Control.controlPickup controlPickupBattery;

    public warehouse.Core.coreRobotSpwanner robotSpwanner1;
    public warehouse.Core.coreRobotSpwanner robotSpwanner2;

    public cGameData data;

    private void Awake()
    {
        data = cSaveManager.Load();
        cLoadGame();
    }
    public void cSaveGame()
    {
        data.pLock1 = controlParking1.isLocked;
        data.pLock2 = controlParking2.isLocked;
        data.pLock3 = controlParking3.isLocked;

        data.cCharger1 = controlCharger1.isLocked;
        data.cCharger2 = controlCharger2.isLocked;
        data.cCharger3 = controlCharger3.isLocked;
        data.cCharger4 = controlCharger4.isLocked;

        data.RedMaxCapacity = controlPickupOrange.MaxCapacity;
        data.YellowMaxCapacity = controlPickupViolet.MaxCapacity;
        data.BatteryMaxCapacity = controlPickupBattery.MaxCapacity;

        data.RoboSpwannedInRobo1 = robotSpwanner1.HireCount;
        data.RoboSpwannedInRobo2 = robotSpwanner2.HireCount;
        cSaveManager.cSave(data);
    }
    public void cLoadGame()
    {
        controlParking1.isLocked = data.pLock1;
        controlParking2.isLocked = data.pLock2;
        controlParking3.isLocked = data.pLock3;

        controlCharger1.isLocked = data.cCharger1;
        controlCharger2.isLocked = data.cCharger2;
        controlCharger3.isLocked = data.cCharger3;
        controlCharger4.isLocked = data.cCharger4;

        controlPickupOrange.MaxCapacity = data.RedMaxCapacity;
        controlPickupViolet.MaxCapacity = data.YellowMaxCapacity;
        controlPickupBattery.MaxCapacity = data.BatteryMaxCapacity;

        robotSpwanner1.HireCount = data.RoboSpwannedInRobo1;
        robotSpwanner2.HireCount = data.RoboSpwannedInRobo2;

    }
}
