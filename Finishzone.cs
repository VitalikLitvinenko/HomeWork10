using UnityEngine;

public class FinishZone : MonoBehaviour
{
    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _uiManager.Victory();
    }
}
