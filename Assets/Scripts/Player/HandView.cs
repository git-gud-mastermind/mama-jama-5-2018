﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandView : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void AddCardToHand(GameObject cardDrawn){
		cardDrawn.transform.SetParent(transform);
	}
}
