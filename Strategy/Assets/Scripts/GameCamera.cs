using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {


    Map m;
    Camera c; 

    // Use this for initialization
    void Start () {

        m = GameObject.Find("Map").GetComponent<Map>();
       c = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}



    Vector2 touchDeltaPosition;
    Vector2 newPosition;
    Vector2 lastPosition;
    bool moving = false;
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
            c.transform.Translate(touchDeltaPosition.x * -cameraSpeed, touchDeltaPosition.y * -cameraSpeed, 0);
            touchDeltaPosition = Input.mousePosition;
            lastPosition = newPosition;
        }
#endif
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                moving = true;
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                c.transform.Translate(touchDeltaPosition.x * -cameraSpeed, touchDeltaPosition.y * -cameraSpeed, 0);
            }
        }
#endif
    }

    public void PointClick()
    {
#if UNITY_STANDALONE_WIN


        if (Input.GetMouseButtonDown(0))
        {
            m.callMenu();
            m.ScreenRay(0);
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
                    m.callMenu();
                    m.ScreenRay(0);
                }
                    

            }
        }
#endif
    }

}
