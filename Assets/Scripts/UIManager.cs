using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button exitButton;
    public GameObject inventoryUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideInventoryUI()
    {
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(false);
        }
    }

    public void ShowInventoryUI()
    {
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(true);
        }
    }
}
