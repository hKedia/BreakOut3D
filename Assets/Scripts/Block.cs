using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour {

	static int numBlocks = 0;
	int hitPoints = 1;
	public AudioClip[] explodeAudio;
    private GameObject player;
    public Rigidbody ballPrefab;
    private ParticleSystem _particle;
    public GameObject gameManager;
    public GameObject paddle;

    public enum BlockType{
        PaddleScaleDown = 35,
        SpeedUp = 40,
        ScaleDown = 45,

        Generic = 50,
        
        SpeedDown = 55,
        ScaleUp = 60,
        Multiply = 65,
        PaddleScaleUp = 70,
        Life = 75,
	}

	public BlockType type = BlockType.Generic;
	// Use this for initialization
	void Start () {
		numBlocks++;
		if (type == BlockType.Generic) {
			hitPoints = 2;
		}
    }

	void OnCollisionEnter(Collision col){
		AudioSource.PlayClipAtPoint (explodeAudio[Random.Range(0,explodeAudio.Length - 1)], transform.position, 0.25f);
		hitPoints--;

        player = col.gameObject;

        // Scale Up
        if (type == BlockType.ScaleUp)
        {
            player.transform.localScale += new Vector3(0.25f, 0.25f, 0.25f);
            
        }

        // Speed Up
        if (type == BlockType.SpeedUp)
        {
            player.GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity * 5;
            player.GetComponent<Rigidbody>().mass = player.GetComponent<Rigidbody>().mass * 2;
        }

        // Scale Down
        if (type == BlockType.ScaleDown)
        {
            player.transform.localScale -= new Vector3(0.25f, 0.25f, 0.25f);
        }

        //SpeedDown
        if (type == BlockType.SpeedDown)
        {
            player.GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity / 5;
            player.GetComponent<Rigidbody>().mass = player.GetComponent<Rigidbody>().mass / 2;
        }

        // Multiple ball
        if (type == BlockType.Multiply)
        {
            Rigidbody ballInstance;
            ballInstance = Instantiate(ballPrefab, player.transform.position, player.transform.rotation) as Rigidbody;
            ballInstance.isKinematic = false;
            ballInstance.velocity = player.GetComponent<Rigidbody>().velocity;
        }

        // Additional Life
        if (type == BlockType.Life)
        {
            gameManager.GetComponent<GameManager>().currentHearts++;

        }

        // Paddle Scale Up
        if (type == BlockType.PaddleScaleUp)
        {
            paddle.transform.localScale += new Vector3(0.25f, 0.25f, 0.25f);
        }

        // Paddle Scale Down
        if (type == BlockType.PaddleScaleDown)
        {
            paddle.transform.localScale -= new Vector3(0.25f, 0.25f, 0.25f);
        }
        if (hitPoints <= 0) {
			Die();
		}
	}

	void Die(){
		GameManager.score += (int)type;
        _particle = GetComponent<ParticleSystem>();
        _particle.Play();
        float totalDuration = _particle.duration + _particle.startLifetime;
        Destroy (gameObject, totalDuration);
		numBlocks--;
		if (numBlocks <= 0) {
            SceneManager.LoadScene("Win");
		}
	}
}
