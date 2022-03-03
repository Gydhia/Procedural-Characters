using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    public void LoadBeginningScene()
    {
        CharacterManager.Instance.AssemblateCharacter();
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "StartLevel") LoadBeginningScene(); 
    }
}
