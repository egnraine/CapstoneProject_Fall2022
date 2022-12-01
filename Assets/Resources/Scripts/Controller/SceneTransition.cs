using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject fadeIn;
    public GameObject fadeOut;
    public float fadeWait;

    private void Awake()
    {
        if (fadeIn != null)
        {
            GameObject panel = Instantiate(fadeIn, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    public void onBttnClick()
    {
        StartCoroutine(FadeCo());
    }

    public IEnumerator FadeCo()
    {
        if (fadeOut != null)
        {
            Instantiate(fadeOut, Vector3.zero, Quaternion.identity);
        }

        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
