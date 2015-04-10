using UnityEngine;
using System.Collections;

/*
 * 
 * button 2: click to process next on the list of the UI new student
 * 
 */

public class button2 : MonoBehaviour {
   
    public GameObject central;
	// Use this for initialization
	void Start () {
	
	}

    void OnMouseDown()
    {
        Debug.Log("CLick2");
        central = GameObject.FindGameObjectWithTag("Central");
        central.SendMessage("Next");
    }



	// Update is called once per frame
	void Update () {
	
	}
}
