using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public SortedList<GameObject, int> labs = new SortedList<GameObject, int>();
    public SortedList<GameObject, int> teachs = new SortedList<GameObject, int>();
    public SortedList<GameObject, int> facils = new SortedList<GameObject, int>();
    public GameObject[] class_list, lab_list, facil_list, teach_list;
    public int years;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Add_list(GameObject facult)
    {
        Debug.Log("Get_add");
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
            labs.Add(facult, prior);
        else if (type == 2)
            teachs.Add(facult, prior);
        else if (type == 3)
            facils.Add(facult, prior);
    }
}
