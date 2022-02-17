using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pos1, pos2;
    public Vector3 nextPos;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = pos1.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, pos1.position) < 0.2)
        {
            nextPos = pos2.position;
        }
        else if (Vector3.Distance(transform.position, pos2.position) < 0.2)
        {
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.transform.parent = gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("exit!");
            other.gameObject.transform.parent = null;
        }
    }
}
