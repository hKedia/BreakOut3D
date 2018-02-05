using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<GUIText>().text = "Score: " + GameManager.score;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown){
			Destroy(GameObject.FindGameObjectWithTag("GameController"));
			SceneManager.LoadScene("Level1");
		}
	}
}
