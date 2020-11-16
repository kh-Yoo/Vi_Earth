using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectoryButton : MonoBehaviour
{
    //일과표 버튼과 수직 레이어 그룹
    public GameObject directoryButton, Directory_Background;

    //버튼 사용 유무
    bool isitClicked = false;
    
    //일과표 버튼 UI 변경 함수
    public void changeScale()
    {
        //버튼 크기
        RectTransform rectTran = directoryButton.GetComponent<RectTransform>();
        //버튼 위치
        Vector3 position = directoryButton.transform.localPosition;
        //버튼이 사용되지 않은 경우
        if (!isitClicked)
        {
            //크기(너비,높이) 변경
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 20);
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
            //위치(X,Y) 변경
            position.x = -375;
            position.y = 170;
            directoryButton.transform.localPosition = position;
            //수직 레이어 그룹 비활성화
            Directory_Background.SetActive(false);
            //버튼 사용됨 설정
            isitClicked = true;
        }
        //버튼이 사용된 경우
        else
        {
            //크기(너비,높이) 변경
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
            //위치(X,Y) 변경
            position.x = -285;
            position.y = 170;
            directoryButton.transform.localPosition = position;
            //수직 레이어 그룹 활성화
            Directory_Background.SetActive(true);
            //버튼 사용되지 않음 설정
            isitClicked = false;
        }
    }
}