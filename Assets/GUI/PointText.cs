using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointText : MonoBehaviour {
	/// <summary>
	/// Ссылка на управляющий объект
	/// </summary>
	private GameObject _controlObject;
	[SerializeField] Text PointElemet;
	// Start is called before the first frame update
	void Start() {
		_controlObject = GameObject.FindGameObjectsWithTag("MainControl")[0];
		_controlObject.GetComponent<WinAndLose>().PointUPEvent += PointText_PointUPEvent;
		PointElemet.text = "0";
	}

	private void PointText_PointUPEvent() => PointElemet.text = (int.Parse( PointElemet.text ) + 1 ).ToString();
}