using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavigator : MonoBehaviour {

	private float stepSize = 1.0f;
	private List<Vector2> navigationPath = new List<Vector2>();
	[SerializeField] LayerMask obstacles;

	// Use this for initialization
	void Start () {

		Vector2 currentLocation;
		currentLocation.x = transform.position.x;
		currentLocation.y = transform.position.z;

		Vector2 target = new Vector2(5,0);

		calculateMovement(currentLocation,target);
		GetComponent<CharacterMover>().navigationPath = navigationPath;
		//TODO DEBUG
		foreach(Vector2 positionToVisit in navigationPath){
			print(positionToVisit.ToString());
		}
	}
	
	// Update is called once per frame
	void Update () {
		print(Physics.CheckBox(new Vector3(1,1,1),new Vector3(0.5f,0.5f,0.5f),Quaternion.identity,obstacles));
	}

	// Sets a new target for the character
	void setNewTarget(Vector2 target){

	}

	// Finds all possible movement possibilities for a given location
	List<Vector2> findPossibleNeighbours(Vector2 position){
		List<Vector2> neighbours = new List<Vector2>();
		Vector2 possiblePosition;
		for (int row = -1; row <= 1; row++){
			for (int col = -1; col <=1; col++){
				
				if (row == 0 && col == 0){
					continue;
				}
				
				possiblePosition.x = position.x + col * stepSize;
				possiblePosition.y = position.y + row * stepSize;

				if (isPositionFree(possiblePosition)){
					neighbours.Add(possiblePosition);
				} else {
					print("NOT FREE:" + possiblePosition.ToString());
				}

			}
		}
		return neighbours;
	}

	bool isPositionFree(Vector2 position){
		return !(Physics.CheckBox(new Vector3(position.x,transform.position.y,position.y),new Vector3 (0.4f,0.4f,0.4f),Quaternion.identity,obstacles));
	}

	void calculateMovement(Vector2 currentLocation, Vector2 target){
		List<Vector2> neighbours = findPossibleNeighbours(currentLocation);

		// For all possible movement find the costs
		float minCost = float.MaxValue;
		Vector2 nextLocation = Vector2.zero;
		foreach(Vector2 neighbour in neighbours){
			if (calculateLocationCost(neighbour,target) < minCost){
				minCost = calculateLocationCost(neighbour,target);
				nextLocation = neighbour;
			}
		}

		if (isTarget(nextLocation,target)){
			// We reached our target
			navigationPath.Add(target);
		} else {
			// Choose the location with the lowest cost
			// And repeat recursively
			navigationPath.Add(nextLocation);
			calculateMovement(nextLocation,target);
		}
	}

	// Use a isTarget function to make sure a location is within half a step size
	// of the target
    private bool isTarget(Vector2 nextLocation, Vector2 target)
    {
        return ((target-nextLocation).magnitude < stepSize/2);
    }

    private float calculateLocationCost(Vector2 position, Vector2 target)
    {
        return (target - position).magnitude;
    }
}
