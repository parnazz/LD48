using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningScene : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _images;

    [SerializeField]
    private Image _fadeImage;

    private int _currentIndex = 0;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (_currentIndex < _images.Count - 1)
                StartCoroutine(ChangeBackgroundCoroutine());
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator ChangeBackgroundCoroutine()
    {
        //if (_currentIndex >= _images.Count)
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            _fadeImage.color = new Color(_fadeImage.color.r,
                _fadeImage.color.g,
                _fadeImage.color.b,
                i);
            yield return null;
        }

        _images[_currentIndex].SetActive(false);
        _currentIndex++;
        _images[_currentIndex].SetActive(true);

        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            _fadeImage.color = new Color(_fadeImage.color.r,
                _fadeImage.color.g,
                _fadeImage.color.b,
                i);
            yield return null;
        }
    }
}
