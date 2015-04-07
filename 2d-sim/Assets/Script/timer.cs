using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/*
 *This is the script for clock timer
 *It first find all the available character
 *Then whenever the clock hit 5/10/15/20, it sends signal to everyone so they will drop everything and go to class
 *Every 3 seconds is 10 minutes
 *A term have 4 days, and 1 freeday
 *a year have 3 term plus 3 preparation day.
 *so total: 5* 3 + 3 = 18 days
 * 
 */

public class timer : MonoBehaviour {
    public int year = 1;
    public int day = 1;
    public int hour = 0;
    public int mins = 0;
    public GameObject uitimer;
	// Use this for initialization
	void Start () {
        uitimer = GameObject.FindGameObjectWithTag("UiClock");
        StartCoroutine("Time");
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Time()
    {
        Text text1 = uitimer.GetComponent<Text>();
        while (true)
        {
            yield return new WaitForSeconds(1);
            mins = mins + 10;
            if (mins == 60)
            {
                hour = hour + 1;
                mins = 0;
            }
            if (hour == 24)
            {
                day = day + 1;
                hour = 0;
            }
            if ( day == 19)
            {
                year = year + 1;
                day = 0;
            }
            if ((hour == 5 || hour == 15) && (day != 0|| day != 5 || day != 10 || day != 15) && mins == 0)
            {
                SendNewMessage();
            }
            if(hour==1 && (day ==0 || day == 5 || day == 10) && mins == 0)
            {
                SendCentral();
            }
            text1.text = "Year:     " + year + "    Day     " + day + "     Time:       " + hour + " : " + mins; 
        }
    }

    void SendNewMessage()
    {
        GameObject[] students, professors;
        students = GameObject.FindGameObjectsWithTag("Student");
        professors = GameObject.FindGameObjectsWithTag("Prof");
        int i = 0;
        for(i = 0; i < students.Length; i++)
        {
            students[i].SendMessage("ItsTime");
            professors[i].SendMessage("ItsTime");
        }
    }

    void SendCentral()
    {
        GameObject central;
        central = GameObject.FindGameObjectWithTag("Central");
        central.SendMessage("ItsTime");
    }

}
