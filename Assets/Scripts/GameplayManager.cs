using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }
    
    [Range(2, 5)]
    [SerializeField] public int width, height;

    [SerializeField] private bool checkColor;

    public bool GameOver { get; private set; }
    
    public void Startup()
    {
        Debug.Log("Gameplay manager starting...");

        GameOver = true;

        Status = ManagerStatus.Started;
    }

    public void StartGame()
    {
        GameOver = false;
    }

    public void CheckConnection (PuzzlePiece piece)
    {
        if (!GameOver)
        {
            //Get all borders for check

            BorderInfo leftPiece, topPiece, rightPiece, bottomPiece;

            List<BorderInfo> tempList = new();
            List<PuzzlePiece> neighbours = new();

            if (piece.currentSlot.Coll > 0)
            {
                PuzzleSlot leftSlot = Managers.Grid.slotMatrix[piece.currentSlot.Row][piece.currentSlot.Coll - 1];
                //Debug.Log("left " + leftSlot.gameObject.name);
                if (leftSlot.CurrentPiece != null)
                {
                    leftPiece = leftSlot.CurrentPiece.GetBorder(BorderSide.Right);
                    neighbours.Add(leftSlot.CurrentPiece);
                    tempList.Add(leftPiece);
                }
            }

            if (piece.currentSlot.Row > 0)
            {
                PuzzleSlot topSlot = Managers.Grid.slotMatrix[piece.currentSlot.Row - 1][piece.currentSlot.Coll];
                //Debug.Log("top " + topSlot.gameObject.name);
                if (topSlot.CurrentPiece != null)
                {
                    topPiece = topSlot.CurrentPiece.GetBorder(BorderSide.Bottom);
                    neighbours.Add(topSlot.CurrentPiece);
                    tempList.Add(topPiece);
                }
            }

            if (piece.currentSlot.Coll < width - 1)
            {

                PuzzleSlot rightSlot = Managers.Grid.slotMatrix[piece.currentSlot.Row][piece.currentSlot.Coll + 1];
                //Debug.Log("right " + rightSlot.gameObject.name);
                if (rightSlot.CurrentPiece != null)
                {
                    rightPiece = rightSlot.CurrentPiece.GetBorder(BorderSide.Left);
                    neighbours.Add(rightSlot.CurrentPiece);
                    tempList.Add(rightPiece);
                }
            }

            if (piece.currentSlot.Row < height - 1)
            {
                PuzzleSlot bottomSlot = Managers.Grid.slotMatrix[piece.currentSlot.Row + 1][piece.currentSlot.Coll];
                //Debug.Log("bottom " + bottomSlot.gameObject.name);
                if (bottomSlot.CurrentPiece != null)
                {
                    bottomPiece = bottomSlot.CurrentPiece.GetBorder(BorderSide.Top);
                    neighbours.Add(bottomSlot.CurrentPiece);
                    tempList.Add(bottomPiece);
                }
            }

            int correctBorders = 0;
            
            if (tempList.Count > 0)
            {
                foreach (var puzzlePiece in tempList)
                {
                    BorderInfo currentPieceBorder = piece.GetBorder(Utils.GetOppositeSide(puzzlePiece.Side));
                    if (currentPieceBorder.Shape == Utils.GetOppositeShape(puzzlePiece.Shape))
                    {
                        if (checkColor)
                        {
                            if (currentPieceBorder.Color == puzzlePiece.Color) correctBorders++;
                        }
                        else correctBorders++;
                    }
                }
            }

            if (correctBorders == tempList.Count && correctBorders > 0 &&
                (piece.currentSlot.Coll == piece.correctSlot.Coll
                 && piece.currentSlot.Row == piece.correctSlot.Row))
            {
                piece.AttachToSlot();
                foreach(var otherPiece in neighbours)
                {
                    otherPiece.AttachToSlot();
                }
            }
        }

    }
}

