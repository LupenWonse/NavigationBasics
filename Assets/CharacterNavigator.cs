using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavigator : MonoBehaviour {

	private float stepSize = 1.0f;

	// Use this for initialization
	void Start () {
		calculateMovement(new Vector2(2,2));
	}
	
	// Update is called once per frame
	void Update () {
		
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
				possiblePosition.x = position.x + col * stepSize;
				possiblePosition.y = position.y + row * stepSize;

				if (isPositionFree(possiblePosition)){
					neighbours.Add(possiblePosition);
				}

			}
		}
		return neighbours;
	}

	bool isPositionFree(Vector2 position){
		return true;
	}

	void calculateMovement(Vector2 target){
		// Get current location
		Vector2 currentLocation;
		currentLocation.x = transform.position.x;
		currentLocation.y = transform.position.z;
		// From the current location find all possible movements
		List<Vector2> neighbours = findPossibleNeighbours(currentLocation);


		float minCost = float.MaxValue;
		Vector2 nextLocation = Vector2.zero;
		//DEBUG
		foreach(Vector2 neighbour in neighbours){
			print(neighbour.ToString());
			print(calculateLocationCost(neighbour,target).ToString());

			if (calculateLocationCost(neighbour,target) < minCost){
				minCost = calculateLocationCost(neighbour,target);
				nextLocation = neighbour;
			}
		}

		print(nextLocation.ToString());


		// For all possible movement find the costs

		// Choose the location with the lowest cost

		// Repeat until 
	}

    private float calculateLocationCost(Vector2 position, Vector2 target)
    {
        return (target - position).magnitude;
    }
}
