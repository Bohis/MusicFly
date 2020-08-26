using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Перезапуск приложения
/// </summary>
public class Restart : MonoBehaviour {
	/// <summary>
	/// Ссылка на класс с управления стратом и финишом
	/// </summary>
	private WinAndLose _eventGame;

	/// <summary>
	/// Старт
	/// </summary>
	void Start() {
		_eventGame = GameObject.FindGameObjectsWithTag("MainControl")[0].GetComponent<WinAndLose>();
		_eventGame.EndEvent += EventGame_EndEvent;
	}

	/// <summary>
	/// Перезапуск сцены
	/// </summary>
	private void EventGame_EndEvent() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}