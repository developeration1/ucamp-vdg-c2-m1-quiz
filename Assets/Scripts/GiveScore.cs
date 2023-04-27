using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiveScore : MonoBehaviour
{
    private TMP_Text scoreText;
    
    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = PlayerPrefs.GetInt("Score").ToString();
    }
}
