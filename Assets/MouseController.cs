using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

	CharacterNavigator navigator;

	// Use this for initialization
	void Start () {
		navigator = GetComponent<CharacterNavigator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray,out hit, 100.0f,LayerMask.GetMask(new string[] {"Board"}))){
				navigator.setNewTarget(new Vector2(hit.transform.position.x,hit.transform.position.z));
			}
		}
	}
}
