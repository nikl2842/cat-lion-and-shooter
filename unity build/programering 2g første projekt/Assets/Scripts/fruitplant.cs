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
    public bool fruitspawned = false;
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
   
       
    }



    public void spawnfruit()
    {
       
        for (int i = 0; i < maxfruit; i++)
        {
            Vector3 fruitspawnlocation = transform.position + Random.insideUnitSphere * fruitspawnradius;
            GameObject newfruit = Instantiate(fruit, fruitspawnlocation+new Vector3(0,spawnfruitheight,0), Quaternion.identity);
            StartCoroutine(despawnfruit(newfruit));
            fruitspawned = true;
        }

    }


    public IEnumerator despawnfruit(GameObject _newfruit)
    {

        yield return new WaitForSecondsRealtime(fruitdespawntime);
        Destroy(_newfruit);
        
    }



}
