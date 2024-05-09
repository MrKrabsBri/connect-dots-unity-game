using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFieldHandler : MonoBehaviour
{

    private string input;

    public void ReadStringInput(string s) {

        input = s;
        Debug.Log(input);
    }

/*    public void OnInputFieldValueChanged(string value) {

        if (int.TryParse(value, out int intValue)) {

            Debug.Log("Input field value: " + intValue);
            LoadLevel loadLevel = FindObjectOfType<LoadLevel>();
            if (loadLevel != null) {
                loadLevel.SpawnLevelDots(intValue);
            }
            else {
                Debug.LogWarning("LoadLevel script not found in scene.");
            }
        }
        else {
            // Value is not a valid integer
            Debug.LogWarning("Invalid input field value: " + value);
        }
    }*/
}
