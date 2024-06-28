using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 2f;

    

    // Update is called once per frame
    void Update()
    {
               
    }

    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 3)
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        else
            LoadMainMenu();
        
    }
    public void ReloadLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        
    }
    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel(0));
        
    }
    public void LoadHallLevel()
    {
        StartCoroutine(LoadLevel(2));
        
    }
    public void LoadCityLevel()
    {
        StartCoroutine(LoadLevel(3));
        
    }
    public void LoadParkLevel()
    {
        StartCoroutine(LoadLevel(6));
        
    }
    public void LoadGorSadLevel()
    {
        StartCoroutine(LoadLevel(4));
        
    }
    public void LoadBonusLevel()
    {
        StartCoroutine(LoadLevel(7));
        
    }
    public void LoadDucksLevel()
    {
        StartCoroutine(LoadLevel(5));
        
    }
    public void LoadWinLevel()
    {
        StartCoroutine(LoadLevel(8));
        
    }
    public void LoadLooseLevel()
    {
        StartCoroutine(LoadLevel(9));
        
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
