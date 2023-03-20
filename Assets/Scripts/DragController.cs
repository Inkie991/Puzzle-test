using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    private bool _isDragActive = false;

    private Vector2 _screenPos;
    private Vector3 _worldPos;
    private Draggable _lastDragged;

    public Draggable LastDragged
    {
        get { return _lastDragged; }
    }

    void Awake()
    {
        DragController[] controllers = FindObjectsOfType<DragController>();
        if (controllers.Length > 1) Destroy(gameObject);
    }
    void Update()
    {
        if (_isDragActive && 
            (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            Drop();
            return;
        }
        
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            _screenPos = new Vector2(mousePos.x, mousePos.y);
        } else if (Input.touchCount > 0)
        {
            _screenPos = Input.GetTouch(0).position;
        } else return;

        _worldPos = Camera.main.ScreenToWorldPoint(_screenPos);
        
        if (_isDragActive) Drag();
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(_worldPos, Vector2.zero);
            if (hit.collider != null)
            {
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();
                if (draggable != null)
                {
                    _lastDragged = draggable;
                    InitDrag();
                }
            }
        }
        
    }

    void InitDrag()
    {
        _lastDragged.LastPosition = _lastDragged.transform.position;
        UpdateDragStatus(true);
    }

    void Drag()
    {
        _lastDragged.transform.position = new Vector3(_worldPos.x, _worldPos.y);
    }

    void Drop()
    {
        UpdateDragStatus(false);
    }

    void UpdateDragStatus(bool isDragging)
    {
        _isDragActive = _lastDragged.IsDragging = isDragging;
        _lastDragged.gameObject.layer = isDragging ? Layer.Dragging : Layer.Default;
    }
}
