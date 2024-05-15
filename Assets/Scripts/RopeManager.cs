using System.Collections;
using UnityEngine;

public class RopeManager : MonoBehaviour {

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

    private void Start() {
        loadLevel = GetComponent<LoadLevel>();
    }

    public bool RopeHasReachedPoint(GameObject rope, Transform startPoint, Transform targetPoint) {

        ropeSpriteRenderer = rope.GetComponent<SpriteRenderer>();
        ropeHeight = ropeSpriteRenderer.size.y;

        if (ropeHeight >= FindDistanceBetweenDots(startPoint, targetPoint)) {
            StopAnimation(rope);
            return true;
        }
        return false;
    }

    void StopAnimation(GameObject rope) {

        Animator ropeAnimator = rope.GetComponent<Animator>();
        ropeAnimator.enabled = false;
    }

    // Calculate direction vector from start point to target point
    public GameObject SpawnRope(Transform startPoint, Transform targetPoint) {


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
