using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class RopeManager : MonoBehaviour
{
    public GameObject ropePrefab;
    private GameObject startPoint;
    private GameObject endPoint;
    private GameObject currentRope;

    // Start is called before the first frame update
    private void Start() {
        // Initialize variables
        startPoint = null;
        endPoint = null;
        currentRope = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateRope() {
        // Calculate midpoint between start and end points
        Vector3 midpoint = (startPoint.transform.position + endPoint.transform.position) / 2f;

        // Instantiate rope prefab
        currentRope = Instantiate(ropePrefab, midpoint, Quaternion.identity);

        // Position and rotate the rope
        currentRope.transform.LookAt(endPoint.transform);
        float ropeLength = Vector3.Distance(startPoint.transform.position, endPoint.transform.position);
        currentRope.transform.localScale = new Vector3(1, 1, ropeLength);
    }

}
