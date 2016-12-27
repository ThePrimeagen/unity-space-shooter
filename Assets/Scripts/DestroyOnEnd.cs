using UnityEngine;
using UnityEngine.Events;

public class DestroyOnEnd : MonoBehaviour {
	public GameObject explosion;

	private UnityAction listener;

	void Awake() {
		if (listener == null) {
			listener = new UnityAction(DestroyOnPlayerDeath);
		}
	}

	void OnEnable() {
		EventManager.AddListener(EventManager.onPlayerDeath, listener);
	}

	void OnDisable() {
		EventManager.RemoveListener(EventManager.onPlayerDeath, listener);
	}

	void OnDestroy() {
		listener = null;
	}

	void DestroyOnPlayerDeath() {
		Destroy(gameObject);
		Instantiate(explosion, transform.position, transform.rotation);
	}
}
