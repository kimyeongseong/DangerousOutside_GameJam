using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField]
    float EmoticonTime = 1f;

    [SerializeField]
    Image EmoticonImg;

    [SerializeField]
    GameObject RedEffect;

    [SerializeField]
    Animation anim;

    public void ShowSpeechBubble(EmoticonType emoticon, bool isRed)
    {
        ChangeEmoticon(emoticon);
        RedEffect.SetActive(isRed);

        anim.Play();
    }

    public void ChangeEmoticon(EmoticonType emoticon)
    {
        EmoticonImg.sprite = emoticon.Icon();
    }

    public void HideSpeechBubble()
    {
        if (anim.isPlaying)
            anim.Stop();
    }
}