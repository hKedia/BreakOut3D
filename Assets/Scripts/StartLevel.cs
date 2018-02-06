using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour {

	// Use this for initialization
	public void Play () {
        SceneManager.LoadScene("Level 1");
	}

    public void Quit()
    {
        Application.Quit();
    }
}
