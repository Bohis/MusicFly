using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Динамическое изменение размера блоков
/// </summary>
public class DynamicResizingBlocks : MonoBehaviour {
	/// <summary>
	/// Максимальный коэфициент высота блока
	/// </summary>
	public float MaxSizeCoof = 20;
	/// <summary>
	/// Диапазон изменения размера блока
	/// </summary>
	public float ChanceLongColumns = 20;

	/// <summary>
	/// Стартовое изменение размера блока
	/// </summary>
	/// <param name="thisObject">Объект подлежащий изменению размера</param>
	public void StartResizeObject(GameObject thisObject) {

		Transform newSize = thisObject.transform;
		float newY = 0;

		if ((int)Random.Range(0, ChanceLongColumns) == ChanceLongColumns - 1) {
			newY = MaxSizeCoof * newSize.localScale.y * 5 * Random.Range(1, 2.5f);
			thisObject.GetComponent<AnimationBlock>().UpDownMove = true;
		}
		else {
			newY = Random.Range(newSize.localScale.y, newSize.localScale.y * MaxSizeCoof);
		}

		thisObject.GetComponent<AnimationBlock>().NewSize = newY;
		thisObject.GetComponent<AnimationBlock>().SpawnAnimation = true;
		thisObject.GetComponent<AnimationBlock>().RandomTimeCoof = Random.Range(1, 5);
	}
}