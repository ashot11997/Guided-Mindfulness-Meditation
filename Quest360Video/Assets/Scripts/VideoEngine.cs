using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VideoEngine : MonoBehaviour
{
    public MediaPlayer VideoPlayer;

    public Button BackButton;

    private void Awake()
    {
        BackButton.onClick.AddListener(GoToMenu);
    }

    private void Start()
    {
        string url = System.IO.Path.Combine(Application.persistentDataPath, System.IO.Path.GetFileName(PlayerPrefs.GetInt("ID")+ ".mp4"));
        VideoPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, url);
    }

    void GoToMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
