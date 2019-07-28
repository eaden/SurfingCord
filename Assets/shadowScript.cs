using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowScript : MonoBehaviour {

    public GameObject player;

    Vector3 startingPos;

	// Use this for initialization
	void Start () {
        startingPos = transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.transform.position.x, startingPos.y, player.transform.position.z);
	}
}
