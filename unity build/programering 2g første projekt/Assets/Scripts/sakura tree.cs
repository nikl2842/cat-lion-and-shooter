using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sakuratree : fruitplant
{
    GameObject flower;
    public float flowerspawntime;
    public bool flowerspawned = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        timeLived = timeLived + Time.deltaTime;
        if (fruitspawntime < timeLived && fruitspawned == false)
        {
            spawnfruit();

        }
        
        if (flowerspawntime < timeLived && flowerspawned == false)
        {
            fruit = flower;
            spawnfruit();

        }
    }
}
