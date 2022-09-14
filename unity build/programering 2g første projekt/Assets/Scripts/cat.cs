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
    public State myState;                                       //enum-objekt som holder styr på hvilken state man er i
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
        switch (myState)                                    //switch case som kigger på myState og afvikler den state
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
        GameObject closestGrass;                            //Lokal gameobject-variabel til det nærmeste stykke mad
        float shortestDistance = 9999;                      //lokal float-variabel til at holde den korteste afstand til et stykke mad

        foreach (GameObject food in allFood)              //foreach er ligesom et for-loop, bare det kører igennem alle elementer i en liste eller et array
        {
            if (food == null)                              //Hvis maden ikke eksisterer
            {
                continue;                                   //foreach-loopet springer resten af sin kode over, og kører videre til næste iteration
            }

            if (Vector3.Distance(food.transform.position, this.transform.position) < shortestDistance)              //this afstanen mellem mad og løve er mindre end shortestDistance
            {


                shortestDistance = Vector3.Distance(food.transform.position, this.transform.position);             //shortestDistance = afstanden mellem løve og mad
                closestGrass = food;                                                                               //Det stykke mad foreach-loopet arbejder på lige nu, gemmes i variablen closestFood
                agent.SetDestination(closestGrass.transform.position);                                              //Dette stykke mad sættes som agentens (løvens) destination
            }
        }
    }

    private void OnTriggerEnter(Collider other)             //metode som bliver kaldt når et gameobject rammer ind i den runde trigger der sidder på løven
    {
        if (other.tag == "Horse"||other.tag=="lion")
        {
            allFood.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)              //metode som bliver kaldt når et gameobject bevæger sig ud af den runde trigger der sidder på løven
    {
        if (other.tag == "Horse"||other.tag=="lion")
        {
            allFood.Remove(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)          //Metode som kaldes når løven fysisk rammer ind i noget
    {
        if (collision.collider.tag == "Horse"|| collision.collider.tag =="lion")                  //Hvis det løven rammer er tagget "Horse"
        {
            foodEaten += 1;


            if (foodEaten >= foodRequiredToGiveBirth && grown == true && numberOfcats < maxcats)     //hvis der er spist nok OG grown == true OG antal løver er under maxLions
            {
                foodEaten = 0;
                StartCoroutine(GiveBirth());                    //Starter coroutine
            }

            allFood.Remove(collision.collider.gameObject);     //Fjern det stykke mad fra listen allFood
            collision.collider.gameObject.GetComponent<Horse>().DecreaseNumberOgHorsesByOne();   //Kalder en metode der nedsætter variablen NumberOfHorses med 1. Vi kan ikke ændre denne variabel direkte fra denne klasse da den er static
            Destroy(collision.collider.gameObject);             //Fjern det stykke mad helt fra spillet
            myState = State.Sleep;
            StartCoroutine(WakeUpInXSeconds(5));
        }
    }

    IEnumerator GiveBirth()
    {
        for (int i = 0; i < 80; i++)                                                    //for-loop som kører 80 gange
        {
            this.transform.localScale += new Vector3(0.004f, 0.004f, 0.004f);             //Gør løven større
            yield return new WaitForSeconds(0.01f);                                     //venter 0.05 sekund
        }

        this.transform.localScale = new Vector3(1, 1, 1);                                 //sætter løven til almindelig størrelse
        Instantiate(catObj, this.transform.position + new Vector3(1, 0, 0), Quaternion.identity);             //spanwner en ny løve
    }

    IEnumerator GrowUp()
    {

        this.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);                        //sætter løven til lille størrelse

        for (int i = 0; i < 350; i++)
        {
            this.transform.localScale += new Vector3(0.002f, 0.002f, 0.002f);             //Gør løven større
            yield return new WaitForSeconds(0.01f);
        }

        this.transform.localScale = new Vector3(1, 1, 1);                                 //sætter løven til almindelig størrelse
        grown = true;                                                                     //bool-variabel
        myState = State.Eat;                                                              //eat state
    }

    IEnumerator WakeUpInXSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        myState = State.Eat;
    }


}

