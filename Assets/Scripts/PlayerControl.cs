using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	public GameObject bullet;

	public string controller = "Keyboard 1";
	public string horizontal = "Horizontal";
	public string vertical = "Vertical";
	public float speed = 1.0f;
	private Vector3 fireDirection;

	public float shootDelay = 0.1f;

	private bool canShoot = true;


	void ResetShot () {
		canShoot = true;
	}


	// Update is called once per frame
	void Update () {
		Vector3 direction = Vector3.right * Input.GetAxis(horizontalAxis()) + Vector3.up * Input.GetAxis(verticalAxis());

		if (direction.sqrMagnitude > 0.0f) {
			transform.position += direction.normalized * speed * Time.deltaTime;

			if (direction.x != 0) {
				SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
				sprite.flipX = direction.x < 0;

				fireDirection.x = direction.x;
			}

			if (direction.y != 0) {
				fireDirection.y = direction.y;
			}
		}

		if(canShoot) {
			Instantiate(bullet, transform.position, Quaternion.LookRotation(fireDirection, Vector3.up));

			canShoot = false;
			Invoke("ResetShot", shootDelay);
		}
	}

	private string horizontalAxis() {
		return controller + " " + horizontal;
	}

	private string verticalAxis() {
		return controller + " " + vertical;
	}
}
