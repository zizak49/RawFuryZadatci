using UnityEngine;

public class Task2UI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject findPathButton;
    [SerializeField] private MazeManager mazeManager;

    public void EnableFindPathButton() 
    {
        findPathButton.SetActive(true);
    }

    public void OnClickAddStart() 
    {
        mazeManager.placement = MazeManager.TileTypePlacement.Start;
    }

    public void OnClickAddEnd()
    {
        mazeManager.placement = MazeManager.TileTypePlacement.End;
    }

    public void OnClickFindPath() 
    {
        mazeManager.CalculatePath();
    }
}
