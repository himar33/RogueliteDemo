using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    TESTSCENE,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isPaused = false;
    //public AudioManager AudioManager { get; private set; }
    //public UIManager UIManager { get; private set; }
    //public TransitionManager transManager { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        //AudioManager = GetComponentInChildren<AudioManager>();
        //UIManager = GetComponentInChildren<UIManager>();
        //transManager = GetComponentInChildren<TransitionManager>();
    }
    public static void ChangeSceneTo(Scene sceneIndex)
    {
        //TODO: Transition system
        SceneManager.LoadScene((int)sceneIndex);
        Debug.Log("Loading scene with name: " + sceneIndex.ToString());
    }
    public void GameOver()
    {
        //TODO: Transition system
        ChangeSceneTo(Scene.TESTSCENE);
    }
    public void PauseGame()
    {
        foreach (var item in FindObjectsOfType<Enemy>())
        {
            item.Stop();
        }
    }
}
