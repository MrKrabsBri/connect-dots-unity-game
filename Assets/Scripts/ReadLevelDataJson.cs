using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.Mathematics;
using UnityEngine.Rendering.Universal.Internal;
using System.Drawing;
using Unity.VisualScripting;
using JetBrains.Annotations;
using UnityEngine.UIElements;
using System;

[System.Serializable]
public class LevelDataWrapper {
    public LevelData[] levels;
}

[System.Serializable]
public class LevelData {
    public string[] level_data;
}

[DefaultExecutionOrder(1)]
public class ReadLevelDataJson : MonoBehaviour {
    const float xDIVIDER = 36.63f;
    const float yDIVIDER = 65.34f;
    private string jsonFilePath; // Path to your JSON file

    void Start() {
        ReadDataToLevelList();
    }

    public List<List<Dot>> ReadDataToLevelList() {
        jsonFilePath = Application.dataPath + "/Data/level_data.json";
        string jsonContent = File.ReadAllText(jsonFilePath);
        LevelDataWrapper levelDataWrapper = JsonUtility.FromJson<LevelDataWrapper>(jsonContent);
        int levelNumber = 1;
        float pointCoordinate;
        Dot levelPoint;
        /*List<Dot> pointsOfOneLevel = new List<Dot>();*/
        List<List<Dot>> listOfAllLevels = new List<List<Dot>>();


        foreach (LevelData level in levelDataWrapper.levels) {
            Debug.Log("Hi, welcome to level : " + levelNumber);
            List<Dot> pointsOfOneLevel = new List<Dot>();

            int tempCounter = 0; // counter for testing

            for (int i = 0; i < level.level_data.Length; i += 2) {
                string xCoordinate = level.level_data[i];
                string yCoordinate = level.level_data[i + 1];

                float xValue = float.Parse(xCoordinate) / xDIVIDER;
                float yValue = -float.Parse(yCoordinate) / yDIVIDER; // Adjusting the y-coordinate as per your previous code

                // Create a new Dot object for each pair of coordinates
                Dot newDot = new Dot(xValue, yValue);
                pointsOfOneLevel.Add(newDot);
                Debug.Log(pointsOfOneLevel[tempCounter].x.Value + " | "+ pointsOfOneLevel[tempCounter].y.Value);
                tempCounter++;
            }


            listOfAllLevels.Add(pointsOfOneLevel);
            levelNumber++;
        }
        return listOfAllLevels;
    }
}



/*
    public class ReadLevelDataJson : MonoBehaviour {
    const float xDIVIDER = 36.63f;
    const float yDIVIDER = 65.34f;
    private string jsonFilePath; // Path to your JSON file

    void Start() {
        ReadDataToLevelList();
    }
     public void ReadDataToLevelList() {
        jsonFilePath = Application.dataPath + "/Data/level_data.json";
        string jsonContent = File.ReadAllText(jsonFilePath);
        LevelDataWrapper levelDataWrapper = JsonUtility.FromJson<LevelDataWrapper>(jsonContent);
        int levelNumber = 1;
        List<Dot> pointsOfOneLevel = new List<Dot>();
        List<List<Dot>> listOfAllLevels = new List<List<Dot>>();

        foreach (LevelData level in levelDataWrapper.levels) {
            Debug.Log("Hi, welcome to level : " + levelNumber);
            pointsOfOneLevel.Clear();

            foreach (string data in level.level_data) {
                float pointCoordinate;
                if (pointsOfOneLevel.Count < 2) {
                    pointCoordinate = float.Parse(data);
                    if (pointsOfOneLevel.Count == 0)
                        pointsOfOneLevel.Add(new Dot(pointCoordinate / xDIVIDER, null));
                    else
                        pointsOfOneLevel.Add(new Dot(null, pointCoordinate / yDIVIDER));
                    Debug.Log(pointsOfOneLevel);
                }
            }

            listOfAllLevels.Add(new List<Dot>(pointsOfOneLevel)); // Copy points to the list of all levels
            levelNumber++;
        }
    }
}*/


