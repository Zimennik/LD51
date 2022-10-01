using UnityEngine;

public class LocationChanger : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _currentLocation;
    [SerializeField] private GameObject _newLocation;

    [SerializeField] private ItemSO _requiredItem;

    public string InteractionText =>
        (_requiredItem == null || CharacterController.Instance.inventory.HasItem(_requiredItem))
            ? "Press Space to open door"
            : $"You need a {_requiredItem.Name}";

    public void Interact()
    {
        if (_requiredItem == null || CharacterController.Instance.inventory.HasItem(_requiredItem))
        {
            _currentLocation.SetActive(false);
            _newLocation.SetActive(true);
        }
    }

    public void EnterInteractionZone()
    {
    }

    public void ExitInteractionZone()
    {
    }
}