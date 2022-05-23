using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Task2UIController : MonoBehaviour
{
    [SerializeField] private GameObject findPathButton;
    [SerializeField] private GameObject mazeControlls;
    [SerializeField] private MazeManager mazeManager;

    [SerializeField] private Image diagonalIndicator;

    void Start() 
    {
        diagonalIndicator.color = mazeManager.UseDiagonal ? Color.green : Color.red;
    }

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
        mazeManager.CalculateMazePath();
    }

    public void OnClickBlankMaze() 
    {
        Debug.Log("Create maze...");
        mazeControlls.SetActive(false);
        mazeManager.InstantiateMaze(false);
    }

    public void OnClickUseDiagonal()
    {
        if (mazeManager.UseDiagonal)
        {
            mazeManager.UseDiagonal = false;
            diagonalIndicator.color = Color.red;
        }
        else
        {
            mazeManager.UseDiagonal = true;
            diagonalIndicator.color = Color.green;
        }
    }

    public void OnClickGenerateMaze() 
    {
        mazeManager.GenerateMaze();
    }
}
