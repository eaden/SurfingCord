using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

    Rigidbody rigid;
    float speed = 1;
    public Vector3 dir = new Vector3(-1, 0, 0);
    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody>();
        speed = 120;
    }
	
	// Update is called once per frame
	void Update () {
        rigid.velocity = dir * speed * Time.deltaTime;
    }
}
