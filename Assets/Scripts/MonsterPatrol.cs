using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPatrol : MonoBehaviour {

    public float speed;

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Move();
    }

    void Move()
    {
        Vector2 temp = rb.velocity;

        temp.x = speed;
        rb.velocity = temp;
    }
}
