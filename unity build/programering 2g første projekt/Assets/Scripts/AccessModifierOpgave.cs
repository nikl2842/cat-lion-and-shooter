using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessModifierOpgave : MonoBehaviour
{
    public Mand mand;

    void Start()
    {
        print("Mandens navn er: ");
        string mandsundercovernavn = mand.findundercovernavn();
        print(mand.navn);
        print("mandens alias er: ");
        print(mandsundercovernavn);
        print("Mandens hemmelige er job: ");
        string hanshemmeligejob = mand.joob();
        print(hanshemmeligejob);
        mand.skiftJob();
        print("Mandens nye hemmelige er job: ");
        string hanshemmeligejob2 = mand.joob();
        print(hanshemmeligejob2);
    }
 
}
