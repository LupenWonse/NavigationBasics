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
		Vector3 target = transform.position;

		// Check there are waypoints in the path
		if (navigationPath.Count > 0){
			target.x = navigationPath[0].x;
			target.z = navigationPath[0].y;
			
		// New Location
			Vector3 nextLocation = Vector3.MoveTowards(transform.position,target,1f);
		
			if (Physics.CheckBox(nextLocation,new Vector3(0.4f,0.4f,0.4f),Quaternion.identity,LayerMask.GetMask(new string[] {"Obstacles"}))){
				GetComponent<CharacterNavigator>().setNewTarget(navigationPath[navigationPath.Count-1]);
			} else {
				rigidbody.MovePosition(Vector3.MoveTowards(transform.position,target,movementSpeed));
			}
			
			if ((transform.position-target).magnitude<0.1){
				navigationPath.RemoveAt(0);
			}
		}
	}
}
