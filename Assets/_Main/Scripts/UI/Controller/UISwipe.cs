using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace DE
{

    public class UISwipe : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
    {
        [System.Serializable]
        public class Event : UnityEvent<Vector2> { }

        [Header("Rect References")]
        public RectTransform containerRect;
        public RectTransform handleRect;

        [Header("Settings")]
        public bool clampToMagnitude;
        public float magnitudeMultiplier = 1f;
        public bool invertXOutputValue;
        public bool invertYOutputValue;

        //Stored Pointer Values
        [SerializeField] private Vector2 pointerDownPosition;
        [SerializeField] private Vector2 currentPointerPosition;

        [Header("Output")]
        public Event touchZoneOutputEvent;


        public void OnPointerDown(PointerEventData pointerEvent)
        {
            Debug.Log("pointer down");
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Start Drag");
            RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, eventData.position, eventData.pressEventCamera, out currentPointerPosition);
        }

        public void OnEndDrag(PointerEventData pointerEvent)
        {
            Debug.Log("Endrag Drag");
        }

        // void Start()
        // {
        //     SetupHandle();
        // }

        // private void SetupHandle()
        // {
        //     if (handleRect)
        //     {
        //         SetObjectActiveState(handleRect.gameObject, false);
        //     }
        // }

        // public void OnPointerDown(PointerEventData eventData)
        // {

        //     RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, eventData.position, eventData.pressEventCamera, out pointerDownPosition);

        //     if (handleRect)
        //     {
        //         SetObjectActiveState(handleRect.gameObject, true);
        //         UpdateHandleRectPosition(pointerDownPosition);
        //     }
        // }

        public void OnDrag(PointerEventData eventData)
        {

            Debug.Log("Drag");
            //     RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, eventData.position, eventData.pressEventCamera, out currentPointerPosition);

            //     Vector2 positionDelta = GetDeltaBetweenPositions(pointerDownPosition, currentPointerPosition);

            //    Vector2 clampedPosition = ClampValuesToMagnitude(positionDelta);

            //     Vector2 outputPosition = ApplyInversionFilter(positionDelta);

            //     OutputPointerEventValue(outputPosition * magnitudeMultiplier);
        }

        // public void OnPointerUp(PointerEventData eventData)
        // {
        //     pointerDownPosition = Vector2.zero;
        //     currentPointerPosition = Vector2.zero;

        //     OutputPointerEventValue(Vector2.zero);

        //     if (handleRect)
        //     {
        //         SetObjectActiveState(handleRect.gameObject, false);
        //         UpdateHandleRectPosition(Vector2.zero);
        //     }
        // }

        // void OutputPointerEventValue(Vector2 pointerPosition)
        // {
        //     touchZoneOutputEvent.Invoke(pointerPosition);
        // }

        // void UpdateHandleRectPosition(Vector2 newPosition)
        // {
        //     handleRect.anchoredPosition = newPosition;
        // }

        // void SetObjectActiveState(GameObject targetObject, bool newState)
        // {
        //     targetObject.SetActive(newState);
        // }

        // Vector2 GetDeltaBetweenPositions(Vector2 firstPosition, Vector2 secondPosition)
        // {
        //     return secondPosition - firstPosition;
        // }

        // Vector2 ClampValuesToMagnitude(Vector2 position)
        // {
        //     return Vector2.ClampMagnitude(position, 1);
        // }

        // Vector2 ApplyInversionFilter(Vector2 position)
        // {
        //     if (invertXOutputValue)
        //     {
        //         position.x = InvertValue(position.x);
        //     }

        //     if (invertYOutputValue)
        //     {
        //         position.y = InvertValue(position.y);
        //     }

        //     return position;
        // }

        // float InvertValue(float value)
        // {
        //     return -value;
        // }

    }

}
