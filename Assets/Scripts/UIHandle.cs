using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; //To use UI Elements

public class UIHandle : MonoBehaviour
{
    public static UIHandle instance { get; private set; }

    private VisualElement m_healthBar;
    private Label m_amountOfWater;
    private Label m_amountOfCake;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_healthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        m_amountOfWater = uiDocument.rootVisualElement.Q<Label>("AmountOfWater");
        m_amountOfCake = uiDocument.rootVisualElement.Q<Label>("AmountOfCake");

        SetHealthValue(1.0f);
        SetAmountOfWater(0);
        SetAmountOfCake(0);
    }

    public void SetHealthValue(float healthValue)
    {
        m_healthBar.style.width = Length.Percent(healthValue * 100.0f);
    }

    public void SetAmountOfWater(int amount)
    {
        m_amountOfWater.text = " x " + amount;
    }

    public void SetAmountOfCake(int amount)
    {
        m_amountOfCake.text = " x " + amount;
    }
}
