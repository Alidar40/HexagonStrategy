using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameCamera : MonoBehaviour {
    Map m;
    public Camera c;
    public GameObject gc;
    public GameObject FieldOpportunitiesAttack, FieldOpportunitiesMoving;
    void Start () {

        m = GameObject.Find("Map").GetComponent<Map>();
        c = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        //gc.gameObject.transform.localScale = new Vector3(c.orthographicSize * 0.2f, c.orthographicSize * 0.2f, 1);
        gc.GetComponent<Camera>().orthographicSize = c.orthographicSize;
    }




    Vector2 touchDeltaPosition;
    Vector2 newPosition;
    Vector2 lastPosition;
    bool moving = false, MovingUnit = false, AttackUnit = false;
    public float cameraSpeed = 1F;

    public void cameraMoving()
    {
#if UNITY_STANDALONE_WIN

        Vector3 LLCell, URCell;
        URCell = m.GetCell(m.NumberOfCellsOnAxisX - 1, m.NumberOfCellsOnAxisY - 1).gameObject.transform.position;
        LLCell = m.GetCell(0, 0).gameObject.transform.position;


        if (Input.GetMouseButton(1))
        {
            newPosition = Input.mousePosition;
            touchDeltaPosition = (lastPosition - newPosition) * cameraSpeed;

            if ((c.transform.position.x + touchDeltaPosition.x <= URCell.x || touchDeltaPosition.x < 0) &&
                (c.transform.position.x + touchDeltaPosition.x >= LLCell.x || touchDeltaPosition.x > 0))
                c.transform.position += new Vector3(touchDeltaPosition.x, 0);

            if ((c.transform.position.y + touchDeltaPosition.y <= URCell.y || touchDeltaPosition.y < 0) &&
                (c.transform.position.y + touchDeltaPosition.y >= LLCell.y || touchDeltaPosition.y > 0))
                c.transform.position += new Vector3(0, touchDeltaPosition.y);

            lastPosition = newPosition;
        }
        else
            lastPosition = Input.mousePosition;
#endif
#if UNITY_ANDROID
        Vector3 LLCell, URCell;
        URCell = m.GetCell(m.NumberOfCellsOnAxisX - 1, m.NumberOfCellsOnAxisY - 1).gameObject.transform.position;
        LLCell = m.GetCell(0, 0).gameObject.transform.position;

        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                newPosition = (Input.GetTouch(0).position);
                touchDeltaPosition = (lastPosition - newPosition) * cameraSpeed * 0.2f;

                if ((c.transform.position.x + touchDeltaPosition.x <= URCell.x || touchDeltaPosition.x < 0) &&
                    (c.transform.position.x + touchDeltaPosition.x >= LLCell.x || touchDeltaPosition.x > 0))
                    c.transform.position += new Vector3(touchDeltaPosition.x, 0);

                if ((c.transform.position.y + touchDeltaPosition.y <= URCell.y || touchDeltaPosition.y < 0) &&
                    (c.transform.position.y + touchDeltaPosition.y >= LLCell.y || touchDeltaPosition.y > 0))
                    c.transform.position += new Vector3(0, touchDeltaPosition.y);

                lastPosition = newPosition;

            }
            else
                lastPosition = Input.GetTouch(0).position;
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

            if (Input.GetTouch(0).phase == TouchPhase.Began)
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
