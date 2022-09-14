using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class cat : MonoBehaviour
{

    public enum State                                       //enum som viser hvilke states der findes 
    {
        Sleep,
        Eat,
    }

    public List<GameObject> allFood = new List<GameObject>();  //Liste til alt mad i scenen
    public State myState;                                       //enum-objekt som holder styr p� hvilken state man er i
    //public GameObject bed;                                  //senge-objekt
    public NavMeshAgent agent;                              //navmesh-agent
    public GameObject catObj;
    public int foodEaten;
    public int foodRequiredToGiveBirth;
    bool grown = false;
    static int numberOfcats;
    public int maxcats;

    void Start()
    {
        numberOfcats++;
        // bed = GameObject.Find("Bed");                       //Finder et gameobject i scenen der hedder "Bed" og gemmer det i bed-variablen
        myState = State.Sleep;                                    //Starter altid i Eat-state

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
        }
    }

    void Sleep()                                            //sleep-metode
    {

    }

    void Eat()                                              //Eat-metoden
    {
        GameObject closestGrass;                            //Lokal gameobject-variabel til det n�rmeste stykke mad
        float shortestDistance = 9999;                      //lokal float-variabel til at holde den korteste afstand til et stykke mad

        foreach (GameObject food in allFood)              //foreach er ligesom et for-loop, bare det k�rer igennem alle elementer i en liste eller et array
        {
            if (food == null)                              //Hvis maden ikke eksisterer
            {
                continue;                                   //foreach-loopet springer resten af sin kode over, og k�rer videre til n�ste iteration
            }

            if (Vector3.Distance(food.transform.position, this.transform.position) < shortestDistance)              //this afstanen mellem mad og l�ve er mindre end shortestDistance
            {


                shortestDistance = Vector3.Distance(food.transform.position, this.transform.position);             //shortestDistance = afstanden mellem l�ve og mad
                closestGrass = food;                                                                               //Det stykke mad foreach-loopet arbejder p� lige nu, gemmes i variablen closestFood
                agent.SetDestination(closestGrass.transform.position);                                              //Dette stykke mad s�ttes som agentens (l�vens) destination
            }
        }
    }

    private void OnTriggerEnter(Collider other)             //metode som bliver kaldt n�r et gameobject rammer ind i den runde trigger der sidder p� l�ven
    {
        if (other.tag == "Horse"||other.tag=="lion")
        {
            allFood.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)              //metode som bliver kaldt n�r et gameobject bev�ger sig ud af den runde trigger der sidder p� l�ven
    {
        if (other.tag == "Horse"||other.tag=="lion")
        {
            allFood.Remove(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)          //Metode som kaldes n�r l�ven fysisk rammer ind i noget
    {
        if (collision.collider.tag == "Horse"|| collision.collider.tag =="lion")                  //Hvis det l�ven rammer er tagget "Horse"
        {
            foodEaten += 1;


            if (foodEaten >= foodRequiredToGiveBirth && grown == true && numberOfcats < maxcats)     //hvis der er spist nok OG grown == true OG antal l�ver er under maxLions
            {
                foodEaten = 0;
                StartCoroutine(GiveBirth());                    //Starter coroutine
            }

            allFood.Remove(collision.collider.gameObject);     //Fjern det stykke mad fra listen allFood
            collision.collider.gameObject.GetComponent<Horse>().DecreaseNumberOgHorsesByOne();   //Kalder en metode der neds�tter variablen NumberOfHorses med 1. Vi kan ikke �ndre denne variabel direkte fra denne klasse da den er static
            Destroy(collision.collider.gameObject);             //Fjern det stykke mad helt fra spillet
            myState = State.Sleep;
            StartCoroutine(WakeUpInXSeconds(5));
        }
    }

    IEnumerator GiveBirth()
    {
        for (int i = 0; i < 80; i++)                                                    //for-loop som k�rer 80 gange
        {
            this.transform.localScale += new Vector3(0.004f, 0.004f, 0.004f);             //G�r l�ven st�rre
            yield return new WaitForSeconds(0.01f);                                     //venter 0.05 sekund
        }

        this.transform.localScale = new Vector3(1, 1, 1);                                 //s�tter l�ven til almindelig st�rrelse
        Instantiate(catObj, this.transform.position + new Vector3(1, 0, 0), Quaternion.identity);             //spanwner en ny l�ve
    }

    IEnumerator GrowUp()
    {

        this.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);                        //s�tter l�ven til lille st�rrelse

        for (int i = 0; i < 350; i++)
        {
            this.transform.localScale += new Vector3(0.002f, 0.002f, 0.002f);             //G�r l�ven st�rre
            yield return new WaitForSeconds(0.01f);
        }

        this.transform.localScale = new Vector3(1, 1, 1);                                 //s�tter l�ven til almindelig st�rrelse
        grown = true;                                                                     //bool-variabel
        myState = State.Eat;                                                              //eat state
    }

    IEnumerator WakeUpInXSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        myState = State.Eat;
    }


}

