﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour {

	[SerializeField] private float movementSpeed;
	public List<Vector2> navigationPath = new List<Vector2>();
	
	// Update is called once per frame
	void Update () {
		Vector3 target = transform.position;

		// Check there are waypoints in the path
		if (navigationPath.Count > 0){
			target.x = navigationPath[0].x;
			target.z = navigationPath[0].y;
			
			transform.position = Vector3.MoveTowards(transform.position,target,Time.deltaTime * movementSpeed);

			if (transform.position.Equals(target)){
				navigationPath.RemoveAt(0);
			}
		}
	}
}