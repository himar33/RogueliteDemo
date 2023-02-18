using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    TESTSCENE,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private bool _isPaused;
    public bool IsPaused
    {
        get { return _isPaused; }
        set
        {
            _isPaused = value;
            Debug.Log("Game state is changed!");
            switch (value)
            {
                case true:
                    PauseGame();
                    break;
                case false:
                    ResumeGame();
                    break;
            }
        }
    }
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Instance.IsPaused = !Instance.IsPaused;
        }
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
    public void ResumeGame()
    {
        foreach (var item in FindObjectsOfType<Enemy>())
        {
            item.Resume();
        }

        FindObjectOfType<PlayerController>().Resume();
    }
    public void PauseGame()
    {
        foreach (var item in FindObjectsOfType<Enemy>())
        {
            item.Stop();
        }

        FindObjectOfType<PlayerController>().Stop();
    }
}
