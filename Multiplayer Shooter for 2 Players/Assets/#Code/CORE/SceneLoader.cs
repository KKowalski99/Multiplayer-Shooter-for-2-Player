using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   public static SceneLoader Instance;

    const string _uiSceneName = "UIScene";
    const string _gameSceneName = "GameScene";

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
      if (SceneManager.GetSceneByName(_uiSceneName).isLoaded) return;

        SceneManager.LoadSceneAsync(_uiSceneName, mode: LoadSceneMode.Additive);

    }
        
    public void Restart()
    {
        SceneManager.LoadScene(_gameSceneName);
    }
    

 

}
