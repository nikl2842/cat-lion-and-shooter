using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class crowswitchcase : MonoBehaviour
{
    public GameObject fisk;
    public GameObject fisk1;
    public NavMeshAgent krage;

    public enum state
    {
        scheme,
        eat,
        attack
    }

    public state kragefugl;
    // Start is called before the first frame update
    void Start()
    {
        krage.SetDestination(fisk.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        switch (kragefugl)
        {
            case state.attack:
                krage.SetDestination(fisk.transform.position);
                break;
            case state.eat:
                krage.SetDestination(fisk1.transform.position);
                break;
            case state.scheme:
               
                break;
        }
    }
}
