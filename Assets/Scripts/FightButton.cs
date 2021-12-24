using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FightButton : MonoBehaviour
{
    public GameObject _Player;
    public Animation anim;
    public bool Tapped = false;
    public bool paused = false;
    public GameObject PauseMenu;
    public GameObject DeadScreen;
    public GameObject WinScreen;
    public bool Sword_Status = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Restart()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        paused = false;
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
    public void Dead()
    {
        DeadScreen.SetActive(true);
        Time.timeScale = 0;
    }
    public void Win()
    {
        WinScreen.SetActive(true);
        Time.timeScale = 0;
    }
    public void BackToMainMenu()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        paused = false;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    public void TappedUpdate()
    {
        Tapped = false;
    }
    public void UnPause()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }
    public void Pause()
    {
        if (!paused)
        {
            Time.timeScale = 0;
            paused = true;
            PauseMenu.SetActive(true);
        }
        else
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
            paused = false;
           
        }
    }
    public void Fight()
    {
        Debug.Log("Fight");
        StartCoroutine(Attack());
        
       


    }
    IEnumerator Attack()
    {
        Sword_Status = true;
        
       
        yield return new WaitForSeconds(0.2f);

        Sword_Status = false;

    }
    public void TaskBtn()
    {
        if (!GameObject.FindWithTag("Enemy"))
        {
            GameObject.FindWithTag("WorldCenter").GetComponent<SpawnScript>().EnemyOff();
            StartCoroutine(TaskWait());
        }
        
        

    }
    IEnumerator TaskWait()
    {
        Tapped = true;

        yield return new WaitForSeconds(1);

        Tapped = false;

    }

    void FixedUpdate()
    {
        
        _Player = GameObject.FindWithTag("Player");
        //anim = _Player.GetComponent<Animation>();
    }
}
