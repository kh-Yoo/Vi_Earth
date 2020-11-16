using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Map_Script : MonoBehaviour
{
    public GameObject miniMap;
    public GameObject player;
    public GameObject miniPlayer;
    Vector3 pos = new Vector3(0,-10,0);

    void Start()
    {
        //미니맵 비활성화
        miniMap.SetActive(false);
        //기존 플레이어의 좌표값을 저장합니다.
        //pos = player.transform.position;
        //해당 미니맵 내 플레이어의 위치를 표현해 주는 미니플레이어의 좌표를 기존 플레이어의 위치와 동일하게 설정합니다.
        miniPlayer.transform.position = pos;
    }

    public void Map_Button_OnMouseDown()
    {
        //미니맵이 비활성화되어있습니다.
        if (miniMap.activeSelf == false)
        {
            //미니맵을 활성화합니다.
            miniMap.SetActive(true);
        }
        //미니맵이 활성화되어있습니다
        else if (miniMap.activeSelf == true)
        {
            //미니맵을 비활성화합니다.
            miniMap.SetActive(false);
        }
    }
}
