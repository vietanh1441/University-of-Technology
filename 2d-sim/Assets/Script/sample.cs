﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PolyNavAgent))]
public class sample : MonoBehaviour
{
    /*
    //A character need 8 stats
    //4 study stats: Math, Physics, Chemistry, Bio
    //
    //
    //2 Health stats: body / mental
    //
    //body health, if run low, student need rest. 
    //
    // (More desscription needed)
    //
    //If over 80, student gain +10% of their study
    //50 - 80: normal
    //30-50: lose 10%
    //under 30: lose 20%, + 10% skip class, -1% mental health per minute 
    //under 10: can't do anything unless rest, -2% per min
    //also, body health affect mental health
    //
    //mental health, most important, if run low, student may decide to leave school/ drop out
    //
    //(More description needed)
    //
    //If over 90, and body over 90: gain aura, double every study gain, however, body will wear out twice as fast, lose aura when body < 60.
    //over 80: +10% gain
    //50-80: normal
    //20-50: lose 10% gain, +10% skip class
    //under 20: lost 20% gain, + 20% skip class. !risk of dropout.
    //
    //
    //4 body stats: Intelligence: How fast a student can study
    //Body: How fast student wear out physically (i.e. need rest/ relax)
    //Mental: How fast student wear out mentally
    //Money: Current money and income
    //
    //type:
    // 0 = student
    // 1 = professor
    // 2 = staff
    */
    /*
     * Name:\n Age: \n\n Trait 1:\n Trait 2:\n Money:\n Income:
     */


    public float math, phys, chem, bio;
    public float body_hp, mental_hp;
    public float intel, body, mental;
    public Vector2 money;
    public int type, stud_type1, stud_type2, prof_type1, prof_type2;
    public int stud_years; //years in school of student
    //public int years; //overall years of school

    public string name;
    public int age;

    public bool new_char = true; //determine if character is new, thus when load, doesn't go to starting point.
    public bool get_ans = false, answer = false;
    public int code;
    public GameObject faculty;
    public GameObject central_obj, timer_obj;
    public bool ok = false;
    private PolyNavAgent _agent;
    public float elevator_x = -4.8f;
    float seed;
    private bool class_time = false;
    int i;//global counter
    public PolyNavAgent agent
    {
        get
        {
            if (!_agent)
                _agent = GetComponent<PolyNavAgent>();
            return _agent;
        }
    }

    void Update()
    {
       /* if(Input.GetMouseButton(0)&& ok == false)
        {
           ok = true;
            Okthen(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }*/
    }

    /// <summary>
    /// When character first come in scene, move them from left into the gate
    /// The character then decide what to do
    /// First, check what is the available action. Each avaiable action have an initial percentage modifier.
    /// For example, if type = student, char have 50% study, 30% research, 20% break initial.
    /// After study, study -5%, each other + 2.5%
    /// Then choose destination, check available from top to bottom.
    /// After finish choosing, send signal to dest so dest can mark unavailable that spot and then send back the coordinate
    /// Go to the coordinate
    /// Then applied action modifier
    /// For example, we have a,b,c ac
    /// </summary>
    void Start()
    {
        timer_obj = GameObject.FindGameObjectWithTag("Timer");
        //First set the character in place if character is new
        if(new_char)
        {
            transform.position = new Vector3(-20,0.61f,-1);
        }
        //Init stats
        if (type == 1) // student
        {
            //gameObject.tag = "Student_New";
            Init_stud_stats();
            //Send info to central or something that handle selection of new student
            central_obj.SendMessage("Add_student", gameObject);
        }

        

        //
        
        
      
        //Ask(faculty);
    }

    void Stud_RealStart()
    {
        new_char = false;
        transform.position = new Vector3(seed, 0.61f, -1);
        gameObject.tag = "Student";
		Okthen (new Vector3(-1, 1, 4));
        StartCoroutine("WaitAbit");
    }

    /// <summary>
    /// For Student:
    /// From here, starting the circle of action
    /// if no class, choose class-research-facils then go to random according
    /// when it's time, decide whether to skip class, if do, continue on, otherwise, drop everything and go to class
    /// After that, continue on
    /// </summary>

