using UnityEngine;
using System.Collections;


public class PlayerScript : MonoBehaviour
{
	/// <summary>
	/// 1 - The speed of the ship
	/// </summary>
	public Vector3 speed = new Vector3(50, 50, 0);

	public float linearDamping = 0.5f;

	public float angularDamping = 0.5f;

	private Vector3 screenPoint;
	private Vector3 offset;

	// 2 - Store the movement
	private Vector3 movement;

	static int selectedCoin = 0;
	
	void Update()
	{
		// 3 - Retrieve axis information
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		
		// 4 - Movement per direction
		movement = new Vector3(
			speed.x * inputX,
			speed.y * inputY,
			0.0f);
	}
	
	void FixedUpdate()
	{
		// 5 - Move the game object
		rigidbody.velocity = movement;
		rigidbody.drag = linearDamping;
		rigidbody.angularDrag = angularDamping;
	}

	void OnDrawGizmos() {
		Vector3 center = renderer.bounds.center;
		float radius = renderer.bounds.extents.magnitude;
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(center, radius);
	}

	void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(
			new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
	
	
	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	}
}

