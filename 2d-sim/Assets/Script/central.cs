using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/*
 * 
 * The central will take all the list of all the facility
 * then sort them by priority
 * then return it to the student looking for stuffs to do
 * 
 * Whenever a new facility is build, Central discard the old sorted list and build a new list
 * 
 */


//TO DO: TEST BY HAVING 2 class and test by giving timer let student choose class.
public class central : MonoBehaviour {
    public SortedList<int, GameObject> classes = new SortedList<int, GameObject>();
    public SortedList<int, GameObject> labs = new SortedList<int, GameObject>();
    public SortedList<int, GameObject> teachs = new SortedList<int, GameObject>();
    public SortedList<int, GameObject> facils = new SortedList<int, GameObject>();
    public GameObject[] class_list, lab_list, facil_list, teach_list;
    public List<GameObject> new_stud_list = new List<GameObject>();
    public int years;
    public GameObject line1;
    public GameObject book, button2,button3, button4, button5;
    public int i = 0; //counter
	// Use this for initialization
	void Start () {
        line1 = GameObject.FindGameObjectWithTag("line1");
        button2 = GameObject.FindGameObjectWithTag("button2");
        button3 = GameObject.FindGameObjectWithTag("button3");
        button4 = GameObject.FindGameObjectWithTag("button4");
        button5 = GameObject.FindGameObjectWithTag("button5");
        book = GameObject.FindGameObjectWithTag("book");
       
	}
	
    void ItsTime()
    {
        //Stop time
        Time.timeScale = 0;
        //First telling player they need to choose new student

        //Starting displaying
        Display(new_stud_list[0]);
    }

    void Display(GameObject stud)
    {
        sample stud_script = stud.GetComponent<sample>();
        Text text1 = line1.GetComponent<Text>();
        //Time.timeScale = 0;
        text1.text = "Name" + stud_script.name;
        book.transform.localPosition = new Vector3(1, 0, 10);
        button3.transform.localPosition = new Vector3(6.25f, 3, 9);
        button2.transform.localPosition = new Vector3(6.25f, .5f, 9);
        button5.transform.localPosition = new Vector3(3.25f, 0, 9);
        
    }
	
    /// <summary>
    /// When next button is push, check if there is next in list, if it does, display the next new student.
    /// Otherwise, do nothing.
    /// </summary>
    /// <param name="i"></param>
    void Next()
    {
        i++;
        Debug.Log(new_stud_list.Count);
        if (i < new_stud_list.Count)
        {
            button4.transform.localPosition = new Vector3(0f, .5f, 9);
            Debug.Log(i + "lol" +  new_stud_list.Count);
            if (i == new_stud_list.Count-1)
            {
                button2.transform.position = new Vector3(0, 0, 110);
                Debug.Log("WHYNOT?");
                
            }
            Display(new_stud_list[i]);
        }
        else
            button2.transform.localPosition = new Vector3(0, 0, 110);

    }

    void Back()
    {
        if (i != 0)
        {
            i--;
            Display(new_stud_list[i]);
        }
        else
            button4.transform.localPosition = new Vector3(0, 0, 110);
    }

    void Choose()
    {
        new_stud_list[i].SendMessage("RealStart");
    }

    /// <summary>
    /// Function handle closing all the UI for the new student
    /// </summary>
    void CloseNewStud()
    {
        //clean up, reset button2 counter and make time run
        Text text1 = line1.GetComponent<Text>();
        //Time.timeScale = 0;
        text1.text = " ";
        book.transform.localPosition = new Vector3(0, 0, 110);
        button2.transform.localPosition = new Vector3(0, 0, 110);
        button3.transform.localPosition = new Vector3(0, 0, 110);
        button4.transform.localPosition = new Vector3(0, 0, 110);
        button5.transform.localPosition = new Vector3(0, 0, 110);
        i = 0;
        Time.timeScale = 1;
    }

    void Add_student(GameObject stud)
    {
        sample stud_script = stud.GetComponent<sample>();
        int type = stud_script.type;
        new_stud_list.Add(stud);
    }

    //TO DO: NEED A MINUS_LIST
    void Add_list(GameObject facult)
    {
        faculty fac_script = facult.GetComponent<faculty>();
        int type = fac_script.type;
        Debug.Log(type);
        if (type == 0)
        {
            class_list = GameObject.FindGameObjectsWithTag("Class");
            Debug.Log(class_list.Length);
            classes.Clear();
            Start_sort(0);
        }
        else if (type == 1)
        {
            lab_list = GameObject.FindGameObjectsWithTag("Lab");
            labs.Clear();
            Start_sort(1);
        }
        else if (type == 2)
        {
            teach_list = GameObject.FindGameObjectsWithTag("Teach");
            teachs.Clear();
            Start_sort(2);
        }
        else 
        {
            facil_list = GameObject.FindGameObjectsWithTag("Facil");
            facils.Clear();
            Start_sort(3);
        }
    }

    void Start_sort(int type)
    {
        int i;
        if(type == 0)
        for(i=0;i<class_list.Length; i++)
        {
            Sort_list(class_list[i], 0);
        }
        else if (type == 1)
        for (i = 0; i < lab_list.Length; i++)
        {
            Sort_list(lab_list[i], 1);
        }
        else if (type == 2)
            for (i = 0; i < teach_list.Length; i++)
            {
                Sort_list(teach_list[i], 2);
            }
        else if (type == 3)
            for (i = 0; i < facil_list.Length; i++)
            {
                Sort_list(facil_list[i], 3);
            }
    }

    void Sort_list(GameObject facult, int type)
    {
        faculty fac_script = facult.GetComponent<faculty>();
        int prior = fac_script.priority;
        Debug.Log("Prior" + prior);
        if(type == 0)
            classes.Add(prior,facult);
        else if (type == 1)
            labs.Add(prior, facult);
        else if (type == 2)
            teachs.Add(prior, facult);
        else if (type == 3)
            facils.Add(prior, facult);
    }
}
