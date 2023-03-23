using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridManager))]
[RequireComponent(typeof(PuzzleManager))]

public class Managers : MonoBehaviour
{
        public static GridManager Grid { get; private set; }
        public static PuzzleManager Puzzle { get; private set; }
        public static GameplayManager Gameplay { get; private set; }

        private List<IGameManager> _startSequence;

        void Awake()
        {
            Application.targetFrameRate = 60;
            Grid = GetComponent<GridManager>();
            Gameplay = GetComponent<GameplayManager>();
            Puzzle = GetComponent<PuzzleManager>();

            _startSequence = new List<IGameManager>
            {
                Gameplay,
                Grid,
                Puzzle,
            };

            DontDestroyOnLoad(gameObject);
            StartCoroutine(StartupManagers());
        }

        private IEnumerator<Object> StartupManagers()
        {
            foreach (IGameManager manager in _startSequence)
            {
                manager.Startup();
            }

            yield return null;

            int numModules = _startSequence.Count;
            int numReady = 0;

            while (numReady < numModules)
            {
                int lastReady = numReady;
                numReady = 0;

                foreach (IGameManager manager in _startSequence)
                {
                    if (manager.Status == ManagerStatus.Started)
                    {
                        numReady++;
                    }
                }

                if (numReady > lastReady)
                    Debug.Log("Progress: " + numReady + "/" + numModules);
                yield return null;
            }

            Debug.Log("All managers started up");
            
            Gameplay.StartGame();
    }
}

