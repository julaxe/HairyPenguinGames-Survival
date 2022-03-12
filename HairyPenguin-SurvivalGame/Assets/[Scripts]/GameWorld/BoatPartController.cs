using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatPartController : MonoBehaviour
{
    GameStateController gameState;
    public int id;
   public SaveAndLoad saver;
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.Find("GameController").GetComponent<GameStateController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int getId()
    {
        return id;
    }
    private void OnTriggerEnter(Collider other)
    {
        saver.SaveBoatID(id);
        gameState.addedBoatPart();
        Destroy(this.gameObject);
    }
}
