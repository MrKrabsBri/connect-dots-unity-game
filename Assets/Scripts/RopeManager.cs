using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeManager : MonoBehaviour {

    //public GameManager gameManager;
    public LoadLevel loadLevel;

    public Transform firstPoint;
    public Transform secondPoint;
    public GameObject currentInstantiatedRope;

    public GameObject ropePrefab;
    private SpriteRenderer ropeSpriteRenderer;
    public float ropeHeight;


    public bool ropeIsStillDrawing = false;



    public void StartUpdateOfRopeDrawClass() {
        ropeIsStillDrawing = true;
    }
    public void StopUpdateOfRopeDrawClass() {
        ropeIsStillDrawing = false;
    }

    //change to Update later
    private void Start() {
        Debug.Log("hi");
        loadLevel = GetComponent<LoadLevel>();
        //StartUpdateOfRopeDrawClass();

        /*currentInstantiatedRope = SpawnRope(firstPoint, secondPoint); //GOOD kitoj klasej | test with this in test scene
        StartUpdateOfRopeDrawClass(); //GOOD kitoj klasej | test with this in test scene */


        //StartCoroutine(DelayedAction());
        /* float? distance = FindDistanceBetweenDots(firstPoint, secondPoint);
         Debug.Log("Distance between Point1 and Point2: " + distance);*/

    }



    public bool RopeHasReachedPoint(GameObject rope, Transform startPoint, Transform targetPoint) {
        //firstPoint = startPoint;
        //secondPoint = targetPoint;
        ropeSpriteRenderer = rope.GetComponent<SpriteRenderer>();
        ropeHeight = ropeSpriteRenderer.size.y;

        if (ropeHeight >= FindDistanceBetweenDots(startPoint, targetPoint)) {
            StopAnimation(rope);
            return true;
        }
        return false;
    }



    void StopAnimation(GameObject rope) {
        // Get the Animator component from the GameObject
        Animator ropeAnimator = rope.GetComponent<Animator>();

        // Disable the Animator component to stop the animation
        ropeAnimator.enabled = false;
    }

    public GameObject SpawnRope(Transform startPoint, Transform targetPoint) {// Calculate direction vector from start point to target point


        Vector3 direction = targetPoint.position - startPoint.position;

        // Calculate the angle in radians between the direction vector and the positive z-axis
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Create a quaternion rotation around the z-axis using the calculated angle
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        // Adjust rotation to make bottom middle face target point
        rotation *= Quaternion.Euler(0f, 0f, 90f);

        // Spawn the rope with the rotation towards the target point
        GameObject rope = Instantiate(ropePrefab, startPoint.position, rotation);

        //ropeSpriteRenderer = rope.GetComponent<SpriteRenderer>(); // gal reik comment del coroutines
       // currentInstantiatedRope = rope; // gal reik comment del coroutines
        return rope;
    }

    public float? FindDistanceBetweenDots(Transform startPoint, Transform targetPoint) {

        Vector3 startPosition = startPoint.position;
        Vector3 targetPosition = targetPoint.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        return distance;
    }

    private IEnumerator SpawnRopeAndWaitForCompletion(Transform startPoint, Transform targetPoint) {
        // Spawn the rope

        GameObject rope = SpawnRope(startPoint, targetPoint);

        // Loop until the rope has reached the target point
        while (!RopeHasReachedPoint(rope, startPoint, targetPoint)) {
            yield return null; // Wait for the next frame
        }
        // Rope has reached the target point, coroutine is complete
    }

    // Method to spawn the rope and start the coroutine
    public void SpawnRopeAndCheckCompletion(Transform startPoint, Transform targetPoint) {
        StartCoroutine(SpawnRopeAndWaitForCompletion(startPoint, targetPoint));
    }


}
