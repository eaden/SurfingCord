using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBody : MonoBehaviour {

    Movement_Player playerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (!playerScript.isHit && other.gameObject.tag != "Player")
            playerScript.isHit = true;
    }

    // Use this for initialization
    void Start () {
        playerScript = transform.parent.GetComponent<Movement_Player>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
