using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private Transform _currentSpawnPoint;
    private GameObject _player;
    private AudioSource _deathAudio;
    private PlayerHealthController _playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Elf");
        _deathAudio = GameObject.Find("GameOverSoundtrack").GetComponent<AudioSource>();
        _playerHealth = _player.GetComponent<PlayerHealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerHealth.getCurrentHealth() <= 0)
        {
            deathState();
        }
    }

    public IEnumerator deathState()
    {
        yield return new WaitForSeconds(2);
        _player.transform.position = _currentSpawnPoint.transform.position;
        _deathAudio.Play();
    }
    public void updateCurrentSpawnPoint(Vector3 position)
    {
        _currentSpawnPoint.position = position;
    }
}
