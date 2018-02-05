/// <summary>
/// Get player input to move paddle on x-y plane
/// </summary>

using UnityEngine;
using System.Collections;

public class PlayerPaddle : MonoBehaviour {

	public float paddleDepth;
	public GameObject ballPrefab;
	public GameObject sceneLight;
	private Transform _t;
	private GameObject attachedBall;
	private Transform ballTransform;
	private Rigidbody ballRigidbody;
	private Light _sLight;

	void Awake(){
		_t = transform;
		_sLight = sceneLight.GetComponent<Light>();
	}

	// Use this for initialization
	void Start () {
		SpawnBall ();
	}

	public void SpawnBall(){
		if (ballPrefab == null) {
			Debug.Log("no linked ball prefab");
		}

		attachedBall = (GameObject)Instantiate (ballPrefab, transform.position + new Vector3(0,0,-0.5f), Quaternion.identity);
		StartCoroutine(FadeLightOn(_sLight));
		ballTransform = attachedBall.transform;
		ballRigidbody = attachedBall.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mouse = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, paddleDepth);
		if (mouse.x < Screen.width && mouse.x > 0 && mouse.y < Screen.height && mouse.y > 0) {
			mouse = Camera.main.ScreenToWorldPoint (mouse);
			_t.position = mouse;
		}

		if(attachedBall){
			ballTransform.position = transform.position + new Vector3(0,0,-0.5f);
			if(Input.GetButtonDown("LaunchButton") && _t.position.x < 4.5f && _t.position.x > -4.5f && _t.position.y > 0.5f &&_t.position.y < 9.5f){

				ballRigidbody.isKinematic = false;
				ballRigidbody.AddForce(Input.GetAxis("Mouse X") * -50f,Input.GetAxis("Mouse Y") * 50f,-300f);
				attachedBall = null;
				StartCoroutine(FadeLightOff(_sLight));
			}
		}
		
	}

	void OnCollisionEnter(Collision col){
		foreach (ContactPoint contact in col){
			if(contact.thisCollider == GetComponent<Collider>()){
				float englishHorizontal = contact.point.x - _t.position.x;
				float englishVertical = contact.point.y - _t.position.y;

				contact.otherCollider.GetComponent<Rigidbody>().AddForce(75f * englishHorizontal, 75f * englishVertical, 0);
			}
		}
	}

	IEnumerator FadeLightOff (Light light){
		while (light.intensity > 0) {
			light.intensity = Mathf.Lerp(light.intensity, -0.75f, Time.deltaTime);
			yield return null;
		}
	}

	IEnumerator FadeLightOn (Light light){
		while (light.intensity < 1) {
			light.intensity = Mathf.Lerp(light.intensity, 1.75f, 0.5f * Time.deltaTime);
			yield return null;
		}
	}
}
