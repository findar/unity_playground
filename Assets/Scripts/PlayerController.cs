using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float movementSpeed = 6f;

	private Vector3 movement;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;
	bool isFalling = false;

	void Start ()
	{
		playerRigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		//Input.GetAxisRaw is -1, 0, or 1 and directly correlated to up/down/etc
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		Move(h, v);
		Animating(h, v);
	}

	void Update()
	{
		if (Input.GetKeyDown("space") && !isFalling)
        {
            isFalling = true;
            playerRigidbody.velocity += movementSpeed * Vector3.up;
        }
	}

	void OnCollisionStay(Collision collisionInfo)
	{
		//we are on something
		isFalling = false;
	}

	void Move(float h, float v)
	{
		movement.Set(h, 0f, v);
		// Delta time prevents flying
		movement = movement.normalized * movementSpeed * Time.deltaTime;

		// make the player face the direction they are running towards
		// taken from: http://answers.unity3d.com/questions/803365/make-the-player-face-his-movement-direction.html
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
		transform.Translate (movement * movementSpeed * Time.deltaTime, Space.World);

		playerRigidbody.MovePosition(transform.position + movement); 
	}

	void Animating(float h, float v)
	{
		// stubbed out since we don't have an animation controller for now
		// bool walking = (h != 0f) || (v != 0f);
		// anim.SetBool("IsWalking", walking);
	}
}
