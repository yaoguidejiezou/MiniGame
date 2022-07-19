using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    public int m_Money;
    public DialogSystem m_DialogSystem;

    bool Buy(int cost)
    {
        if (m_Money <= cost)
        {
            m_DialogSystem.ShowDialog("资金不足");
            return false;
        }
        m_Money -= cost;
        return true;
    }
}
