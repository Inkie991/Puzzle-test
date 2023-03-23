using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlot : MonoBehaviour
{
    public SlotId id;

    private PuzzlePiece _currentPiece;

    public PuzzlePiece CurrentPiece
    {
        get => _currentPiece;
        set => _currentPiece = value;
    }
}
