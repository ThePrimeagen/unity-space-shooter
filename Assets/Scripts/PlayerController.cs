using UnityEngine;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
	public float speed;
	public float tilt;

	public Boundary b;

	public GameObject shot;
	public Transform shotSpawn;

	public float shotRate;

	private float lastShot = 0;

	void Update() {

		if (Time.time > lastShot && Input.GetButton("Fire1")) {
			lastShot = Time.time + shotRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play();
		}
	}

	void FixedUpdate() {
		Rigidbody rb = GetComponent<Rigidbody>();
		float mHor = Input.GetAxis("Horizontal");
		float mVert = Input.GetAxis("Vertical");

		Vector3 mov = new Vector3(mHor, 0f, mVert).normalized * speed;
		rb.velocity = mov;

		float boundX = Mathf.Clamp(rb.position.x, b.xMin, b.xMax);
		float boundY = 0;
		float boundZ = Mathf.Clamp(rb.position.z, b.zMin, b.zMax);

		rb.position = new Vector3(boundX, boundY, boundZ);

		rb.rotation = Quaternion.Euler(0f, 0f, -mov.x * tilt);
	}
}
