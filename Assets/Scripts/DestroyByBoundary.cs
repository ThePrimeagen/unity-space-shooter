using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit(Collider c) {
		Destroy(c.gameObject);

		if (c.tag == "Asteroid") {
			GameObject gc = GameObject.FindWithTag("GameController");
			gc.GetComponent<GameController>().AddScoreByDodgingAsteroid();
		}
	}
}
