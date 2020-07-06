using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// import large file downloader
using LargeFileDownloader;

public class LargeFileDownloaderExample : MonoBehaviour {

	const string FILE_URL_MP4 = "http://www.sample-videos.com/video/mp4/720/big_buck_bunny_720p_30mb.mp4";
	const string FILE_URL_FLV = "http://www.sample-videos.com/video/flv/720/big_buck_bunny_720p_30mb.flv";

	FileDownloader downloader;

	DownloadEvent evt = new DownloadEvent();

    private string[] VideoURLs;

	// Use this for initialization
	void Start () {

        VideoURLs = new string[10] { "http://Biohack.network/1.mp4", "http://Biohack.network/2.mp4", "http://biohack.network/3.mp4", "http://Biohack.network/4.mp4", "http://Biohack.network/5.mp4", "http://Biohack.network/6.mp4", "http://Biohack.network/7.mp4", "http://Biohack.network/8.mp4", "http://Biohack.network/9.mp4", "http://Biohack.network/10.mp4" };

		// create downloader instance
		downloader = new FileDownloader ();

		// add events listners
		FileDownloader.onComplete += OnDownloadComplete;
		FileDownloader.onProgress += OnProgress;

		Debug.Log (Application.persistentDataPath);
	}


	void OnDownloadComplete(DownloadEvent e)
	{
		evt = e;

		if (evt.error != null)
			Debug.Log (evt.error);
	}

	void OnProgress(DownloadEvent e)
	{
		evt = e;
	}


    /*void OnGUI()
	{
		if(GUILayout.Button("Download MP4"))// && !downloader.IsInQueue(FILE_URL_MP4)
		{
			// start downloading
			string pathToSave = System.IO.Path.Combine (Application.persistentDataPath, System.IO.Path.GetFileName(FILE_URL_MP4));
			downloader.Download (FILE_URL_MP4, pathToSave);
		}

		if(GUILayout.Button("Download FLV"))//&& !downloader.IsInQueue(FILE_URL_FLV)
		{
			// start downloading
			string pathToSave = System.IO.Path.Combine (Application.persistentDataPath, System.IO.Path.GetFileName(FILE_URL_FLV));
			downloader.Download (FILE_URL_FLV, pathToSave);
		}

		if (GUILayout.Button ("Download in queue MP4") && !downloader.IsInQueue(FILE_URL_MP4)) {
			string pathToSave = System.IO.Path.Combine (Application.persistentDataPath, System.IO.Path.GetFileName(FILE_URL_MP4));
			downloader.DownloadInQueue(FILE_URL_MP4, pathToSave);
		}

		if (GUILayout.Button ("Download in queue FLV") && !downloader.IsInQueue(FILE_URL_FLV)) {
			string pathToSave = System.IO.Path.Combine (Application.persistentDataPath, System.IO.Path.GetFileName(FILE_URL_FLV));
			downloader.DownloadInQueue(FILE_URL_FLV, pathToSave);
		}

		if (GUILayout.Button ("Cancel")) {
			downloader.Cancel();
		}

		// status
		GUILayout.Label("Total Bytes : " +evt.totalBytes);
		GUILayout.Label("Downloaded Bytes : " +evt.downloadedBytes);
		GUILayout.Label("Downloading Progress (%): " +evt.progress);
		GUILayout.Label ("\nStatus : " + evt.status);
		GUILayout.Label ("\nError : " + ((!string.IsNullOrEmpty(evt.error)) ? evt.error : ""));

	}*/


    private bool IsStartedDownloading = false;
    private Text CurrentPercentageText;
    private Slider CurrentFillingBar;

    private VideoThumbnailContainer CurrentVideoThumbnail;

    private string pathToSave;

    public void DownloadVideo(int id, Text percentageText, Slider Fillingbar, GameObject LoadingBar, VideoThumbnailContainer VideoThumbnail) {
        int newId = id - 1;
        CurrentPercentageText = percentageText;
        CurrentFillingBar = Fillingbar;
        LoadingBar.SetActive(true);
        CurrentVideoThumbnail = VideoThumbnail;

        IsStartedDownloading = true;
        pathToSave = System.IO.Path.Combine(Application.persistentDataPath, System.IO.Path.GetFileName(VideoURLs[newId]));
        downloader.Download(VideoURLs[newId], pathToSave);
    }

    private void Update()
    {
        if (IsStartedDownloading)
        {
            CurrentPercentageText.text = evt.progress.ToString() + " %";
            CurrentFillingBar.value = evt.progress;

            if (evt.progress == 100)
            {
                StartCoroutine(CurrentVideoThumbnail.VideoDownloaded(pathToSave));
                IsStartedDownloading = false;
            }
        }
    }
}
