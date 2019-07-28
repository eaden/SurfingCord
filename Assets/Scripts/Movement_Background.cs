using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Background : MonoBehaviour {

    Rigidbody rigid;
    float speed = 1;
    Vector3 dir = Vector3.zero;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody>();
        speed = 60;
        dir = new Vector3(-1, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        rigid.velocity = dir * speed * Time.deltaTime;
        if (transform.position.x < -39.5f)
            transform.position = new Vector3(transform.position.x + 39.5f*2, 0, 0);
    }
}
