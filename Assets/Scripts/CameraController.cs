using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool m_Following;
    private float m_LastPressTime = 0;
    private float m_TimeInterval = 0.5f;
    public GameObject m_Player;
    public GameObject m_UpToDownPoint;
    // Start is called before the first frame update
    void Start()
    {
        m_Following = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab) && Time.time - m_LastPressTime >= m_TimeInterval)
        {
            m_Following = !m_Following;
            m_LastPressTime = Time.time;
        }

        if (m_Following)
        {
            transform.position = m_Player.transform.position + new Vector3(0, 0.4f, 0);
            transform.rotation = m_Player.transform.rotation;
        }
        else
        {
            transform.position = m_UpToDownPoint.transform.position;
            transform.rotation = m_UpToDownPoint.transform.rotation;
        }
    }
}
