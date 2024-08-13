using UnityEngine;
using TMPro;
public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Singleton;
    [SerializeField] TextMeshProUGUI correctPiecesCount;
    [SerializeField] TextMeshProUGUI correctPiecesText;
    [SerializeField] private Transform targetBox;
    private GameObject slotsColliders;
    private int piecesInPlace;
    private int totalPiecesCount;

    private void Awake()
    {
        if (Singleton == null)
            Singleton = this;
        else
            Destroy(gameObject);
    }
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void SetPiecesCount(int count,int slotsCollidersIndex)
    {
        totalPiecesCount = count;
        piecesInPlace = 0;
        slotsColliders = targetBox.GetChild(slotsCollidersIndex).gameObject;
    }
    public void EnableSlotColliders()
    {
        slotsColliders.SetActive(true);
    }

    public void DisableSlotsColliders()
    {
        slotsColliders.SetActive(false);
    }


    public void CorrectPiecePutInPlace()
    {
        piecesInPlace++;
        correctPiecesCount.text = piecesInPlace.ToString();
        if (piecesInPlace == totalPiecesCount)
            OnWin();
    }

    public void PieceRemovedFromCorrectPlace()
    {
        piecesInPlace--;
    }
    private void OnWin()
    {
        correctPiecesText.text = "Congrats you've won";
    }
    
}
