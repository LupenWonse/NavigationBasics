using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavigator : MonoBehaviour {

	[SerializeField] private int maxIterations = 0;
	private int iterations = 0;
	private float stepSize = 1.0f;
	private List<Vector2> navigationPath = new List<Vector2>();
	public LayerMask obstacles;

	// Use this for initialization
	void Start () {
	}

	// Sets a new target for the character
	public void setNewTarget(Vector2 target){
		navigationPath = new List<Vector2>();
		
		Vector2 currentLocation;
		currentLocation.x = transform.position.x;
		currentLocation.y = transform.position.z;
		if(calculateMovement(currentLocation, currentLocation, target)){
			GetComponent<CharacterMover>().navigationPath = navigationPath;
		} else {
			Debug.LogWarning("Navigation Failed. Check iteration Count Or Loop");
			calculateMovement(currentLocation, currentLocation, target);
		}
	}

	// Finds all possible movement possibilities for a given location
	List<Vector2> findPossibleNeighbours(Vector2 position, Vector2 previousPosition){
		List<Vector2> neighbours = new List<Vector2>();
		Vector2 possiblePosition;
		for (int row = -1; row <= 1; row++){
			for (int col = -1; col <=1; col++){
				
				possiblePosition.x = position.x + col * stepSize;
				possiblePosition.y = position.y + row * stepSize;

				if (possiblePosition == position || possiblePosition == previousPosition){
					continue;
				}

				if (isPositionFree(possiblePosition)){
					neighbours.Add(possiblePosition);
				}
			}
		}
		return neighbours;
	}

	bool isPositionFree(Vector2 position){
		return !Physics.CheckBox(new Vector3(position.x,transform.position.y,position.y),new Vector3 (0.4f,0.4f,0.4f),Quaternion.identity,obstacles);
	}

	bool calculateMovement(Vector2 currentLocation, Vector2 previousPosition, Vector2 target){
		iterations ++;
		if (iterations > maxIterations){
			iterations = 0;
			return false;
		}

		List<Vector2> neighbours = findPossibleNeighbours(currentLocation, previousPosition);

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
			iterations = 0;
			return true;
		} else {
			// Choose the location with the lowest cost
			// And repeat recursively
			navigationPath.Add(nextLocation);
			return calculateMovement(nextLocation,currentLocation,target);
		}
	}

	// Use a isTarget function to make sure a location is within half a step size
	// of the target
    private bool isTarget(Vector2 nextLocation, Vector2 target)
    {
        return ((target-nextLocation).magnitude -0.1f <= stepSize/2);
    }

    private float calculateLocationCost(Vector2 position, Vector2 target)
    {
        return (target - position).magnitude;
    }
}
