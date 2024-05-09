using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelSelectionHandler : MonoBehaviour
{
    public static string buttonValue;

    public void ButtonClicked(Button button) {
        buttonValue = button.GetComponentInChildren<Text>().text;
        Debug.Log(buttonValue);
        OpenScene();

    }

    public void OpenScene() {
        SceneManager.LoadScene("Main Scene");
    }
}
