using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to represent a point with x and y coordinates
[System.Serializable]
public class Point {
    public float? x { get; set; }
    public float? y { get; set; }

    public Point(float? x, float? y) {
        this.x = x;
        this.y = y;
    }

}