    void Init_stud_stats()
    {
        seed = Random.Range(-9.0f, -7.0f);
        name = "Student" + (int)Random.Range(0,100);
        central_obj = GameObject.FindGameObjectsWithTag("Central")[0];
        central c_script = central_obj.GetComponent<central>();
        int years = c_script.years;
        int i = 5;
        stud_type1 = Get_type();
        stud_type2 = Get_type();
        math = Random.Range(0, 20 + i * years);
        phys = Random.Range(0, 20 + i * years);
        chem = Random.Range(0, 20 + i * years);
        bio = Random.Range(0, 20 + i * years);
        body_hp = Random.Range(60, 80);
        mental_hp = Random.Range(60, 80);
        intel = Random.Range(0, 100);
        body = Random.Range(0, 100);
        mental = Random.Range(0, 100);
        Debug.Log(stud_type1);
        if (intel > 97)
            intel = Random.Range(160, 180);
        else if (intel > 90)
            intel = Random.Range(140, 160);
        else if (intel > 75)
            intel = Random.Range(120, 140);
        else if (intel > 30)
            intel = Random.Range(90, 120);
        else
            intel = Random.Range(60, 90);
        money = new Vector2(Random.Range(300, 1000), Random.Range(0, 500));
        Init_stat_modifier();
    }


    void Init_stat_modifier()
    {
        while((stud_type1 == 4 && stud_type2 == 5)||(stud_type1 == 5 && stud_type2 == 4))
        {
            stud_type1 = Get_type();
            stud_type2 = Get_type();
        }
        if (stud_type1 == 1 || stud_type2 == 1)
            intel = Random.Range(200, 220);
        if (stud_type1 == 4 || stud_type2 == 4)
            money = new Vector2(Random.Range(2000, 3000), Random.Range(0, 500));
        if (stud_type1 == 5 || stud_type2 == 5)
            money = new Vector2(Random.Range(0, 300), Random.Range(0, 500));
        if (stud_type1 == 7 || stud_type2 == 7)
            body = Random.Range(80, 100);
        if (stud_type1 == 8 || stud_type2 == 8)
            mental = Random.Range(80, 100);
    }

    //Different type of student/prof
    //
    //student:
    // 1 : genius   (One in hundred)    --  IQ > 200
    // 5 : fast learner     --  +20% learning
    // 3 : researcher       --  +20% research work
    // 4 : rich             --  $MONEY$, however, if money fall below 500, become weak-will
    // 5 : poor             --  almost no money, become inspired after 2 years, can't be same with rich
    // 2 : inspire          --  all character (including self) in the same room +10% gain as well as work
    // 6 : hard-worker      --  body health effect appear with 10 less (e.g. 80% aura, 40% normal, 10% stop doing work etc)
    // 7 : athlete          --  body reduction rate halves, body stats > 80
    // 8 : strong-will      --  mental hp reduction rate halves, mental stats > 80
    // 9 : weak-will        --  mental hp reduction rate x 1.5, mental stats < 60
    // 10: independent      --  doesn't get modifier from teacher or anything else, but gained + 70% modifier
    // 11: dependent        --  if teacher is good/inspired, gain extra modifier, but if bad, loose modifier

    int Get_type()
    {
        float percentage = Random.Range(0, 100);
        Debug.Log(percentage);
        int type = 0;
        if (percentage > 99)
            type = 1;
        else if (percentage > 98)
            type = 2;
        else if (percentage > 95)
            type = 3;
        else if (percentage > 90)
            type = 4;
        else if (percentage > 60)
            type = 0;
        else
            type = (int)(percentage / 3 + 5);
        return type;
    }


    //Circle of action:
    //To start a term, a student first get class schedule
    //Performing money check -- if student fund get low, mental stats - 10 x modifier
    //Then, student decide if wanting to go to class
    //If deciding not to skip class, go to class according to schedule
    //During class, student gain stats
    //At the end of class, performing stats check
    //Free period, decide where to go next
    //After free period, perform stats check
    //Go to next class, repeat
    //Exam day, depend on stats, check how well student does on exam according to its state
    //Get result, depend on result, modify mental stats.

    /// <summary>
    /// Deciding if student want to skip class
    /// </summary>
    bool Skip_class()
    {
        bool skip = false;
        //a function to determine if student want to skip
        return skip;
    }

    /// <summary>
    /// Take in a Vector 3 as destination of the character, move the character to the destination point
    /// </summary>
    /// <param name="dest_v"></param>
    void Okthen(Vector3 dest_v)
    {
        float x = dest_v.x;
        float y = dest_v.y;
        float z = dest_v.z;
        //Calculate the current floor of the character
        double s_y = (transform.position.y - 0.3) / 2;
        Debug.Log(s_y);
        int s_floor = (int)s_y;
        Debug.Log(s_floor);

        //Calculate the destination floor
        double e_y = (y - 0.3) / 2;
        Debug.Log(e_y);
        int e_floor = (int)e_y;
        Debug.Log(e_floor);
        //Check if the same floor then move to destination
        if (s_floor == e_floor)
        {
            Debug.Log("WTF");
            StartCoroutine(Do(x,y,z));
            
            
            //agent.SetDestination(dest);
        }

        //If different floor, first move to the stairs, then increase z to make a "going into staircase illusion"
        //then bring to the right floor, then normalize the z, then go to the destination
        else
        {
            Debug.Log("NOT THE SAME");
            StartCoroutine(Dont(s_floor,e_floor,x,y,z));
           
        }
    }

