using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pølsetræ : plante
{
    // Start is called before the first frame update
    void Start()
    {
        timetogrow1 = 5;
        timetogrow2 = 10;
        timetogrow3 = 15;
        
    }

    // Update is called once per frame
    void Update()
    {
        grow();
    }
  
}
