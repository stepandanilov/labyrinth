using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonHandler
{
    private static JsonHandler instance = null;
    private JsonHandler() { }
    public static JsonHandler getInstance()
    {
        if (instance == null)
        {
            instance = new JsonHandler();
        }
        return instance;
    }

    private string jsonPath = Application.dataPath + "/data.json";
    public DataCollection data;
    public void LoadField()
    {
        try
        {
            data = JsonUtility.FromJson<DataCollection>(File.ReadAllText(jsonPath));
        }
        catch
        {
            data = new DataCollection {
                type = 1,
                gammaHeight = 5,
                gammaWidth = 5,
                deltaLength = 6,
                thetaRadius = 5,
                thetaCellNumber = 16
            };
        }
        PlayerPrefs.SetInt("type", data.type);
        PlayerPrefs.SetInt("height", data.gammaHeight);
        PlayerPrefs.SetInt("width", data.gammaWidth);
        PlayerPrefs.SetInt("length", data.deltaLength);
        PlayerPrefs.SetInt("radius", data.thetaRadius);
        PlayerPrefs.SetInt("cellNumber", data.thetaCellNumber);
    }
    public void SaveField()
    {
        File.WriteAllText(jsonPath, JsonUtility.ToJson(data));
    }

    [System.Serializable]
    public class DataCollection
    {
        public int type;
        public int gammaWidth;
        public int gammaHeight;
        public int deltaLength;
        public int thetaRadius;
        public int thetaCellNumber;
    }
}
