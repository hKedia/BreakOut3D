using UnityEngine;
using System.Collections;

public class DeathWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider other){
		GameManager.instanceOf.MissedBall(other.gameObject);
	}

}
