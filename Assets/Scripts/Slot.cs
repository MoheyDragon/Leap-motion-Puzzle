using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool isMainBox;
    private PuzzlePiece currentPiece;
    private int index;

    private void Awake()
    {
        if (isMainBox) return;
        index = int.Parse(name);
    }

    public bool CompareIndex(int puzzleIndex)
    {
        if (isMainBox) return false;
        return puzzleIndex == index;
    }

    public void SetNewPiece(PuzzlePiece newPiece)
    {
        if (isMainBox) return;
        currentPiece = newPiece;
    }

    public bool IsEmpty()
    {
        return currentPiece == null;
    }
}
