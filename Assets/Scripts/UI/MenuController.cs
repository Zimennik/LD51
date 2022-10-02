using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;

    bool _isTransitioning;

    void Awake()
    {
        _isTransitioning = true;
        _fadeImage.color = Color.black;
        _fadeImage.DOFade(0, 2f).OnComplete((() => _isTransitioning = false));
    }

    void Update()
    {
        if (_isTransitioning) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isTransitioning = true;
            _fadeImage.DOFade(1, 2f).OnComplete(() => SceneManager.LoadScene("game"));
        }
    }
}