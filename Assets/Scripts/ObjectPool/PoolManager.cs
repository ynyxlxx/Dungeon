using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

    Dictionary<int, Queue<ObjectInstance>> poolDict = new Dictionary<int, Queue<ObjectInstance>> ();

    //保证pool manager 只有一个实例
    private static PoolManager _instance;

    public static PoolManager instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<PoolManager> ();
            }

            return _instance;
        }
    }

    public void CreatePool (GameObject prefab, int poolSize) {
        int poolKey = prefab.GetInstanceID ();

        //将生成的实例保存在一个holder里面
        GameObject poolHolder = new GameObject (prefab.name + " pool");
        poolHolder.transform.parent = transform;

        if (!poolDict.ContainsKey (poolKey)) {
            poolDict.Add (poolKey, new Queue<ObjectInstance> ());

            for (int i = 0; i < poolSize; i++) {
                ObjectInstance newObject = new ObjectInstance (Instantiate (prefab) as GameObject);
                poolDict[poolKey].Enqueue (newObject);
                newObject.SetParent (poolHolder.transform);
            }
        }
    }

    public void ReuseObject (GameObject prefab, Vector3 position, Quaternion rotation) {
        int poolKey = prefab.GetInstanceID ();

        if (poolDict.ContainsKey (poolKey)) {
            //取得待用对象的引用
            ObjectInstance objectToReuse = poolDict[poolKey].Dequeue ();
            poolDict[poolKey].Enqueue (objectToReuse);

            //调用待用对象的方法
            objectToReuse.Reuse (position, rotation);
        }
    }

    //对象实例类
    public class ObjectInstance {
        GameObject gameObject;
        Transform transform;

        bool hasPoolObjectComponent;
        PoolObject poolObjectScript;

        //Constructor
        public ObjectInstance (GameObject objectInstance) {
            gameObject = objectInstance;
            gameObject.SetActive (false);
            transform = gameObject.transform;

            if (gameObject.GetComponent<PoolObject> ()) {
                hasPoolObjectComponent = true;
                poolObjectScript = gameObject.GetComponent<PoolObject> ();
            }
        }
        //重用方法
        public void Reuse (Vector3 position, Quaternion rotation) {
            if (hasPoolObjectComponent) {
                poolObjectScript.OnObjectReuse ();
            }

            gameObject.SetActive (true);
            transform.position = position;
            transform.rotation = rotation;
        }

        public void SetParent (Transform parent) {
            transform.parent = parent;
        }

    }
}