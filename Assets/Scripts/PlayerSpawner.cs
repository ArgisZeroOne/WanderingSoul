using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject _Player;
    
    void Start()
    {
        
    }


    void Update()
    {
        
    }
    public void SpawnEnemy(GameObject _Enemy)
    {
        Instantiate(_Enemy, transform.position,
                         transform.rotation);
    }
    public void Spawn()
    {
        
        Destroy(GameObject.FindWithTag("Player"));
        Instantiate(_Player, transform.position,
                         transform.rotation);
        
    }
}
