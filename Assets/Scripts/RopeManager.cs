using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeManager : MonoBehaviour {

    public Transform startPoint;
    public Transform targetPoint;

    public GameObject ropePrefab;
    private GameObject instantiatedRope;

    public AnimationClip ropeAnimationClip;
    private Animation ropeAnimation;
    private bool isAnimationPlaying;


    //change to Update later
    private void Start() {
        Debug.Log("hi");

        SpawnRope();
        //LengthenRope();
    }

    private void Update() {

    }

    public void SpawnRope() {// Calculate direction vector from start point to target point
        Vector3 direction = targetPoint.position - startPoint.position;

        // Calculate the angle in radians between the direction vector and the positive z-axis
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Create a quaternion rotation around the z-axis using the calculated angle
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        // Adjust rotation to make bottom middle face target point
        rotation *= Quaternion.Euler(0f, 0f, 90f);

        // Spawn the rope with the rotation towards the target point
        GameObject rope = Instantiate(ropePrefab, startPoint.position, rotation);

        // Optionally, you can adjust the position of the rope to match the start point
        //rope.transform.position = startPoint.position;
    }





    private void OnMouseDown() {
        // Disable collider to prevent further clicks
        GetComponent<Collider2D>().enabled = false;
    }

/*    private void OnTriggerEnter2D(Collider2D collision) {
        // Stop the rope animation when it reaches Dot2
        if (collision.gameObject == ropePrefab) {
            ropeAnimationClip.Stop();
        }
    }*/

}
