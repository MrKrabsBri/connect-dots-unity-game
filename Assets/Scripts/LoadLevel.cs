using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {

    public GameObject dotPrefab;
    private ReadLevelDataJson levelDataJson;
    public static List<GameObject> listOfInstantiatedDots;
    //List<Dot> listOfLevelDots;

    public List<Dot> GetListOfDotsForThisLevel(int level) {

        List<Dot> listOfThisLevelDots;

        levelDataJson = GetComponent<ReadLevelDataJson>();
        List<List<Dot>> listOfAllLevels = levelDataJson.ReadDataToLevelList();
        listOfThisLevelDots = listOfAllLevels[level - 1];

        return listOfThisLevelDots;
    }

    public void SpawnLevelDots(List<Dot> dotsOfThisLevel, int level) {

        if (level >= 1 && level <= dotsOfThisLevel.Count) {
            int numberOfDot = 1;
            listOfInstantiatedDots = new List<GameObject>();
            foreach (Dot dot in dotsOfThisLevel) {
                GameObject newDot = Instantiate(dotPrefab,
                    new Vector3(dot.x.Value, dot.y.Value, 0f), Quaternion.identity);

                listOfInstantiatedDots.Add(newDot);

                Text textField = newDot.GetComponentInChildren<Text>();
                if (textField != null) {
                    textField.text = numberOfDot.ToString();
                    numberOfDot++;
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
