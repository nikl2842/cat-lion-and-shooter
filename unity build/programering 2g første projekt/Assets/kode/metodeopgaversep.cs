using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class metodeopgaversep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AAA();
        BBB("hvad så");
        print(CCC());
        BBB(CCC());
    }


    void AAA()
    {
        print("hej");
    }

    void BBB(string BBBB)
    {
        print(BBBB);
    }


    string CCC()
    {
        return "ikke noget";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
