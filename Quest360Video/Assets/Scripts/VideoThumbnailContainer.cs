using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoThumbnailContainer : MonoBehaviour
{
    public Button DownloadBtn;
    public Button StreamBtn;

    private int ID;
    private bool isDownloaded;

    public Text percentageText;

    public GameObject LoadingBar;

    public Image FillingBar;

    private LargeFileDownloaderExample VideoDownloader;

    void Awake()
    {
        DownloadBtn.onClick.AddListener(DownloadVideo);
    }

    void Start()
    {
        Text text = GetComponentInChildren<Text>();
        if (text != null)
        {
            string[] newText = text.text.Split(' ');
            int id = int.Parse(newText[1]);
            Setup(id);
        }

        GameObject downloader = GameObject.FindGameObjectWithTag("VideoDownloader");
        if (downloader != null)
        {
            VideoDownloader = downloader.GetComponent<LargeFileDownloaderExample>();
        }

        LoadingBar.SetActive(false);
        DownloadBtn.gameObject.SetActive(true);

        /*if (PlayerPrefs.GetInt("downloaded") == 0 || PlayerPrefs.GetInt("downloaded") == null)
        {
            StreamBtn.interactable = false;
            DownloadBtn.interactable = true;
        }
        else
        {
            StreamBtn.interactable = true;
            DownloadBtn.interactable = false;
        }*/
    }

    public void Setup(int id) {
        ID = id;
    }

    public void Downloaded() {
        StreamBtn.interactable = true;
        DownloadBtn.interactable = false;
    }

    void DownloadVideo() {
        Debug.Log("Starting " + ID + " Video");
        VideoDownloader.DownloadVideo(ID, percentageText, FillingBar, LoadingBar, this);
    }

    public void VideoDownloaded() {
        isDownloaded = true;
        Debug.LogError("Video Successfully Downloaded");
    }

}
