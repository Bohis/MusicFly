using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Уничтожение объекта при столкновении
/// </summary>
public class Destroy : MonoBehaviour {
	/// <summary>
	/// Столкновение
	/// </summary>
	/// <param name="gameObject">уничтожабщийся объект</param>
	void OnCollisionEnter(Collision gameObject) {
		GameObject.FindGameObjectsWithTag("MainControl")[0].GetComponent<WinAndLose>().StartTrigger = false;
		GameObject.FindGameObjectsWithTag("MainControl")[0].GetComponent<WinAndLose>().EndTrigger = true;
		Destroy(this.gameObject);
	}
}