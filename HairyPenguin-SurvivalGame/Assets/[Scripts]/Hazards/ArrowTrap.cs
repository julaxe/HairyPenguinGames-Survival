using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public GameObject[] arrowSpawners;
    public GameObject arrow;

    private float timer;

    private float smallinterval = 0.3f;
    private float Biginterval = 1.0f;
    private int counter = 0;

    public Transform Player;

    public AudioSource sound;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(Player.position, transform.position) < 20)
        {

            timer += Time.deltaTime;

            if(counter < 3)
            {
                if(timer > smallinterval)
                {
                    foreach(var s in arrowSpawners)
                    {
                        GameObject go = Instantiate(arrow, s.transform.position, Quaternion.identity);
                        go.GetComponent<Rigidbody>().AddForce(Vector3.forward * 25, ForceMode.Impulse);
                        sound.Play();
                    }
                    counter++;
                    timer = 0;
                }
            
           
            }
            else
            {
                if (timer > Biginterval)
                {
                    //foreach (var s in arrowSpawners)
                    //{
                    //    GameObject go = Instantiate(arrow, s.transform.position, Quaternion.identity);
                    //    go.GetComponent<Rigidbody>().AddForce(Vector3.forward * 50, ForceMode.Impulse);
                    //}

                    counter = 0;
                    timer = 0;
                }
            }
        }
    }
}
