using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float range;                                             //Størrelsen på området den spawner i
    public float spawnCooldown;                                     //Cooldown på spawn, hold tallet over 0
    public GameObject objectToSpawn;                                //Træk det objekt ind der skal spawnes
    public static int numberOfGrass;                                       //Static betyder den deles af alle objekter af klassen Spawner
    public int maxGrass;
   
    void Start()
    {
        InvokeRepeating("Spawn", 0, 1);  // OPGAVE: Lav så den spawner med While-loop.
    }

    public void NumberOfGrassMinusOne()                                             //En metode andre klasser kalder for at sænke numberOfGrass med én
    {
        numberOfGrass--;
    }

    void Spawn()                                                    //Spawn-metoden som spawner et objekt i en random position
    {
        //print(numberOfGrass);

        float randomX = Random.Range(-range,range);
        float randomZ = Random.Range(-range,range);

        RaycastHit hit;
        Ray lookDownRay = new Ray(new Vector3(this.transform.position.x + randomX, 50f, this.transform.position.z + randomZ), Vector3.down);            //raycast

        if (Physics.Raycast(lookDownRay, out hit, 100f) == true)
        {           
            if (hit.collider.tag == "Terrain")
            {
                if (numberOfGrass < maxGrass)
                {
                    Instantiate(objectToSpawn, hit.point + new Vector3(0, 0.0f, 0), Quaternion.identity);            //Spawner objektet der hvor strålen rammer
                    numberOfGrass++;                                                                                 // +1
                }             
            }
        }
    }
}
