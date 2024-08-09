using UnityEngine;
using System.Collections.Generic;

public class PuzzlePiecesManager : MonoBehaviour
{
    [SerializeField] private float detectingSlotsRadius = 0.5f;
    [SerializeField] private PuzzlePiece[] puzzlePiecePrefabs;
    [Header("Grid Settings")]
    [SerializeField] private int rows = 3;
    [SerializeField] private int columns = 3;
    [SerializeField] private Vector2 xClamp, zClamp;
    [SerializeField] private bool shuffle;
    [SerializeField] private float randomOffset = 0.5f;

    private const float fixedY = 0.06f;

    private void Start()
    {
        if (shuffle)
        {
            puzzlePiecePrefabs = ShuffleArray(puzzlePiecePrefabs);
        }
        CreatePuzzlePieces();
    }

    private void CreatePuzzlePieces()
    {
        List<Vector3> positions = new List<Vector3>();

        int totalPieces = puzzlePiecePrefabs.Length;
        int totalGridCells = Mathf.CeilToInt(Mathf.Sqrt(totalPieces));

        float xStep = (xClamp.y - xClamp.x) / totalGridCells;
        float zStep = (zClamp.y - zClamp.x) / totalGridCells;

        for (int i = 0; i < totalGridCells; i++)
        {
            for (int j = 0; j < totalGridCells; j++)
            {
                if (positions.Count >= totalPieces) break;

                float x = xClamp.x + i * xStep;
                float z = zClamp.x + j * zStep;

                float randomX = shuffle ? Random.Range(-randomOffset, randomOffset) * xStep : 0;
                float randomZ = shuffle ? Random.Range(-randomOffset, randomOffset) * zStep : 0;

                Vector3 position = new Vector3(x + randomX, fixedY, z + randomZ);
                positions.Add(position);
            }
        }

        PuzzleManager.Singleton.SetPiecesCount(rows * columns);

        for (int pieceIndex = 0; pieceIndex < puzzlePiecePrefabs.Length; pieceIndex++)
        {
            PuzzlePiece newPiece = Instantiate(puzzlePiecePrefabs[pieceIndex], transform);
            newPiece.SetupPiece(positions[pieceIndex], detectingSlotsRadius);
        }
    }

    private void Update()
    {
        if (!shuffle) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            puzzlePiecePrefabs = ShuffleArray(puzzlePiecePrefabs);
            CreatePuzzlePieces();
        }
    }

    private T[] ShuffleArray<T>(T[] array)
    {
        T[] shuffledArray = array;
        for (int i = 0; i < array.Length; i++)
        {
            T temp = shuffledArray[i];
            int randomIndex = Random.Range(i, array.Length);
            shuffledArray[i] = shuffledArray[randomIndex];
            shuffledArray[randomIndex] = temp;
        }
        return shuffledArray;
    }
}
