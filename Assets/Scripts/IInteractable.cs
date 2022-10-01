public interface IInteractable
{
    string InteractionText { get; }
    void Interact();
    void EnterInteractionZone();
    void ExitInteractionZone();
}