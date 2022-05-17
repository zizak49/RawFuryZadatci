using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController _instance;
    public static SceneController Instance { get { return _instance; } }

    [SerializeField] private GameObject startCanvas;

    private void Awake()
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

    public void LoadStartScene()
    {
        SceneManager.LoadSceneAsync("Loader", LoadSceneMode.Single);
        startCanvas.SetActive(true);
    }

    public void LoadTask1() 
    {
        SceneManager.LoadSceneAsync("Task1-AI", LoadSceneMode.Additive);
        startCanvas.SetActive(false);
    }

    public void LoadTask2() 
    {
        SceneManager.LoadSceneAsync("Task2-Maze", LoadSceneMode.Additive);     
        startCanvas.SetActive(false);
    }

    public void OnClickQuit() 
    {
        Application.Quit();
    }
}
