using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Controller : MonoBehaviour
{
    public UnityEvent<Vector3> PrimaryMouseButtonClickedEvent;
    //mouse scroll event with params
    public UnityEvent<float> MouseScrollUpdateEvent;
    private float scrollDeltaY;
    public float scrollSensitivity = 1.0f;

    void Update()
    {
        //Send signals that the mouse's primary button has been clicked
        if(Input.GetMouseButtonDown(0))
        {
            PrimaryMouseButtonClickedEvent.Invoke(Input.mousePosition);
        }

        //Send signal that the mouse has been scrolled
        if(scrollDeltaY != Input.mouseScrollDelta.y )
        {
            scrollDeltaY = Input.mouseScrollDelta.y * scrollSensitivity;
            MouseScrollUpdateEvent.Invoke(scrollDeltaY);
        }
    }
}
