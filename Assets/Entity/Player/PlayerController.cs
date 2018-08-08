using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 15.0f;
	public float padding = 1.0f;
	public float firingRate = 0.2f;
	public float health = 250f;
	public GameObject projectile;
	public GameObject LifeObject;
	public float projectileSpeed;
	private int lifeCount = 5;
	
	public AudioClip fireSound;
	public AudioClip hitSound;
	public AudioClip deathSound;
	

	float xmin;
	float xmax;
	
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;	
	}
	
	void Update () {
		MoveWithArrows ();
		shootLaser ();
		Alive ();
	}
	
	void MoveWithArrows () {
		if (Input.GetKey(KeyCode.A)) {	
			transform.position += Vector3.left * speed * Time.deltaTime;		
		} else if (Input.GetKey(KeyCode.D)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
	
	void Fire () {
		GameObject PlayerLaser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		PlayerLaser.rigidbody2D.velocity = new Vector3(0, projectileSpeed, 0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	
	// TODO have player shoot powerup and increase firing rate for certain amount of time!
	void shootLaser () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke ("Fire");
		}
	}
	
	// TODO have player shoot powerup and increase lifeCount by 1!
	
	void OnTriggerEnter2D (Collider2D collider) {
		Projectile missle = collider.gameObject.GetComponent<Projectile>();
		if (missle) {
			health -= missle.getDamage ();
			missle.Hit ();
			AudioSource.PlayClipAtPoint(hitSound, transform.position);
			if (health <= 0) {
				lifeCount--;
				DestroyCorrectLifeObject();
				health = 250f;			
			}
		}
	}
	
	void DestroyCorrectLifeObject () {
		if (lifeCount == 4) {
			Destroy(GameObject.Find("LifeObject1"));
			
		}
		if (lifeCount == 3) {
			Destroy(GameObject.Find("LifeObject2"));
			
		}
		if (lifeCount == 2) {
			Destroy(GameObject.Find("LifeObject3"));
			
		}
		if (lifeCount == 1) {
			Destroy(GameObject.Find("LifeObject4"));
			
		}
		if (lifeCount == 0) {
			Destroy(GameObject.Find("LifeObject5"));
			
		}
	}
	
	void Alive () {
		if (lifeCount < 0) {
			Die ();
		}
	}
	
	void Die () {
		Destroy(gameObject);
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Lose");
	}
}