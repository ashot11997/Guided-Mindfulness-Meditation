using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class VideoEngine : MonoBehaviour
{
    public MediaPlayer VideoPlayer;

    public string MenuName;
    
    public Button BackButton;

    public AudioSource Audio_Voice;
    public AudioSource Audio_Music;

    private void Awake()
    {
        BackButton.onClick.AddListener(GoToMenu);
    }

    private void Start()
    {
        if (Audio_Voice == null)
        {
            // this is for first project
            string url = System.IO.Path.Combine(Application.persistentDataPath, System.IO.Path.GetFileName(PlayerPrefs.GetInt("ID") + ".mp4"));
            VideoPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, url);
        }
        else
        {
            // this is for second project
            string url = PlayerPrefs.GetString("VideoUrl");
            VideoPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, url);
            StartCoroutine(PickUpRandomVoice(PlayerPrefs.GetString("VoiceUrl")));
            StartCoroutine(PickUpRandomMusic(PlayerPrefs.GetString("MusicUrl")));
        }
        
    }

    void GoToMenu() {
        SceneManager.LoadScene(MenuName);
    }

    private IEnumerator PickUpRandomVoice(string url) {
        WWW www = new WWW(url);
        yield return www;

        AudioClip audio = www.GetAudioClip(false,true, AudioType.MPEG);
        if (audio == null || audio.loadState == AudioDataLoadState.Unloaded)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Audio_Voice.clip = audio;
        Audio_Voice.Play();
    }
    private IEnumerator PickUpRandomMusic(string url)
    {
        WWW www = new WWW(url);
        yield return www;

        AudioClip audio = www.GetAudioClip(false, true, AudioType.MPEG);
        if (audio == null || audio.loadState == AudioDataLoadState.Unloaded)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Audio_Music.clip = audio;
        Audio_Music.Play();
    }
}
