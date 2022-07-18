using CharacterCreation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int[] EquippedRef;
    public MeshCombiner MeshesHolder;

    public static GameManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadBeginningScene()
    {
        MeshCombiner.Instance.SetEquippedReferences();
        CharacterManager.Instance.SetCharacterBody(MeshCombiner.Instance.equippedRef);
        this.EquippedRef = MeshCombiner.Instance.equipped;
        SceneManager.LoadScene("StartLevel");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (SceneManager.GetActiveScene().name == "StartLevel")
        {
            MeshCombiner meshesHolder = Instantiate(MeshesHolder);

            meshesHolder.gameObject.SetActive(false);

            MeshCombiner.Instance.equipped = this.EquippedRef;
            MeshCombiner.Instance.SetEquippedReferences();
            CharacterManager.Instance.SetCharacterBody(MeshCombiner.Instance.equippedRef);
            CharacterManager.Instance.AssemblateCharacter();
        }
    }
}
