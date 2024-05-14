using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour {
    //private ReadLevelDataJson readlevelDataJson;
    private LoadLevel loadLevel;
    private RopeManager ropeManager; //+++
    private int atCurrentPoint = 0;
    private int nextPointClicked = 0;
    private int numberOfClickedPoint;

    public GameObject spawnedPoint;
    public GameObject spawnedRope;
    public GameObject ropePrefab; // +++

    private bool levelIsCompleted = false;
    private bool firstPointWasClicked = false;
    private bool ropeIsDrawing = false;
    private bool ropeIsWaitingToBeDrawn = false;
    private bool clickedOnTheLastPoint = false;
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
                LoadLevel.listOfInstantiatedDots[1].GetComponent<CircleCollider2D>().enabled = true;
            }
            //LoadLevel.listOfInstantiatedDots[1].GetComponent<CircleCollider2D>().enabled = true; ==> pasidaro active

            if (hit.collider != null && hit.collider.gameObject.tag == "Point"
                && (int.Parse(hit.collider.gameObject.GetComponentInChildren<Text>().text) != 1)
                && firstPointWasClicked /*take a look ar reik*/
                && !ropeIsDrawing) {

                HandleDrawingRope(hit);

            }
            //checks if last point is clicked to finish level
/*            if (clickedOnTheLastPoint && !levelIsCompleted && !ropeIsDrawing) {

                StartCoroutine(MyCoroutine(0.5f, ropeManager,
                                LoadLevel.listOfInstantiatedDots[LoadLevel.listOfInstantiatedDots.Count - 1].transform,
                                LoadLevel.listOfInstantiatedDots[0].transform));

                float position = spawnedRope.transform.position.x;
                Debug.Log("Last ropes x position! : " + position);
                levelIsCompleted = true;
            }*/

        }
        //commentinu del coroutine example
        if (spawnedRope != null && ropeIsDrawing) {
            ropeIsDrawing = !ropeManager.RopeHasReachedPoint(spawnedRope, start, target);

            if (clickedOnTheLastPoint && !levelIsCompleted && !ropeIsDrawing) {
                StartCoroutine(MyCoroutine(2f, ropeManager,
                                LoadLevel.listOfInstantiatedDots[LoadLevel.listOfInstantiatedDots.Count - 1].transform,
                                LoadLevel.listOfInstantiatedDots[0].transform));
/*                float position = spawnedRope.transform.position.x;
                Debug.Log("Last ropes x position! : " + position);*/
                levelIsCompleted = true;
                ropeIsDrawing = true;
            }

            /*if (ropeIsWaitingToBeDrawn && !ropeIsDrawing) {
                HandleDrawingRope(hit); // hit gali buti klaidu nes sena value idedama
            }*/
            Debug.Log("has rope reached yet? :" + ropeManager.RopeHasReachedPoint(spawnedRope, start, target));
        }
        if (!ropeIsDrawing && !clickedOnTheLastPoint) {
            LoadLevel.listOfInstantiatedDots[numberOfClickedPoint].GetComponent<CircleCollider2D>().enabled = true;
        }

        /*        if (spawnedRope != null && ropeIsDrawing) {

                    StartCoroutine(MyCoroutine(ropeManager.RopeHasReachedPoint(spawnedRope, start, target)));
                }*/



    }


    public void HandleDrawingRope(RaycastHit2D rayHit) {
        /*int */numberOfClickedPoint = int.Parse(rayHit.collider.gameObject.GetComponentInChildren<Text>().text);
        Debug.Log(numberOfClickedPoint);
        target = LoadLevel.listOfInstantiatedDots[numberOfClickedPoint - 1].transform;
        start = LoadLevel.listOfInstantiatedDots[numberOfClickedPoint - 2].transform;

        spawnedRope = ropeManager.SpawnRope(start, target); // komentuoju tik del coroutines testing 05.14
        //ropeManager.SpawnRopeAndCheckCompletion(start, target);

        if (numberOfClickedPoint == LoadLevel.listOfInstantiatedDots.Count) {
            clickedOnTheLastPoint = true;
        }
        else {
            //good, bet testuoju 05.14 15h
           // LoadLevel.listOfInstantiatedDots[numberOfClickedPoint].GetComponent<CircleCollider2D>().enabled = true;
        }


        /*}*/


        if (ropeIsDrawing) {
            ropeIsWaitingToBeDrawn = true;
        }
        if (!ropeIsDrawing) {
            ropeIsWaitingToBeDrawn = false;
        }

        ropeIsDrawing = true;

    }

    public void EnableOrDisablePoints(List<GameObject> points, bool EnableOrDisable) {
        foreach (GameObject point in points.Skip(1)) {
            Debug.Log(points.Count);
            point.GetComponent<CircleCollider2D>().enabled = EnableOrDisable;
        }
    }

    private IEnumerator MyCoroutine(float delay, RopeManager ropeManager, Transform startPoint, Transform targetPoint/*bool ropeHasReachedPoint*/) {
        Debug.Log("Coroutine started");

        /*        if (!ropeHasReachedPoint) {
                    Debug.Log("Checking if Rope is still drawing ");
                    yield return null; // Wait for the next frame
                }*/

        //pvz del paskutinio point
        yield return new WaitForSeconds(delay);

        // Call DrawingRope after the delay
        start = startPoint;
        target = targetPoint;
        spawnedRope = ropeManager.SpawnRope(start, target);
        Debug.Log("Coroutine finished. rope finished drawing");
        ropeIsDrawing = true;
    }

    /*        // Check if a pre-click has occurred and if the rope is currently drawing
        if (ropeIsDrawing && previousPointIsClicked) {
            // If the rope is drawing and a pre-click has occurred, start drawing from the previous point to the current point
            StartCoroutine(DrawRopeCoroutine(start, target));
            previousPointIsClicked = false; // Reset the pre-click flag
        }
        else {
            // Otherwise, initiate the rope drawing process from the current point to the next point
            StartCoroutine(DrawRopeCoroutine(start, target));
            previousPointIsClicked = true; // Set the pre-click flag to indicate a pre-click has occurred
        }*/

    /*    IEnumerator DrawRopeCoroutine(Transform start, Transform target) {
            ropeIsDrawing = true;
            // Spawn the rope
            spawnedRope = ropeManager.SpawnRope(start, target);

            yield return new WaitUntil(() => ropeManager.RopeHasReachedPoint(spawnedRope, start, target));
            ropeIsDrawing = false;

        }*/




    /*    IEnumerator MyCoroutine() {                                   // Testing
            // Wait until the condition is satisfied
            while (spawnedRope != null && ropeIsDrawing) {
                yield return null; // Wait for one frame
            }

            // Once the condition is satisfied, continue with the code
            Debug.Log("Condition satisfied! Drawing next rope that I clicked on");

            // Your attack code goes here
        }*/

    //list of Dots of this level - starts at 0       accessed with:  dotsOfSelectedLevel[0].id.ToString()
    //currentPoint skaitliukas - starts at 0;
    //pointClicked - starts at 1;                    acessed with:   hit.collider.gameObject.GetComponentInChildren<Text>().text;
    //get instantiated object with                   hit.collider.gameObject.name
    // get list of instantiated objects with         LoadLevel.listOfInstantiatedDots
    //                  LoadLevel.listOfInstantiatedDots[numberOfPoint].GetComponent<CircleCollider2D>().enabled = true;

    /*

    //list of Dots of this level - starts at 0          accessed with:  dotsOfSelectedLevel[0].id.ToString()
    //currentPoint skaitliukas - starts at 0;
    //pointClicked - starts at 1;                       acessed with:   hit.collider.gameObject.GetComponentInChildren<Text>().text;
    //get instantiated object with                      hit.collider.gameObject.name
    // get list of instantiated objects - starts at 0   LoadLevel.listOfInstantiatedDots
    // LoadLevel.listOfInstantiatedDots[0].GetComponent<CircleCollider2D>().enabled = true;


    */



}
