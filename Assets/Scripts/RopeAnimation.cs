using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeAnimation : MonoBehaviour {

    public Transform startPoint;
    public Transform targetPoint;
    //public AnimationClip ropeAnimation;

    public GameObject ropePrefab;
   /* public Vector3 spawnPosition;
    public Vector3 spawnRotation;*/

    public AnimationClip ropeAnimationClip;
    private Animation ropeAnimation;
    private bool isAnimationPlaying;


    //change to Update later
    private void Start() {
        Debug.Log("hi");

        SpawnRope();

    }

    private void Update() {
        // Check if rope animation is playing
        if (isAnimationPlaying) {
            // Calculate distance between rope's end position and Point2
            float distanceToTarget = Vector3.Distance(transform.position, targetPoint.position);

            // Define threshold for considering the rope reaching the target
            float threshold = 0.1f; // Adjust as needed

            // If distance is less than threshold, stop the animation
            if (distanceToTarget < threshold) {
                ropeAnimation.Stop();
                isAnimationPlaying = false; // Update animation playing flag
            }
        }
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
        rope.transform.position = startPoint.position;
    }

    public void LengthenRope(GameObject ropeObject) {
        // Instantiate the rope prefab as a GameObject
        GameObject instantiatedRope = Instantiate(ropeObject);

        // Attach animation clip to the instantiated rope
        ropeAnimation = instantiatedRope.AddComponent<Animation>();
        ropeAnimation.AddClip(ropeAnimationClip, "ropeAnimation");
        ropeAnimation.Play("ropeAnimation");

        // Set animation playing flag
        isAnimationPlaying = true;
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
/*    public void LengthenRope(GameObject ropeObject, Transform targetPoint) {
            // Instantiate the rope prefab as a GameObject
            GameObject instantiatedRope = Instantiate(ropeObject);

            // Calculate the direction vector from the rope's start point to the target point
            Vector3 direction = targetPoint.position - instantiatedRope.transform.position;

            // Calculate the distance between the rope's start point and the target point
            //float distanceToTarget = direction.magnitude;


            // Get the current scale of the rope object
            Vector3 currentScale = instantiatedRope.transform.localScale;

            // Update the scale to only modify the height component
            instantiatedRope.transform.localScale = new Vector3(currentScale.x, distanceToTarget, currentScale.z);

            // Adjust the rope's position to match the start point
            instantiatedRope.transform.position = (instantiatedRope.transform.position + targetPoint.position) / 2f;

            // Attach animation clip to the instantiated rope
            Animation animation = instantiatedRope.AddComponent<Animation>();
            animation.AddClip(ropeAnimationClip, "ropeAnimation");
            animation.Play("ropeAnimation");
        }*/