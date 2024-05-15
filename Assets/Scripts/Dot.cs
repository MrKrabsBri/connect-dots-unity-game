using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to represent a point with x and y coordinates
[System.Serializable]
public class Dot {
    public float? x { get; set; }
    public float? y { get; set; }

    public Dot(float? x, float? y) {
        this.x = x;
        this.y = y;
    }

}