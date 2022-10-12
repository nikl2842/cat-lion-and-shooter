using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public float maxSize;               //maximum st�rrelse planten kan f�
    public float startSize;             //startst�rrelse
    public float growRate;              //hastighed planten vokser med
    public float reproductionCooldown;  //hastigheden planten reproducerer sig med
    public float reproductionDistance;  //afstanden planten kan sprede sig
    public int maxPlants;               //maximalt tilladt antal planter
    public static int numberOfPlants;   //antal planter i spillet
    public float lifeExpectancy;        //levetid for planten
    public float timeLived;             //tid planten har levet
    public bool startedDying;           //er planten g�et i gang med sin d�dsproces
    public int deathDuration;           //hvor lang tid d�dsprocessen skal str�kkes over (ikke i sekunder)
    public GameObject newPlant;         //gameObject til ny plante der kommer ved reproduktion
    public Renderer myRenderer;         //rendereren til planten (til at lave om i udseendet)

    
    void Start()
    {
        numberOfPlants++;                                                           //l�gger �n til antal planter
        myRenderer = GetComponent<Renderer>();                                      //finder rendereren p� planten og gemmer den i myRenderer-variablen s� vi senere kan referere til den
        ResetGlobalVariableValues();                                                //kalder metode
        InvokeRepeating("Reproduce", reproductionCooldown, reproductionCooldown);   //kalder metoden Reproduce() hver gang der er g�et "reproductionCooldown"-sekunder
        StartCoroutine("Grow");                                                     //starter Grow() coroutinen
    }
    
    void Update()
    {
        timeLived = timeLived + Time.deltaTime;                                     //der l�gges lidt til tiden planten har levet

        if (timeLived > lifeExpectancy && startedDying == false)                    //hvis tiden planten har levet er st�rre end plantens levetid
        {
            StartCoroutine("Die");                                                  //starter coroutine Die()
        }
    }

    public void ResetGlobalVariableValues()                                                //s�tter nogle globale variabler til nogle startv�rdier fordi Unity ved en fejl kopierer de gamle v�rdier fra planten som en ny plante kommer fra
    {
        transform.localScale = new Vector3(startSize, startSize, startSize);        //s�tter planten til en bestemt st�rrelse
        timeLived = 0;                                                              //s�tter timeLived til 0
        startedDying = false;                                                       //s�tter startedDying til false
        myRenderer.material.color = new Color(1, 1, 1);                             //s�tter farven til hvid
    }

    public IEnumerator Grow()
    {
        while (transform.localScale.magnitude < maxSize && startedDying == false)       //mens plantens st�rrelse er under maxSize OG den ikke er startet d�dsprocessen
        {
            transform.localScale += new Vector3(growRate, growRate, growRate);          //planten vokser
            yield return new WaitForSecondsRealtime(0.05f);                             //vent 0.05 sekunder
        }
    }

    public void Reproduce() 
    {
        if (numberOfPlants < maxPlants)                                                     //hvis antal planter er under max tilladte antal planter
        {
            float randomX = Random.Range(-reproductionDistance, reproductionDistance);      //laver random tal
            float randomZ = Random.Range(-reproductionDistance, reproductionDistance);      //laver random tal
            RaycastHit hit;
            Ray lookDownRay = new Ray(new Vector3(this.transform.position.x + randomX, 50f, this.transform.position.z + randomZ), Vector3.down);            //skyder en laserstr�le ned fra himlen

            if (Physics.Raycast(lookDownRay, out hit, 100f) == true)
            {
                if (hit.collider.tag == "Terrain")                                                                                                          //hvis lasertr�len rammer Terrain
                {
                    Instantiate(newPlant, hit.point + new Vector3(0, 0.0f, 0), Quaternion.identity);                                                        //Spawner objektet der hvor str�len rammer
                }
            }
        }
    }

    public IEnumerator Die()                                                           //coroutine til n�r planten skal visne og d�
    {
        startedDying = true;                                                    //starteddying is true now
        for (int i = 0; i < deathDuration; i++)                                           //for-loop der k�rer 150 gange
        {
            myRenderer.material.color -= new Color(0.01f,0.01f,0.01f);       //g�r farven lidt m�rkere

            if (transform.localScale.x > 0)                             //hvis planten er st�rre end 0
            {
                transform.localScale += new Vector3(-growRate * 0.3f, -growRate * 0.3f, -growRate * 0.3f);  //g�r planten mindre
            }
            
            yield return new WaitForSecondsRealtime(0.05f);                     //venter 0.05 sekunder
        }

        Destroy(gameObject);                                                    //fjerner planten fra spillet  
        numberOfPlants--;                                                       //tr�kker �n fra antal planter
    }
}
