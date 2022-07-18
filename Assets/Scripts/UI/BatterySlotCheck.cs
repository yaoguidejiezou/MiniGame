using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BatterySlotCheck : MonoBehaviour
{
    public Camera m_Camera;
    private int m_BatteryLayerMask;
    public GameObject m_ConstructPanel;
    private bool selceting = false;
    private Transform[] m_Buttons;
    public BatteryManager m_BatteryManager;
    // Start is called before the first frame update
    void Start()
    {
            m_BatteryLayerMask = (1 << LayerMask.NameToLayer("Battery"));
    }
    

    // Update is called once per frame
    void Update()
    {
        if (m_Camera.GetComponent<CameraController>().m_Following == false)
        {
            
            Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            
             //没有选择一个塔基或者重新点击鼠标时
             if (!selceting || Input.GetMouseButtonDown(0))
             {
                 if (EventSystem.current.IsPointerOverGameObject()) //鼠标放到UI上
                 {
                     //do Nothing
                 }
                 else if (Physics.Raycast(ray, out hitInfo, 1000, m_BatteryLayerMask, new QueryTriggerInteraction())) //射线检测到塔基对象
                 {
                     //Debug.DrawLine(ray.origin, hitInfo.point, Color.blue);
                     //Debug.Log(hitInfo.collider.name);
                     m_BatteryManager.TargetTowerBase = hitInfo.collider.gameObject;
                     if (Input.GetMouseButton(0))
                     {
                         ShowPanel();
                         
                     }
                 }
                 else
                 {
                     //towerManager.TargetTowerBase = null;
            
                     UnshowPanel();
                 }
             }
            
        }

    }
    
    public void ShowPanel ()
    {
        m_ConstructPanel.transform.position = Input.mousePosition;
        m_ConstructPanel.SetActive(true);
        selceting = true;
    }
    public void UnshowPanel()
    {
        m_ConstructPanel.SetActive(false);
        selceting = false;
    }
}
