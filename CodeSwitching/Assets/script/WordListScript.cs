using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordListScript : MonoBehaviour
{
    public Text Lan1, Lan2;
    public GameObject WordContent;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void ContentUpdata(string lan1, string lan2){
        Lan1.text = lan1;
        Lan1.text = lan2;
    }
}
