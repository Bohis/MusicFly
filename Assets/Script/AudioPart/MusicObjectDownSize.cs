using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Постоянное уменьшения высоты музыкальных объетов
/// </summary>
public class MusicObjectDownSize : MonoBehaviour {
	/// <summary>
	/// Обновление данных
	/// </summary>
	void FixedUpdate() {
		if (this.transform.localScale.y > 1) {
			this.transform.localScale -= new Vector3(0, this.transform.localScale.y / 20f, 0);
		}
	}
}