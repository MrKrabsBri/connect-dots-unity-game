using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeManager : MonoBehaviour {

    public Transform firstPoint;
    public Transform secondPoint;

    public GameObject ropePrefab;
    private GameObject instantiatedRope;
    private SpriteRenderer ropeSpriteRenderer;
    float spriteHeight;



    //change to Update later
    private void Start() {
        Debug.Log("hi");



        instantiatedRope = SpawnRope(firstPoint,secondPoint);
        ropeSpriteRenderer = instantiatedRope.GetComponent<SpriteRenderer>();

        if (ropeSpriteRenderer != null) {
            spriteHeight = ropeSpriteRenderer.sprite.bounds.size.y;
            Debug.Log("Sprite Height: " + spriteHeight);
        }


        //StartCoroutine(DelayedAction());
        float? distance = FindDistanceBetweenDots(firstPoint, secondPoint);
        Debug.Log("Distance between object1 and object2: " + distance);

    }

    private void Update() {
        spriteHeight = ropeSpriteRenderer.size.y;
        //Debug.Log("Sprite Height: " + spriteHeight);

        if (spriteHeight >= FindDistanceBetweenDots(firstPoint, secondPoint)) {
            StopAnimation(instantiatedRope);
        }


    }


    private IEnumerator DelayedAction() {
        Debug.Log("Action will be delayed for 3 seconds...");
        // Wait for 3 seconds
        yield return new WaitForSeconds(2f);
        //instantiatedRope = SpawnRope();
        StopAnimation(instantiatedRope);
        Debug.Log("Action performed after delay.");
        Debug.Log("Thx for waiting");
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

        return rope;
    }

    public float? FindDistanceBetweenDots(Transform startPoint, Transform targetPoint) {

        Vector3 startPosition = startPoint.position;
        Vector3 targetPosition = targetPoint.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        return distance;
    }



}
