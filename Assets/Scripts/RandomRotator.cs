using UnityEngine;

public class RandomRotator : MonoBehaviour {

	public float maxTumble;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().angularVelocity =
			Random.insideUnitSphere * maxTumble;
	}
}
