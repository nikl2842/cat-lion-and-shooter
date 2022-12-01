using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sakuratree : fruitplant
{
    public GameObject flower;
    public float flowerspawntime;
    public float numberofflowerspawns;
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
        
        timeLived = timeLived + Time.deltaTime;
        timepassedbetweenfruitspawn = timepassedbetweenfruitspawn + Time.deltaTime;
        if (flowerspawntime < timeLived && numberofflowerspawns > 0 && timepassedbetweenfruitspawn > timebetweenfruitspawn)
        {
            maxfruit = 100;
            fruit = flower;
            spawnfruit();
            numberofflowerspawns = numberofflowerspawns - 1;
            timepassedbetweenfruitspawn = 0;
        }

        timepassedbetweenfruitspawn = timepassedbetweenfruitspawn + Time.deltaTime;
        timeLived = timeLived + Time.deltaTime;
        if (fruitspawntime < timeLived && numberoffruitspawns > 0 && timepassedbetweenfruitspawn > timebetweenfruitspawn)
        {
            maxfruit = 20;
            spawnfruit();
            numberoffruitspawns = numberoffruitspawns - 1;
            timepassedbetweenfruitspawn = 0;
        }
        if (timeLived > lifeExpectancy && startedDying == false)                    //hvis tiden planten har levet er større end plantens levetid
        {
            StartCoroutine("Die");                                                  //starter coroutine Die()
        }
    }
}
