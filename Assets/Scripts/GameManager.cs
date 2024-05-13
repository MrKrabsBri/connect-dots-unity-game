using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    //private ReadLevelDataJson readlevelDataJson;
    private LoadLevel loadLevel;
    private RopeManager ropeManager; //+++
    private int atCurrentPoint = 0;
    private int nextPointClicked = 0;

    public GameObject spawnedPoint;
    public GameObject spawnedRope;
    public GameObject ropePrefab; // +++

    private bool levelIsCompleted = false;
    private bool previousPointIsClicked = false;
    private bool firstPointWasClicked = false;
    private bool ropeIsDrawing = false;
    RaycastHit2D hit;

    Transform start;
    Transform target;

    List<Dot> dotsOfSelectedLevel;
    List<Dot> dotListOfCurrentLevel;

    public bool ropeIsStillDrawing = false;
    public void StartUpdateOfRopeDrawClass() {
        ropeIsStillDrawing = true;
    }
    public void StopUpdateOfRopeDrawClass() {
        ropeIsStillDrawing = false;
    }

    /*    public void Testing() {
            loadLevel = GetComponent<LoadLevel>();
            dotsOfSelectedLevel = loadLevel.SpawnLevelDots(0);
        }*/

    private void Awake() {
        ropeManager = GetComponent<RopeManager>();
        loadLevel = GetComponent<LoadLevel>();
    }

    // Start is called before the first frame update
    void Start() {




        try {
            //ropeManager = GetComponent<RopeManager>();


            //ropeManager.StartUpdateOfRopeDrawClass();

            if (LevelSelectionHandler.buttonValue != null) {
                string buttonValue = LevelSelectionHandler.buttonValue;
                int level = int.Parse(buttonValue);
                //-----------#########-----------
                dotListOfCurrentLevel = loadLevel.GetListOfDotsForThisLevel(level);
                loadLevel.SpawnLevelDots(dotListOfCurrentLevel, level);


                EnableOrDisablePoints(LoadLevel.listOfInstantiatedDots, false);




                //----------------------------------
                //Debug.Log(dotsOfSelectedLevel[0].id + " | " + dotsOfSelectedLevel[1].id);
                Debug.Log(dotListOfCurrentLevel[2].x + " | " + dotListOfCurrentLevel[2].y);
                //Debug.Log(dotsOfSelectedLevel[0].id);
                Debug.Log("=================================================");

            }
            else {
                Debug.LogWarning("Button value is null.");
            }
        } catch (FormatException e) {
            Debug.LogError("Button value is not a valid number: " + e.Message);
        }
        Debug.Log("Count " + dotListOfCurrentLevel.Count);
        Debug.Log("instantiated dot count : " + LoadLevel.listOfInstantiatedDots.Count);
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == LoadLevel.listOfInstantiatedDots[0]
                            && !firstPointWasClicked) {
                Debug.Log("You hit first point, it turns blue, rope is not drawn.");
                atCurrentPoint = 1;
                firstPointWasClicked = true;
                previousPointIsClicked = true;
                LoadLevel.listOfInstantiatedDots[1].GetComponent<CircleCollider2D>().enabled = true;
            }
            //LoadLevel.listOfInstantiatedDots[1].GetComponent<CircleCollider2D>().enabled = true; ==> pasidaro active

            if (hit.collider != null && hit.collider.gameObject.tag == "Point"
                && (int.Parse(hit.collider.gameObject.GetComponentInChildren<Text>().text) != 1)
                && firstPointWasClicked /*take a look ar reik*/
                && previousPointIsClicked/*idk ar reik?*/ && !ropeIsDrawing) {

                HandleDrawingRope(hit);
            }
            /*            if (hit.collider != null && hit.collider.gameObject == LoadLevel.listOfInstantiatedDots[1]
                                        && firstPointWasClicked && previousPointIsClicked && !ropeIsDrawing) {
                            Debug.Log("You hit second point, it turns blue, drawing rope.");  //test
                            Debug.Log("you hit point number: " + hit.collider.gameObject.GetComponentInChildren<Text>().text);

                            start = LoadLevel.listOfInstantiatedDots[0].transform;
                            //Debug.Log(start.position.x);
                            target = LoadLevel.listOfInstantiatedDots[1].transform;

                            spawnedRope = ropeManager.SpawnRope(start, target);
                            previousPointIsClicked = false;
                            ropeIsDrawing = true;

                        }*/   // GOOD HARDCODED EXAMPLE
        }
        if (spawnedRope != null && ropeIsDrawing) {
            ropeIsDrawing = !ropeManager.RopeHasReachedPoint(spawnedRope, start, target);
            /*if (!ropeIsDrawing)
                EnableOrDisablePoints(LoadLevel.listOfInstantiatedDots, true);*/  // Ko gero nenaudosiu sito 05.14
            Debug.Log("has rope reached yet? :" + ropeManager.RopeHasReachedPoint(spawnedRope, start, target));

        }


        /*            while (ropeIsDrawing) {
                        ropeIsDrawing = !ropeManager.RopeHasReachedPoint(spawnedRope, start, target);
                    }*/
    }

    public void HandleDrawingRope(RaycastHit2D rayHit) {
        int numberOfClickedPoint = int.Parse(rayHit.collider.gameObject.GetComponentInChildren<Text>().text);//  (2)
        target = LoadLevel.listOfInstantiatedDots[numberOfClickedPoint - 1].transform;                  // liste (1)
        start = LoadLevel.listOfInstantiatedDots[numberOfClickedPoint - 2].transform;                  // liste (0)
        spawnedRope = ropeManager.SpawnRope(start, target);
        LoadLevel.listOfInstantiatedDots[numberOfClickedPoint].GetComponent<CircleCollider2D>().enabled = true;
        //previousPointIsClicked = false;
        ropeIsDrawing = true;
        /*EnableOrDisablePoints(LoadLevel.listOfInstantiatedDots, false);*/  // ko gero nenaudosiu
        //handle last click, list out of bounds
    }

    public void EnableOrDisablePoints(List<GameObject> points, bool EnableOrDisable) {
        foreach (GameObject point in points.Skip(1)) { // TESTING 05.14
            Debug.Log(points.Count);
            point.GetComponent<CircleCollider2D>().enabled = EnableOrDisable;
        }
    }

    //list of Dots of this level - starts at 0       accessed with:  dotsOfSelectedLevel[0].id.ToString()
    //currentPoint skaitliukas - starts at 0;
    //pointClicked - starts at 1;                    acessed with:   hit.collider.gameObject.GetComponentInChildren<Text>().text;
    //get instantiated object with                   hit.collider.gameObject.name
    // get list of instantiated objects with         LoadLevel.listOfInstantiatedDots
    //                  LoadLevel.listOfInstantiatedDots[numberOfPoint].GetComponent<CircleCollider2D>().enabled = true;

    /*    public void OnNextPointClick(RaycastHit2D hit) {


            //int number = int.Parse(point.GetComponentInChildren<Text>().text);

            int numberOfPoint = int.Parse(hit.collider.gameObject.GetComponentInChildren<Text>().text);
            //pvz spaudziam ant number = 1
            //active pasidaro point 2.
            //point1 pasidaro NEactive

            LoadLevel.listOfInstantiatedDots[numberOfPoint].GetComponent<CircleCollider2D>().enabled = true;
            LoadLevel.listOfInstantiatedDots[numberOfPoint - 1].GetComponent<CircleCollider2D>().enabled = false;
            if (numberOfPoint != 1) {
                GameObject newSpawnedRope = ropeManager.SpawnRope(LoadLevel.listOfInstantiatedDots[numberOfPoint - 1].transform,
                                LoadLevel.listOfInstantiatedDots[numberOfPoint].transform);
                ropeManager.currentInstantiatedRope = newSpawnedRope;
                ropeManager.firstPoint = LoadLevel.listOfInstantiatedDots[numberOfPoint - 1].transform;
                ropeManager.secondPoint = LoadLevel.listOfInstantiatedDots[numberOfPoint].transform;

                //StartUpdateOfRopeDrawClass();
            }



            *//*            OnClickDrawRope(dotsOfSelectedLevel, hit, );*//*

        }*/

    //}
    /*    public void RunTheGame() {
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

        }*/

    //list of Dots of this level - starts at 0          accessed with:  dotsOfSelectedLevel[0].id.ToString()
    //currentPoint skaitliukas - starts at 0;
    //pointClicked - starts at 1;                       acessed with:   hit.collider.gameObject.GetComponentInChildren<Text>().text;
    //get instantiated object with                      hit.collider.gameObject.name
    // get list of instantiated objects - starts at 0   LoadLevel.listOfInstantiatedDots
    // LoadLevel.listOfInstantiatedDots[0].GetComponent<CircleCollider2D>().enabled = true;



    /*    public void OnClickDrawRope(List<Dot> listOfPoints) {

            if (Input.GetMouseButtonDown(0)) {
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                    if (hit.collider != null) {
                        Debug.Log("Will draw a line towards " + hit.collider.gameObject.name);

                        nextPointClicked = int.Parse(hit.collider.gameObject.GetComponentInChildren<Text>().text);


                        Debug.Log("the text of clicked dot is : " + hit.collider.gameObject.
                            GetComponentInChildren<Text>().text);

                        Debug.Log("Meanwhile, the id of dot nr. 4 is : " + listOfPoints[3].id);
                        Debug.Log("+++++++++++++++++");
                    }
                }
        }*/



}
