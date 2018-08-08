using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public GameObject projectile;
	public float projectileSpeed;
	public float firingRate = 0.2f;
	public float health = 150f;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 150;
	
	public AudioClip fireSound;
	public AudioClip deathSound;
	
	
	private ScoreKeeper scoreKeeper;
	
	void Start () {
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	}
	
	void Update () {
		float probability = Time.deltaTime * shotsPerSecond;
		if (Random.value < probability) {
			Fire ();
		}
	}
	
	void Fire () {
		GameObject missle = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		missle.rigidbody2D.velocity = new Vector2(0, -projectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
		
	}

	void OnTriggerEnter2D (Collider2D collider) {
		Projectile missle = collider.gameObject.GetComponent<Projectile>();
		if (missle) {
			health -= missle.getDamage();
			missle.Hit();
			if (health <= 0) {
				Die();
			}
		}
	}
	
	void Die () {
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		Destroy(gameObject);
		scoreKeeper.GetScore(scoreValue);
	}
}