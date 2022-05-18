using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2UI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject findPathButton;


    public void EnableFindPathButton() 
    {
        findPathButton.SetActive(true);
    }

    public void OnClickAddStart() 
    {
        MazeManager.Instance.placement = MazeManager.TileTypePlacement.Start;
    }

    public void OnClickAddEnd()
    {
        MazeManager.Instance.placement = MazeManager.TileTypePlacement.End;
    }

    public void OnClickFindPath() 
    {
        MazeManager.Instance.CalculatePath();
    }
}
