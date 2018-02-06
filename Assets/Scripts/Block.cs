using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour {

	static int numBlocks = 0;
	int hitPoints = 1;
	public AudioClip[] explodeAudio;
    private GameObject player;


	public enum BlockType{
        //PowerUp
		Red = 10,
		Green = 25,
        //PowerDown
		Purple = 50,
		Yellow = 75,
        //Generic
        Orange = 100,
        Blue = 125
	}

	public BlockType bType = BlockType.Red;
	// Use this for initialization
	void Start () {
		numBlocks++;
		MeshRenderer mRenderer = GetComponent<MeshRenderer>();
		mRenderer.material = Resources.Load ("Materials/block" + bType.ToString()) as Material;
		if (bType == BlockType.Blue || bType == BlockType.Orange) {
			hitPoints = 2;
		}
    }

	void OnCollisionEnter(Collision col){
		AudioSource.PlayClipAtPoint (explodeAudio[Random.Range(0,explodeAudio.Length - 1)], transform.position, 0.25f);
		hitPoints--;

        player = GameObject.FindGameObjectWithTag("Player");
        // Power Up
        if (bType == BlockType.Red)
        {
            player.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            
        }
        if (bType == BlockType.Green)
        {
            Debug.Log("Green Block Hit");
            player.GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity * 5;
            player.GetComponent<Rigidbody>().mass = player.GetComponent<Rigidbody>().mass * 2;
        }

        // Power Down
        if (bType == BlockType.Purple)
        {
            player.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        }
        if (bType == BlockType.Yellow)
        {
            Debug.Log("Yellow Block Hit");
            player.GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity / 5;
            player.GetComponent<Rigidbody>().mass = player.GetComponent<Rigidbody>().mass / 2;
        }
        if (hitPoints <= 0) {
			Die();
		}
	}

	void Die(){
		GameManager.score += (int)bType;
		Destroy (gameObject);
		numBlocks--;
		if (numBlocks <= 0) {
            SceneManager.LoadScene("Win");
		}
	}
}
