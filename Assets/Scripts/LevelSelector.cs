using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{

    public static int selectedLevel;
    public int level;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void OpenScene() {
        selectedLevel = level;
        SceneManager.LoadScene("Main Scene");
    }
}
