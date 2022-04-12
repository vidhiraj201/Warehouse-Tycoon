using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    float x;
    public float xCounter = 1;
    public uSLData uSLData;
    public aSLData aSLData;
    public bSLData bSLData;
    public cSLData cSLData;
    private void Update()
    {
        if (x > 0)
            x -= Time.deltaTime;
        if (x <= 0)
        {
            try
            {
                uSLData.uSaveGame();
                aSLData.aSaveGame();
                bSLData.bSaveGame();
                cSLData.cSaveGame();
                print("[DATA HAS BEEN SAVED]");
                x = xCounter;
            }
            catch
            {

            }
        }
    }
    public void Save()
    {
        try
        {
            uSLData.uSaveGame();
            aSLData.aSaveGame();
            bSLData.bSaveGame();
            cSLData.cSaveGame();
            print("[DATA HAS BEEN SAVED]");            
        }
        catch
        {

        }
    }
}
