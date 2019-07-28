using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilScript : MonoBehaviour {

    public GameObject[] bullet = new GameObject[3];

    float moveTimer;
    float moveTimer_limit;
    float dir;
    float shootingTimer = 3;
    Vector3 shootingDir = Vector3.zero;
    bool isShooting = false;

    // Use this for initialization
    void Start () {
        moveTimer_limit = 3;
        moveTimer = moveTimer_limit;
        dir = 0.3f;
    }
	
	// Update is called once per frame
	void Update () {
        moveTimer -= Time.deltaTime;
        shootingTimer -= Time.deltaTime;
        if (moveTimer < 0)
        {
            moveTimer = Random.Range(2, 6);
            int nextDirection = Random.Range(0, 2);
            if (nextDirection == 0)
                dir *= -1;
        }
        if(shootingTimer < 0)
        {
            shootingTimer = Random.Range(4, 9);
            isShooting = true;
        }
        
        transform.Translate(dir * Time.deltaTime, 0, 0) ;
        if(isShooting)
        {
            int whichObject = Random.Range(0, 3);
            float x = Random.Range(0, 70) / (float)10;
            float z = (Random.Range(50, 120)-30) / (float)10;
            shootingDir.x = x;
            shootingDir.z = z;
            shootingDir.Normalize();
            isShooting = false;
            GameObject hello = GameObject.Instantiate(bullet[whichObject], transform.position, bullet[whichObject].transform.rotation) as GameObject;
            hello.GetComponent<bulletScript>().dir = shootingDir;
        }
	}
}
