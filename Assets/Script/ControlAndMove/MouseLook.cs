using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Управление мышью
/// </summary>
public class MouseLook : MonoBehaviour {
	/// <summary>
	/// Коллекция осей
	/// </summary>
	public enum RotationAxes {
		MouseXAndY = 0,
		MouseX = 1,
		MouseY = 2
	}
	/// <summary>
	/// Действующие оси
	/// </summary>
	public RotationAxes Axes = RotationAxes.MouseXAndY;
	/// <summary>
	/// Чуствительность по горизонтали
	/// </summary>
	public float SensitivityHor = 9.0f;
	/// <summary>
	/// Чуствительность по вертикали
	/// </summary>
	public float SensitivityVert = 9.0f;
	/// <summary>
	/// Минимальный угол наклона
	/// </summary>
	public float MinimumVert = -45.0f;
	/// <summary>
	/// Максимальный угол наклона
	/// </summary>
	public float MaximumVert = 45.0f;

	/// <summary>
	/// Поворот по оси X
	/// </summary>
	private float _rotationX = 0;
	/// <summary>
	/// Поворот по оси Z
	/// </summary>
	private float _rotationZ = 0;

	/// <summary>
	/// Старт работы
	/// </summary>
	void Start() {
		Rigidbody body = GetComponent<Rigidbody>();
		if (body != null)
			body.freezeRotation = true;
	}

	/// <summary>
	/// Обновление данных
	/// </summary>
	void FixedUpdate() {
		if (Axes == RotationAxes.MouseX) {
			transform.Rotate(0, Input.GetAxis("Mouse X") * SensitivityHor, 0);
		}
		else
		if (Axes == RotationAxes.MouseY) {
			_rotationX -= Input.GetAxis("Mouse Y") * SensitivityVert;
			_rotationX = Mathf.Clamp(_rotationX, MinimumVert, MaximumVert);
			float rotationY = transform.localEulerAngles.y;
			transform.localEulerAngles = new Vector3(_rotationX, rotationY, _rotationZ);
		}
		else {
			_rotationX -= Input.GetAxis("Mouse Y") * SensitivityVert;
			_rotationX = Mathf.Clamp(_rotationX, MinimumVert, MaximumVert);
			float delta = Input.GetAxis("Mouse X") * SensitivityHor;
			float rotationY = transform.localEulerAngles.y + delta;
			transform.localEulerAngles = new Vector3(_rotationX, rotationY, _rotationZ);
		}

		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			float rotationZ = 10f;
			transform.localEulerAngles += new Vector3(0, 0, rotationZ);
			_rotationZ = transform.localEulerAngles.z;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			float rotationZ = -10f;
			transform.localEulerAngles += new Vector3(0, 0, rotationZ);
			_rotationZ = transform.localEulerAngles.z;
		}
	}
}