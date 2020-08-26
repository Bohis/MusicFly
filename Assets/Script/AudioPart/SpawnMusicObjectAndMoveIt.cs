using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Стартовый спавн музыкальных объетов и их движение
/// </summary>
public class SpawnMusicObjectAndMoveIt : MonoBehaviour {
	/// <summary>
	/// Префаб музыкального объекта
	/// </summary>
	public GameObject PrefabMusicObject;
	/// <summary>
	/// Ссылка на основной объект
	/// </summary>
	private GameObject _mainObject;
	/// <summary>
	/// Ссылка на управляющий объект
	/// </summary>
	private GameObject _controlObject;
	/// <summary>
	/// Ссыдка на коллекцию музыкальных объектов  
	/// </summary>
	private List<GameObject> _linkCollection;
	/// <summary>
	/// Ссылка на класс с управления стратом и финишом
	/// </summary>
	private WinAndLose _eventGame;

	/// <summary>
	/// Стартовая высота объекта
	/// </summary>
	public float StartHeightMusicObject = 1;

	/// <summary>
	/// Дистанция до музыкальных объектов
	/// </summary>
	private float _distance;
	/// <summary>
	/// Расстояние между объектами
	/// </summary>
	private float _space;
	/// <summary>
	/// Коэфициент размера музыкальных объктов
	/// </summary>
	private float _coofSize = 2;

	/// <summary>
	/// Позиция музыкальных блоков по оси X
	/// </summary>
	private float _positionY = -30;

	/// <summary>
	/// Метод старта
	/// </summary>
	void Start() {
		_mainObject = GameObject.FindGameObjectsWithTag("SpaceShip")[0];
		_controlObject = GameObject.FindGameObjectsWithTag("MainControl")[0];
		_eventGame = _controlObject.GetComponent<WinAndLose>();

		_linkCollection = _controlObject.gameObject.GetComponent<CollectionObject>().CollectionMusicObject;
		
		_space = PrefabMusicObject.transform.localScale.x / 4f;

		_linkCollection.Add(Instantiate(PrefabMusicObject, new Vector3(0, _positionY, _distance), new Quaternion()));

	}

	public void StartSpawn(){
		GameObject prefabColumn = GetComponent<DynamicSpawnBlock>().PrefabBlock;

		_distance = _controlObject.GetComponent<DynamicSpawnBlock>().ZLoadingZone + _controlObject.GetComponent<DynamicSpawnBlock>().PrefabBlock.transform.localScale.z * 2;

		// =====================================================
		// Стартовый спавн музыкальных объектов
		_linkCollection[0].transform.localScale =
		new Vector3(prefabColumn.transform.localScale.x * _coofSize, StartHeightMusicObject, prefabColumn.transform.localScale.z * _coofSize);

		for (int i = 1; i <= 8; ++i) {
			GameObject objTemp =
			Instantiate(PrefabMusicObject, new Vector3(_linkCollection[0].transform.localScale.x * i + _space * i, _positionY, _distance), new Quaternion());

			objTemp.transform.localScale =
			new Vector3(prefabColumn.transform.localScale.x * _coofSize, StartHeightMusicObject, prefabColumn.transform.localScale.z * _coofSize);

			_linkCollection.Add(objTemp);
		}

		for (int i = 1; i <= 8; ++i) {
			GameObject objTemp =
			Instantiate(PrefabMusicObject, new Vector3(-_linkCollection[0].transform.localScale.x * i - _space * i, _positionY, _distance), new Quaternion());

			objTemp.transform.localScale =
			new Vector3(prefabColumn.transform.localScale.x * _coofSize, StartHeightMusicObject, prefabColumn.transform.localScale.z * _coofSize);

			_linkCollection.Add(objTemp);
		}
		// =====================================================
	}

	/// <summary>
	/// Обновление данных
	/// </summary>
	void FixedUpdate() {
		if (_eventGame.StartTrigger) {
			_linkCollection[0].transform.position =
			new Vector3(_mainObject.transform.position.x, _positionY, _mainObject.transform.position.z + _distance);

			for (int i = 1; i <= 8; ++i) {
				_linkCollection[i].transform.position
				= new Vector3(
				_linkCollection[0].transform.position.x + _linkCollection[0].transform.localScale.x * i + _space * i,
				_positionY,
				_mainObject.transform.position.z + _distance);
			}

			for (int i = 1; i <= 8; ++i) {
				_linkCollection[i + 8].transform.position
				= new Vector3(
				_linkCollection[0].transform.position.x - _linkCollection[0].transform.localScale.x * i - _space * i,
				_positionY,
				_mainObject.transform.position.z + _distance);
			}
		}
	}
}