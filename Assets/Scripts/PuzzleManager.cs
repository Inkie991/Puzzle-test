using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private PuzzlePiece _puzzlePiecePrefab;
    
    private List<PuzzleSlot> _slotsList;
    private List<PuzzlePiece> _puzzlePieces;
    private int _width, _height;

    private void GeneratePuzzle()
    {
        _puzzlePieces = new List<PuzzlePiece>();

        //spawn all pieces
        foreach (var slot in _slotsList)
        {
            var piece = Instantiate(_puzzlePiecePrefab);
            piece.correctSlot = slot.id;
            piece.transform.position = slot.transform.position;
            _puzzlePieces.Add(piece);
        }

        //configuration of all pieces

        foreach (var piece in _puzzlePieces)
        {
            
        }
    }

    public void SetSlotsAndGenerate(List<PuzzleSlot> slots, int width, int height)
    {
        _slotsList = slots;
        _width = width;
        _height = height;
        GeneratePuzzle();
    }
}
