using UnityEngine;
using System.Collections.Generic;
public class PuzzlePiecesManager : MonoBehaviour
{
    [SerializeField] PuzzlePiece[] puzzlePiecePrefabs;

    [Header("Grid Settings")]
    [SerializeField] int rows = 3; 
    [SerializeField] int columns = 3;
    [SerializeField] float spacing = 1.0f;
    [SerializeField] Vector2 xClamp, zClamp;
    [SerializeField] bool shuffle;
    [SerializeField] float randomOffset=.5f;
    const float fixedY = 0.06f;
    private void Start()
    {
        if(shuffle)
        puzzlePiecePrefabs = ShuffleArray(puzzlePiecePrefabs);
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

                // Add random offset within the cell
                float randomX = 0;
                float randomZ = 0;
                if (shuffle)
                {
                    randomX = Random.Range(-randomOffset, randomOffset) * xStep;
                    randomZ = Random.Range(-randomOffset, randomOffset) * zStep;
                }
                Vector3 position = new Vector3(x + randomX, fixedY, z + randomZ);
                positions.Add(position);
            }
        }

        // Shuffle positions to add randomness

        // Place the puzzle pieces
        for (int pieceIndex = 0; pieceIndex < puzzlePiecePrefabs.Length ; pieceIndex++)
        {
            PuzzlePiece newPiece = Instantiate(puzzlePiecePrefabs[pieceIndex], transform);
            newPiece.SetLocalPosition(positions[pieceIndex]);
        }
    }
    private void Update()
    {
        if (!shuffle) return;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject); 
            }
            puzzlePiecePrefabs= ShuffleArray(puzzlePiecePrefabs);
            CreatePuzzlePieces();

        }
    }
    private T[] ShuffleArray<T>(T[] array)
    {
        T[] shuffeldArray = array;
        for (int i = 0; i < array.Length; i++)
        {
            T temp = shuffeldArray[i];
            int randomIndex = Random.Range(i, array.Length);
            shuffeldArray[i] = shuffeldArray[randomIndex];
            shuffeldArray[randomIndex] = temp;
        }
        return shuffeldArray;
    }
}
