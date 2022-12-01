using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitplant : Plant
    
{
    public GameObject fruit;
    public float fruitspawntime;
    public float fruitdespawntime;
    public float fruitspawnradius;
    public float maxfruit;
    public float spawnfruitheight;
    public int numberoffruitspawns;
    public float timepassedbetweenfruitspawn;
    public float timebetweenfruitspawn;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        timepassedbetweenfruitspawn = timepassedbetweenfruitspawn + Time.deltaTime;
        timeLived = timeLived + Time.deltaTime;
        if (fruitspawntime < timeLived  && numberoffruitspawns>0 && timepassedbetweenfruitspawn > timebetweenfruitspawn)
        {
            spawnfruit();
            numberoffruitspawns = numberoffruitspawns - 1;
            timepassedbetweenfruitspawn = 0;
        }
   
       
    }



    public void spawnfruit()
    {
       
        for (int i = 0; i < maxfruit; i++)
        {
            Vector3 fruitspawnlocation = transform.position + Random.insideUnitSphere * fruitspawnradius;
            GameObject newfruit = Instantiate(fruit, fruitspawnlocation+new Vector3(0,spawnfruitheight,0), Quaternion.identity);
            StartCoroutine(despawnfruit(newfruit));
            
        }

    }


    public IEnumerator despawnfruit(GameObject _newfruit)
    {

        yield return new WaitForSecondsRealtime(fruitdespawntime);
        Destroy(_newfruit);
        
    }



}
