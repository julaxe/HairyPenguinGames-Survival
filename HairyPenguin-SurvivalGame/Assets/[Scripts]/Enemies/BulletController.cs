using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    public int BulletDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Do something when bullet hits player
            Destroy(gameObject);
            other.gameObject.GetComponent<PlayerHealthController>().setHealth(-BulletDamage);
        }

        if (other.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
