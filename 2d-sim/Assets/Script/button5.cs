using UnityEngine;
using System.Collections;

public class button5 : MonoBehaviour {
    public GameObject central;
    // Use this for initialization
    void Start()
    {
        central = GameObject.FindGameObjectWithTag("Central");
    }

    void OnMouseDown()
    {
        central.SendMessage("Choose");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
