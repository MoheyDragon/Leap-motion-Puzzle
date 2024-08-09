using UnityEngine;
using TMPro;
public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Singleton;
    [SerializeField] TextMeshProUGUI correctPiecesCount;
    [SerializeField] TextMeshProUGUI correctPiecesText;
    [SerializeField] private GameObject slotsColliders;
    private int piecesInPlace;
    private int totalPiecesCount;

    private void Awake()
    {
        if (Singleton == null)
            Singleton = this;
        else
            Destroy(gameObject);
    }

    public void EnableSlotColliders()
    {
        slotsColliders.SetActive(true);
    }

    public void DisableSlotsColliders()
    {
        slotsColliders.SetActive(false);
    }

    public void SetPiecesCount(int count)
    {
        totalPiecesCount = count;
        piecesInPlace = 0;
    }

    public void CorrectPiecePutInPlace()
    {
        piecesInPlace++;
        correctPiecesCount.text = piecesInPlace.ToString();
        if (piecesInPlace == totalPiecesCount)
            correctPiecesText.text = "Congrats you've won";
    }

    public void PieceRemovedFromCorrectPlace()
    {
        piecesInPlace--;
    }
}
