using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Play()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
    public void Exit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
