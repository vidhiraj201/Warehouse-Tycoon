[System.Serializable]
public class aGameData 
{
    public bool pLock1;
    public bool pLock2;
    public bool pLock3;

    public bool cCharger1;
    public bool cCharger2;
    public bool cCharger3;
    public bool cCharger4;

    public int RedMaxCapacity;
    public int YellowMaxCapacity;
    public int BatteryMaxCapacity;

    public int RoboSpwannedInRobo1;
    public int RoboSpwannedInRobo2;

    public aGameData()
    {
        pLock1 = false;
        pLock2 = true;
        pLock3 = true;

        cCharger1 = true;
        cCharger2 = true;
        cCharger3 = true;
        cCharger4 = true;

        RedMaxCapacity = 147;
        YellowMaxCapacity = 147;
        BatteryMaxCapacity = 5;

        RoboSpwannedInRobo1 = 0;
        RoboSpwannedInRobo2 = 0;

    }


}
