using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mand : MonoBehaviour
{
    public string navn;
    string undercoverNavn;
    string hemmeligtJob;
   



    void Awake()
    {
        
        hemmeligtJob = "pusher";
        navn = "jessor oinkman";
        undercoverNavn = "kap'n crunch";
        
    }

    public void skiftJob()
    {
        hemmeligtJob = "sexworker";
    }

    void skiftUndercoverNavn()
    {
        undercoverNavn = "";
    }


    //I må ændre koden herfra og ned
    public string findundercovernavn()
    {
        return undercoverNavn; // jeg laver en string som returnerer undercovernavn for at kunne definere det i andre classes
    }
    public string joob()
    {
        return hemmeligtJob;
    }
}
