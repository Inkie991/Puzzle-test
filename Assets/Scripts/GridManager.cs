using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private const float SpawnOffset = 0.95f;
    
    [Range(2, 5)]
    [SerializeField] private int _width, _height;

    [SerializeField] private PuzzleSlot _slotPrefab;

    private List<PuzzleSlot> _gridList;
    private Transform _gameGrid;

    private PuzzleManager _puzzleManager;

    void GenerateGrid()
    {
        Vector3 startPos = new Vector3((float)(_width - 1)/2 * SpawnOffset * -1, (float)(_height - 1) / 2 * SpawnOffset, 0);

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                PuzzleSlot slot = Instantiate(_slotPrefab, _gameGrid);

                float posX = (SpawnOffset * x) + startPos.x;
                float posY = -(SpawnOffset * y) + startPos.y;
                slot.transform.position = new Vector3(posX, posY, startPos.z);
                slot.id.Row = x;
                slot.id.Coll = y;
                _gridList.Add(slot);
            }
        }
        
        _puzzleManager.SetSlotsAndGenerate(_gridList, _width, _height);
    }
    

    void Start()
    {
        _puzzleManager = GetComponent<PuzzleManager>();
        _gridList = new();
        _gameGrid = GameObject.FindGameObjectWithTag("Grid").transform;
        GenerateGrid();
    }
}
