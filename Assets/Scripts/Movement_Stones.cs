using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Stones : MonoBehaviour {

    Rigidbody rigid;
    float speed = 1;
    Vector3 dir = Vector3.zero;
    float deltaCounter = 0;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        speed = 60;
        dir = new Vector3(-1,0, 0);
        deltaCounter += Random.Range(0, 7) * 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
        dir.y = Mathf.Sin(deltaCounter)/3;
        rigid.velocity = dir * speed * Time.deltaTime;
        deltaCounter += Time.deltaTime*2;
    }
}
