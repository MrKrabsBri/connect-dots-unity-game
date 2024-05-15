using UnityEngine;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour, ClickInterface {

    public Sprite checkedPoint;

    private void Update() {
        PointClicked();
    }

    public void PointClicked() {
        if (Input.GetMouseButtonDown(0)) {

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null) {
                Debug.Log("Clicked on: " + hit.collider.gameObject.name);

                if (hit.collider.gameObject.tag == "Point") {
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = checkedPoint;
                    hit.collider.gameObject.GetComponentInChildren<Text>().enabled = false;
                    hit.collider.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                }
            }
        }
    }
}






