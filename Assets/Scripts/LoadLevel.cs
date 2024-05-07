using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

//[DefaultExecutionOrder(2)]
public class LoadLevel :MonoBehaviour {
    public GameObject dotPrefab;
   // public int levelToLoad;
    private ReadLevelDataJson levelDataJson;

     void Start() {

        try {
            if (LevelSelector.buttonValue != null) {
                 string buttonValue = LevelSelector.buttonValue;
                 int level = int.Parse(buttonValue);
                 SpawnLevelDots(level);
                 }
            else {
                 Debug.LogWarning("Button value is null.");
                 }
        } catch (FormatException e) {
            Debug.LogError("Button value is not a valid number: " + e.Message);
        }







        //levelDataJson = GetComponent<ReadLevelDataJson>(); // using this instead of new() keyword
        // SpawnLevelDots(levelToLoad);
    }

    public void SpawnLevelDots(int level) {

        levelDataJson = GetComponent<ReadLevelDataJson>(); // using this instead of new() keyword
        List<List<Dot>> listOfAllLevels = levelDataJson.ReadDataToLevelList();
        List<Dot> listOfLevelDots = listOfAllLevels[level - 1];
        if (level >= 1 && level <= listOfAllLevels.Count) {
            int numberOfDot = 1;
            Debug.Log("This level exists and we are loading it now");
            List<Dot> dotsForThisLevel = listOfAllLevels[level - 1];
            Debug.Log("size of the dots list " + dotsForThisLevel.Count); // shows list not for this level, but all levels dots.
            foreach (Dot dot in listOfLevelDots) {
                //Debug.Log(dot.ToString());
                GameObject newDot = Instantiate(dotPrefab,
                    new Vector3(dot.x.Value, dot.y.Value, 0f), Quaternion.identity);

                Text textField = newDot.GetComponentInChildren<Text>();
                if (textField != null) {
                    textField.text = numberOfDot.ToString();
                    numberOfDot++;
                }
                else {
                    Debug.LogWarning("Text field not found in instantiated object's hierarchy.");
                }

            }
        }
        else {
            Debug.LogWarning("Level " + level + " does not exist.");
        }
    }

}




/*[System.Serializable]
public class LevelData {
    public List<string> level_data;
}*//*

public class LoadLevel : MonoBehaviour
{
    public GameObject pointPrefab;
    public ListOfAllLevels levelDataWrapper;

    // Start is called before the first frame update
    void Start() {
        SpawnLevelPoints(0);
    }


    public void SpawnLevelPoints(int levelToLoad) {

        Debug.Log(levelDataWrapper.levels.Length);

        if (levelToLoad < 0 || levelToLoad >= levelDataWrapper.levels.Length) {

            Debug.LogError("Invalid level index: " + levelToLoad);
            return;
        }

        LevelData level = levelDataWrapper.levels[levelToLoad];

        foreach (string coordinate in level.level_data) {
            Debug.Log("Spawned at " + coordinate);
        }


*//*        LevelDataWrapper listOfAllLevels = rldj.ReadDataFromJson();
        LevelData dataOfALevel = listOfAllLevels.levels[level];

        foreach(LevelData currentLevel in listOfAllLevels.levels) {
            foreach (string coordinate in currentLevel.level_data) {
                Debug.Log(currentLevel.level_data[0]);
            }

        }

        Debug.Log(dataOfALevel.level_data);*//*
    }


}
*/