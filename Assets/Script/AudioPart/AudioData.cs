using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Получение аудио данных  и калибровка данных
/// </summary>
public class AudioData : MonoBehaviour {
	/// <summary>
	/// Исходные данные
	/// </summary>
	private float[] _dataColumn;
	/// <summary>
	/// Данные после калибровки
	/// </summary>
	private float[] _dataCalibrated;
	/// <summary>
	/// Дельта калиброванные данные
	/// </summary>
	private float[] _dataChanges;
	/// <summary>
	/// Коэфициенты компоновки
	/// </summary>
	private int[] _layoutData = { 1, 3, 3, 3, 5, 6, 6, 32 };
	/// <summary>
	/// Триггер начала работы
	/// </summary>
	private WinAndLose _eventGame;

	/// <summary>
	/// Размер исходных данных
	/// </summary>
	private int _sizeDataColumn = 64;
	/// <summary>
	/// Размер масива коэфициентов компоновки
	/// </summary>
	private int _sizeDataCalibrated = 9;

	/// <summary>
	/// Базовый делегат
	/// </summary>
	public delegate void BasicEvent();
	/// <summary>
	/// События обновления калиброванных данных
	/// </summary>
	public event BasicEvent NewMusicData;

	/// <summary>
	/// Коэфициент разницы между макс. и остальными данными
	/// </summary>
	public float DeltaValueRatioMaxToBackward = 0.3f;
	/// <summary>
	/// Коэфициент увелечения к уровню макс. значения
	/// </summary>
	public float MagnificationRatio = 1.8f;
	/// <summary>
	/// Коэфициент уменьшения макс. значения
	/// </summary>
	public float ReductionRatio = 0.4f;

	/// <summary>
	/// Метод работающий при старте
	/// </summary>
	void Start() {
		_dataColumn = new float[_sizeDataColumn];
		_dataCalibrated = new float[_sizeDataCalibrated];
		_dataChanges = new float[_sizeDataCalibrated];

		_eventGame = GameObject.FindGameObjectsWithTag("MainControl")[0].GetComponent<WinAndLose>();

		StartCoroutine(GetDataMusic());
	}

	/// <summary>
	/// Получение данных музыки
	/// </summary>
	private IEnumerator GetDataMusic() {
		while (true) {
			yield return new WaitForFixedUpdate();

			if (_eventGame.StartTrigger) {
				try {
					// ==================================
					// Получение данных
					AudioListener.GetSpectrumData(_dataColumn, 0, FFTWindow.Rectangular);
					float temp = 0, maxValue = 0;
					int index = _layoutData[0];

					// ==================================
					// Компоновка данных
					for (int i = 0, k = 0; i < _sizeDataColumn; ++i) {
						temp += _dataColumn[i];

						if (i == index) {
							temp /= _layoutData[k];
							_dataChanges[k] = Mathf.Abs(temp - _dataCalibrated[k]);
							_dataCalibrated[k] = _dataChanges[k];

							if (_dataChanges[k] > maxValue)
								maxValue = _dataChanges[k];

							++k;

							if (k == _sizeDataCalibrated - 1)
								break;

							index += _layoutData[k];
							temp = 0;
						}
					}

					// ==================================
					// Калибровка данных
					for (int i = 0; i < _sizeDataCalibrated; ++i) {
						float delta = maxValue - _dataChanges[i];

						if (delta > maxValue * DeltaValueRatioMaxToBackward) {
							_dataChanges[i] *= MagnificationRatio;
						}
						if (maxValue == _dataChanges[i]) {
							_dataChanges[i] *= ReductionRatio;
						}
					}

					NewMusicData?.Invoke();
				}
				catch { }
			}
		}
	}

	/// <summary>
	/// Данные после компоновки
	/// </summary>
	public float[] DataCalibrated { get => _dataCalibrated; }
	/// <summary>
	/// Дельта данные
	/// </summary>
	public float[] DataChanges { get => _dataChanges; }
}