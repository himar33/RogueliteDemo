using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public enum UIStates
    {
        MAIN_MENU,
        INGAME_MENU,
        POWERUPS
    }

    [SerializeField] private VisualTreeAsset powerUpUI;
    private UIDocument doc;

    private void Start()
    {
        doc = GetComponent<UIDocument>();
        QuiteUI();
    }

    public void SetUI(UIStates uiType)
    {
        switch (uiType)
        {
            case UIStates.MAIN_MENU:
                break;
            case UIStates.INGAME_MENU:
                break;
            case UIStates.POWERUPS:
                doc.visualTreeAsset = powerUpUI;
                break;
            default:
                break;
        }
    }

    public void QuiteUI()
    {
        doc.rootVisualElement.Clear();
    }
}
