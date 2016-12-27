using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;

	private GameController gc;

	void Start() {
		gc = GameObject.
			FindWithTag("GameController").
			GetComponent<GameController>();
	}

	void OnTriggerEnter(Collider c) {
		if (c.tag == "Boundary" || c.tag == "Asteroid") {
			return;
		}

		Destroy(c.gameObject);
		Destroy(gameObject);

		Instantiate(explosion, transform.position, transform.rotation);

		if (c.tag == "Player") {
			Instantiate(playerExplosion,
				c.transform.position, c.transform.rotation);

            gc.GameOver();
			EventManager.TriggerEvent(EventManager.onPlayerDeath);
		}

		else {
			gc.AddScoreByDestroyingAsteroid();
		}
	}
}
