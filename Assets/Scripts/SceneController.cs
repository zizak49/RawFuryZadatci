using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneController : MonoBehaviour
{
    [Serializable]
    public class SceneInfo
    {
        public AssetReference assetReference;
        public string sceneName;
    }

    [SerializeField] private SceneInfo TASK1AI, TASK2MAZE;

    private Dictionary<string, AsyncOperationHandle<SceneInstance>> sceneHandlesByName = new Dictionary<string, AsyncOperationHandle<SceneInstance>>();

    private static SceneController _instance;
    public static SceneController Instance { get { return _instance; } }

    [SerializeField] private GameObject startCanvas;

    private void Awake()
    {
        Singleton();       
    }

    public void LoadTask1() 
    {
        startCanvas.SetActive(false);
        GameManager.Instance.currentGameState = GameManager.GameState.Task1;
        LoadScene(TASK1AI);
    }

    public void LoadTask2()
    {
        startCanvas.SetActive(false);
        GameManager.Instance.currentGameState = GameManager.GameState.Task2;
        LoadScene(TASK2MAZE);
    }

    public void LoadStartScene() 
    {
        if (GameManager.Instance.currentGameState == GameManager.GameState.Task1) 
        {
            UnloadScene(TASK1AI);
        }
        else
        {
            UnloadScene(TASK2MAZE);
        }
        startCanvas.SetActive(true);
        GameManager.Instance.currentGameState = GameManager.GameState.Start;
    }

    private void LoadScene(SceneInfo scene)
    {
        Addressables.LoadSceneAsync(scene.assetReference, LoadSceneMode.Additive).Completed += SceneLoadComplited;
    }

    private void SceneLoadComplited(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Successfully loaded scene: " + obj.Result.Scene.name);
            if (!sceneHandlesByName.ContainsKey(obj.Result.Scene.name))
            {
                sceneHandlesByName.Add(obj.Result.Scene.name, obj);
            }
        }
    }

    private void UnloadScene(SceneInfo scene)
    {
        if (sceneHandlesByName.ContainsKey(scene.sceneName))
        {
            Addressables.UnloadSceneAsync(sceneHandlesByName[scene.sceneName]).Completed += SceneUnloadComplited;
            sceneHandlesByName.Remove(scene.sceneName);
        }
        else
        { 
            return; 
        }
    }

    private void SceneUnloadComplited(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Successfully unloaded scene: " + obj.Result.Scene.name);
        }
    }

    public void OnClickQuit() 
    {
        Application.Quit();
    }

    private void Singleton() 
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
