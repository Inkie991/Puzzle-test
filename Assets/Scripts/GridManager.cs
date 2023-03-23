using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager : MonoBehaviour, IGameManager
{
    private const float SpawnOffset = 0.92f;

    [SerializeField] private PuzzleSlot _slotPrefab;
    
    public ManagerStatus Status { get; private set; }
    
    public PuzzleSlot[][] slotMatrix;
    private Transform _gameGrid;

    void GenerateGrid()
    {
        // Vector3 startPos = new Vector3((float)(_width - 1)/2 * SpawnOffset * -1, (float)(_height - 1) / 2 * SpawnOffset, 0);
        
        List<List<PuzzleSlot>> _gridList = new();
        
        Vector3 startPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 100f * 13.5f, Screen.height - Screen.height / 100f * 5.8f));
        startPos.z = 0;

        for (int x = 0; x < Managers.Gameplay.height; x++)
        {
            List<PuzzleSlot> rowList = new List<PuzzleSlot>();
            
            for (int y = 0; y < Managers.Gameplay.width; y++)
            {
                PuzzleSlot slot = Instantiate(_slotPrefab, _gameGrid);

                float posX = (SpawnOffset * y) + startPos.x;
                float posY = -(SpawnOffset * x) + startPos.y;
                slot.transform.position = new Vector3(posX, posY, startPos.z);
                slot.id.Row = x;
                slot.id.Coll = y;
                slot.name = slot.name + " " + x + "/" + y;
                rowList.Add(slot);
            }
            
            _gridList.Add(rowList);
        }
        
        slotMatrix = _gridList.Select(a => a.ToArray()).ToArray();
        
        Managers.Puzzle.SetSlotsAndGenerate(_gridList);
    }


    public void Startup()
    {
        Debug.Log("Grid manager starting...");
        
        _gameGrid = GameObject.FindGameObjectWithTag("Grid").transform;
        GenerateGrid();
        
        Status = ManagerStatus.Started;
    }
}
