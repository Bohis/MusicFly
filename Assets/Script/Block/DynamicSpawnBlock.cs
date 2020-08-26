using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Спавн блоков вокруг основного объекта, их подрузка и удаление
/// </summary>
public class DynamicSpawnBlock : MonoBehaviour {
	/// <summary>
	/// Префаб  блоков
	/// </summary>
	public GameObject PrefabBlock;

	/// <summary>
	/// Ссылка на коллекцию объектов
	/// </summary>
	private CollectionObject _collectionLink;
	/// <summary>
	/// Скрипт динамического изменения данных
	/// </summary>
	private DynamicResizingBlocks _resizingColumns;

	/// <summary>
	/// Количество объетов по радиусу
	/// </summary>
	public int CountObject = 20;

	/// <summary>
	/// Зона достижения подгрузки по оси Х
	/// </summary>
	private int _xLoadingRange;
	/// <summary>
	/// Зона достижения подгрузки по оси Z
	/// </summary>
	private int _zLoadingRange;

	/// <summary>
	/// Зона подгрузки по оси Х
	/// </summary>
	private int _xLoadingZone;
	/// <summary>
	/// Зона подгрузки по оси Z
	/// </summary>
	private int _zLoadingZone;

	/// <summary>
	/// Зона подгрузки по оси Z
	/// </summary>
	public int ZLoadingZone{
		get => _zLoadingZone;
	}

	/// <summary>
	/// Предедущие координаты объекта по оси X 
	/// </summary>
	private float _oldXThisObject;
	/// <summary>
	/// Предедущие координаты объекта по оси Z 
	/// </summary>
	private float _oldZThisObject;

	/// <summary>
	/// Ссылка на основной объект
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
	/// Старт работы
	/// </summary>
	void Start() {
		_mainObject = GameObject.FindGameObjectsWithTag("SpaceShip")[0];
		_controlObject = GameObject.FindGameObjectsWithTag("MainControl")[0];
		_eventGame = _controlObject.GetComponent<WinAndLose>();

		_oldXThisObject = _mainObject.transform.position.x;
		_oldZThisObject = _mainObject.transform.position.z;

		_collectionLink = _controlObject.GetComponent<CollectionObject>();

		_xLoadingZone = CountObject * (int)PrefabBlock.transform.localScale.x;
		_zLoadingZone = CountObject * (int)PrefabBlock.transform.localScale.z;

		_controlObject.GetComponent<SpawnMusicObjectAndMoveIt>().StartSpawn();

		_xLoadingRange = (int)PrefabBlock.transform.localScale.x;
		_zLoadingRange = (int)PrefabBlock.transform.localScale.z;

		_resizingColumns = _controlObject.GetComponent<DynamicResizingBlocks>();

		Debug.Log("StartSpawn");
		for (float zPosition = _mainObject.transform.position.z - _zLoadingZone / 3f;
		zPosition < _mainObject.transform.position.z + _zLoadingZone; zPosition += PrefabBlock.transform.localScale.z) {

			List<GameObject> newRow = new List<GameObject>();

			for (float xPosition = _mainObject.transform.position.x - _xLoadingZone / 2f;
			xPosition < _mainObject.transform.position.x + _xLoadingZone / 2f; xPosition += PrefabBlock.transform.localScale.x) {

				newRow.Add(Instantiate(PrefabBlock,
				   new Vector3(xPosition, 0, zPosition), new Quaternion()));

				_resizingColumns.StartResizeObject(newRow[newRow.Count - 1]);
			}
			_collectionLink.CollectionBlock.Add(newRow);
		}
		Debug.Log("Start = " + _collectionLink.CollectionBlock.Count * _collectionLink.CollectionBlock[0].Count);
	}

