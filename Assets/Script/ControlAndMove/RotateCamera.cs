using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Поворот объекта и камеры
/// </summary>
public class RotateCamera : MonoBehaviour {
	/// <summary>
	/// Ссылка на камеру
	/// </summary>
	private Camera _camera;
	/// <summary>
	/// Ссылка на объект
	/// </summary>
	private GameObject _mainObject;
	/// <summary>
	/// Ссылка на управляющий объект
	/// </summary>
	private GameObject _controlObject;
	/// <summary>
	/// Ссылка на класс с управления стратом и финишом
	/// </summary>
	private WinAndLose _eventGame;
	/// <summary>
	/// Скорость поворота
	/// </summary>
	private float SpeedRotation = 3f;

	/// <summary>
	/// Старт работы
	/// </summary>
	void Start() {
		_camera = Camera.main;

		_mainObject = GameObject.FindGameObjectsWithTag("SpaceShip")[0];
		_controlObject = GameObject.FindGameObjectsWithTag("MainControl")[0];
		_eventGame = _controlObject.GetComponent<WinAndLose>();
		_camera = Camera.main;
	}

	/// <summary>
	/// Обновления данных
	/// </summary>
	void FixedUpdate() {
		if (_eventGame.StartTrigger) {
			// =====================================================================================
			// Вычисление коэфициентов изменения углов наклона камеры
			float coofZ = 0;
			float coofX = 0;
			float coofY = 0;

			if (_camera.transform.rotation.eulerAngles.z > 350 || _camera.transform.rotation.eulerAngles.z < 10) {
				coofZ = 0;
			}
			else {
				if (_camera.transform.rotation.eulerAngles.z > 0 && _camera.transform.rotation.eulerAngles.z < 180) {
					coofZ = -_camera.transform.rotation.eulerAngles.z / 90f;
				}
				else {
					coofZ = ( 360 - _camera.transform.rotation.eulerAngles.z ) / 90f;
				}
			}

			if (_camera.transform.rotation.eulerAngles.x > 0 && _camera.transform.rotation.eulerAngles.x < 180) {
				coofX = -_camera.transform.rotation.eulerAngles.x / 35f;
			}
			else {
				coofX = ( 360 - _camera.transform.rotation.eulerAngles.x ) / 60f;
			}

			if (_camera.transform.rotation.eulerAngles.y > 0 && _camera.transform.rotation.eulerAngles.y < 45) {
				coofY = _camera.transform.rotation.eulerAngles.y / 45f;
			}
			if (_camera.transform.rotation.eulerAngles.y > 315 && _camera.transform.rotation.eulerAngles.y < 360) {
				coofY = -( 360 - _camera.transform.rotation.eulerAngles.y ) / 45f;
			}

			// =======================================================================================

			// Увелечение скорости относительно наклона по оси X
			_controlObject.GetComponent<MoveMain>().SpeedReal += _controlObject.GetComponent<MoveMain>().SpeedReal * coofX;

			// Движение в стороны по наклону камеры по осям Y, Z
			_controlObject.transform.position += new Vector3(( SpeedRotation * coofZ ) + ( SpeedRotation * coofY ), 0, 0);

			// Поворот объекта синхронизированный с камерой
			_mainObject.transform.rotation =
			new Quaternion(_camera.transform.rotation.x, _camera.transform.rotation.y, _camera.transform.rotation.z * 1.2f, _camera.transform.rotation.w);


			// =======================================================================================
			// Движение по оси Y камеры относительно угла наклона
			float positionY = 0, coofAngleY = 3;

			if (_camera.transform.rotation.eulerAngles.x < 360 && _camera.transform.rotation.eulerAngles.x >= 315) {
				positionY = 80 - ( 40 / Mathf.Cos(Mathf.Abs(( ( _camera.transform.rotation.eulerAngles.x - 360 ) / coofAngleY ) * 0.0175f)) );
			}
			else {
				positionY = ( 40 / Mathf.Cos(Mathf.Abs(( _camera.transform.rotation.eulerAngles.x / coofAngleY ) * 0.0175f)) );
			}

			_camera.transform.position =
			new Vector3(_camera.transform.position.x, positionY, _camera.transform.position.z);

			// ========================================================================================
		}
	}
}