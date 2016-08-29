using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject[] balls;
    public GameObject gameOverText;
    public GameObject restartText;
    public GameObject startButton;
    public float timeLeft;
    public Text timerText;
    public HatController hatController;

    private Camera cam;
    private float maxWidth;
    private bool playing;

    void Awake()
    {
        gameOverText.SetActive(false);
        restartText.SetActive(false);
        startButton.SetActive(true);
    }

    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        playing = false;
        FindMaxWidth();
        UpdateText();
    }

    void FixedUpdate()
    {
        if (playing)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 5)
                timerText.color = Color.red;

            if (timeLeft < 0)
                timeLeft = 0;

            UpdateText();
        }
    }

    public void StartGame()
    {
        startButton.SetActive(false);
        hatController.toggleHatControl(true);
        StartCoroutine(Spawn());
    }

    void FindMaxWidth()
    {
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        float ballWidth = balls[0].GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - ballWidth;
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);
        playing = true;

        while (timeLeft > 0)
        {
            GameObject ball = balls[Random.Range(0, balls.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-maxWidth, maxWidth), transform.position.y, 0f);
            Quaternion spawnRotation = Quaternion.identity; // no rotation;
            float spawnNum = Random.Range(-maxWidth, maxWidth);
            Instantiate(ball, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }

        yield return new WaitForSeconds(1f);
        gameOverText.SetActive(true);
        yield return new WaitForSeconds(1f);
        restartText.SetActive(true);
    }

    void UpdateText()
    {
        timerText.text = "Time Left:\n" + Mathf.RoundToInt(timeLeft);
    }
}
