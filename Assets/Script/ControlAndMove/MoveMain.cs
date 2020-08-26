using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

/// <summary>
/// Основное движение
/// </summary>
public class MoveMain : MonoBehaviour {
	/// <summary>
	/// Максимальная скорость
	/// </summary>
	public float SpeedMax = 4f;
	/// <summary>
	/// Минимальная скорость
	/// </summary>
	public float SpeedMin = 1f;
	/// <summary>
	/// Реальная скорость
	/// </summary>
	private float _speedReal;
	/// <summary>
	/// Ссылка на класс с очками
	/// </summary>
	private WinAndLose _point;

	/// <summary>
	/// Ссылка на управляющий объект
	/// </summary>
	private GameObject _controlObject;

	/// <summary>
	/// Старт приложения
	/// </summary>
	void Start() {
		_controlObject = GameObject.FindGameObjectsWithTag("MainControl")[0];

		_point = _controlObject.GetComponent<WinAndLose>();
		
		_point.PointUPEvent += Point_PointUPEvent;
		_speedReal = SpeedMax / 2f;
	}

	private void Point_PointUPEvent() {
		SpeedReal += 0.1f;
		if (_point.PointWin > 10 && _point.PointWin % 10 == 0 ){
			SpeedMax += .1f;
		}
	}

	/// <summary>
	/// Реальная скорость
	/// </summary>
	public float SpeedReal {
		get => _speedReal;
		set {
			if (value >= SpeedMin && value <= SpeedMax) {
				_speedReal = value;
			}
			else {
				if (value >= SpeedMax) {
					_speedReal = SpeedMax;
				}
				else {
					_speedReal = SpeedMin;
				}
			}
		}
	}

	/// <summary>
	/// Обновление данных
	/// </summary>
	void FixedUpdate() {
		if (_point.StartTrigger) {
			_controlObject.gameObject.transform.position += new Vector3(0, 0, SpeedReal);
		}
		else {
			if (_point.EndTrigger && _speedStartSlowMove == -1) {
				_speedStartSlowMove = SpeedReal;
				StartCoroutine(PostGameSlowMove());
			}
		}
	}

	/// <summary>
	/// Скорость замедления
	/// </summary>
	private float _speedStartSlowMove = -1;
	/// <summary>
	/// Шаг уменьшения скорости замедления
	/// </summary>
	private float StepSlowMove = 0.9f;

	/// <summary>
	/// Куратина замедления движения
	/// </summary>
	IEnumerator PostGameSlowMove() {
		while (true) {
			_controlObject.gameObject.transform.position += new Vector3(0, 0, _speedStartSlowMove);
			_speedStartSlowMove *= StepSlowMove;
			_speedReal = _speedStartSlowMove;
			if (_speedStartSlowMove > 0.1f) {
				yield return new WaitForFixedUpdate();
			}
			else {
				_speedReal = 0;
				break;
			}
		}
	}
}