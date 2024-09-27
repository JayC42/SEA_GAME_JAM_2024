using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance { get; private set; }
    [SerializeField] private GameObject loadingScreenObject;
    [SerializeField] private float minLoadTime = 5f;
    [SerializeField] private float maxLoadTime = 10f;

    private void Awake()
    {
        loadingScreenObject.SetActive(false);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NowLoading()
    {
        if (loadingScreenObject != null)
        {
            loadingScreenObject.SetActive(true);
            StartCoroutine(LoadingRoutine());
        }
        else
        {
            Debug.LogError("Loading Canvas not found in the scene.");
        }
    }

    private IEnumerator LoadingRoutine()
    {
        float loadTime = Random.Range(minLoadTime, maxLoadTime);
        yield return new WaitForSeconds(loadTime);

        loadingScreenObject.SetActive(false);
    }
}
