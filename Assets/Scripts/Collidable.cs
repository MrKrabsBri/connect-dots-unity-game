using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour {

    private BoxCollider2D boxCollider;

    public ContactFilter2D filter;

    private Collider2D[] collisions = new Collider2D[10];



    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //boxCollider.OverlapCollider(filter, collisions);

        for (int i = 0; i < collisions.Length; i++) {
            if (collisions[i] == null)
                continue;


            collisions[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll) {

    }

}
