using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* How faculty work
 * 
 * first when created, depend on the type that is created, it will either be a 
 * classroom: where student study;
 * lab: where teacher/student do research;
 * teacher lounge: allowed more teacher to be hired, also let the admin do admin research
 * facility: stuff to relax, regain body/mental
 * 
 * All of the stuffs here should be pre-build, nothing random.
 * 
 * If the facility is classroom, at the start of each term, player will choose what class it is
 * After choosing, additional stats will be added.
 * At the end, the class will be destroyed and return to just classroom. 
 *
 */

public class faculty : MonoBehaviour {

    //faculty will have severals variables
    //name
    //cost  : cost to attend/use    -- changeable
    //math, phys, bio, chem : stats gained while using it.
    //priority: the higher the priority, student will priority in taking it.
    //requirement: number of knowledge needed to take it
    //
    //
    public int cost = 0;
    public int type = 0;
    public string name = "class";
    public float math, phys, bio, chem;
    public float math_req, phys_req, bio_req, chem_req;
    public int priority;
    public GameObject central;
    public bool okay = true;
    public GameObject line1;
    public GameObject book, button1;
	// Use this for initialization
	void Start () {
        line1 = GameObject.FindGameObjectWithTag("line1");
        button1 = GameObject.FindGameObjectWithTag("button1");
        book = GameObject.FindGameObjectWithTag("book");
        central = GameObject.FindGameObjectWithTag("Central");
        central.SendMessage("Add_list", gameObject);
        //Add_class(1);
	}
	
	// Update is called once per frame
	void Update () {

	    
	}

    void Add_class(int code)
    {
        switch(code)
        {
            case 1:
                name = "Math 101";
                math = math + 5;
                phys = phys + 0;
                bio = bio + 0;
                chem = chem + 0;
                math_req = 50;
                phys_req = 20;
                bio_req = 20;
                chem_req = 20;
                priority = 2;
                break;
            default:
                break;

        }
    }

    /// <summary>
    /// When click on, stop game and let player see info.
    /// </summary>
    void OnMouseDown()
    {
        Text text1 = line1.GetComponent<Text>();
        //Time.timeScale = 0;
        text1.text = " ok\n then";
        book.transform.localPosition = new Vector3(1, 0, 10);
        button1.transform.localPosition = new Vector3(6.25f, 3, 9);
        //tx.SetActive(true);
    }

    void Asked(GameObject character)
    {
        Debug.Log("GetAsked");
        if(okay == true)
        {
            okay = false;
            character.SendMessage("Replied", true);
        }
        else
        {
            character.SendMessage("Replied", false);
        }
    }
}
