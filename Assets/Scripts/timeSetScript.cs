using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeSetScript : MonoBehaviour {

    public Text text;

	// Use this for initialization
	void Start () {
        text.text = "Your score: "+GameManager.time*2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
