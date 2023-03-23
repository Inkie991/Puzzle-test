using System.Collections;
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
    public SlotId currentSlot;
    public BorderInfo leftBorder;
    public BorderInfo rightBorder;
    public BorderInfo topBorder;
    public BorderInfo bottomBorder;

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

        // string temp = gameObject.name;
        // gameObject.name = temp + " " + correctSlot.Row + " " + correctSlot.Coll;
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
        innerTriangle.GetComponent<SpriteRenderer>().color = Utils.GetColor(color);
        outerTriangle.GetComponent<SpriteRenderer>().color = Utils.GetColor(color);
        colorBorder.GetComponent<SpriteRenderer>().color = Utils.GetColor(color);
    }

    public BorderInfo GetBorder(BorderSide side)
    {
        switch (side)
        {
            case BorderSide.Left:
                return leftBorder;
            case BorderSide.Top:
                return topBorder;
            case BorderSide.Right:
                return rightBorder;
            case BorderSide.Bottom:
                return bottomBorder;
        }

        return new BorderInfo();
    }

    public void AttachToSlot()
    {
        GetComponent<Draggable>().canMove = false;
        StartCoroutine(Anim());
    }

    private IEnumerator Anim()
    {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sprite in sprites)
        {
            Color color = sprite.color;
            color.a = 0.5f;
            sprite.color = color;
        }

        yield return new WaitForSeconds(1);
        
        foreach (var sprite in sprites)
        {
            Color color = sprite.color;
            color.a = 1f;
            sprite.color = color;
        }
        
        yield return null;
    }
}
