using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CameraController : MonoBehaviour
{
    public bool m_Following;
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
            m_Player.GetComponent<FirstPersonController>().enabled = m_Following;
            m_Player.GetComponent<WeaponController>().enabled = m_Following;
        }

        if (m_Following)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            transform.position = m_Player.transform.position + new Vector3(0, 0.4f, 0);
            transform.rotation = m_Player.transform.rotation;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            transform.position = m_UpToDownPoint.transform.position;
            transform.rotation = m_UpToDownPoint.transform.rotation;
        }
    }
}
