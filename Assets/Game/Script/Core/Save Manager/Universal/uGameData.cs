
[System.Serializable]
public class uGameData 
{
    public int level;
    public int cash;
    public int truckServed;

    public int parkingLot = 500;
    public int Bots = 1500;
    public int chargingStation = 3000;
    public int refillingCost = 500;

    public bool isSection1 = false;
    public bool isSection2 = false;

    public uGameData()
    {
        level = 0;
        cash = 0;
        truckServed = 0;
        parkingLot = 500;
        Bots = 1500;
        chargingStation = 3000;
        refillingCost = 500;
        isSection1 = false;
        isSection2 = false;
}
}
