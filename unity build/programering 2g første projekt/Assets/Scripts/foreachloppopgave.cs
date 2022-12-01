using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foreachloppopgave : MonoBehaviour
{
    public GameObject thisgameobjekt;
    public float timepassed;
    public float Timeforexsplotion;
    public bool idk;
    // Start is called before the first frame update
    void Start()
    {
        idk = true;
    }

    // Update is called once per frame
    void Update()
    {

       
            eksplosion();
       
        
    }

    void eksplosion()
    {
        timepassed = timepassed + Time.deltaTime;
        if (idk)
        {
            thisgameobjekt.AddComponent<Rigidbody>();
            idk = false;
        }
        thisgameobjekt.GetComponent<Rigidbody>().velocity = new Vector3(0, 5, 0);
        


        if (Timeforexsplotion<timepassed)
        {
            Destroy(thisgameobjekt);
            foreach (Transform part in transform)
            {

                GameObject newpart = Instantiate(part.gameObject, part.transform.position, part.rotation);

                newpart.AddComponent<Rigidbody>();
                


                Vector3 retning = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0.5f,1), Random.Range(-0.5f,0.5f));
                newpart.GetComponent<Rigidbody>().velocity = retning * Random.Range(20, 50);
                


            }
        }
        


    }


}
