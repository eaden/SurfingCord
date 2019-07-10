using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Player : MonoBehaviour {

    Vector3 _dir = Vector3.zero;
    Vector3 velocitySave = Vector3.zero;
    Rigidbody rigid = null;
    float speed = 20;

	// Use this for initialization
	void Start () {
        if(!rigid)
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        //--Reset direction--//
        _dir.x = 0;
        _dir.y = 0;
        _dir.z = 0;

        //--Update user input--//
        if (Input.GetKey(KeyCode.UpArrow))
            _dir.z += 300;
        if (Input.GetKey(KeyCode.DownArrow))
            _dir.z -= 300;
        if (Input.GetKey(KeyCode.LeftArrow))
            _dir.x -= 300;
        if (Input.GetKey(KeyCode.RightArrow))
            _dir.x += 300;

        //--Normalize vector--//
        _dir.Normalize();
        //--Also possible--// 
        //_dir = _dir.normalized;
        if (speed > 150)
            speed = 150;

        if(_dir.x > 0.1f || _dir.z > 0.1f || _dir.x < -0.1f || _dir.z < -0.1f)
        {
            speed += 5;
            rigid.velocity = _dir * speed * Time.deltaTime;
        }

        //--Move object with the rigidbody component--//
        
        else
        {
            rigid.velocity *= 0.95f;
            speed -= 5;
            if (speed < 20)
                speed = 20;
        }
        if (rigid.velocity.magnitude < 0.2f)
            rigid.velocity = Vector3.zero;


    }
}
