using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool IsDragging;
    public Vector3 LastPosition;

    private BoxCollider2D _collider;
    private DragController _dragController;

    private float _movementTime = 15f;
    private Nullable<Vector3> _movementDestination;

    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _dragController = FindObjectOfType<DragController>();
    }

    private void FixedUpdate()
    {
        if (_movementDestination.HasValue)
        {
            if (IsDragging)
            {
                _movementDestination = null;
                return;
            }

            if (transform.position == _movementDestination)
            {
                gameObject.layer = Layer.Default;
                _movementDestination = null;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, _movementDestination.Value,
                    _movementTime * Time.fixedDeltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Draggable colliderDraggable = other.GetComponent<Draggable>();
        if (colliderDraggable != null && _dragController.LastDragged.gameObject == gameObject)
        {
            ColliderDistance2D collDistance = other.Distance(_collider);
            Vector3 diff = new Vector3(collDistance.normal.x, collDistance.normal.y) * collDistance.distance;
            transform.position -= diff;
        }

        if (other.CompareTag("Slot"))
        {
            if (Vector3.Distance(transform.position, other.transform.position) < 0.55)
                _movementDestination = other.transform.position;
        }
        else _movementDestination = LastPosition;
    }
}
