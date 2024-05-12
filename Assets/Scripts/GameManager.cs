using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    //private ReadLevelDataJson readlevelDataJson;
    private LoadLevel loadLevel;
    private int currentPoint = 0;
    private int nextPointClicked = 0;
    private bool levelIsCompleted = false;
    public GameObject spawnedPoint;

    List<Dot> dotsOfSelectedLevel;





    /*    public void Testing() {
            loadLevel = GetComponent<LoadLevel>();
            dotsOfSelectedLevel = loadLevel.SpawnLevelDots(0);
        }*/


    // Start is called before the first frame update
    void Start() {
        /* Testing();
         Debug.Log("coords : " + dotsOfSelectedLevel[0].x + " | " + dotsOfSelectedLevel[0].y);*/


        try {
            loadLevel = GetComponent<LoadLevel>();
            if (LevelSelectionHandler.buttonValue != null) {
                string buttonValue = LevelSelectionHandler.buttonValue;
                int level = int.Parse(buttonValue);
                //-----------#########-----------
                dotsOfSelectedLevel = loadLevel.SpawnLevelDots(level);
                //----------------------------------
                Debug.Log(dotsOfSelectedLevel[0].id + " | " + dotsOfSelectedLevel[1].id);
                Debug.Log(dotsOfSelectedLevel[2].x + " | " + dotsOfSelectedLevel[2].y);
                Debug.Log(dotsOfSelectedLevel[0].id);

                Debug.Log("=================================================");
            }
            else {
                Debug.LogWarning("Button value is null.");
            }
        } catch (FormatException e) {
            Debug.LogError("Button value is not a valid number: " + e.Message);
        }
        //RetrieveGameObject();
        RunTheGame();

    }

    // Update is called once per frame
    void Update() {
        OnClickDrawRope(dotsOfSelectedLevel);

        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            //hit.collider.gameObject.name
            if (hit.collider != null) {
                Debug.Log("Will draw a line towards " + hit.collider.gameObject.name);
                Debug.Log("Retrieve method gets name: ");
            }
        }

    }
    //list of Dots of this level - starts at 0       accessed with:  dotsOfSelectedLevel[0].id.ToString()
    //currentPoint skaitliukas - starts at 0;
    //pointClicked - starts at 1;                    acessed with:   hit.collider.gameObject.GetComponentInChildren<Text>().text;
    //get instantiated object with                   hit.collider.gameObject.name
    // get list of instantiated objects with         LoadLevel.listOfInstantiatedDots



    //}
    public void RunTheGame() {
        //turn off colliders of all gameObjects except no.1

        // while (!levelIsCompleted) {
        foreach (GameObject point in LoadLevel.listOfInstantiatedDots) {
            point.GetComponent<CircleCollider2D>().enabled = false;
        }
        LoadLevel.listOfInstantiatedDots[0].GetComponent<CircleCollider2D>().enabled = true;
        currentPoint = int.Parse(LoadLevel.listOfInstantiatedDots[0].GetComponentInChildren<Text>().text);

        // metodas i kurio argument idedame paspausta mygtuka (jo nr? idk). tada metodas uzlockina visus mygtukus
        //except sekanti, / ta paspausta padaro blue/ i ta paspausta piesia line.



        //if currentPoint is clicked (if its blue/ if its value changes???)
        //LoadLevel.listOfInstantiatedDots[1].GetComponent<CircleCollider2D>().enabled = true;

    }

    //list of Dots of this level - starts at 0          accessed with:  dotsOfSelectedLevel[0].id.ToString()
    //currentPoint skaitliukas - starts at 0;
    //pointClicked - starts at 1;                       acessed with:   hit.collider.gameObject.GetComponentInChildren<Text>().text;
    //get instantiated object with                      hit.collider.gameObject.name
    // get list of instantiated objects - starts at 0   LoadLevel.listOfInstantiatedDots
    // LoadLevel.listOfInstantiatedDots[0].GetComponent<CircleCollider2D>().enabled = true;

    public void OnNextPointClick(RaycastHit2D rh2d) {

        foreach (GameObject point in LoadLevel.listOfInstantiatedDots) {
            int number = int.Parse(point.GetComponentInChildren<Text>().text);
            //pvz number = 1
            LoadLevel.listOfInstantiatedDots[number].GetComponent<CircleCollider2D>().enabled = true;
            LoadLevel.listOfInstantiatedDots[number - 1].GetComponent<CircleCollider2D>().enabled = true;
            //OnClickDrawRope;
        }
    }

    public void OnClickDrawRope(List<Dot> listOfPoints) {

        if (Input.GetMouseButtonDown(0)) {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (hit.collider != null) {
                    Debug.Log("Will draw a line towards " + hit.collider.gameObject.name);
                Debug.Log("Retrieve method gets name: " );
                //RetrieveGameObject();
                    nextPointClicked = int.Parse(hit.collider.gameObject.GetComponentInChildren<Text>().text);


                    Debug.Log("the text of clicked dot is : " + hit.collider.gameObject.
                        GetComponentInChildren<Text>().text);

                    Debug.Log("Meanwhile, the id of dot nr. 4 is : " + listOfPoints[3].id);
                    Debug.Log("+++++++++++++++++");
                }
            }
    }



}
