using UnityEngine;
using UnityEngine.UI;

public class UISubtitles : MonoBehaviour
{
    public Text subtitle;
    public GameObject subtitleBackground;
    private Color _subtitleTextColor;
    private Color _subtitleBackgroundColor;
    public float fadeoutTimer = 1.0F;
    private float _subtitleTimer;
    private float _fadeoutTimer;

    private void Awake ()
    {
        _subtitleBackgroundColor = subtitleBackground.GetComponent<Image>().color;
        _subtitleTextColor = subtitle.GetComponent<Text>().color;
    }

    public void DisplaySubtitles (string speaker, string words, float duration = 10.0F)
    {
        subtitleBackground.GetComponent<Image>().color = _subtitleBackgroundColor;
        subtitle.GetComponent<Text>().color = _subtitleTextColor;

        subtitleBackground.SetActive(true);

        subtitle.text = $"{speaker} - {words}";

        _subtitleTimer = duration;
        _fadeoutTimer = fadeoutTimer;
    }

    void Update ()
    {
        _subtitleTimer -= Time.deltaTime;

        if (_subtitleTimer - _fadeoutTimer <= 0)
        {
            subtitleBackground.GetComponent<Image>().color = new Color(_subtitleBackgroundColor.r, _subtitleBackgroundColor.g, _subtitleBackgroundColor.b, _subtitleTimer);
            subtitle.GetComponent<Text>().color = new Color(_subtitleTextColor.r, _subtitleTextColor.g, _subtitleTextColor.b, _subtitleTimer);
        }
    }
}

