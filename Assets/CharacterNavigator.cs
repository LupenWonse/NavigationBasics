using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavigator : MonoBehaviour {

	private struct Node{
		public float cost;
		public float distance;
		public Vector2 position;
		public Vector2 previous;
	}

	private List<Node> openNodes, closedNodes;
	[SerializeField] private int maxIterations = 0;
	private int iterations = 0;
	[SerializeField] private float stepSize =1.0f;
	private List<Vector2> navigationPath = new List<Vector2>();
	public LayerMask obstacles;

	// Sets a new target for the character
	public void setNewTarget(Vector2 target){
		navigationPath = new List<Vector2>();
		openNodes = new List<Node>();
		closedNodes = new List<Node>();
		
		Vector2 currentLocation;
		currentLocation.x = transform.position.x;
		currentLocation.y = transform.position.z;
		
		if(calculateMovement(currentLocation, target)) {
			print("setting new Target : " + target.ToString());
			GetComponent<CharacterMover>().navigationPath = navigationPath;
		} else {
			Debug.LogWarning("Navigation Failed. Check iteration Count Or Loop");
		}
	}

    private void generateNodeGraph()
    {
        
    }

	void Start(){
		
	}

	void updateNeighbours(Node parent , Vector2 target){
		print("Updating Neighbours at : " + parent.position.ToString() );
		
		Node neighbour = new Node();
		
		for (int row = -1; row <= 1; row++){
			for (int col = -1; col <=1; col++){
				neighbour.position.x = parent.position.x + col * stepSize;
				neighbour.position.y = parent.position.y + row * stepSize;
				
				// Skip the same position
				if (row == 0 && col == 0){
					continue;
				} 
				// Already evaluated or unavailable
				else if (isNodeClosed(neighbour)){
					continue;
				}
				// Position is blocked by some object
				else if (!isPositionFree(neighbour.position)){
					closedNodes.Add(neighbour);
				}
				// Id the node is already open update the cost
				else if (isNodeOpen(neighbour)){
					neighbour = getOpenNode(neighbour);
					openNodes.Remove(neighbour);
					if (neighbour.cost > parent.cost + 1){
						neighbour.cost = parent.cost +1;
						neighbour.previous = parent.position;
					}
					openNodes.Add(neighbour);
				} 
				// Open new node
				else {
					neighbour.cost = parent.cost +1;
					neighbour.distance = calculateLocationCost(neighbour.position,target);
					neighbour.previous = parent.position;
					openNodes.Add(neighbour);
				}
			}
		}
	}

	bool isPositionFree(Vector2 position){
		return !Physics.CheckBox(new Vector3(position.x,transform.position.y,position.y),new Vector3 (stepSize/2,0.5f,stepSize/2),Quaternion.identity,obstacles);
	}

	bool calculateMovement(Vector2 start, Vector2 target){

		Node startNode = new Node();
		startNode.position =start;
		startNode.cost = 0;
		startNode.distance = calculateLocationCost(start,target);


		openNodes.Add(startNode);

		while (openNodes.Count > 0){
			Node current;
			current = findNextNode();
			print("Current Node: " + current.position.ToString());
				if (isTarget(current.position,target)){
					// SUCCESS
					current.position = target;
					closedNodes.Add(current);
					constructPath(current,start);
					return true;
				}
			updateNeighbours(current,target);
			openNodes.Remove(current);
			closedNodes.Add(current);

			iterations ++;
			if (iterations > maxIterations){
				iterations = 0;
				return false;
			}
		}
		return false;
	}

    private void constructPath(Node final, Vector2 start)
    {
        navigationPath = new List<Vector2>();
		navigationPath.Add(final.position);
		while(final.position != start){
			final.position = final.previous;
			final = getClosedNode(final);
			navigationPath.Insert(0,final.position);
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
		// Eucledian heurustic
		return (target - position).magnitude;
    }

    private Node getOpenNode(Node neighbour)
    {
        foreach(Node currentNode in openNodes){
			if (currentNode.position == neighbour.position){
				return currentNode;
			}
		}
		throw new Exception();
    }

    private Node getClosedNode(Node neighbour)
    {
        foreach(Node currentNode in closedNodes){
			if (currentNode.position == neighbour.position){
				return currentNode;
			}
		}
		throw new Exception();
    }

    private bool isNodeOpen(Node neighbour)
    {
        foreach(Node currentNode in openNodes){
			if (currentNode.position == neighbour.position){
				return true;
			}
		}
		return false;
    }

    private bool isNodeClosed(Node neighbour)
    {
        foreach(Node currentNode in closedNodes){
			if (currentNode.position == neighbour.position){
				return true;
			}
		}
		return false;
    }

    private Node findNextNode()
    {
		Node minNode = new Node();
		float minCost = float.MaxValue;
        foreach(Node node in openNodes){
			if (node.cost + node.distance < minCost){
				minCost = node.cost + node.distance;
				minNode = node;
			}
		}
	return minNode;
	}
}
