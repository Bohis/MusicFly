using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Анимация блоков 
/// </summary>
public class AnimationBlock : MonoBehaviour {
	/// <summary>
	/// Триггер стартовой анимации
	/// </summary>
	public bool SpawnAnimation = false;
	/// <summary>
	/// Анимация изменения размера
	/// </summary>
	public bool UpDownMove = false;

	/// <summary>
	/// Максимальный размер
	/// </summary>
	public float NewSize = 0;
	/// <summary>
	/// Коэфициент времени анимации
	/// </summary>
	public float TimeCoof = 3f;
	/// <summary>
	/// Скорость анимации
	/// </summary>
	public float RandomTimeCoof = 1;

	// Старт работы
	void Start() {
		if (TimeCoof <= 0) {
			TimeCoof = 2;
		}
		if (RandomTimeCoof < 1) {
			RandomTimeCoof = 1;
		}
	}

	// Обновление данных
	void FixedUpdate() {
		// ==========================================
		// Стартовая анимация
		if (SpawnAnimation) {
			float stepSize = ( NewSize / TimeCoof ) * RandomTimeCoof;

			this.gameObject.transform.localScale =
			new Vector3(this.gameObject.transform.localScale.x,
			this.gameObject.transform.localScale.y + stepSize * Time.deltaTime,
			this.gameObject.transform.localScale.z);

			this.gameObject.transform.position =
			new Vector3(this.gameObject.transform.position.x,
			0,
			this.gameObject.transform.position.z);

			if (NewSize <= this.gameObject.transform.localScale.y) {
				SpawnAnimation = false;
				NewSize = this.gameObject.transform.localScale.y;
			}
		}
		// ==========================================
		// Анимация объекта
		if (UpDownMove && !SpawnAnimation) {
			float stepSize = ( NewSize / TimeCoof ) * RandomTimeCoof;

			this.gameObject.transform.localScale =
			new Vector3(this.gameObject.transform.localScale.x,
			this.gameObject.transform.localScale.y + stepSize * Time.deltaTime,
			this.gameObject.transform.localScale.z);

			this.gameObject.transform.position =
			new Vector3(this.gameObject.transform.position.x,
			0,
			this.gameObject.transform.position.z);

			if (this.gameObject.transform.localScale.y >= NewSize && NewSize > 0) {
				NewSize = -NewSize;
			}

			if (this.gameObject.transform.localScale.y <= 0 && NewSize < 0) {
				NewSize = -NewSize;
			}
		}
	}
}