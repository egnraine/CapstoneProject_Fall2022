using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using EZCameraShake;
using UnityEngine.SceneManagement;

public class PowerBar : MonoBehaviour
{
    [SerializeField]
    private Image imagePowerUp;

    private bool isPowerUp = false;
    private bool isDirectionUp = true;
    private float amtPower = 0.0f;
    private int counter = 0;

    public float powerSpeed;
    public Button powerUpBttn;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI counterText;
    public Image blur;

    private bool useTimer = true;
    private float timeDuration = 30.0f;

    public Animator anim;
    public Animator anim2;
    public Animator minigame;

    public string[] sceneNames;


    private void Start()
    {
        anim.speed = 1.3f;

        useTimer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (useTimer)
        {
            timeDuration -= Time.deltaTime;
        }

        timerText.text = string.Format("Timer: " + "{0:00}", timeDuration);
        counterText.text = "Total Breaths Taken: " + counter;
        
        if (imagePowerUp.fillAmount == 0)
        {
            GetComponent<EventTrigger>().enabled = true;
        }

        if (anim.speed != 0.4f)
        {
            if (isPowerUp)
            {
                PowerActive();
            }

            if (isPowerUp == false)
            {
                amtPower -= Time.deltaTime * powerSpeed;
                imagePowerUp.fillAmount = amtPower / 100.0f;
            }
        }

        if (anim.speed == 0.4f)
        {
            timerText.text = "I'm feeling calmer now...";

            isPowerUp = false;
            useTimer = false;

            minigame.SetTrigger("victory");
            CameraShaker.Instance.enabled = false;
            blur.enabled = false;
        }

        if (timeDuration < 0 && anim.speed > 0.4f)
        {
            timerText.text = "Times Up";

            isPowerUp = false;
            useTimer = false;

            minigame.SetTrigger("fail");
        }
    }

    void PowerActive()
    {
        if (isDirectionUp)
        {
            amtPower += Time.deltaTime * powerSpeed;
            if(amtPower > 100)
            {
                isDirectionUp = false;
                amtPower = 100.0f;
            }
        }

        imagePowerUp.fillAmount = amtPower / 100.0f;
    }

    public void StartPowerUp()
    {
        useTimer = true;

        isPowerUp = true;
        amtPower = 0.0f;
        isDirectionUp = true;
    }

    public void EndPowerUp()
    {
        isPowerUp = false;

        powerSpeed -= 1.0f;
        Debug.Log("Speed: " + powerSpeed);

        if (imagePowerUp.fillAmount == 1)
        {
            powerSpeed -= 3.0f;
            Debug.Log("Speed: " + powerSpeed);
        }

        if (imagePowerUp.fillAmount >= 0.70 && imagePowerUp.fillAmount <= 0.80)
        {
            counter++;
            Debug.Log("Counter: " + counter);

            powerSpeed -= 7.0f;
            Debug.Log("Speed: " + powerSpeed);

            if (counter == 5)
            {
                anim.speed = 1f;
                anim2.speed = anim.speed;
            }

            if (counter == 10)
            {
                anim.speed = 0.7f;
                anim2.speed = anim.speed;
            }

            if (counter == 15)
            {
                anim.speed = 0.4f;
                anim2.speed = anim.speed;

                GameManager.instance.GrantXp(2);
            }
        }
    }

    public void BadEnding()
    {
        string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
        SceneManager.LoadScene(sceneName);
    }
}
