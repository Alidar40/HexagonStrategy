using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameCamera : MonoBehaviour {
    Map m;
    Camera c;
    public GameObject gc;
    public GameObject FieldOpportunitiesAttack, FieldOpportunitiesMoving;
    void Start () {

        m = GameObject.Find("Map").GetComponent<Map>();
        c = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        gc.gameObject.transform.localScale = new Vector3(c.orthographicSize * 0.2f, c.orthographicSize * 0.2f, 1);
        gc.GetComponent<Camera>().orthographicSize = c.orthographicSize;
    }


    

    Vector2 touchDeltaPosition;
    Vector2 newPosition;
    Vector2 lastPosition;
    bool moving = false, MovingUnit = false, AttackUnit = false;
    public float cameraSpeed = 0.1F;

    public void cameraMoving()
    {
#if UNITY_STANDALONE_WIN


        if (Input.GetMouseButtonDown(1))
        {
            newPosition = Input.mousePosition;
            lastPosition = newPosition;
        }

        if (Input.GetMouseButton(1))
        {
            newPosition = Input.mousePosition;
            touchDeltaPosition = newPosition - lastPosition;
            if ((c.transform.position.x + touchDeltaPosition.x * (-cameraSpeed) > 3f + (5f / 3f) * (c.orthographicSize - 2f)) && (c.transform.position.x + touchDeltaPosition.x * (-cameraSpeed) < 15f - (5f / 3f) * (c.orthographicSize - 2f)) && (c.transform.position.y + touchDeltaPosition.y * (-cameraSpeed) > 0f + (2f / 3f) * (c.orthographicSize - 2f)) && (c.transform.position.y + touchDeltaPosition.y * (-cameraSpeed) < 8f - (2f / 3f) * (c.orthographicSize - 2f)))
            {
                c.transform.Translate(touchDeltaPosition.x * -cameraSpeed, touchDeltaPosition.y * -cameraSpeed, 0);
            }
            else
            {
                Vector3 v = new Vector3();
                v = c.transform.position;
                if (c.transform.position.x <= 3f + (5f/3f)*(c.orthographicSize - 2f))
                {
                    v.x = 3f + (5f / 3f) * (c.orthographicSize - 2f) + 000001f; 
                    c.transform.position = v;
                }
                if (c.transform.position.x >= 15f - (5f / 3f) * (c.orthographicSize - 2f))  
                {
                    v.x = 15f - (5f / 3f) * (c.orthographicSize - 2f) - 000001f;            
                    c.transform.position = v;
                }
                if (c.transform.position.y <= 0f + (2f / 3f) * (c.orthographicSize - 2f))    
                {
                    v.y = 0f + (2f / 3f) * (c.orthographicSize - 2f) + 000001f;  
                    c.transform.position = v;
                }
                if (c.transform.position.y >= 8f - (2f / 3f) * (c.orthographicSize - 2f))   
                {
                    v.y = 8f - (2f / 3f) * (c.orthographicSize - 2f) - 000001f;
                    c.transform.position = v;
                }
            }
            
            touchDeltaPosition = Input.mousePosition;
            lastPosition = newPosition;
        }
#endif
#if UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                moving = true;
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                if ((c.transform.position.x + touchDeltaPosition.x * (-cameraSpeed) > 3f + (5f / 3f) * (c.orthographicSize - 2f)) && (c.transform.position.x + touchDeltaPosition.x * (-cameraSpeed) < 15f - (5f / 3f) * (c.orthographicSize - 2f)) && (c.transform.position.y + touchDeltaPosition.y * (-cameraSpeed) > 0f + (2f / 3f) * (c.orthographicSize - 2f)) && (c.transform.position.y + touchDeltaPosition.y * (-cameraSpeed) < 8f - (2f / 3f) * (c.orthographicSize - 2f)))
                {
                    c.transform.Translate(touchDeltaPosition.x * -cameraSpeed, touchDeltaPosition.y * -cameraSpeed, 0);
                }
                else
                {
                    Vector3 v = new Vector3();
                    v = c.transform.position;
                    if (c.transform.position.x <= 3f + (5f / 3f) * (c.orthographicSize - 2f))
                    {
                        v.x = 3f + (5f / 3f) * (c.orthographicSize - 2f) + 000001f; 
                        c.transform.position = v;
                    }
                    if (c.transform.position.x >= 15f - (5f / 3f) * (c.orthographicSize - 2f))  
                    {
                        v.x = 15f - (5f / 3f) * (c.orthographicSize - 2f) - 000001f;               
                        c.transform.position = v;
                    }
                    if (c.transform.position.y <= 0f + (2f / 3f) * (c.orthographicSize - 2f))    
                    {
                        v.y = 0f + (2f / 3f) * (c.orthographicSize - 2f) + 000001f;  
                        c.transform.position = v;
                    }
                    if (c.transform.position.y >= 8f - (2f / 3f) * (c.orthographicSize - 2f))     
                    {
                        v.y = 8f - (2f / 3f) * (c.orthographicSize - 2f) - 000001f; 
                        c.transform.position = v;
                    }
                }
            }
        }
