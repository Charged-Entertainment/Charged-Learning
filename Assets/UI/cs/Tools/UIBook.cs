using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public partial class UI
{
    public static Button BookButton { get; private set; } = null;
    private class UIBookIcon : UIBaseElement
    {
        private void Start()
        {
            BookButton = document.Q<Button>("book-btn");
            
            // WiP
        }
    }

}