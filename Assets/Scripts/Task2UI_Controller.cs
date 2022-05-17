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
        GridManager.Instance.placement = GridManager.TileTypePlacement.Start;
    }

    public void OnClickAddEnd()
    {
        GridManager.Instance.placement = GridManager.TileTypePlacement.End;
    }

    public void OnClickFindPath() 
    {
        GridManager.Instance.CalculatePath();
    }
}
