using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plante : MonoBehaviour
{
    public string growx;
    public float timelived;
    public float timetogrow1;
    public float timetogrow2;
    public float timetogrow3;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        grow();
    }
    virtual public void grow()
    {

        timelived += Time.deltaTime;

        this.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        if (timelived > timetogrow1)
        {
            this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        if (timelived > timetogrow2)
        {
            this.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
        if (timelived > timetogrow3)
        {
            this.transform.localScale = new Vector3(1f, 1f, 1f);  //grow with a different time
        }


        growx = "grow1mm";
        //print(growx);


    }
}
