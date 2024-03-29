using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Display : MonoBehaviour
{
    [SerializeField]private Text textElement;
    // Start is called before the first frame update
    void Awake()
    {
        textElement = GetComponentInChildren<Text>();
    }


    public void Write(string message){
        textElement.text = message.ToString();
    }

    public void TurnOff(){
        textElement.text = "";
    }
}
