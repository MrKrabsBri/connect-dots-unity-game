using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour {

    private BoxCollider2D boxCollider;

    public ContactFilter2D filter;

    private Collider2D[] collisions = new Collider2D[10];

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        for (int i = 0; i < collisions.Length; i++) {
            if (collisions[i] == null)
                continue;


            collisions[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll) {
        Debug.Log("collided with " + coll.name);

    }

}
