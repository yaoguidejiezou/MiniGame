using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    public int m_Money;
    public DialogSystem m_DialogSystem;
    public Text m_MoneyText;

    private void Start()
    {
        Show();
    }

    public bool Buy(int cost)
    {
        if (m_Money < cost)
        {
            m_DialogSystem.ShowDialog("资金不足");
            return false;
        }
        m_Money -= cost;
        Show();
        return true;
    }

    void Show()
    {
        m_MoneyText.text = "$" + m_Money;
    }
    
}
