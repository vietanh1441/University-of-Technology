using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/*
 8 hour day
day 0: choose student + professor + assign class need to be done before 8
day 1: (whole day) student choose class
day 2: class start at 4
day 3: class
day 4: class
day 5: mid term + assign class
day 6: student choose class
day 7: class
day 8: class
day 9: class
day 10: final + choose student + assign class
day 11-20: same with 1-10
day 21-30: same
day 31: End of year stuffs
 */

public class timer : MonoBehaviour {
    public int year = 1;
    public int day = 1;
    public int hour = 0;
    public int mins = 0;
    public GameObject button6;
    public GameObject uitimer;
    int i; //global counter;
	// Use this for initialization
	void Start () {
        uitimer = GameObject.FindGameObjectWithTag("UiClock");
        StartCoroutine("Time");
        button6 = GameObject.FindGameObjectWithTag("button6");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Time()
    {
        Text text1 = uitimer.GetComponent<Text>();
        while (true)
        {
            yield return new WaitForSeconds(2);
            mins = mins + 30;
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
            if ( day == 32)
            {
                year = year + 1;
                day = 0;
            }
            //if((hour == 15)&&(day ==0))

            if ((hour == 3 || hour == 15) && (day == 0 && day != 5 && day != 10 && day != 15) && mins == 0)
            {
                SendNewMessage();
            }
            if(hour==1 && (day ==0 || day == 5 || day == 10) && mins == 0)
            {
                SendCentral();
            }
            if(hour==0 && mins == 0 && (day == 1 || day == 6 || day == 11))
            {
                button6.transform.localPosition = new Vector3(0, 0, 110);
            }
            text1.text = "Year:     " + year + "    Day     " + day + "     Time:       " + hour + " : " + mins; 
        }
    }

    void SendNewMessage()
    {
        //GameObject[] students, professors;
        //students = GameObject.FindGameObjectsWithTag("Student");
        //professors = GameObject.FindGameObjectsWithTag("Prof");
        int i = 0;
        GameObject[] students;
        students = GameObject.FindGameObjectsWithTag("Student");
        //students[i].SendMessage("ItsTime");
        for (i = 0; i < students.Length;i++ )
            students[i].SendMessage("ItsClassTime");
            //StudMessage(i);
                
          //  if(professors.Length>0)
            //    professors[i].SendMessage("ItsTime");

        
    }

    void StudMessage(int i)
    {
        Debug.Log(i);
        GameObject[] students;
        students = GameObject.FindGameObjectsWithTag("Student");
        students[i].SendMessage("ItsTime");
    }

    void Stud_next()
    {
        Debug.Log("NEXTTTT");
        StudMessage(i + 1);
        
    }

    void SendCentral()
    {
        GameObject central;
        central = GameObject.FindGameObjectWithTag("Central");
        central.SendMessage("ItsTime");
    }

}
