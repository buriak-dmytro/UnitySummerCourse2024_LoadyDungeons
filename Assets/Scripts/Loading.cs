using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    private static AsyncOperationHandle<SceneInstance> m_SceneLoadOpHandler;

    [SerializeField]
    private Slider m_LoadingSlider;

    [SerializeField]
    private GameObject m_PlayButton, m_LoadingText;

    private void Awake()
    {
        StartCoroutine(LoadNextLevel("Level_0" + GameManager.s_CurrentLevel));
    }

    private IEnumerator LoadNextLevel(string level)
    {
        m_SceneLoadOpHandler =
            Addressables.LoadSceneAsync(level, activateOnLoad: false);

        while (!m_SceneLoadOpHandler.IsDone)
        {
            m_LoadingSlider.value = m_SceneLoadOpHandler.PercentComplete;

            if (m_SceneLoadOpHandler.PercentComplete >= 0.9f &&
                !m_PlayButton.activeInHierarchy)
            {
                m_PlayButton.SetActive(true);
            }

            yield return null;
        }

        Debug.Log($"Loaded Level {level}");
    }
}
