using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class Cell : MonoBehaviour {

	public Material material;
	
	public enum States {
		Dead, Alive
	}

	public States currentState;
	public States nextState;

	private void Awake() {
		this.material = GetComponent<SpriteRenderer>().material;
		this.material.color = Color.black;
		this.currentState = States.Dead;

	}

}

