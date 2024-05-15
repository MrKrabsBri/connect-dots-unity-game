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
/*    const float xDIVIDER = 1f;
    const float yDIVIDER = 1f;*/

    private string jsonFilePath;

    void Start() {
        ReadDataToLevelList();
    }

    public List<List<Point>> ReadDataToLevelList() {
        //jsonFilePath = Application.dataPath + "/Data/level_data.json";
        jsonFilePath = Path.Combine(Application.streamingAssetsPath, "Data", "level_data.json");
        string jsonContent = File.ReadAllText(jsonFilePath);
        LevelDataWrapper levelDataWrapper = JsonUtility.FromJson<LevelDataWrapper>(jsonContent);
        int levelNumber = 1;
        int pointID = 0;
        List<List<Point>> listOfAllLevels = new List<List<Point>>();

        foreach (LevelData level in levelDataWrapper.levels) {
            List<Point> pointsOfOneLevel = new List<Point>();

            for (int i = 0; i < level.level_data.Length; i += 2) {
                string xCoordinate = level.level_data[i];
                string yCoordinate = level.level_data[i + 1];

                float xValue = float.Parse(xCoordinate) / xDIVIDER;
                float yValue = -float.Parse(yCoordinate) / yDIVIDER;

                Point newPoint = new Point(xValue, yValue);
                pointsOfOneLevel.Add(newPoint);
                pointID++;
            }

            listOfAllLevels.Add(pointsOfOneLevel);
            levelNumber++;
            pointID = 0;
        }
        return listOfAllLevels;
    }
}

