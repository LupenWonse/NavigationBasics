using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	private CharacterNavigator[] navigators;


public void Start(){
	// Find all navigators on the scene
	navigators = GameObject.FindObjectsOfType<CharacterNavigator>();
}

public void OnHeuristicsChange(int value){
	CharacterNavigator.Heuristic newHeuristic =  CharacterNavigator.Heuristic.Eucledian;
	
	switch(value){
		case 0:
		newHeuristic = CharacterNavigator.Heuristic.Eucledian;
		break;
		case 1:
		newHeuristic = CharacterNavigator.Heuristic.Manhattan;
		break;
	}

	foreach(CharacterNavigator navigator in navigators){
		navigator.heurustic = newHeuristic;
	}
}

public void OnCostChange(int value){
	CharacterNavigator.Cost newCost =  CharacterNavigator.Cost.Greedy;
	
	switch(value){
		case 0:
		newCost = CharacterNavigator.Cost.Greedy;
		break;
		case 1:
		newCost = CharacterNavigator.Cost.Astar;
		break;
		default:
		Debug.LogWarning("Unhandled Cost Change");
		break;
	}

	foreach(CharacterNavigator navigator in navigators){
		navigator.cost = newCost;
	}
}

}
