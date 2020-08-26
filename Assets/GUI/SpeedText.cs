using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedText : MonoBehaviour {
	/// <summary>
	/// Ссылка на управляющий объект
	/// </summary>
	private GameObject _controlObject;
	private MoveMain _speedMove;
	[SerializeField] Text SpeedElemet;
	// Start is called before the first frame update
	void Start() {
		_controlObject = GameObject.FindGameObjectsWithTag("MainControl")[0];
		_speedMove = _controlObject.GetComponent<MoveMain>();
	}

	// Update is called once per frame
	void FixedUpdate() {
		SpeedElemet.text = string.Format("{0:N1}", _speedMove.SpeedReal);
	}
}