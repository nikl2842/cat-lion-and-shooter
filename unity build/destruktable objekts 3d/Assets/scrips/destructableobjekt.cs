using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructableobjekt : MonoBehaviour
{
    Vector3 spawnrange;
    public float startofx;
    public float endofx;
    public float startofz;
    public float endofz;
    public int objektstospawn;
    public GameObject destructableobjekts;
    // Start is called before the first frame update
    void Start()
    {
        spawndestructableobjekts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void animate()
    {
        Destroy(this);

    }
    public IEnumerator spawn()
    {
        spawnrange = new Vector3(Random.Range(startofx, endofx), 0.8f, Random.Range(startofz, endofz));
        Instantiate(destructableobjekts, spawnrange, Quaternion.identity);

        yield return new WaitForSecondsRealtime(0);

    }
    public void spawndestructableobjekts()
    {
        for (int i = 0; i < objektstospawn; i++)
        {
            StartCoroutine(spawn());
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "destructableobjekt")
        {
            StartCoroutine(destroyobjekt(objekthit));


        }



    }

    public IEnumerator destroyobjekt(GameObjekt objekthit)
    {





        yield return new WaitForSecondsRealtime(4);

        Destroy(objekthit);

    }
}
