using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//Keep track of coin count, support spirits, items of player
public class PlayerManager : MonoBehaviour
{

    public int coinCount = 0;
    public TextMeshProUGUI coinText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCoinCount(){
        coinCount++;
        coinText.text = "x " + coinCount.ToString();
    }
}
