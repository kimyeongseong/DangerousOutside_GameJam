using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChatDataManager
{
    private static TutorialChatDataManager ins;
    public static TutorialChatDataManager Ins
    {
        get
        {
            if (ins == null)
                ins = new TutorialChatDataManager();
            return ins;
        }
    }

    List<TutorialChatData> tutorialChatDataList = new List<TutorialChatData>();

    private TutorialChatDataManager()
    {
        DataSet();
    }

    void DataSet()
    {
        tutorialChatDataList = new List<TutorialChatData>()
        {
            // 튜토리얼 0
            new TutorialChatData("0_0",0,"정보","지금부터 빨강 타일을 없애는 방법을 알려줄께"),
            new TutorialChatData("0_0",1,"정보","빨강타일은 방역이를 소환해서 없앨수 있는데"),
            new TutorialChatData("0_0",2,"정보","방역이는 세금을 소비해서 소환할수 있어"),
            new TutorialChatData("0_0",3,"정보","그리고 세금은 일정 시간이 지나면 조금씩 얻을수 있어"),

            new TutorialChatData("0_1",0,"정보","소환하는 방법은 아래 방역이 아이콘을 드래그 해서 원하는곳에 놓으면 소환이 완료되"),
            new TutorialChatData("0_1",1,"정보","지금 세금이 충분하니 방역이를 소환해봐!"),

            new TutorialChatData("0_2",0,"정보","잘했어! 방역이는 일정 시간마다 이동하면서 빨강타일을 제거해."),
            new TutorialChatData("0_2",1,"정보","방역이 소환하는 방법은 익혔으니 빨강타일을 전부 제거해줘!"),
            new TutorialChatData("0_2",2,"정보","그럼 잘 부탁해!"),

            // 튜토리얼 1
            new TutorialChatData("1_0",0,"정보","방역이 사용법은 잘 익힌것 같네."),
            new TutorialChatData("1_0",1,"정보","이번엔 집에 대해 알아보자"),

            new TutorialChatData("1_1",0,"정보","집에는 시민들이 살고 각 집마다 구호물품이 있지"),
            new TutorialChatData("1_1",1,"정보","구호물품은 일정 시간마다 소비 되고 구호물품이 떨어지면 시민은 집 밖으로 나오게 되"),

            new TutorialChatData("1_2",0,"정보","집밖으로 나오게 되면 오염구역에 감염될 확률이 높아지니 구호물품이 안떨어지게 관리해주어야되"),
            new TutorialChatData("1_2",1,"정보","마침 구호물품이 떨어졌으니 집을 터치 해서 구호물품을 채워봐"),

            new TutorialChatData("1_3",0,"정보","잘 했어! 집을 터치하면 시청에서 구호물품을 일정량 채워주게 되"),
            new TutorialChatData("1_3",1,"정보","시청의 구호물품은 일정시간마다 채워지고 다 떨어지면 배달이 불가능해"),

            new TutorialChatData("1_4",0,"정보","그럼 앞으로는 구호물품이 안떨어지도록 잘 관리하면서 빨강타일을 없애줘!"),
            new TutorialChatData("1_4",1,"정보","잘 부탁해!"),

            // 튜토리얼 2
            new TutorialChatData("2_0",0,"정보","이제 방역이와 시청의 사용법에는 익숙해진것 같네."),
            new TutorialChatData("2_0",1,"정보","이번엔 세탁소에 대해 알아보자"),

            new TutorialChatData("2_1",0,"정보","세탁소는 빨강주민을 데리고 가서 하양주민으로 바꿔주는 일을 해"),
            new TutorialChatData("2_1",1,"정보","한번 세탁소를 터치 해봐"),

            new TutorialChatData("2_2",0,"정보","잘 했어! 세탁소 아이콘을 터치 하면 자동으로 빨강 시민을 데려와"),
            new TutorialChatData("2_2",1,"정보","빨강 시민은 돌아다니게 되면 오염구역이 늘어나게 되니 발견하면 바로 세탁소로 보내는것이 좋아"),
            new TutorialChatData("2_2",2,"정보","세탁소를 잘 이용하면 오염구역이 늘어나는것을 막을수 있어"),
            new TutorialChatData("2_2",3,"정보","그럼 아쉽지만 이것으로 기본적인 정보는 다 전해줬네!"),
            new TutorialChatData("2_2",4,"정보","앞으로 모르는것이 있으면 커뮤니티 카페를 찾아가봐~"),
            new TutorialChatData("2_2",5,"정보","그럼 앞으로도 잘 부탁해~"),
        };
    }

    public List<TutorialChatData> GetData(string mainId)
    {
        return tutorialChatDataList.FindAll(data => data.chatMainId == mainId);
    }
}

public class TutorialChatData
{
    public string chatMainId = "";
    public int id = 0;
    public string name = "";
    public string contens = "";

    public TutorialChatData(string chatMainId, int id, string name, string contens)
    {
        this.chatMainId = chatMainId;
        this.id = id;
        this.name = name;
        this.contens = contens;
    }
}