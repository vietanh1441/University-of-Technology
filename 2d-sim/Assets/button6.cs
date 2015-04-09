using UnityEngine;
using System.Collections;

public class button6 : MonoBehaviour {
    public GameObject central;
    // Use this for initialization
    void Start()
    {
        //transform.localPosition = new Vector3(6.25f, 3, 9);
        central = GameObject.FindGameObjectWithTag("Central");
    }

    void OnMouseDown()
    {
        central.SendMessage("StartChooseNewStudent");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
