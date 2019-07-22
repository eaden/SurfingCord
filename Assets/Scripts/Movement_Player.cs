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
    float upper = 7;
    float downer = 6;
    float maxSpeed = 200f;

    bool isMoving = false;
    bool isJumping = false;
    bool movingLeft = false;
    bool movingRight = false;
    bool movingUp = false;
    bool movingDown = false;
    float speedHorizontal = 0;
    float speedVertical = 0;
    Vector3 pos;

    float jumpSquare_set = 2;
    float jumpSquare = 2;
    bool jumpIsBeginning = false;
    bool jumpIsEnding = false;
    Vector3 jumpVector = new Vector3(0, -0.01f, 0);
    float forCos = 0;

    // Use this for initialization
    void Start () {
        if(!rigid)
        rigid = GetComponent<Rigidbody>();
        pos = transform.position;

        jumpSquare_set = 2;
        jumpSquare = jumpSquare_set;
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
        /*
        if (Input.GetKey(KeyCode.Z))
            transform.position = new Vector3(pos.x, 3, pos.z);
        if (Input.GetKey(KeyCode.H))
            transform.position = new Vector3(pos.x, 0.1f, pos.z);
        */
        //--Update user input--//


        // jumping
        if (!isJumping && Input.GetKey(KeyCode.Space))
        {
            isJumping = true;
        }
        if(isJumping)
        {
            transform.Translate(new Vector3(0,0.1f*Mathf.Cos(forCos)*1.5f, 0));
            forCos += Time.deltaTime*3.7f;
            if (forCos > 3.1f)
            {
                transform.position = new Vector3(pos.x, 0.1f, pos.z);
                forCos = 0;
                isJumping = false;
            }

            //if (transform.position.y > 2)
            //    jumpIsEnding = true;
            /*
            if (jumpIsEnding)
            {
                transform.Translate(jumpVector* jumpSquare);
                jumpSquare *= 1.05f;
            }
            if(transform.position.y < 0.1f)
            {
                transform.position = new Vector3(pos.x, 0.1f, pos.z);
                isJumping = false;
                jumpIsEnding = false;
                jumpSquare = jumpSquare_set;
            }
                */
        }

        // ducking
        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
        {
            
        }

        if (Input.GetKey(KeyCode.W))
        {
            _dir.z += 300;
            speedUp += upper;
            isMoving = true;
            movingUp = true;
        }
            
         else if (Input.GetKey(KeyCode.S))
        {
            _dir.z -= 300;
            speedDown += upper;
            isMoving = true;
            movingDown = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _dir.x -= 300;
            speedLeft += upper;
            isMoving = true;
            movingLeft = true;
        }
            
        else if (Input.GetKey(KeyCode.D))
        {
            _dir.x += 300;
            speedRight += upper;
            isMoving = true;
            movingRight = true;
        }
        if(!movingDown  && !movingUp && !movingLeft && !movingRight)
            isMoving = false;

        if (speed > maxSpeed)
            speed = maxSpeed;
        if (speedLeft > maxSpeed)
            speedLeft = maxSpeed;
        if (speedRight > maxSpeed)
            speedRight = maxSpeed;
        if (speedUp > maxSpeed)
            speedUp = maxSpeed;
        if (speedDown > maxSpeed)
            speedDown = maxSpeed;

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
            speedRight -= downer;
            if (speedRight < 20)
                speedRight = 20;
        }
        if (movingRight)
        {
            speedLeft -= downer;
            if (speedLeft < 20)
                speedLeft = 20;
        }
        if (movingUp)
        {
            speedDown -= downer;
            if (speedDown < 20)
                speedDown = 20;
        }
        if (movingDown)
        {
            speedUp -= downer;
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
            speedLeft -= downer;
            if (speedLeft < 20)
                speedLeft = 20;
            speedRight -= downer;
            if (speedRight < 20)
                speedRight = 20;
            speedUp -= downer;
            if (speedUp < 20)
                speedUp = 20;
            speedDown -= downer;
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
