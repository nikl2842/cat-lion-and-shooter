using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitTree : MonoBehaviour
{
    public GameObject[] typesOfFruit; //liste af frugt
    public Transform fruitSpawnPoint; //transform eller position af frugter
    
    void Start()
    {
        StartCoroutine(SpawnFruitsAllTheTime(1f));
    }

    IEnumerator SpawnFruitsAllTheTime(float timeBetweenFruits) 
    {
        while (true) 
        {
            yield return new WaitForSeconds(timeBetweenFruits);
            SpawnAFruit(fruitSpawnPoint, GetRandomFruit());
        }
    }

    GameObject GetRandomFruit() 
    {
        int a = GetARandomInt(0,typesOfFruit.Length); //vælger et tilfældigt tal
        GameObject randomFruit = typesOfFruit[a]; //giver en frugt
        return randomFruit;
    }

    void SpawnAFruit(Transform spawnPoint, GameObject randomFruit) 
    {
        Instantiate(randomFruit, spawnPoint.position, Quaternion.identity); //spawn en tilfædldig frugt ud fra position
    }

    int GetARandomInt(int a, int b) 
    {
        int c = Random.Range(a, b); //giver tilfældig int
        return c;
    }
}
