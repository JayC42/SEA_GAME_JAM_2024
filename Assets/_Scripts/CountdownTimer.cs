using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public static bool IsCountdownFinished { get; private set; }
    public TextMeshProUGUI countdownText;
    public float countdownDuration = 1f;
    public SoundPlayer soundPlayer;

    private void Start()
    {
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        soundPlayer.PlayCountdown123SFX();
        IsCountdownFinished = false;
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(countdownDuration);
        }
        soundPlayer.PlayCountdownFinishSFX();
  
        yield return new WaitForSeconds(countdownDuration);
        IsCountdownFinished = true;
        gameObject.SetActive(false);
    }
}