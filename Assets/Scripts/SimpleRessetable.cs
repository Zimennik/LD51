using UnityEngine;

public class SimpleRessetable : MonoBehaviour, IResetable
{
    [SerializeField] private bool _shouldBeOn;


    public void ResetObject()
    {
        gameObject.SetActive(_shouldBeOn);
    }
}