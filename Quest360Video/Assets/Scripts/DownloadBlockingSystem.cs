using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownloadBlockingSystem : MonoBehaviour
{
    public List<Button> DownloadButtons = new List<Button>();

    public void ShowButtons() {
        foreach (var item in DownloadButtons)
        {
            if (item.gameObject.tag != "ThisBtn")
            {
                item.interactable = true;
            }
        }
    }
    public void HideButtons()
    {
        foreach (var item in DownloadButtons)
        {
            
            item.interactable = false;
        }
    }
}
