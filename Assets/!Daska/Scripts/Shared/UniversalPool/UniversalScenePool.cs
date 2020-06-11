using System.Collections.Generic;
using UnityEngine;

public class UniversalScenePool<T> where T : MonoBehaviour {
	private T poolPrefab;
	private Queue<T> pooledObjects = new Queue<T>();

	public UniversalScenePool(T poolPrefab) {
		this.poolPrefab = poolPrefab;
	}

	internal T GetPooledObject() {
		if (pooledObjects.Count == 0) return Object.Instantiate(poolPrefab);
		else {
			T pooledObject = pooledObjects.Dequeue();
			pooledObject.gameObject.SetActive(true);
			return pooledObject;
		}
	}

	internal void ReturnPooledObject(T returningObject) {
		(returningObject as IUniversalPoolHandler)?.OnReturnToPool(); //Allows optional use of the IUniversalPoolHandler on the pooled object
		returningObject.gameObject.SetActive(false);
		pooledObjects.Enqueue(returningObject);
	}
}