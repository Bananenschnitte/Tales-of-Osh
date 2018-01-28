using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The controller for the main-chamera.
/// Like a 2 layerd frame. An outer and one inner window/Box.
/// If the player moves inside the inner window/box the camera still.
/// As soon the palyer moves outside the camera moves with the player
/// </summary>
public class CameraController : MonoBehaviour {

	/// <summary>
	/// The Offset of the x Axis
	/// </summary>
	public float xOffset = 5f;

	/// <summary>
	/// The Offset of the Z-Axis
	/// </summary>
	public float zOffset = 3f;

	/// <summary>
	/// The minimum value the difference has to be to do the movement
	/// </summary>
	public float treshold = 0.05f;

	/// <summary>
	/// The minimal Distance the camera has to keep to the player
	/// </summary>
	public float minDistanceToPlayer = 150f;
	
	/// <summary>
	/// Reference to the player to look at
	/// </summary>
	private Player player;

	/// <summary>
	/// The Main-Camera to move
	/// </summary>
	private Camera cam;

	void Start () {		
		player = Player.instance;
		cam = Camera.main;
	}
	
	void Update () {
		
		//	Get the y and x movment
		float x = getXMovement();
		float z = getZMovement();

		//	If any movement is neccassrymove it
		if (x != 0 || z != 0) {
			moveCamera(x, z);
		}
	}

	/// <summary>
	/// Determines the X-Movment to move the camera
	/// </summary>
	/// <returns>The amount of X-Movement</returns>
	private float getXMovement() {
		float diff = player.transform.position.x - cam.transform.position.x;
		float val = 0f;

		if (diff > xOffset) {
			val = diff - xOffset;
			if (val > treshold) {
				return val;
			}
			
		}

		if (diff < xOffset * -1) {
			val = diff + xOffset;
			if (val < treshold * -1) {
				return val;
			}
			
		}
		
		return 0f;
	}

	/// <summary>
	/// Determines the Y-Movement to move the camera
	/// </summary>
	/// <returns>The amount of Y-Movement</returns>
	private float getZMovement() {
		float diff = player.transform.position.z - cam.transform.position.z - minDistanceToPlayer;
		float val = 0f;

		if (diff > zOffset) {
			val = diff - zOffset;
			if (val > treshold) {
				return val;
			}
		}

		if (diff < zOffset * -1) {
			val = diff + zOffset;
			if (val < treshold * -1) {
				return val;
			}			
		}

		return 0f;
	}

	/// <summary>
	/// Moves the camera with the given x and y Paaramters
	/// </summary>
	/// <param name="x">The amount of the movement in the x-axis</param>
	/// <param name="z">The amount of the movement in the y-Axis</param>
	private void moveCamera(float x, float z) {
		Vector3 offset = new Vector3(x, 0, z);
		Debug.Log("[CameraController] (z=" + z + ") (x=" + x + ")");
		cam.transform.position = cam.transform.position + offset;
	}

}
