using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadLevel :MonoBehaviour {
    public GameObject dotPrefab;
    public int levelToLoadForTest;
    //ReadLevelDataJson rldj = ;
    //public List<List<Dot>> listOfAllLevels = rldj.ReadDataToLevelList();


    private void Start() {
        /*SpawnLevelDots(levelToLoadForTest, ReadLevelDataJson.ReadDataToLevelList());*/
    }

    public void SpawnLevelDots(int level, List<List<Dot>> listOfAllLevels) {
        if (level >= 1 && level <= listOfAllLevels.Count) {
            Debug.Log("This level exists and we are loading it now");
            List<Dot> dotsForThisLevel = listOfAllLevels[level - 1];
            foreach (Dot dot in dotsForThisLevel) {
                GameObject newDot = Instantiate(dotPrefab,
                    new Vector3(dot.x.Value, dot.y.Value, 0f), Quaternion.identity);
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