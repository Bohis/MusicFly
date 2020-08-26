using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Анимация музыкальных объектов
/// </summary>
public class MusicObjectAnimation : MonoBehaviour {
	/// <summary>
	/// Ссылка на класс преобразования музыки в данные
	/// </summary>
	private AudioData _dataSource;
	/// <summary>
	/// Коллекция музыкальных объетов
	/// </summary>
	private List<GameObject> _collectionMusicObject;
	/// <summary>
	/// Ссылка на управляющий объект
	/// </summary>
	private GameObject _controlObject;

	/// <summary>
	/// Коэфициент изменения высоты объекта 
	/// </summary>
	public float CoofHeightMusicObject = 1;

	/// <summary>
	/// Старт программы
	/// </summary>
	void Start() {
		_controlObject = GameObject.FindGameObjectsWithTag("MainControl")[0];

		_dataSource = Camera.main.gameObject.GetComponent<AudioData>();
		_collectionMusicObject = _controlObject.gameObject.GetComponent<CollectionObject>().CollectionMusicObject;
		_dataSource.NewMusicData += DataSource_NewMusicData;
	}

	/// <summary>
	/// Анимация музыкальных объетов
	/// </summary>
	private void DataSource_NewMusicData() {
		_collectionMusicObject[0].transform.localScale += new Vector3(0, _dataSource.DataChanges[0] * CoofHeightMusicObject, 0);
		for (int i = 1, k = 1; k < _collectionMusicObject.Count; ++i, ++k) {
			_collectionMusicObject[k].transform.localScale += new Vector3(0, _dataSource.DataChanges[i] * CoofHeightMusicObject, 0);
			// ===========================================
			// Поправка на расположения музыкальных объектов
			if (k != _collectionMusicObject.Count - 1 && i == _dataSource.DataChanges.Length - 1) {
				i = 0;
			}
		}
	}
}