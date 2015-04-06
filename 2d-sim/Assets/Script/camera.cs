using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {
    public float speed = 5;
    public float height = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.W)&& transform.position.y < height)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.S) && transform.position.y > 2)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
	}
}
