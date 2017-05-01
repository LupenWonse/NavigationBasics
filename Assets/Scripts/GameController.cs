﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	void resetGame () {
		// Simply reset by re-loading the current level
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

}