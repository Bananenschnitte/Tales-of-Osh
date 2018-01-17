using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

/**
 * Controles the players character.
 */
public class PlayerController : MonoBehaviour {

	public float moveSpeed = 6.0f;	
	public float rotateSpeed = 20f;
	public float gravity = 20f;
	

	private Rigidbody rigidBody;
	private CharacterController characterController;
	private Vector3 moveDirection = Vector3.zero;
	

	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		characterController = GetComponent<CharacterController>();
	}

	void Update () {
		move();
	}

	void move() {
		if (characterController.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //moveDirection = transform.TransformDirection(moveDirection); //Active this line for local transform
            moveDirection *= moveSpeed;
		}
		
		moveDirection.y -= gravity * Time.deltaTime;
		characterController.Move(moveDirection * Time.deltaTime);				

		moveDirection.y = 0.0f;
		float step = rotateSpeed * Time.deltaTime;		
		Vector3 newDir = Vector3.RotateTowards(transform.forward, moveDirection, step, 0.0f);
		transform.rotation = Quaternion.LookRotation(newDir);
	}


}
