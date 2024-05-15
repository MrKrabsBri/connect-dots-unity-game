using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class LevelDataWrapper {
    public LevelData[] levels;
}

[System.Serializable]
public class LevelData {
    public string[] level_data;
}

public class ReadLevelDataJson : MonoBehaviour {
    const float xDIVIDER = 36.63f; // scaled down Point coordinates on X axis to fit background
    const float yDIVIDER = 65.34f; // scaled down Point coordinates on Y axis to fit background
    private string jsonFilePath;

    void Start() {
        ReadDataToLevelList();
    }

    public List<List<Dot>> ReadDataToLevelList() {
        jsonFilePath = Application.dataPath + "/Data/level_data.json";
        string jsonContent = File.ReadAllText(jsonFilePath);
        LevelDataWrapper levelDataWrapper = JsonUtility.FromJson<LevelDataWrapper>(jsonContent);
        int levelNumber = 1;
        int pointID = 0;
        List<List<Dot>> listOfAllLevels = new List<List<Dot>>();

        foreach (LevelData level in levelDataWrapper.levels) {
            List<Dot> pointsOfOneLevel = new List<Dot>();

            for (int i = 0; i < level.level_data.Length; i += 2) {
                string xCoordinate = level.level_data[i];
                string yCoordinate = level.level_data[i + 1];

                float xValue = float.Parse(xCoordinate) / xDIVIDER;
                float yValue = -float.Parse(yCoordinate) / yDIVIDER;

                Dot newDot = new Dot(xValue, yValue);
                pointsOfOneLevel.Add(newDot);
                pointID++;
            }

            listOfAllLevels.Add(pointsOfOneLevel);
            levelNumber++;
            pointID = 0;
        }
        return listOfAllLevels;
    }
}

