using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    void Update()
    {
        coinText.text = "You gathered " + CoinManager.coinCount.ToString() + 
                        " coins of your courage\n\n" +
                        "But the glory slips away";
    }
}