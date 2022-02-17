using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatPartController : MonoBehaviour
{
    GameStateController gameState;
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.Find("GameController").GetComponent<GameStateController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        gameState.addedBoatPart();
        Destroy(this.gameObject);
    }
}
