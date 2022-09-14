using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Horse : MonoBehaviour
{

    public enum State                                       //enum som viser hvilke states der findes 
    { 
        Sleep,
        Eat,
        Flee,
    }

    public List<GameObject> allGrass = new List<GameObject>();  //Liste til alt gr�sset i scenen
    public GameObject[] allLions;                                //Array til alle l�ver i scenen
    public State myState;                                       //enum-objekt som holder styr p� hvilken state man er i
    //public GameObject bed;                                  //senge-objekt
    public NavMeshAgent agent;                              //navmesh-agent
    public GameObject horseObj;
    public int grassEaten;
    public int foodRequiredToGiveBirth;
    bool grown = false;
    static int numberOfHorses;
    public int maxHorses;
    public float fleeDistance;
    float fleeTime;
    
    void Start()
    {
        numberOfHorses++;
        // bed = GameObject.Find("Bed");                       //Finder et gameobject i scenen der hedder "Bed" og gemmer det i bed-variablen
        myState = State.Eat;                                    //Starter altid i Eat-state

        StartCoroutine(GrowUp());
    }
    
    void Update()
    {
        switch (myState)                                    //switch case som kigger p� myState og afvikler den state
        {
            case State.Sleep:
                Sleep();                                    //sleep-metoden kaldes
                break;

            case State.Eat:
                Eat();
                break;

            case State.Flee:
                Flee();
                break;
        }  
    }

    void CheckForLionsNearby() 
    {
        GameObject closestLion;
        float shortestDistance = 9999;
        allLions = GameObject.FindGameObjectsWithTag("Lion");

        foreach (GameObject lion in allLions)
        {
            if (lion == null)
            {
                continue;
            }

            if (Vector3.Distance(lion.transform.position, this.transform.position) < shortestDistance)
            {
                shortestDistance = Vector3.Distance(lion.transform.position, this.transform.position);
                closestLion = lion;
                if (shortestDistance < fleeDistance)
                {
                    myState = State.Flee;
                }
            }
        }
    }

    void Flee() 
    {
        fleeTime += Time.deltaTime;

        if (fleeTime > 3)
        {
            fleeTime = 0;
            myState = State.Eat;
        }

        GameObject closestLion;
        float shortestDistance = 9999;

        foreach (GameObject lion in allLions)
        {
            if (lion == null)
            {
                continue;
            }

            if (Vector3.Distance(lion.transform.position, this.transform.position) < shortestDistance)
            {
                shortestDistance = Vector3.Distance(lion.transform.position, this.transform.position);
                closestLion = lion;
                agent.SetDestination(transform.position + transform.position - lion.transform.position);
                //print(transform.position + transform.position - lion.transform.position);
            }
        }
    }

    void Sleep()                                            //sleep-metode
    {
                                               
    }

    void Eat()                                              //Eat-metoden
    {
        CheckForLionsNearby();

        GameObject closestGrass;                            //Lokal gameobject-variabel til det n�rmeste stykke gr�s
        float shortestDistance = 9999;                      //lokal float-variabel til at holde den korteste afstand til et stykke gr�s

        foreach (GameObject grass in allGrass)              //foreach er ligesom et for-loop, bare det k�rer igennem alle elementer i en liste eller et array
        {
            if (grass == null)                              //Hvis gr�sset ikke eksisterer
            {
                continue;                                   //foreach-loopet springer resten af sin kode over, og k�rer videre til n�ste iteration
            }

            if (Vector3.Distance(grass.transform.position,this.transform.position) < shortestDistance)              //this afstanen mellem gr�s og hest er mindre end shortestDistance
            {
                

                shortestDistance = Vector3.Distance(grass.transform.position, this.transform.position);             //shortestDistance = afstanden mellem gr�s og hest
                closestGrass = grass;                                                                               //Det stykke gr�s foreach-loopet arbejder p� lige nu, gemmes i variablen closestGrass
                agent.SetDestination(closestGrass.transform.position);                                              //Dette stykke gr�s s�ttes som agentens (hesten) destination
            }
        }       
    }

    private void OnTriggerEnter(Collider other)             //metode som bliver kaldt n�r et gameobject rammer ind i den runde trigger der sidder p� hesten
    {
        if (other.tag == "Grass")
        {
            allGrass.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)              //metode som bliver kaldt n�r et gameobject bev�ger sig ud af den runde trigger der sidder p� hesten
    {
        if (other.tag == "Grass")
        {
            allGrass.Remove(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)          //Metode som kaldes n�r hesten fysisk rammer ind i noget
    {
        if (collision.collider.tag == "Grass")                  //Hvis det hesten rammer er tagget "Grass"
        {
            grassEaten += 1;
            GameObject.Find("Spawner").GetComponent<Spawner>().NumberOfGrassMinusOne();

            if (grassEaten >= foodRequiredToGiveBirth && grown == true && numberOfHorses < maxHorses)     //hvis der er spist nok OG grown == true OG antal heste er under maxHorses
            {
                grassEaten = 0;
                StartCoroutine(GiveBirth());                    //Starter coroutine
            }

            allGrass.Remove(collision.collider.gameObject);     //Fjern det stykke gr�s fra listen allGrass
            Destroy(collision.collider.gameObject);             //Fjern det stykke gr�s helt fra spillet
        }
    }

    IEnumerator GiveBirth()                                     
    {
        for (int i = 0; i < 80; i++)                                                    //for-loop som k�rer 80 gange
        {
            this.transform.localScale += new Vector3(0.004f,0.004f,0.004f);             //G�r hesten st�rre
            yield return new WaitForSeconds(0.01f);                                     //venter 0.05 sekund
        }

        this.transform.localScale = new Vector3(1,1,1);                                 //s�tter hesten til almindelig st�rrelse
        Instantiate(horseObj, this.transform.position + new Vector3(1,0,0), Quaternion.identity);            //spanwner en ny hest
    }

    IEnumerator GrowUp() 
    {

        this.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);                        //s�tter hesten til lille st�rrelse

        for (int i = 0; i < 350; i++)
        {
            this.transform.localScale += new Vector3(0.002f, 0.002f, 0.002f);             //G�r hesten st�rre
            yield return new WaitForSeconds(0.01f);
        }

        this.transform.localScale = new Vector3(1, 1, 1);                                 //s�tter hesten til almindelig st�rrelse
        grown = true;                                                                     // bool-variabel
    }

    public void DecreaseNumberOgHorsesByOne() 
    {
        numberOfHorses--;
    }

    
}
