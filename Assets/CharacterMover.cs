using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour {

	[SerializeField] private float movementSpeed;
	public List<Vector2> navigationPath = new List<Vector2>();
	private new Rigidbody rigidbody;
	
	void Start() {
		rigidbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate () {		
		Vector3 target = new Vector3();
		//Vector2 currentPosition = new Vector2(transform.position.x,transform.position.z);
		
		// Check there are waypoints in the path
		if (navigationPath.Count > 0){
			target.x = navigationPath[0].x;
			target.y = transform.position.y;
			target.z = navigationPath[0].y;
		} else {
			GetComponent<Patrol>().gotoNextWaypoint();
			return;
		}

		// Check if we are on our target
		if (transform.position == target){
			//print("I am at target");
			navigationPath.RemoveAt(0);
		} else {
			Vector3 movementDirection = (target - transform.position).normalized;// * 0.425f;
			Vector3 nextLocation = transform.position + movementDirection;

			// Check for collisions on our path
			foreach (Collider collider in Physics.OverlapBox(nextLocation,new Vector3(0.05f,0.5f,0.05f),Quaternion.identity,LayerMask.GetMask(new string[] {"Obstacles","Agents"}))){
				if (collider.gameObject != gameObject){
					print("I think I will hit something At: " + nextLocation.ToString());
					// Request new Path
					GetComponent<CharacterNavigator>().setNewTarget(navigationPath[navigationPath.Count-1]);
					return;
				}
			}
		rigidbody.MovePosition(Vector3.MoveTowards(transform.position,target,movementSpeed));
		}
	}
}
