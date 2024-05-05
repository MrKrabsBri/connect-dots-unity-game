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

    public class ReadLevelDataJson : MonoBehaviour {
        const float xDIVIDER = 36.63f;
        const float yDIVIDER = 65.34f;
        private  string jsonFilePath; // Path to your JSON file

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
            List<Dot> pointsOfOneLevel = new List<Dot>();
            List<List<Dot>> listOfAllLevels = new List<List<Dot>>();

            foreach (LevelData level in levelDataWrapper.levels) {
                Debug.Log("Hi, welcome to level : " + levelNumber);
                //List<LevelData> pointsOfOneLevel = new List<LevelData>();


                foreach (string data in level.level_data) {
                    // Assuming data is a string representation of coordinates or values
                    // You might need to convert these strings to numbers or parse them further based on your game logic
                    Debug.Log("Data: " + data);
                    levelPoint = new Dot(null, null);

                    if (levelPoint.x == null) {
                        pointCoordinate = float.Parse(data) / xDIVIDER;
                        Debug.Log(pointCoordinate);
                        levelPoint.x = pointCoordinate;
                    }
                    else if (levelPoint.y == null) {
                        pointCoordinate = float.Parse(data) / yDIVIDER;
                        levelPoint.y = pointCoordinate;
                        Debug.Log("pointCoordinate: ( " + levelPoint.x + " , " + levelPoint.y + " )");
                        pointsOfOneLevel.Add(levelPoint);
                        //levelPoint.Clear();
                    }

                    // Instantiate or process your objects based on the data
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


