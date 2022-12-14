using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    public bool isPressed;

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

}
