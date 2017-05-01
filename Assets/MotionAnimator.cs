using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MotionAnimator : MonoBehaviour {

[SerializeField] private CharacterMover mover;
[SerializeField] private Animator animator;
[SerializeField] private GameObject model;

	// Update is called once per frame
	void Update () {
		if (mover.target != transform.position){
			animator.SetBool("isWalking", true);
			Vector3 movementDirection = mover.target - transform.position;
			model.transform.rotation = Quaternion.FromToRotation(-transform.forward,movementDirection);
		} else {
			animator.SetBool("isWalking", false);
		}
	}
}
