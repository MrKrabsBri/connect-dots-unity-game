using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {

    public GameObject pointPrefab;
    private ReadLevelDataJson levelDataJson;
    public static List<GameObject> listOfInstantiatedPoints;

    public List<Point> GetListOfPointsForThisLevel(int level) {

        List<Point> listOfThisLevelPoints;

        levelDataJson = GetComponent<ReadLevelDataJson>();
        List<List<Point>> listOfAllLevels = levelDataJson.ReadDataToLevelList();
        listOfThisLevelPoints = listOfAllLevels[level - 1];

        return listOfThisLevelPoints;
    }

    public void SpawnLevelPoints(List<Point> pointsOfThisLevel, int level) {

        if (level >= 1 && level <= pointsOfThisLevel.Count) {
            int numberOfPoint = 1;
            listOfInstantiatedPoints = new List<GameObject>();
            foreach (Point point in pointsOfThisLevel) {
                GameObject newPoint = Instantiate(pointPrefab,
                    new Vector3(point.x.Value, point.y.Value, 0f), Quaternion.identity);

                listOfInstantiatedPoints.Add(newPoint);

                Text textField = newPoint.GetComponentInChildren<Text>();
                if (textField != null) {
                    textField.text = numberOfPoint.ToString();
                    numberOfPoint++;
                }
                else {
                    Debug.LogWarning("Text field not found in instantiated object.");
                }
            }
        }
        else {
            Debug.LogWarning("Level " + level + " does not exist in .json file.");
        }
    }
}
