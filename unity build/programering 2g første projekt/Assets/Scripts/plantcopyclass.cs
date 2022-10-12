using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantcopyclass : Plant
{
    // Start is called before the first frame update
    void Start()
    {
        numberOfPlants++;                                                           //lægger én til antal planter
        myRenderer = GetComponent<Renderer>();                                      //finder rendereren på planten og gemmer den i myRenderer-variablen så vi senere kan referere til den
        ResetGlobalVariableValues();                                                //kalder metode
        InvokeRepeating("Reproduce", reproductionCooldown, reproductionCooldown);   //kalder metoden Reproduce() hver gang der er gået "reproductionCooldown"-sekunder
        StartCoroutine("Grow");
    }

    // Update is called once per frame
    void Update()
    {
        timeLived = timeLived + Time.deltaTime;                                     //der lægges lidt til tiden planten har levet

        if (timeLived > lifeExpectancy && startedDying == false)                    //hvis tiden planten har levet er større end plantens levetid
        {
            StartCoroutine("Die");                                                  //starter coroutine Die()
        }
 
    }
}
