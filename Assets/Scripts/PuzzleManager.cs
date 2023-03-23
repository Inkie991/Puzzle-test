using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleManager : MonoBehaviour, IGameManager
{
    [SerializeField] private PuzzlePiece _puzzlePiecePrefab;
    
    public PuzzlePiece[][] puzzleMatrix;

    public ManagerStatus Status { get; private set; }
    
    public void Startup()
    {
        Debug.Log("Puzzle manager starting...");
        
        Status = ManagerStatus.Started;
    }


    //LOWER ALL METHODS FOR PUZZLE GRID GENERATION
    
    private void GeneratePuzzle(List<List<PuzzleSlot>> slots)
    {
        List<List<PuzzlePiece>> _puzzlePieces = new();

        List<PuzzlePiece> shuffleList = new();

        //spawn all pieces
        for (int i = 0; i < slots.Count; i++)
        {
            List<PuzzlePiece> rowList = new List<PuzzlePiece>();
            
            foreach (var slot in slots[i])
            {
                var piece = Instantiate(_puzzlePiecePrefab);
                piece.correctSlot = slot.id;
                piece.gameObject.name =
                    piece.gameObject.name + " " + piece.correctSlot.Row + "/" + piece.correctSlot.Coll;
                piece.transform.position = slot.transform.position;
                rowList.Add(piece);
                shuffleList.Add(piece);
            }

            _puzzlePieces.Add(rowList);
        }
        
        //List of lists to matrix
        puzzleMatrix = _puzzlePieces.Select(a => a.ToArray()).ToArray();

        BorderShape[] shapes = { BorderShape.Flat, BorderShape.TriangleInside, BorderShape.TriangleOutside };
        BorderColor[] colors =
            { BorderColor.Red, BorderColor.Green, BorderColor.Blue, BorderColor.Yellow, BorderColor.None };
        
        //configuration of all pieces
        for (int i = 0; i < Managers.Gameplay.height; i++)
        {
            for (int j = 0; j < Managers.Gameplay.width; j++)
            {
                var rand = new System.Random();
                PuzzlePiece currentPiece = puzzleMatrix[i][j];

                SetSides(currentPiece);

                currentPiece.rightBorder.Shape = shapes[rand.Next(0, shapes.Length)];
                currentPiece.bottomBorder.Shape = shapes[rand.Next(0, shapes.Length)];
                
                currentPiece.rightBorder.Color = colors[rand.Next(0, colors.Length)];
                currentPiece.bottomBorder.Color = colors[rand.Next(0, colors.Length)];

                if (j < Managers.Gameplay.width - 1)
                {
                    puzzleMatrix[i][j + 1].leftBorder.Shape =
                        Utils.GetOppositeShape(currentPiece.rightBorder.Shape);
                    puzzleMatrix[i][j + 1].leftBorder.Color = currentPiece.rightBorder.Color;
                }

                if (i < Managers.Gameplay.height - 1)
                {
                    puzzleMatrix[i + 1][j].topBorder.Shape =
                        Utils.GetOppositeShape(currentPiece.bottomBorder.Shape);
                    puzzleMatrix[i + 1][j].topBorder.Color = currentPiece.bottomBorder.Color;
                }
                
                CheckForBorderline(currentPiece, i, j);
                
                currentPiece.ConstructPiece();
            }
        }
        
        //shuffle
        
        Vector3 startPos = Managers.Grid.slotMatrix[Managers.Gameplay.height - 1][0].transform.position;
        startPos.y -= 1.15f;
        float SpawnOffset = 0.92f;
        
        System.Random randShuffle = new System.Random();
        
        for (int x = 0; x < Managers.Gameplay.height; x++)
        {
            for (int y = 0; y < Managers.Gameplay.width; y++)
            {
                float posX = (SpawnOffset * x) + startPos.x;
                float posY = -(SpawnOffset * y) + startPos.y;
                int index = randShuffle.Next(0, shuffleList.Count);
                shuffleList[index].transform.position = new Vector3(posX, posY, startPos.z);
                shuffleList.RemoveAt(index);
            }
        }
    }

    public void SetSlotsAndGenerate(List<List<PuzzleSlot>> slots)
    {
        GeneratePuzzle(slots);
    }

    public void CheckForBorderline(PuzzlePiece piece, int row, int coll)
    {
        if (row == 0)
        {
            piece.topBorder.Shape = BorderShape.Flat;
            piece.topBorder.Color = BorderColor.None;
        }

        if (coll == 0)
        {
            piece.leftBorder.Shape = BorderShape.Flat;
            piece.leftBorder.Color = BorderColor.None;
        }
        
        if (row == Managers.Gameplay.width - 1)
        {
            piece.bottomBorder.Shape = BorderShape.Flat;
            piece.bottomBorder.Color = BorderColor.None;
        }

        if (coll == Managers.Gameplay.height - 1)
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
