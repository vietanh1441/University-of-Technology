using UnityEngine;
using System.Collections;

/*
 * 
 * button 3: click to close the new student UI
 * 
 */

public class button3 : MonoBehaviour {
    public GameObject central;
	// Use this for initialization
	void Start () {
        central = GameObject.FindGameObjectWithTag("Central");
	}
	
    void OnMouseDown()
    {
        central.SendMessage("CloseNewStud");
    }

	// Update is called once per frame
	void Update () {
	
	}
}
