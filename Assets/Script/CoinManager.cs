using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static int coinCount = 0;
    public TextMeshProUGUI coinText;

    void Update()
    {
        coinText.text = "x" + coinCount.ToString();
    }
}
