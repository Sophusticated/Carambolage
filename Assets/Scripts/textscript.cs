using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textscript : MonoBehaviour
{
    public TextMeshProUGUI textObject;

    void Start()
    {
        textObject = GameObject.FindGameObjectWithTag("Score").GetComponent<TMPro.TextMeshProUGUI>();
    }

 
    void Update()
    {
        textObject.SetText(General.Score + " / " + General.arrivedTrains);
        if (General.gameOver)
        {
            textObject.SetText("");
        }
    }

}
