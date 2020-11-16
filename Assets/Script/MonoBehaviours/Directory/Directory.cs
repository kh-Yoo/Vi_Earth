using UnityEngine;
using UnityEngine.UI;

//플레이어의 미션(퀘스트) 목록을 관리하고 미션(퀘스트) 목록 UI를 담당하는 스크립트
public class Directory : MonoBehaviour
{
    //Quest 프리팹의 참조 저장
    public Quest questPrefab;

    Quest quest;

    //일과표 버튼 객체
    public Button directoryButton;

    //상호작용 오브젝트(코인/하트)
    public InteractObject coin;
    public InteractObject heart;

    //Quest의 갯수 지정
    public const int numQuests = 2;

    //플레이어가 오브젝트와 상호작용할 때, text속성에 아이템이 참조하는 text값을 설정함 
    Text[] locationTexts = new Text[numQuests];
    Text[] contentTexts = new Text[numQuests];
    Text[] maxTexts = new Text[numQuests];

    //InteractObject의 참조 저장 배열
    InteractObject[] interactObjects = new InteractObject[numQuests];

    //quests배열의 각 인덱스는 Quest프리팹을 가리킨다
    Quest[] quests = new Quest[numQuests];

    public void Start()
    {
        CreateQuests();
    }

    public void CreateQuests()
    {
        //Quest 프리팹을 사용하기 전에 유니티 에디터를 통해 설정했는지 확인
        if (questPrefab != null)
        {
            //Quest의 갯수만큼 반복문 실행
            for (int i = 0; i < numQuests; i++)
            {
                //Quest 프리팹의 복사본을 인스턴스화해서 quest에 대입한다
                quest = Instantiate(questPrefab);

                //인스턴스화한 오브젝트의 name 속성에 대입함
                quest.name = "Quest_#" + i;

                //DirectoryBackground오브젝트를 인스턴스화한 Quest의 부모 오브젝트로 설정함
                quest.transform.SetParent(directoryButton.transform.GetChild(1).transform);

                //새로운 Quest 오브젝트를 quests 배열의 현재 인덱스에 대입함
                quests[i] = quest;

                //Quest의 자식 오브젝트 중 인덱스 0,1,2에 해당하는 자식 오브젝트는 LocationTexts, ContentTexts, QuantityTexts이다
                //플레이어가 아이템과 상호작용할 때 텍스트 컴포넌트의 텍스트가 일과표에 나타난다
                locationTexts[i] = quest.transform.GetChild(0).GetComponent<Text>();
                contentTexts[i] = quest.transform.GetChild(1).GetComponent<Text>();
                maxTexts[i] = quest.transform.GetChild(2).GetComponent<Text>();
            }

            //미션 오브젝트 복사본을 인스턴스화해서 interactObjects 배열에 대입함
            interactObjects[0] = Instantiate(heart);
            interactObjects[1] = Instantiate(coin);

            //Quest의 갯수만큼 반복문 실행
            for (int i = 0; i < numQuests; i++)
            {
                //미션 오브젝트의 location,content,quantity속성을 LocationTexts, ContentTexts, QuantityTexts 배열의 텍스트 오브젝트에 대입함
                locationTexts[i].text = interactObjects[i].location;
                contentTexts[i].text = interactObjects[i].content;
                maxTexts[i].text = interactObjects[i].max.ToString() + "개";

                //위치,내용,최대수량 텍스트를 활성화
                locationTexts[i].enabled = true;
                contentTexts[i].enabled = true;
                maxTexts[i].enabled = true;
            }
        }
    }

    //미션이 수행되고 해당 미션오브젝트의 정보를 토대로 일과표를 잘 수정하였는지 나타내는 bool 값을 반환함
    public bool ModifyQuest(InteractObject additional)
    {
        //interactObjects 배열의 크기만큼 반복문 실행
        for (int i = 0; i < interactObjects.Length; i++)
        {
            //1) interactObjects 배열의 현재 인덱스에 미션(퀘스트)가 있는지
            //2) 일과표에 수정하려는 미션(퀘스트)의 objectType과 interactObjects에 있는 미션(퀘스트)의 objectType이 같은지 확인함
            //3) 일과표에 수정하려는 미션(퀘스트)의 location과 interactObjects에 있는 미션(퀘스트)의 location이 같은지 확인함
            //모든 조건이 맞으면 원래 있던 미션(퀘스트)의 갯수 및 UI를 변경함
            if (interactObjects[i] != null && interactObjects[i].objectType == additional.objectType && interactObjects[i].location == additional.location)
            {
                //일과표 내 해당 미션(퀘스트)의 남은 갯수를 1 줄인다
                interactObjects[i].max = interactObjects[i].max - 1;

                //Quest 프리팹을 인스턴스화하면 Quest 스크립트가 들어있는 게임 오브젝트가 만들어진다.
                //이 코드는 Quest 스크립트의 참조를 얻는 코드다 
                //Quest 스크립트에는 Text형식의 자식오브젝트인 taskLocationText, taskContentText, maxTaskText가 들어있다
                Quest questScript = quests[i].gameObject.GetComponent<Quest>();

                //Text오브젝트의 참조를 저장함
                Text locationText = questScript.locationText;
                Text contentText = questScript.contentText;
                Text maxText = questScript.maxText;

                //해당 미션(퀘스트)의 남은 갯수가 1개라도 남아있으면 텍스트를 노란색으로 설정
                if(interactObjects[i].max > 0)
                {
                    //Text오브젝트의 text속성 설정함
                    locationText.text = "<color=#ffff00>" + interactObjects[i].location + "</color>";
                    contentText.text = "<color=#ffff00>" + interactObjects[i].content + "</color>";
                    maxText.text = "<color=#ffff00>" + interactObjects[i].max.ToString() + "개" + "</color>";
                    return false;
                }
                //해당 미션(퀘스트)의 남은 갯수가 0이 되면 택스트를 초록색으로 설정
                else
                {
                    //Text오브젝트의 text속성 설정함
                    locationText.text = "<color=#00ff00>" + interactObjects[i].location + "</color>";
                    contentText.text = "<color=#00ff00>" + interactObjects[i].content + "</color>";
                    maxText.text = "<color=#00ff00>" + "완료" + "</color>";
                    return true;
                }
            }
        }
        return false;
    }
}