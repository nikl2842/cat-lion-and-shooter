using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    Transform player;
    public GameObject
    // Start is called before the first frame update
    void Start()
    {
        player = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))

        {
            player.rotation = player.rotation;// +




        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            player.GetComponent<Rigidbody>().velocity = Vector3.forward;




        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            player.GetComponent<Rigidbody>().velocity = Vector3.back;





        }




    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "destructableobjekt")
        {
            GameObject hit = collision;
            StartCoroutine(destroyobjekt(hit));
           

        }



    }

    public IEnumerator destroyobjekt(GameObject objekthit)
    {





        yield return new WaitForSecondsRealtime(0);

        Destroy(objekthit);

    }



}