    /// <summary>
    /// Coroutine that is called when character move to the same floor
    /// </summary>

    IEnumerator Do(float x, float y, float z)
    {
        Vector3 dest = new Vector3(x, transform.position.y, -1);
        agent.SetDestination(dest);
        yield return new WaitForSeconds(1);
        ok = false;
    }

    /// <summary>
    /// Coroutine that is called when character move to different floor
    /// </summary>

    IEnumerator Dont(int s_floor, int e_floor, float x, float y, float z)
    {
        Vector3 dest = new Vector3(elevator_x, transform.position.y, -1);
        float sec = (transform.position.x + 5) / 2.5f;
        agent.SetDestination(dest);
        yield return new WaitForSeconds(sec);
        transform.Translate(0, 0, 11);
        yield return new WaitForSeconds(0.2f);
        transform.Translate(0, -(s_floor - e_floor)*2 , 0);
        Debug.Log(-(s_floor - e_floor));
        yield return new WaitForSeconds(0.2f);
        transform.Translate(0, 0, -11 );
        yield return new WaitForSeconds(0.2f);
        if (transform.position.y % 2 != 0.84f)
        {
            transform.Translate(0, e_floor * 2 + 0.84f - transform.position.y, 0);
            yield return new WaitForSeconds(0.5f);
        }
        dest = new Vector3(x, transform.position.y, -1);
        agent.SetDestination(dest);
        yield return new WaitForSeconds(1);
        ok = false;
    }
    
    

    IEnumerator WaitAbit()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine("Iddle");
    }
    IEnumerator Iddle()
    {
        float second;
        second = Random.Range(0f, 0.5f);
        yield return new WaitForSeconds(second);
        Debug.Log("IDDLE");
        Vector3 dest = new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y, transform.position.z);
        Okthen(dest);
        float rand = Random.Range(0f, 100f);
        if (class_time && Skip_class() == true)
        {
            Debug.Log("Skip");
            class_time = false;
        }
        else if (class_time && rand < 60)
        {
            Get_list(0);        //Need to be replaced with Go to Class
        }
        else
        {
            Debug.Log("Com'on");
            Stud_DoThings();
            
        }
    }

    void Stud_DoThings()
    {
        Debug.Log("DoThings");
        float rand = Random.Range(0f, 100f);
        if (rand < 70)
            StartCoroutine("Iddle");
        else
            Get_list(0);
    }

    /// <summary>
    /// This function get the sorted list of available room/facil according to its type
    /// then it start asking to join from ascending order.
    /// </summary>
    /// <param name="type"></param>
    void Get_list(int type)
    {
        central cen_script = central_obj.GetComponent<central>();
        SortedList<int, GameObject> list = cen_script.classes;
        Debug.Log("GET_LIST");
        i = 0;
        Debug.Log(list.Count);
        faculty = list.Values[i];
        Ask(faculty);
    }

    void Ask_Next(int i)
    {
        central cen_script = central_obj.GetComponent<central>();
        SortedList<int, GameObject> list = cen_script.classes;
        faculty = list.Values[i];
        Ask(faculty);
    }

    /// <summary>
    /// Send message to the avalable faculty and ask if available
    /// if do, 
    /// </summary>
    /// <param name="faculty"></param>
    void Ask(GameObject faculty)
    {
        faculty.SendMessage("Asked", gameObject);
    }

    void ItsClassTime()
    {
        Debug.Log("received");
        class_time = true;   
    }

    void Replied(bool answer)
    {
		get_ans = true;
        Debug.Log(answer);
        if(answer==true)
        {
            
         //   timer_obj.SendMessage("Stud_next");
            StartCoroutine("GoToPlace");
        }
        else
        {
            Debug.Log("ARE YOU HERE?");
			i = i+1;
            Ask_Next(i);
        }
        
    }

    IEnumerator GoToPlace()
    {
        Debug.Log(faculty);
        Vector3 dest = faculty.transform.position;
        Okthen(dest);
        yield return new WaitForSeconds(1f);
        Interacting();

    }

    void Interacting()
    {
        //Do stuff here
        Finish_Interacting();
    }

    void Finish_Interacting()
    {
        StartCoroutine("WaitAbit");
        faculty.SendMessage("Stud_Finish");
    }
    IEnumerator lol()
    {
        yield return new WaitForSeconds(3);
        Vector3 dest = new Vector3(0.85f,2.78f,-1f);
        Okthen(dest);
        yield return new WaitForSeconds(10);
        dest = new Vector3(0.85f, 0.78f, -1f);
        Okthen(dest);
    }
}

