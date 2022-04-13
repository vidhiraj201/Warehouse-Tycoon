using UnityEngine;

public class bSLData : MonoBehaviour
{
    public warehouse.Control.controlParkingDack controlParking1;
    public warehouse.Control.controlParkingDack controlParking2;
    public warehouse.Control.controlParkingDack controlParking3;

    public warehouse.Control.controlCharger controlCharger1;
    public warehouse.Control.controlCharger controlCharger2;
    public warehouse.Control.controlCharger controlCharger3;
    public warehouse.Control.controlCharger controlCharger4;

    public warehouse.Control.controlPickup controlPickupGreen;
    public warehouse.Control.controlPickup controlPickupBlue;
    public warehouse.Control.controlPickup controlPickupBattery;

    public warehouse.Core.coreRobotSpwanner robotSpwanner1;
    public warehouse.Core.coreRobotSpwanner robotSpwanner2;

    public bGameData data;

    private void Awake()
    {
        data = bSaveManager.Load();
         bLoadGame();
    }
    public void bSaveGame()
    {
        data.pLock1 = controlParking1.isLocked;
        data.pLock2 = controlParking2.isLocked;
        data.pLock3 = controlParking3.isLocked;

        data.cCharger1 = controlCharger1.isLocked;
        data.cCharger2 = controlCharger2.isLocked;
        data.cCharger3 = controlCharger3.isLocked;
        data.cCharger4 = controlCharger4.isLocked;

        data.RedMaxCapacity = controlPickupGreen.MaxCapacity;
        data.YellowMaxCapacity = controlPickupBlue.MaxCapacity;
        data.BatteryMaxCapacity = controlPickupBattery.MaxCapacity;

        data.RoboSpwannedInRobo1 = robotSpwanner1.HireCount;
        data.RoboSpwannedInRobo2 = robotSpwanner2.HireCount;
        bSaveManager.bSave(data);
    }
    public void bLoadGame()
    {
        controlParking1.isLocked = data.pLock1;
        controlParking2.isLocked = data.pLock2;
        controlParking3.isLocked = data.pLock3;

        controlCharger1.isLocked = data.cCharger1;
        controlCharger2.isLocked = data.cCharger2;
        controlCharger3.isLocked = data.cCharger3;
        controlCharger4.isLocked = data.cCharger4;

        controlPickupGreen.MaxCapacity = data.RedMaxCapacity;
        controlPickupBlue.MaxCapacity = data.YellowMaxCapacity;
        controlPickupBattery.MaxCapacity = data.BatteryMaxCapacity;

        robotSpwanner1.HireCount = data.RoboSpwannedInRobo1;
        robotSpwanner2.HireCount = data.RoboSpwannedInRobo2;

    }
}
