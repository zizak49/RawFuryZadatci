using UnityEngine;

public class Task1UIController : MonoBehaviour
{
    private static Task1UIController _instance;
    public static Task1UIController Instance { get { return _instance; } }

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

    public void OnClickExit()
    {
        SceneController.Instance.LoadStartScene();
    }
}
