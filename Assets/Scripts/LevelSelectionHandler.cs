using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionHandler : MonoBehaviour
{
    public static string buttonValue;

    [SerializeField] InputField inputField;

    public void CustomLevelButtonClicked() {
        buttonValue = inputField.text;
        Debug.Log(buttonValue);
        OpenGameScene();
    }

    public void LevelButtonClicked(Button button) {
        buttonValue = button.GetComponentInChildren<Text>().text;
        Debug.Log(buttonValue);
        OpenGameScene();
    }

    public void MainMenuButtonClicked() {
        OpenMainMenuScene();
    }

    public void OpenGameScene() {
        SceneManager.LoadScene("Main Scene");
    }

    public void OpenMainMenuScene() {
        SceneManager.LoadScene("Level Selection");
    }
}
