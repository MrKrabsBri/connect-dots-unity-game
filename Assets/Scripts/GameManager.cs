using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    //private ReadLevelDataJson readlevelDataJson;
    private LoadLevel loadLevel;
    private RopeManager ropeManager; //+++
    private int numberOfClickedPoint;
    public Animator animator; // works su public

    public GameObject spawnedPoint;
    public GameObject spawnedRope;
    public GameObject ropePrefab; // +++

    private bool levelIsCompleted = false;
    private bool firstPointWasClicked = false;
    private bool ropeIsDrawing = false;

    private bool clickedOnTheLastPoint = false;

    RaycastHit2D hit;

    Transform start;
    Transform target;


    List<Point> pointListOfCurrentLevel;

    public bool ropeIsStillDrawing = false;
    public void StartUpdateOfRopeDrawClass() {
        ropeIsStillDrawing = true;
    }
    public void StopUpdateOfRopeDrawClass() {
        ropeIsStillDrawing = false;
    }

    private void Awake() {
        ropeManager = GetComponent<RopeManager>();
        loadLevel = GetComponent<LoadLevel>();
    }

    void Start() {
        /*        animator = gameObject.GetComponent<Animator>();
                if (animator == null) {                         // ???????????????????????????????????????
                    Debug.LogError("Animator component not found!");
                }
                if (animator != null) {                         // ???????????????????????????????????????
                    Debug.LogError("bonk");
                }*/

        try {
            //Spawning level Points according to level selected
            if (LevelSelectionHandler.buttonValue != null) {
                string buttonValue = LevelSelectionHandler.buttonValue;
                int level = int.Parse(buttonValue);
                pointListOfCurrentLevel = loadLevel.GetListOfPointsForThisLevel(level);
                loadLevel.SpawnLevelPoints(pointListOfCurrentLevel, level);
                EnableOrDisablePoints(LoadLevel.listOfInstantiatedPoints, false);
            }
            else {
                Debug.LogWarning("Button value is null.");
            }
        } catch (FormatException e) {
            Debug.LogError("Button value is not a valid number: " + e.Message);
        }
        Debug.Log("Number of Points in the list " + pointListOfCurrentLevel.Count);
        Debug.Log("instantiated point count : " + LoadLevel.listOfInstantiatedPoints.Count);
    }

    void Update() {
        // ????????????????????
        if (Input.GetKeyDown(KeyCode.Space)) {
            // Set the trigger parameter to activate the animation
            animator.SetTrigger("showing");

            // animator.SetInteger("menu_showing",1);
        }

        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == LoadLevel.listOfInstantiatedPoints[0]
                            && !firstPointWasClicked) {
                Debug.Log("You hit first point, it turns blue, rope is not drawn.");
                firstPointWasClicked = true;
                LoadLevel.listOfInstantiatedPoints[1].GetComponent<CircleCollider2D>().enabled = true;
            }

            if (hit.collider != null && hit.collider.gameObject.tag == "Point"
                && (int.Parse(hit.collider.gameObject.GetComponentInChildren<Text>().text) != 1)
                && firstPointWasClicked
                && !ropeIsDrawing) {

                HandleDrawingRope(hit);

            }
        }

        if (spawnedRope != null && ropeIsDrawing) {
            ropeIsDrawing = !ropeManager.RopeHasReachedPoint(spawnedRope, start, target);

            if (clickedOnTheLastPoint && !levelIsCompleted && !ropeIsDrawing) {
                StartCoroutine(DrawTheFinalRope(0.5f, ropeManager,
                                LoadLevel.listOfInstantiatedPoints[LoadLevel.listOfInstantiatedPoints.Count - 1].transform,
                                LoadLevel.listOfInstantiatedPoints[0].transform));
                levelIsCompleted = true;
                ropeIsDrawing = true;

            }


            if (!ropeIsDrawing && levelIsCompleted) {
                StartCoroutine(Stall());
                //             animator.SetTrigger("showing");                 // ?????????
            }

        }
        if (!ropeIsDrawing && !clickedOnTheLastPoint) {
            LoadLevel.listOfInstantiatedPoints[numberOfClickedPoint].GetComponent<CircleCollider2D>().enabled = true;
        }


    }


    public void HandleDrawingRope(RaycastHit2D rayHit) {
        numberOfClickedPoint = int.Parse(rayHit.collider.gameObject.GetComponentInChildren<Text>().text);
        Debug.Log(numberOfClickedPoint);
        target = LoadLevel.listOfInstantiatedPoints[numberOfClickedPoint - 1].transform;
        start = LoadLevel.listOfInstantiatedPoints[numberOfClickedPoint - 2].transform;
        spawnedRope = ropeManager.SpawnRope(start, target);

        if (numberOfClickedPoint == LoadLevel.listOfInstantiatedPoints.Count) {
            clickedOnTheLastPoint = true;
        }
        ropeIsDrawing = true;
    }

    public void EnableOrDisablePoints(List<GameObject> points, bool EnableOrDisable) {
        foreach (GameObject point in points.Skip(1)) {
            Debug.Log(points.Count);
            point.GetComponent<CircleCollider2D>().enabled = EnableOrDisable;
        }
    }

    private IEnumerator DrawTheFinalRope(float delay, RopeManager ropeManager, Transform startPoint, Transform targetPoint) {
        yield return new WaitForSeconds(delay);

        start = startPoint;
        target = targetPoint;
        spawnedRope = ropeManager.SpawnRope(start, target);
        ropeIsDrawing = true;
    }

    IEnumerator Stall() {
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("showing");
    }

}