using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Spheres : MonoBehaviour {


    Rigidbody rigid;
    float speed = 1;
    Vector3 dir;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody>();
        dir = new Vector3(-1, 0, 0);
        speed = 60;
    }
	
	// Update is called once per frame
	void Update () {
        rigid.velocity = dir * speed * Time.deltaTime;
    }
}
