using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseManager : MonoBehaviour
{
    public int m_Health;
    // Start is called before the first frame update
    void Awake()
    {
        m_Health = 300;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Die()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(2);

    }

    public void HurtHouse(int power)
    {
        m_Health -= power;
        Debug.Log(m_Health);
        if (m_Health <= 0)
        {
            Die();
        }
    }
}
