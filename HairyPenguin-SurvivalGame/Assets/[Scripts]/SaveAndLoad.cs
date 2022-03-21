using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    public PlayerHealthController playerHealthController;
    public  GameStateController gameStateController;
    public SaveOptions saveOptions;

    private Bag _playerBag;
    // Start is called before the first frame update

    private void Awake()
    {
        _playerBag = GetComponent<Bag>();
    }

    void Start()
    {
        if (saveOptions.getLoading())
        {
            OnLoad();
        }
    }
    public void OnSave()
    {
        SaveCheckPoint();
        SaveHealth();
        SaveInventory();
        SaveNumberOfBoatParts();
        SaveRotation();
    }
    public void OnLoad()
    {
        LoadBoatfromID();
        LoadCheckPoint();
        LoadHealth();
        LoadInventory();
        LoadRotation();
        
    }

    void SaveCheckPoint()
    {
        var json = JsonUtility.ToJson(transform.eulerAngles);
        PlayerPrefs.SetString("PlayerRotation", json );
    }
    void SaveRotation()
    {
        var json = JsonUtility.ToJson(transform.position);
        PlayerPrefs.SetString("PlayerCheckPoint", json);
    }
    void SaveHealth()
    {
        
        PlayerPrefs.SetFloat("PlayerHealth", playerHealthController.getCurrentHealth());
    }
    void SaveInventory()
    {
        var json = JsonUtility.ToJson(_playerBag.GetSavedBag());
        PlayerPrefs.SetString("PlayerBag", json);
    }
    void SaveNumberOfBoatParts()
    {
      
        PlayerPrefs.SetInt("PlayerBoatParts", gameStateController.getNumberofBoatParts());
      
    }
    public void SaveBoatID(int id)
    {

        PlayerPrefs.SetInt("PlayerBoatId" + id.ToString(), id);
        


    }
    void LoadBoatfromID()
    {
                BoatPartController[] boats = FindObjectsOfType(typeof(BoatPartController)) as BoatPartController[];
        for(int i =0; i <= 8; i++)
        {
            if (PlayerPrefs.HasKey("PlayerBoatId" + i.ToString()))
            {
                var id = PlayerPrefs.GetInt("PlayerBoatId" + i.ToString());
               
                foreach(BoatPartController boat  in boats)
                {
                    if (boat.getId() == id)
                    {
                       
                        gameStateController.addedBoatPart();
                       
                        Destroy(boat.gameObject);
                        
                    }

                }

            }
        }




    }
  
   void LoadCheckPoint()
    {
        if (PlayerPrefs.HasKey("PlayerCheckPoint"))
        {
            var json = PlayerPrefs.GetString("PlayerCheckPoint");
            transform.position =JsonUtility.FromJson<Vector3>(json);
        }
    }
    void LoadRotation()
    {
        if (PlayerPrefs.HasKey("PlayerRotation"))
        {
            var json = PlayerPrefs.GetString("PlayerRotation");
            transform.eulerAngles = JsonUtility.FromJson<Vector3>(json);
        }
    }

    void LoadHealth()
    {
        if (PlayerPrefs.HasKey("PlayerHealth"))
        {
            var json = PlayerPrefs.GetFloat("PlayerHealth");
            playerHealthController.setLastHealth((int)json);
            
        }
    }


    void LoadInventory()
    {
        if (PlayerPrefs.HasKey("PlayerBag"))
        {
            var json = PlayerPrefs.GetString("PlayerBag");
            SavedBag savedBag = JsonUtility.FromJson<SavedBag>(json);
            _playerBag.LoadSavedBag(savedBag);
            
        }
    }
}
