using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Book : Singleton<Book>
{
    public static Action<Page> popupShown;
    public static Action<Page> popupClosed;

    private static VisualElement document, visualElement;

    private static TwoPageLayout emptyPage;

    public void ShowEmpty() { emptyPage.enabled = true; }
    private void Start()
    {
        // test
        emptyPage = Instance.gameObject.AddComponent<TwoPageLayout>();
        emptyPage.closed += () => { popupClosed?.Invoke(emptyPage); emptyPage.enabled = false; };   
    }

    // public static void Show(Page page) {}
}
