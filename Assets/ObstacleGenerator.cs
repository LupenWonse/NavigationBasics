using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {

	public GameObject obstacle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray,out hit, 100.0f,LayerMask.GetMask(new string[] {"Board"}))){
				if (hit.transform.childCount == 0){
					GameObject.Instantiate(obstacle,hit.collider.gameObject.transform.position + Vector3.up*2,Quaternion.identity,hit.transform);
				} else {
					Destroy(hit.transform.GetChild(0).gameObject);
				}
				
				
			}
		}
	}
}
