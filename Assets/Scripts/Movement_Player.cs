using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Player : MonoBehaviour {

    public GameObject head;
    public GameObject body;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftLeg;
    public GameObject rightLeg;
    public GameObject hitZoneBody;

    List<GameObject> bodyParts = new List<GameObject>();

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

    public bool isHit = false;
    bool isDucking = false;

    bool isMoving = false;
    bool isJumping = false;
    bool movingLeft = false;
    bool movingRight = false;
    bool movingUp = false;
    bool movingDown = false;
    float speedHorizontal = 0;
    float speedVertical = 0;
    Vector3 hitZoneBodyOriginalPos;
    Vector3 hitZoneBodyPos;
    Vector3 originalPos;
    Vector3 pos;
    Vector3 headOriginalPos;
    Vector3 bodyOriginalPos;
    Vector3 leftHandOriginalPos;
    Vector3 rightHandOriginalPos;
    Vector3 leftLegOriginalPos;
    Vector3 rightLegOriginalPos;

    float jumpSquare_set = 2;
    float jumpSquare = 2;
    bool jumpIsBeginning = false;
    bool jumpIsEnding = false;
    Vector3 jumpVector = new Vector3(0, -0.01f, 0);
    float forCos = 0;

    float duckingTimer;
    float duckingTimer_limit;

    float hitTimer;
    float hitTimer_limit;

    // Use this for initialization
    void Start () {
        if(!rigid)
        rigid = GetComponent<Rigidbody>();
        originalPos = transform.position;
        pos = originalPos;
        hitZoneBodyOriginalPos = hitZoneBody.transform.position;
        hitZoneBodyPos = hitZoneBodyOriginalPos;

        headOriginalPos = head.transform.position;
        bodyOriginalPos = body.transform.position;
        leftHandOriginalPos = leftHand.transform.position; ;
        rightHandOriginalPos = rightHand.transform.position; ;
        leftLegOriginalPos = leftLeg.transform.position; ;
        rightLegOriginalPos = rightLeg.transform.position; ;

        jumpSquare_set = 2;
        jumpSquare = jumpSquare_set;

        hitTimer_limit = 1f;
        hitTimer = hitTimer_limit;

        duckingTimer_limit = 1f;
        duckingTimer = duckingTimer_limit;


        bodyParts.Add(head);
        bodyParts.Add(body);
        bodyParts.Add(leftHand);
        bodyParts.Add(rightHand);
        bodyParts.Add(leftLeg);
        bodyParts.Add(rightLeg);
    }

    private void OnTriggerEnter(Collider other)
    {
        isHit = true;
    }

    private void LateUpdate()
    {
           
    }

    // Update is called once per frame
    void Update () {
        pos = transform.position;
        hitZoneBodyPos = hitZoneBody.transform.position;
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

        // Player is hit
        if (isHit)
        {
            hitTimer -= Time.deltaTime;

            if (hitTimer < 0)
            {
                isHit = false;
                hitTimer = hitTimer_limit;
                foreach (var item in bodyParts)
                {
                    item.GetComponent<MeshRenderer>().enabled = true;
                }
            }
            if (hitTimer < 0.2f)
            foreach (var item in bodyParts)
            {
                item.GetComponent<MeshRenderer>().enabled = false;
            }
            else if (hitTimer < 0.4f)
                foreach (var item in bodyParts)
                {
                    item.GetComponent<MeshRenderer>().enabled = true;
                }
            else if (hitTimer < 0.6f)
                foreach (var item in bodyParts)
                {
                    item.GetComponent<MeshRenderer>().enabled = false;
                }
            else if (hitTimer < 0.8f)
                foreach (var item in bodyParts)
                {
                    item.GetComponent<MeshRenderer>().enabled = true;
                }
            else if (hitTimer < 1f)
                foreach (var item in bodyParts)
                {
                    item.GetComponent<MeshRenderer>().enabled = false;
                }
        }
        


        // jumping
        if (!isJumping && Input.GetKey(KeyCode.Space) && !isDucking)
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

        }

        // ducking
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isJumping)
        {
            isDucking = true;
            hitZoneBody.transform.position = new Vector3(hitZoneBodyPos.x, -1, hitZoneBodyPos.z);
            head.transform.position = new Vector3(head.transform.position.x, head.transform.position.y - 0.6f, head.transform.position.z);
            body.transform.position = new Vector3(body.transform.position.x, body.transform.position.y - 0.6f, body.transform.position.z);
            leftHand.transform.position = new Vector3(leftHand.transform.position.x, leftHand.transform.position.y - 0.6f, leftHand.transform.position.z);
            rightHand.transform.position = new Vector3(rightHand.transform.position.x, rightHand.transform.position.y - 0.6f, rightHand.transform.position.z);
        }
        if(isDucking)
        {
            duckingTimer -= Time.deltaTime;
            if(duckingTimer < 0)
            {
                hitZoneBody.transform.position = new Vector3(hitZoneBodyPos.x, hitZoneBodyOriginalPos.y, hitZoneBodyPos.z);
                head.transform.position = new Vector3(head.transform.position.x, head.transform.position.y + 0.6f, head.transform.position.z);
                body.transform.position = new Vector3(body.transform.position.x, body.transform.position.y + 0.6f, body.transform.position.z);
                leftHand.transform.position = new Vector3(leftHand.transform.position.x, leftHand.transform.position.y + 0.6f, leftHand.transform.position.z);
                rightHand.transform.position = new Vector3(rightHand.transform.position.x, rightHand.transform.position.y + 0.6f, rightHand.transform.position.z);

                isDucking = false;
                duckingTimer = duckingTimer_limit;
            }

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
