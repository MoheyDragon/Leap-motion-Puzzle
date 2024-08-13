using UnityEngine;
using Leap.Unity.Interaction;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private int puzzleTypeIndex;
    private float detectingSlotsRadius = 0.5f;
    private InteractionBehaviour interactionBehaviour;
    private Slot currentSlot;
    private Vector3 returnToPosition;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Awake()
    {
        interactionBehaviour = GetComponent<InteractionBehaviour>();
        interactionBehaviour.OnGraspBegin += OnPickUp;
        interactionBehaviour.OnGraspEnd += OnLetGo;
        initialRotation = transform.rotation;
    }

    public void SetupPiece(Vector3 position, float radius)
    {
        transform.localPosition = position;
        initialPosition = transform.position;

        detectingSlotsRadius = radius;

    }

    public void OnPickUp()
    {
        returnToPosition = transform.position;
        PuzzleManager.Singleton.EnableSlotColliders();
    }

    public void OnLetGo()
    {
        Slot nearbySlot = FindNearbySlot();

        if (nearbySlot != null && nearbySlot.IsEmpty())
        {
            if (nearbySlot.isMainBox)
                transform.position = initialPosition;
            else
                transform.position = nearbySlot.transform.position;

            if (currentSlot != null)
                currentSlot.SetNewPiece(null);

            currentSlot = nearbySlot;
            currentSlot.SetNewPiece(this);

            bool isInCorrectPlace = currentSlot.CompareIndex(puzzleTypeIndex);
            UpdateCorrectPuzzlePiecesCount(isInCorrectPlace);
        }
        else
        {
            ReturnToInitialPosition();
        }

        transform.rotation = initialRotation;
        PuzzleManager.Singleton.DisableSlotsColliders();
    }

    private void ReturnToInitialPosition()
    {
        transform.position = returnToPosition;
    }

    private Slot FindNearbySlot()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, detectingSlotsRadius, Vector3.down, detectingSlotsRadius);
        Slot nearestSlot = null;
        float nearestDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent(out Slot slot))
            {
                float distance = Vector3.Distance(transform.position, slot.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestSlot = slot;
                }
            }
        }

        return nearestSlot;
    }

    private void UpdateCorrectPuzzlePiecesCount(bool isInCorrectPlace)
    {
        if (isInCorrectPlace)
        {
            GetComponent<InteractionBehaviour>().enabled = false;
            PuzzleManager.Singleton.CorrectPiecePutInPlace();
        }
    }

    public void SetIndex(int index)
    {
        puzzleTypeIndex = index;
    }
}
