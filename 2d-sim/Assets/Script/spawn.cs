using UnityEngine;
using System.Collections;
using UnityEditor;


public class spawn : MonoBehaviour {

    //@MenuItem("AssetDatabase/LoadAssetExample");

   
	// Use this for initialization
	void Start () {
        SpawnObject(2);
	}
	
	// Update is called once per frame
	void Update () {
      //  GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
	}

    void SpawnObject(int code)
    {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Cube.prefab", typeof(GameObject));
        switch (code)
        { 
            case 1:
                prefab = AssetDatabase.LoadAssetAtPath("Assets/Cube.prefab", typeof(GameObject));
                break;
            case 2: 
                prefab = AssetDatabase.LoadAssetAtPath("Assets/Cube 1.prefab", typeof(GameObject));
                break;
            default:
                Debug.Log("Error");
                break;
        }
        GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        clone.transform.parent = transform;
        // Modify the clone to your heart's content
        clone.transform.position = transform.position;
    }
}
