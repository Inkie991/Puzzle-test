using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public readonly Vector3 defaultScale = new Vector3(0.8f, 0.8f, 1f);
    public readonly Vector3 pickUpScale = new Vector3(0.4f, 0.4f, 1f);
    
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
}
