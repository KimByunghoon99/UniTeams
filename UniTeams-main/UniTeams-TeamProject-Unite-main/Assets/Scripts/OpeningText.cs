using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpeningText : MonoBehaviour
{
    public TextMeshProUGUI TextTMP;
    public GameObject background;
    int index = 0;
    public string[] dialogue = { 
        "In a peaceful fantasy village, there lived a boy named A.",
        "But one day, an unknown infectious disease suddenly began to spread.",
        "The epidemic turned his neighbors and family into monsters, and the boy survived alone.",
        "A wandering adventurer saw the boy left alone and became his teacher and family.",
        "The boy who grew up under him decides to go on an adventure to find the cause of the epidemic."
    };

    // Start is called before the first frame update
    void Start()
    {
        TextTMP.text = "GAME START";
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TextTMP.text = dialogue[index];
            if(index == 2)
                background.SetActive(false);
            if(index<4)
                index++;
        }
        
    }
}
