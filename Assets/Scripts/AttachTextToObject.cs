using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttachTextToObject : MonoBehaviour {

    public GameObject textObject; // Assign the Text GameObject in the Inspector
    public Vector3 offset; // Offset from the GameObject's position

    void Start() {
        // Convert the GameObject's world position to a position relative to the Canvas
        Vector2 position = Camera.main.WorldToScreenPoint(transform.position + offset);
        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        rectTransform.position = position;
    }
}