using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void endGame()
    {
        Application.Quit();
    }


}
