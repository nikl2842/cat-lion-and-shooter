using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform camera1;
    public Transform player;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        player = 
        camera1.position = player.position + offset;
    }


   
}
