using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Point : Collidable, ClickInterface {

    public Sprite checkedPoint;

/*    private void OnTriggerEnter2D(Collider2D other) {
       // OnCollide(other);
    }*/

    protected override void Update() {
        ButtonClicked(0);
    }

    /*    protected override void OnCollide(Collider2D coll) {
            Debug.Log("Collided with " + coll.name);
            if (coll.CompareTag("Point")) {
                Debug.Log("changing color");
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = checkedPoint;
            }
        }*/

    // This method is called when another collider enters the trigger collider attached to this GameObject
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collision detected with: " + other.gameObject.name);

        // You can add more logic here to handle the collision
    }


    public void ButtonClicked(int button) {
        if (Input.GetMouseButtonDown(button)) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null) {
                Debug.Log("Clicked on: " + hit.collider.gameObject.name);

                if (hit.collider.gameObject.tag == "Point") {
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite =
                        checkedPoint;
                }


            }
        }
    }
}






