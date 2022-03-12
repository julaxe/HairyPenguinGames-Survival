using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private Transform _currentSpawnPoint;
    private GameObject _player;
    private AudioSource _deathAudio;
    private PlayerHealthController _playerHealth;
    private int numberOfBoatParts;
    [SerializeField] private int numberOfPartInLevel;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Elf");
        _playerHealth = _player.GetComponent<PlayerHealthController>();
        _deathAudio = GameObject.Find("GameOverSoundtrack").GetComponent<AudioSource>();
        //Fix Loading/Saving
        numberOfBoatParts = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (_playerHealth.getCurrentHealth() <= 0)
        {
            _playerHealth.setHealth(100);
            StartCoroutine(deathState(0.0f));
        }
    }

    public IEnumerator deathState(float sec)
    {
        yield return new WaitForSeconds(sec);
        _player.transform.position = _currentSpawnPoint.transform.position;
        _deathAudio.Play();
    }
    
    public void updateCurrentSpawnPoint(Vector3 position)
    {
        _currentSpawnPoint.position = position;
    }

    public void addedBoatPart()
    {
        numberOfBoatParts++;
        if (numberOfBoatParts == numberOfPartInLevel)
        {
            SceneManager.LoadScene("Credits");
        }
    }
    public Transform getspawnPoint()
    {
        return _currentSpawnPoint;
    }
    public int getNumberofBoatParts()
    {
        return numberOfBoatParts;
    }
 

}
