using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public List<AudioClip> musicPlaylist;
    public AudioClip countdown123SFX;
    public AudioClip countdownFinishSFX;
    public AudioClip buttonClickSFX;
    public AudioClip finalScoreSFX;
    public AudioClip chargingAirSFX;
    public AudioClip stopChargingAirSFX;
    public AudioClip balloonHitSFX;
    public AudioClip balloonPopSFX;
    public float crossFadeDuration = 3.0f;
    public float crossFadeStartTime = 3.0f;

    private List<int> playOrder;
    private int currentIndex = -1;
    private float songStartTime;
    private bool isCrossFading = false;

    void Start()
    {
        AudioManager.Instance.StopAllMusic();
        GenerateNewPlayOrder();
        StartCoroutine(WaitForCountdownAndPlayMusic());
    }

    void Update()
    {
        if (!IsTimeToStartCrossFade())
        {
            return;
        }
        if (!isCrossFading && IsTimeToStartCrossFade())
        {
            StartCrossFade();
        }
    }
    private IEnumerator WaitForCountdownAndPlayMusic()
    {
        yield return new WaitUntil(() => CountdownTimer.IsCountdownFinished);
        PlayNextSong();
    }
    bool IsTimeToStartCrossFade()
    {
        if (currentIndex >= 0 && currentIndex < musicPlaylist.Count)
        {
            float songDuration = musicPlaylist[playOrder[currentIndex]].length;
            return Time.time - songStartTime >= songDuration - crossFadeStartTime;
        }
        return false;
    }

    void StartCrossFade()
    {
        isCrossFading = true;
        int nextIndex = (currentIndex + 1) % playOrder.Count;
        if (nextIndex == 0)
        {
            GenerateNewPlayOrder();
        }
        int nextSongIndex = playOrder[nextIndex];
        AudioManager.Instance.PlayMusicWithCrossFade(musicPlaylist[nextSongIndex], crossFadeDuration);
        currentIndex = nextIndex;
        songStartTime = Time.time + crossFadeDuration;
        isCrossFading = false;
    }

    void GenerateNewPlayOrder()
    {
        playOrder = Enumerable.Range(0, musicPlaylist.Count).ToList();
        ShuffleList(playOrder);

        if (currentIndex >= 0 && playOrder[0] == currentIndex)
        {
            int temp = playOrder[0];
            playOrder[0] = playOrder[playOrder.Count - 1];
            playOrder[playOrder.Count - 1] = temp;
        }

        currentIndex = -1;
    }

    void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public void PlayNextSong()
    {
        currentIndex++;
        if (currentIndex >= playOrder.Count)
        {
            GenerateNewPlayOrder();
            currentIndex = 0;
        }

        int songIndex = playOrder[currentIndex];
        AudioManager.Instance.PlayMusic(musicPlaylist[songIndex]);
        songStartTime = Time.time;
    }
    public void PlayCountdown123SFX()
    {
        AudioManager.Instance.PlaySFX(countdown123SFX, 1);
    }
    public void PlayCountdownFinishSFX()
    {
        AudioManager.Instance.PlaySFX(countdownFinishSFX, 1);
    }
    public void PlayButtonClickSFX()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX);
    }
    public void PlayFinalScoreSFX()
    {
        AudioManager.Instance.PlaySFX(finalScoreSFX, 1);
    }
    public void PlayChargingAirSFX()
    {
        AudioManager.Instance.PlaySFX(chargingAirSFX, 0.2f);
    }
    public void PlayStopChargingAirSFX()
    {
        AudioManager.Instance.PlaySFX(stopChargingAirSFX, 0.2f);
    }
    public void PlayBalloonHitSFX()
    {
        AudioManager.Instance.PlaySFX(balloonHitSFX, 0.2f);
    }
    public void PlayBalloonPopSFX()
    {
        AudioManager.Instance.PlaySFX(balloonPopSFX);
    }
    public void StopCurrentSFX()
    {
        AudioManager.Instance.StopSFX();
    }
}
