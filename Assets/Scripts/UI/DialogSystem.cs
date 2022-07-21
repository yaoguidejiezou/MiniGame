using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public Image m_Background;
    public Text m_Text;

    public float m_ComingTime;
    public float m_ShowingTime;
    public float m_FadingTime;
    public float m_Alpha;
    private bool m_Occupy;
    
    private Coroutine m_ShowDialogCoroutine;
    
    void Start()
    {
        m_Occupy = false;
        m_Background.gameObject.SetActive(false);
        m_Text.gameObject.SetActive(false);
    }
    
    public void ShowDialog(string message)
    {
        if(m_Occupy == false) m_ShowDialogCoroutine = StartCoroutine(ShowDialogCor(message));
    }
    
    IEnumerator ShowDialogCor(string message)
    {
        m_Occupy = true;
        m_Background.gameObject.SetActive(true);
        m_Text.gameObject.SetActive(true);
        for (float timer = 0; timer < m_ComingTime; timer += Time.deltaTime)
        {
            m_Text.text = message;
            float value = timer / m_ComingTime >= 1.0f ? 1.0f : timer / m_ComingTime;
            m_Text.color = new Color(0, 0, 0, value);
            m_Background.color = new Color(0, 0, 0, value * m_Alpha);
            yield return 0;
        }
        for (float timer = 0; timer < m_ShowingTime; timer += Time.deltaTime)
        {
            yield return 0;
        }
        for (float timer = 0; timer < m_FadingTime; timer += Time.deltaTime)
        {
            float value = timer / m_FadingTime >= 1.0f ? 1.0f : timer / m_FadingTime;
            m_Text.color = new Color(0, 0, 0, 1 - value);
            m_Background.color = new Color(0, 0, 0, (1 - value) * m_Alpha);
            yield return 0;
        }
        m_Occupy = false;
        m_Background.gameObject.SetActive(false);
        m_Text.gameObject.SetActive(false);
        StopCoroutine(m_ShowDialogCoroutine);
    }
}
