using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Controller : MonoBehaviour
{
    public UnityEvent PrimaryMouseButtonClickedEvent;
    //mouse scroll event with params
    public UnityEvent<float> MouseScrollUpdateEvent;
    private float scrollDeltaY;
    public float scrollSensitivity = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        PrimaryMouseButtonClickedEvent = new UnityEvent();
        MouseScrollUpdateEvent = new UnityEvent<float>();
    }

    void Update()
    {
        //Send signals that the mouse's primary button has been clicked
        if(Input.GetMouseButtonDown(0))
        {
            PrimaryMouseButtonClickedEvent.Invoke();
        }

        //Send signal that the mouse has been scrolled
        if(scrollDeltaY != Input.mouseScrollDelta.y )
        {
            scrollDeltaY = Input.mouseScrollDelta.y * scrollSensitivity;
            MouseScrollUpdateEvent.Invoke(scrollDeltaY);
        }
    }
}
