using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantcopyclass : Plant
{
    // Start is called before the first frame update
    void Start()
    {
        numberOfPlants++;                                                           //l�gger �n til antal planter
        myRenderer = GetComponent<Renderer>();                                      //finder rendereren p� planten og gemmer den i myRenderer-variablen s� vi senere kan referere til den
        ResetGlobalVariableValues();                                                //kalder metode
        InvokeRepeating("Reproduce", reproductionCooldown, reproductionCooldown);   //kalder metoden Reproduce() hver gang der er g�et "reproductionCooldown"-sekunder
        StartCoroutine("Grow");
    }

    // Update is called once per frame
    void Update()
    {
        timeLived = timeLived + Time.deltaTime;                                     //der l�gges lidt til tiden planten har levet

        if (timeLived > lifeExpectancy && startedDying == false)                    //hvis tiden planten har levet er st�rre end plantens levetid
        {
            StartCoroutine("Die");                                                  //starter coroutine Die()
        }
 
    }
}
