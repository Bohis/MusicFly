using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроль работы приложения
/// </summary>
public class WinAndLose : MonoBehaviour {
	/// <summary>
	/// Начало игры
	/// </summary>
	private bool _startTrigger = false;
	/// <summary>
	/// Конец игры
	/// </summary>
	private bool _endTrigger = false;

	/// <summary>
	/// Очки игры
	/// </summary>
	public int PointWin = 0;

	/// <summary>
	/// Базовый делегат
	/// </summary>
	public delegate void BaseMode();
	/// <summary>
	/// Событие начала игры
	/// </summary>
	public event BaseMode StartEvent;
	/// <summary>
	/// Событие конец игры
	/// </summary>
	public event BaseMode EndEvent;
	/// <summary>
	/// Событие добавления очков
	/// </summary>
	public event BaseMode PointUPEvent;

	/// <summary>
	/// Старт игры
	/// </summary>
	public bool StartTrigger {
		get => _startTrigger;
		set { _startTrigger = value; StartEvent?.Invoke(); }
	}

	/// <summary>
	/// Конец игры
	/// </summary>
	public bool EndTrigger {
		get => _endTrigger;
		set { _endTrigger = value; EndEvent?.Invoke(); }
	}

	/// <summary>
	/// Старт работы
	/// </summary>
	void Start() {
		StartCoroutine(PointUP());
	}

	/// <summary>
	/// Начисление очков
	/// </summary>
	IEnumerator PointUP() {
		while (true) {
			if (_startTrigger) {
				PointWin++;
				PointUPEvent?.Invoke();
			}
			if (_endTrigger) {
				yield return null;
			}
			yield return new WaitForSeconds(1f);
		}
	}
}