using UnityEngine;
using System.Collections;

public class button4 : MonoBehaviour {
    public GameObject central;
    // Use this for initialization
    void Start()
    {
        central = GameObject.FindGameObjectWithTag("Central");
    }

    void OnMouseDown()
    {
        central.SendMessage("Back");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
