using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [HideInInspector]
    public bool IsDragging;
    [HideInInspector]
    public Vector3 LastPosition;
    
    private Transform _LastSlot;

    private BoxCollider2D _collider;
    private DragController _dragController;
    private PuzzlePiece _piece;

    private float _movementTime = 15f;
    private Nullable<Vector3> _movementDestination;

    private void Awake()
    {
        _piece = GetComponent<PuzzlePiece>();
    }

    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _dragController = FindObjectOfType<DragController>();
    }

    private void Update()
    {
        if (_LastSlot == null) return;
        
        if (transform.position == _LastSlot.position)
        {
            _piece.inSlot = true;
        }
        else
        {
            _piece.inSlot = false;
        }
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
        // Draggable colliderDraggable = other.GetComponent<Draggable>();
        // if (colliderDraggable != null && _dragController.LastDragged.gameObject == gameObject)
        // {
        //     ColliderDistance2D collDistance = other.Distance(_collider);
        //     Vector3 diff = new Vector3(collDistance.normal.x, collDistance.normal.y) * collDistance.distance;
        //     transform.position -= diff;
        // }

        if (other.CompareTag("Slot"))
        {
            if (Vector3.Distance(transform.position, other.transform.position) < 0.55)
            {
                _LastSlot = other.transform;
                _movementDestination = _LastSlot.position;
            }
        }
        else _movementDestination = LastPosition;
    }
}
