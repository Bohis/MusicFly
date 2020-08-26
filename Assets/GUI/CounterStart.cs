using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterStart : MonoBehaviour {
	public int StartValueCounter = 3;
	/// <summary>
	/// Ссылка на класс с управления стратом и финишом
	/// </summary>
	private WinAndLose _eventGame;
	/// <summary>
	/// Ссылка на управляющий объект
	/// </summary>
	private GameObject _controlObject;
	[SerializeField] Text CounterText;

	float maximum, minimum;


	// Start is called before the first frame update
	void Start() {
		_controlObject = GameObject.FindGameObjectsWithTag("MainControl")[0];
		_eventGame = _controlObject.GetComponent<WinAndLose>();
		CounterText.text = StartValueCounter.ToString();

		maximum = CounterText.transform.localScale.x;
		minimum = maximum * 0.6f;

		StartCoroutine(Countdown());
	}

	private IEnumerator Countdown() {
		while (true) {
			
			yield return new WaitForSeconds(1f);
			CounterText.text = ( --StartValueCounter ).ToString();
			if (StartValueCounter <= 0) {
				_eventGame.StartTrigger = true;
				CounterText.enabled = false;
				break;
			}
		}
	}

	private void Update() {
		CounterText.transform.localScale = new Vector2(Mathf.PingPong(Time.time, maximum - minimum) + minimum, Mathf.PingPong(Time.time, maximum - minimum) + minimum);
	}
}