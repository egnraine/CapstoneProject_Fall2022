using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject fadeIn;
    public double time;
    public double currentTime;
    public GameObject HUD;

    private void Start()
    {
        time = GameObject.Find("IntroAnimation").GetComponent<VideoPlayer>().clip.length;
    }

    private void Update()
    {
        currentTime = GameObject.Find("IntroAnimation").GetComponent<VideoPlayer>().time;

        if (currentTime > 77)
        {
            SceneManager.LoadScene("Home");
        }
    }

    public void PlayGame()
    {
        if (fadeIn != null)
        {
            GameObject panel = Instantiate(fadeIn, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }

        videoPlayer.Play();
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
