using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Коллекции объектов
/// </summary>
public class CollectionObject : MonoBehaviour {
	/// <summary>
	/// Коллекция блоков 
	/// </summary>
	public List<List<GameObject>> CollectionBlock = new List<List<GameObject>>();
	/// <summary>
	/// Коллекция музыкальных объетов
	/// </summary>
	public List<GameObject> CollectionMusicObject = new List<GameObject>();
}