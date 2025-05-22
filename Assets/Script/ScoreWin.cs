using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreWin : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    void Update()
    {
        coinText.text = "You won collecting "+ CoinManager.coinCount.ToString() +" coins of courage";
    }
    
}
