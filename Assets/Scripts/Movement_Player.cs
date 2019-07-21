using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Player : MonoBehaviour {

    Vector3 _dir = Vector3.zero;
    Vector3 velocitySave = Vector3.zero;
    Rigidbody rigid = null;
    float speed = 25;
    float speedLeft = 0;
    float speedRight= 0;
    float speedUp = 0;
    float speedDown = 0;
    float upper = 6;
    bool isMoving = false;
    bool movingLeft = false;
    bool movingRight = false;
    bool movingUp = false;
    bool movingDown = false;
    float speedHorizontal = 0;
    float speedVertical = 0;
    Vector3 pos;

    // Use this for initialization
    void Start () {
        if(!rigid)
        rigid = GetComponent<Rigidbody>();
        pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        pos = transform.position;
        if (transform.position.x > 6)
            transform.position = new Vector3(6, pos.y, pos.z);
        if (transform.position.x < -6)
            transform.position = new Vector3(-6, pos.y, pos.z);
        if (transform.position.z > 4.1f)
            transform.position = new Vector3(pos.x, pos.y, 4.1f);
        if (transform.position.z < -4.1f)
            transform.position = new Vector3(pos.x, pos.y, -4.1f);

        //--Reset direction--//
        _dir.x = 0;
        _dir.y = 0;
        _dir.z = 0;

        movingLeft = false;
        movingRight = false;
        movingUp = false;
        movingDown = false;

        //--Update user input--//
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _dir.z += 300;
            speedUp += upper;
            isMoving = true;
            movingUp = true;
        }
            
         else if (Input.GetKey(KeyCode.DownArrow))
        {
            _dir.z -= 300;
            speedDown += upper;
            isMoving = true;
            movingDown = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _dir.x -= 300;
            speedLeft += upper;
            isMoving = true;
            movingLeft = true;
        }
            
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _dir.x += 300;
            speedRight += upper;
            isMoving = true;
            movingRight = true;
        }
        if(!movingDown  && !movingUp && !movingLeft && !movingRight)
            isMoving = false;

        if (speed > 180)
            speed = 180;
        if (speedLeft > 180)
            speedLeft = 180;
        if (speedRight > 180)
            speedRight = 180;
        if (speedUp > 180)
            speedUp = 180;
        if (speedDown > 180)
            speedDown = 180;

        if (movingLeft)
            speedHorizontal = speedLeft;
        if (movingRight)
            speedHorizontal = speedRight;
        if (movingUp)
            speedVertical = speedUp;
        if (movingDown)
            speedVertical = speedDown;

        //--Normalize vector--//
        _dir.Normalize();
        //--Also possible--// 
        //_dir = _dir.normalized;
        


        /*
        if (_dir.x > 0.1f || _dir.z > 0.1f || _dir.x < -0.1f || _dir.z < -0.1f)
        {
            speed += 5;
            rigid.velocity = _dir * speed * Time.deltaTime;
        }*/
        if (movingLeft)
        {
            speedRight -= 6;
            if (speedRight < 20)
                speedRight = 20;
        }
        if (movingRight)
        {
            speedLeft -= 6;
            if (speedLeft < 20)
                speedLeft = 20;
        }
        if (movingUp)
        {
            speedDown -= 6;
            if (speedDown < 20)
                speedDown = 20;
        }
        if (movingDown)
        {
            speedUp -= 6;
            if (speedUp < 20)
                speedUp = 20;
        }

        if (isMoving)
        {
            _dir.x *= speedHorizontal;
            _dir.z *= speedVertical;
            rigid.velocity = _dir * Time.deltaTime;
            //rigid.velocity = _dir * speed * Time.deltaTime; // alt. speed ist alt
        }
        else
        {
            rigid.velocity *= 0.95f;
            speedLeft -= 6;
            if (speedLeft < 20)
                speedLeft = 20;
            speedRight -= 6;
            if (speedRight < 20)
                speedRight = 20;
            speedUp -= 6;
            if (speedUp < 20)
                speedUp = 20;
            speedDown -= 6;
            if (speedDown < 20)
                speedDown = 20;
            /*
            speed -= 5;
            if (speed < 20)
                speed = 20;
                */
        }
        

        if (rigid.velocity.magnitude < 0.2f)
            rigid.velocity = Vector3.zero;


    }
}
