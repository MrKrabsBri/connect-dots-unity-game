/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.Mathematics;
using UnityEngine.Rendering.Universal.Internal;
using System.Drawing;
using Unity.VisualScripting;
using JetBrains.Annotations;
using UnityEngine.UIElements;

[System.Serializable]
public class PointDot {
    *//*  public float? x;
        public float? y;*//*
    public List<float?> coordinates = new List<float?>();
}

[System.Serializable]
public class Level {
    public List<PointDot> points = new List<PointDot>();
}

[System.Serializable]
public class ListOfAllLevels {
    public List<Level> levels = new List<Level>();
}

*//*[System.Serializable]
public class LevelData {
    public string[] level_data;
}
*//*





public class ReadLevelDataJson : MonoBehaviour {

    private string jsonFilePath;
    const float xDIVIDER = 36.63f;
    const float yDIVIDER = 65.34f;
    float? adjustedX;
    float? adjustedY;

    private void Start() {

        List<Level> levelList = LoadLevelData();

    }


    List<Level> LoadLevelData() {

        List<PointDot> listOfPointsOfLevel = new List<PointDot>();
        List<Level> listOfLevelsWithPoints = new List<Level>();
        // PointDot adjustedPoint = new PointDot();

        *//* List<float> point = new List<float>();
         List< List<float> > pointsListOfLevel = new List< List<float> >();
         List<List<List<float>>> allLevelsList = new List<List<List<float>>>();*//*

        jsonFilePath = Application.dataPath + "/Data/level_data.json"; // Path to your JSON file
        string jsonContent = File.ReadAllText(jsonFilePath);
        ListOfAllLevels listOfAllLevels = JsonUtility.FromJson<ListOfAllLevels>(jsonContent);

        foreach (Level level in listOfAllLevels.levels) {
            Debug.Log(level);

            //SpawnObjectsForLevel(level);
            // Check if the level contains coordinates data

            foreach (PointDot point in level.points) {
                //List<float?> adjustedPoint = new List<float?>();
                PointDot adjustedPoint = new PointDot();

                Debug.Log("A");
                if (adjustedPoint.coordinates[0] == null) {
                    adjustedX = point.coordinates[0] / xDIVIDER;
                    adjustedPoint.coordinates[0] = adjustedX;
                }
                else if (adjustedPoint.coordinates[1] == null) {
                    adjustedY = -point.coordinates[1] / yDIVIDER;
                    adjustedPoint.coordinates[1] = adjustedY;
                    listOfPointsOfLevel.Add(adjustedPoint);
                    adjustedPoint.coordinates[0] = null;
                    adjustedPoint.coordinates[1] = null;
                    Debug.Log(listOfPointsOfLevel[0]);

                    //instantiate a prefab
                    //Instantiate(prefab, new Vector2(point[0], point[1]), Quaternion.identity);

                }

                *//*            foreach (string data in level.level_data) {
                                // You might need to implement a more robust check based on your data format
                                //Debug.Log("Data: " + data);

                                if (point.Count == 0) {
                                    adjustedX = int.Parse(data) / xDIVIDER;
                                    point.Add(adjustedX);
                                }
                                else if (point.Count == 1) {
                                    //point[1] = -point[1];
                                    adjustedY = -int.Parse(data) / yDIVIDER;
                                    point.Add(adjustedY);
                                    //instantiate a prefab
                                    //Instantiate(prefab, new Vector2(point[0], point[1]), Quaternion.identity);
                                    pointsListOfLevel.Add(point);
                                    allLevelsList.Add(pointsListOfLevel);
                                }
                            }*//*

            }



        }
        return new List<Level>();
    }

    void SpawnObjectsForLevel(Level level) {

        foreach (PointDot point in level.points) {
            Debug.Log("Read Coordinate from Json: " + point);
        }

    }




    *//*List<float> point = new List<float>();

    foreach (LevelData level in levelDataWrapper.levels) {
        foreach (string data in level.level_data) {

            //Debug.Log("Data: " + data);

            if (point.Count == 0) {
                adjustedX = int.Parse(data) / xDIVIDER;
                point.Add(adjustedX);
            }
            else if (point.Count == 1) {
                //point[1] = -point[1];
                adjustedY = -int.Parse(data) / yDIVIDER;
                point.Add(adjustedY);
                //instantiate a prefab
                Instantiate(prefab, new Vector2(point[0], point[1]) , Quaternion.identity);
                point.Clear();
            }

        }
    }*//*
}
*/