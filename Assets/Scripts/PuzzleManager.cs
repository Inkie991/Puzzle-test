using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private PuzzlePiece _puzzlePiecePrefab;
    
    private List<List<PuzzleSlot>> _slotsList;
    public PuzzlePiece[][] puzzleMatrix;
    private int _width, _height;

    private void GeneratePuzzle()
    {
        List<List<PuzzlePiece>> _puzzlePieces = new();
        
        //spawn all pieces
        for (int i = 0; i < _slotsList.Count; i++)
        {
            List<PuzzlePiece> rowList = new List<PuzzlePiece>();
            
            foreach (var slot in _slotsList[i])
            {
                var piece = Instantiate(_puzzlePiecePrefab);
                piece.correctSlot = slot.id;
                piece.transform.position = slot.transform.position;
                rowList.Add(piece);
            }

            _puzzlePieces.Add(rowList);
        }
        
        //List of lists to matrix
        puzzleMatrix = _puzzlePieces.Select(a => a.ToArray()).ToArray();

        BorderShape[] shapes = { BorderShape.Flat, BorderShape.TriangleInside, BorderShape.TriangleOutside };
        BorderColor[] colors =
            { BorderColor.Red, BorderColor.Green, BorderColor.Blue, BorderColor.Yellow, BorderColor.None };
        
        //configuration of all pieces
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                var rand = new System.Random();
                PuzzlePiece currentPiece = puzzleMatrix[i][j];

                SetSides(currentPiece);

                currentPiece.rightBorder.Shape = shapes[rand.Next(0, shapes.Length)];
                currentPiece.bottomBorder.Shape = shapes[rand.Next(0, shapes.Length)];
                
                currentPiece.rightBorder.Color = colors[rand.Next(0, colors.Length)];
                currentPiece.bottomBorder.Color = colors[rand.Next(0, colors.Length)];

                if (i < _width - 1)
                {
                    puzzleMatrix[i + 1][j].leftBorder.Shape =
                        BorderManager.GetOppositeShape(currentPiece.rightBorder.Shape);
                    puzzleMatrix[i + 1][j].leftBorder.Color = currentPiece.rightBorder.Color;
                }

                if (j < _height - 1)
                {
                    puzzleMatrix[i][j + 1].topBorder.Shape =
                        BorderManager.GetOppositeShape(currentPiece.bottomBorder.Shape);
                    puzzleMatrix[i][j + 1].topBorder.Color = currentPiece.bottomBorder.Color;
                }
                
                CheckForBorderline(currentPiece, i, j);
                
                currentPiece.ConstructPiece();
            }
        }
    }

    public void SetSlotsAndGenerate(List<List<PuzzleSlot>> slots, int width, int height)
    {
        _slotsList = slots; 
        _width = width;
        _height = height;
        GeneratePuzzle();
    }

    public void CheckForBorderline(PuzzlePiece piece, int row, int coll)
    {
        if (coll == 0)
        {
            piece.topBorder.Shape = BorderShape.Flat;
            piece.topBorder.Color = BorderColor.None;
        }

        if (row == 0)
        {
            piece.leftBorder.Shape = BorderShape.Flat;
            piece.leftBorder.Color = BorderColor.None;
        }
        
        if (coll == _width - 1)
        {
            piece.bottomBorder.Shape = BorderShape.Flat;
            piece.bottomBorder.Color = BorderColor.None;
        }

        if (row == _height - 1)
        {
            piece.rightBorder.Shape = BorderShape.Flat;
            piece.rightBorder.Color = BorderColor.None;
        }
    }

    private void SetSides(PuzzlePiece piece)
    {
        piece.leftBorder.Side = BorderSide.Left;
        piece.rightBorder.Side = BorderSide.Right;
        piece.topBorder.Side = BorderSide.Top;
        piece.bottomBorder.Side = BorderSide.Bottom;
    }
}
