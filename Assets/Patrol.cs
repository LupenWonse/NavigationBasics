using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Patrol : MonoBehaviour {

[SerializeField] Vector2[] waypoints;

private CharacterNavigator navigator;
private int currentWaypoint;
private Vector2 currentPosition;

	// Use this for initialization
	void Start () {

		navigator = GetComponent<CharacterNavigator>();
		currentWaypoint = 0;
		gotoWaypoint(currentWaypoint);
	}
	
	// Update is called once per frame
	void Update () {
		currentPosition.x = transform.position.x;
		currentPosition.y = transform.position.z;

		if (currentPosition == waypoints[currentWaypoint]){
			gotoNextWaypoint();
		}
	}

	public void gotoWaypoint(int waypoint){
		navigator.setNewTarget(waypoints[waypoint]);
	}

	void gotoNextWaypoint(){
		currentWaypoint++;

		if (currentWaypoint >= waypoints.Length){
			currentWaypoint = 0;
		}
		gotoWaypoint(currentWaypoint);
	}
}
