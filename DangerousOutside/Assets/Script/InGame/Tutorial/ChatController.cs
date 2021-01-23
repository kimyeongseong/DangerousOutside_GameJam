using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChatController : MonoBehaviour
{
    public List<TutorialChatData> tutorialChatDataList = new List<TutorialChatData>();

    [SerializeField] RectTransform chatPopup;

    //변경할 변수
    [SerializeField] float delay;
    [SerializeField] float Skip_delay;
    private int state;

    bool text_cut;
    bool text_full;
    [SerializeField] Text nameText;
    [SerializeField] Text contentText;
    [SerializeField] Image nextImage;

    public UnityAction chatEndEvent;

    public void DataSet(List<TutorialChatData> tutorialChatDataList)
    {
        state = 0;
        text_full = false;
        text_cut = false;
        this.tutorialChatDataList = tutorialChatDataList;
        
        ChatPopupActive(true);
        StartCoroutine(ShowText());
    }

    //다음버튼함수
    public void End_Typing()
    {
        //다음 텍스트 호출
        if (text_full == true)
        {
            text_full = false;
            text_cut = false;
            state++;

            if (state >= tutorialChatDataList.Count)
            {
                if (chatEndEvent != null)
                {
                    chatEndEvent();
                }
            }
            else
            {
                StartCoroutine(ShowText());
            }
            
        }
        //텍스트 타이핑 생략
        else
        {
            text_cut = true;
        }
    }


    IEnumerator ShowText()
    {
        TutorialChatData tutorialChatData = tutorialChatDataList[state];
        nextImage.gameObject.SetActive(false);
        nameText.text = tutorialChatData.name;
        ContentTextReset(string.Empty);
        
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < tutorialChatData.contens.Length; i++)
        {
            if (text_cut == transform)
            {
                break;
            }

            ContentTextReset(tutorialChatData.contens.Substring(0, i + 1));
            
            yield return new WaitForSeconds(delay);
        }

        ContentTextReset(tutorialChatData.contens);

        nextImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(Skip_delay);

        text_full = true;
    }

    void ContentTextReset(string str)
    {
        contentText.text = str;
    }

    public void ChatPopupActive(bool active)
    {
        chatPopup.gameObject.SetActive(active);
    }

}