#endif
    }

    public delegate void ClickOnScreen();

    public event ClickOnScreen COSevent;

    public void PointClick()
    {
#if UNITY_STANDALONE_WIN


        if (!MovingUnit && !AttackUnit && Input.GetMouseButtonDown(0))
        {
            COSevent();
        }
        if (MovingUnit && Input.GetMouseButtonDown(0))
        {
            MovingUnit = false;
            m.MovingUnit(0);
            m.ActiveUnit.DeleteFieldOpportunities();
        }
        if (AttackUnit && Input.GetMouseButtonDown(0))
        {
            AttackUnit = false;
            m.AttackUnit(0);
            m.ActiveUnit.DeleteFieldOpportunities();
        }

#endif
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (moving)
                {
                    moving = false;
                }
                else
                {
                    if (!MovingUnit && !AttackUnit)
                    {
                        COSevent();
                    }
                    if (MovingUnit)
                    {
                        MovingUnit = false;
                        m.MovingUnit(0);
                        m.ActiveUnit.DeleteFieldOpportunities();
                    }
                    if (AttackUnit)
                    {
                        AttackUnit = false;
                        m.AttackUnit(0);
                        m.ActiveUnit.DeleteFieldOpportunities();
                    }
                }
                    

            }
        }
#endif
    }

    public void StartMovingUnit()
    {
        MovingUnit = true;
        AttackUnit = false;
        ActionButtons.actionButtons.ActivateCancelActionButton();
        m.ActiveUnit.GenerateFieldOpportunitiesForMoving(FieldOpportunitiesMoving, m.ActiveUnit.CurrentNumberActionPoints);
    }
    public void StartAttackUnit()
    {
        MovingUnit = false;
        AttackUnit = true;
        ActionButtons.actionButtons.ActivateCancelActionButton();
        m.ActiveUnit.GenerateFieldOpportunities(FieldOpportunitiesAttack, m.ActiveUnit.AttackRadius);
    }
    public void CancelAction()
    {
        MovingUnit = false;
        AttackUnit = false;
        ActionButtons.actionButtons.HideCancelActionButton();
        m.ActiveUnit.DeleteFieldOpportunities();
    }

    float newDistance, lastDistance;
    public void CameraZoom()
    {

#if UNITY_STANDALONE_WIN
        if (c.orthographicSize - 10f * Input.GetAxis("Mouse ScrollWheel") * cameraSpeed >= 2f && c.orthographicSize - 10f * Input.GetAxis("Mouse ScrollWheel") * cameraSpeed <= 5f)
        {
            c.orthographicSize -= 10f * Input.GetAxis("Mouse ScrollWheel") * cameraSpeed;

        }
        else
        {
            if (c.orthographicSize < 2f) c.orthographicSize = 2f;
            if (c.orthographicSize > 5f) c.orthographicSize = 5f;
        }

        
#endif

#if UNITY_ANDROID
        if (Input.touchCount >= 2)
        {
            Vector2 touch0, touch1;
            touch0 = Input.GetTouch(0).position;
            touch1 = Input.GetTouch(1).position;
            newDistance = Vector2.Distance(touch0, touch1);
            if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Began)
            {
                lastDistance = newDistance;
            }
            if (c.orthographicSize - 0.1f * (newDistance - lastDistance) * cameraSpeed >= 2f && c.orthographicSize - 0.1f * (newDistance - lastDistance) * cameraSpeed <= 5f)
            {
                c.orthographicSize -= 0.1f * (newDistance - lastDistance) * cameraSpeed;
            }
            else
            {
                if (c.orthographicSize < 2f) c.orthographicSize = 2f;
                if (c.orthographicSize > 5f) c.orthographicSize = 5f;
            }
            lastDistance = newDistance;
        }
#endif

    }
}
