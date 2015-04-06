using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class button1 : MonoBehaviour {
    public GameObject line1;
    public GameObject book;
	// Use this for initialization
	void Start () {
        line1 = GameObject.FindGameObjectWithTag("line1");
        book = GameObject.FindGameObjectWithTag("book");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        Debug.Log("Click");
        Text text1 = line1.GetComponent<Text>();
        //Time.timeScale = 0;
        text1.text = " "    ;
        book.transform.localPosition = new Vector3(0, 0, 110);
        transform.localPosition = new Vector3(0, 0, 110);
    }
}
