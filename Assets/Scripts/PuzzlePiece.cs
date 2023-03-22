using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [Header("Left Border")]
    [SerializeField] private GameObject leftInnerTriangle;
    [SerializeField] private GameObject leftOuterTriangle;
    [SerializeField] private GameObject leftColorBorder;
    
    [Header("Top Border")]
    [SerializeField] private GameObject topInnerTriangle;
    [SerializeField] private GameObject topOuterTriangle;
    [SerializeField] private GameObject topColorBorder;
    
    [Header("Right Border")]
    [SerializeField] private GameObject rightInnerTriangle;
    [SerializeField] private GameObject rightOuterTriangle;
    [SerializeField] private GameObject rightColorBorder;
    
    [Header("Bottom Border")]
    [SerializeField] private GameObject bottomInnerTriangle;
    [SerializeField] private GameObject bottomOuterTriangle;
    [SerializeField] private GameObject bottomColorBorder;
    
    [HideInInspector]
    public bool inSlot;
    public SlotId correctSlot;
    public BorderInfo leftBorder;
    public BorderInfo rightBorder;
    public BorderInfo topBorder;
    public BorderInfo bottomBorder;
    
    void Update()
    {

    }
    
    void Start()
    {
        
    }

    // public bool CheckConnection()
    // {
    //     
    // }

    public void ConstructPiece()
    {
        BorderInfo[] borders = { leftBorder, topBorder, rightBorder, bottomBorder };

        foreach (var border in borders)
        {
            switch (border.Side)
            {
                case BorderSide.Left:
                    SetColor(border.Color, leftInnerTriangle, leftOuterTriangle, leftColorBorder);
                    SetShape(border.Shape, leftInnerTriangle, leftOuterTriangle);
                    break;
                case BorderSide.Top:
                    SetColor(border.Color, topInnerTriangle, topOuterTriangle, topColorBorder);
                    SetShape(border.Shape, topInnerTriangle, topOuterTriangle);
                    break;
                case BorderSide.Right:
                    SetColor(border.Color, rightInnerTriangle, rightOuterTriangle, rightColorBorder);
                    SetShape(border.Shape, rightInnerTriangle, rightOuterTriangle);
                    break;
                case BorderSide.Bottom:
                    SetColor(border.Color, bottomInnerTriangle, bottomOuterTriangle, bottomColorBorder);
                    SetShape(border.Shape, bottomInnerTriangle, bottomOuterTriangle);
                    break;
            }
        }

    }

    private void SetShape(BorderShape shape, GameObject innerTriangle, GameObject outerTriangle)
    {
        switch (shape)
        {
            case BorderShape.Flat:
                innerTriangle.SetActive(true);
                outerTriangle.SetActive(false);
                break;
            case BorderShape.TriangleInside:
                innerTriangle.SetActive(false);
                outerTriangle.SetActive(false);
                break;
            case BorderShape.TriangleOutside:
                innerTriangle.SetActive(true);
                outerTriangle.SetActive(true);
                break;
        }
    }

    private void SetColor(BorderColor color, GameObject innerTriangle, GameObject outerTriangle, GameObject colorBorder)
    {
        innerTriangle.GetComponent<SpriteRenderer>().color = BorderManager.GetColor(color);
        outerTriangle.GetComponent<SpriteRenderer>().color = BorderManager.GetColor(color);
        colorBorder.GetComponent<SpriteRenderer>().color = BorderManager.GetColor(color);
    }
}