	/// <summary>
	/// Обновление данных
	/// </summary>
	void FixedUpdate() {
		if (_eventGame.StartTrigger) {
			// ========================================================
			// Подгрузка по оси Z
			if (( _mainObject.transform.position.z - _oldZThisObject ) >= _zLoadingRange) {

				List<GameObject> newRow = new List<GameObject>();
				float zPosition = _collectionLink.CollectionBlock[_collectionLink.CollectionBlock.Count - 1][0].transform.position.z +
				PrefabBlock.transform.localScale.z;

				for (float xPosition = _mainObject.transform.position.x - _xLoadingZone / 2f;
				xPosition < _mainObject.transform.position.x + _xLoadingZone / 2f; xPosition += PrefabBlock.transform.localScale.x) {

					newRow.Add(Instantiate(PrefabBlock,
					   new Vector3((int)xPosition, 0, zPosition), new Quaternion()));

					_resizingColumns.StartResizeObject(newRow[newRow.Count - 1]);
				}

				_collectionLink.CollectionBlock.Add(newRow);
				RemoveCollumn();

				_oldZThisObject = _mainObject.transform.position.z;

				if (_collectionLink.CollectionBlock[_collectionLink.CollectionBlock.Count - 1][0].transform.position.z
				<= _zLoadingZone + _mainObject.transform.position.z) {

					float zone = (_zLoadingZone + _mainObject.transform.position.z) - _collectionLink.CollectionBlock[_collectionLink.CollectionBlock.Count - 1][0].transform.position.z;

					for (float i = 0, j = 0; i < zone; i += PrefabBlock.transform.localScale.z, ++j) {

						newRow = new List<GameObject>();
						zPosition = _collectionLink.CollectionBlock[_collectionLink.CollectionBlock.Count - 1][0].transform.position.z +
						PrefabBlock.transform.localScale.z;

						for (float xPosition = _mainObject.transform.position.x - _xLoadingZone / 2f;
						xPosition < _mainObject.transform.position.x + _xLoadingZone / 2f; xPosition += PrefabBlock.transform.localScale.x) {

							newRow.Add(Instantiate(PrefabBlock,
							   new Vector3((int)xPosition, 0, zPosition), new Quaternion()));

							_resizingColumns.StartResizeObject(newRow[newRow.Count - 1]);
						}

						_collectionLink.CollectionBlock.Add(newRow);
						RemoveCollumn();
					}
				}
				_oldZThisObject = _mainObject.transform.position.z;
			}

			// ========================================================
			// Подгрузка по оси X
			if (Math.Abs(_mainObject.transform.position.x - _oldXThisObject) >= _xLoadingRange) {

				float xPosition = _mainObject.transform.position.x;

				if (xPosition - _oldXThisObject > 0) {
					Debug.Log("SpawnX right");
					foreach (List<GameObject> listObj in _collectionLink.CollectionBlock) {
						listObj.Add(Instantiate(PrefabBlock,
						   new Vector3(listObj[listObj.Count - 1].transform.position.x + PrefabBlock.transform.localScale.x, 0, listObj[listObj.Count - 1].transform.position.z),
						   new Quaternion()));

						_resizingColumns.StartResizeObject(listObj[listObj.Count - 1]);
					}
					LeftDelete();
				}
				else {
					Debug.Log("SpawnX left");
					foreach (List<GameObject> listObj in _collectionLink.CollectionBlock) {
						listObj.Insert(0, Instantiate(PrefabBlock,
						   new Vector3(listObj[0].transform.position.x - PrefabBlock.transform.localScale.x, 0, listObj[0].transform.position.z),
						   new Quaternion()));
						_resizingColumns.StartResizeObject(listObj[0]);
					}
					RightDelete();
				}

				_oldXThisObject = xPosition;
			}
		}
	}

	/// <summary>
	/// Удаленеи последний линии объектов
	/// </summary>
	private void RemoveCollumn() {
		Debug.Log("RemoveZ");
		foreach (GameObject obj in _collectionLink.CollectionBlock[0]) {
			Destroy(obj);
		}
		_collectionLink.CollectionBlock.RemoveAt(0);
	}

	/// <summary>
	/// Удаление крайней полосы с лева
	/// </summary>
	private void LeftDelete() {
		foreach (List<GameObject> listObj in _collectionLink.CollectionBlock) {
			Destroy(listObj[0]);
			listObj.RemoveAt(0);
		}
	}

	/// <summary>
	/// Удаление крайней полосы с права
	/// </summary>
	private void RightDelete() {
		foreach (List<GameObject> listObj in _collectionLink.CollectionBlock) {
			Destroy(listObj[listObj.Count - 1]);
			listObj.RemoveAt(listObj.Count - 1);
		}
	}
}