using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    //private ReadLevelDataJson readlevelDataJson;
    private LoadLevel loadLevel;
    private int currentPoint;
    List<Dot> dotsOfSelectedLevel;




/*    public void Testing() {
        loadLevel = GetComponent<LoadLevel>();
        dotsOfSelectedLevel = loadLevel.SpawnLevelDots(0);
    }*/


    // Start is called before the first frame update
    void Start() {
        /* Testing();
         Debug.Log("coords : " + dotsOfSelectedLevel[0].x + " | " + dotsOfSelectedLevel[0].y);*/

        try {
            loadLevel = GetComponent<LoadLevel>();
            if (LevelSelectionHandler.buttonValue != null) {
                string buttonValue = LevelSelectionHandler.buttonValue;
                int level = int.Parse(buttonValue);
                //-----------#########-----------
                dotsOfSelectedLevel = loadLevel.SpawnLevelDots(level);
                //----------------------------------
                Debug.Log(dotsOfSelectedLevel[0].x  +" | "+ dotsOfSelectedLevel[0].y);
                Debug.Log(dotsOfSelectedLevel[1].x + " | " + dotsOfSelectedLevel[1].y);
                Debug.Log(dotsOfSelectedLevel[2].x + " | " + dotsOfSelectedLevel[2].y);
                Debug.Log(dotsOfSelectedLevel[0].id + " | " + dotsOfSelectedLevel[1].id);
                Debug.Log(dotsOfSelectedLevel[2].id + " | " + dotsOfSelectedLevel[3].id);
                Debug.Log(dotsOfSelectedLevel[4].id + " | " + dotsOfSelectedLevel[5].id);
                Debug.Log("=================================================");
            }
            else {
                Debug.LogWarning("Button value is null.");
            }
        } catch (FormatException e) {
            Debug.LogError("Button value is not a valid number: " + e.Message);
        }


    }

    // Update is called once per frame
    void Update() {

    }

    public void OnClickDrawRope(List<Dot> listOfPoints) {

        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null) {
                Debug.Log("Will draw a line towards " + hit.collider.gameObject.name);

/*                if (hit.collider.gameObject == listOfPoints[currentPoint + 1]) {
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite =
                        checkedPoint;
                }*/


            }

        }
    }


    /*            if (paspaudziauAntSekancioElemento negu dabar esu) {
                     }*//*
        }*/



    //check when i click first dot/element

    //check what button you click
    // public

}
