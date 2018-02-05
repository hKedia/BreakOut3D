using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour {

	static int numBlocks = 0;
	int hitPoints = 1;
	public AudioClip[] explodeAudio;


	public enum BlockType{
		Red = 10,
		Green = 25,
		Blue = 50,
		Yellow = 75,
		Brick = 125
	}

	public BlockType bType = BlockType.Red;
	// Use this for initialization
	void Start () {
		numBlocks++;
		MeshRenderer mRenderer = GetComponent<MeshRenderer>();
		mRenderer.material = Resources.Load ("Materials/block" + bType.ToString()) as Material;
		if (bType == BlockType.Brick) {
			hitPoints = 2;
		}
	}

	void OnCollisionEnter(Collision col){
		AudioSource.PlayClipAtPoint (explodeAudio[Random.Range(0,explodeAudio.Length - 1)], transform.position, 0.25f);
		hitPoints--;
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
