using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractBtn : MonoBehaviour
{
    //플레이어 게임오브젝트
    public GameObject player;

    public GameObject street_lamp;

    //조이스틱 게임오브젝트
    public GameObject visual_joystick;

    public const int numCoins = 3;
    public const int numHearts = 3;

    public GameObject[] coins = new GameObject[numCoins];
    public GameObject[] hearts = new GameObject[numHearts];

    List<GameObject> missions = new List<GameObject>();

    double[] distances;
    double min;
    public GameObject nearbyObject;

    //상호작용 게임오브젝트 이미지 객체
    SpriteRenderer objImage;
    SpriteOutline objectBorder;

    //버튼 배경 게임오브젝트 이미지 객체
    Image btnImage;
    Button interactButton;

    //컬러 객체
    Color normal = new Color(255, 255, 255, 255);
    Color yellow = new Color(255, 255, 0, 255);
    Color transparent = new Color(255, 255, 255, 0.5f);

    // Start is called before the first frame update
    void Start()
    {
        //미니게임패널 비활성화
        street_lamp.SetActive(false);

        //미션오브젝트를 리스트에 합칩니다.
        for (int i=0;i<numCoins;i++) {
            missions.Add(coins[i]);
        }
        for (int i = 0; i < numHearts; i++) {
            missions.Add(hearts[i]);
        }


        btnImage = this.GetComponent<Image>();
        interactButton = this.GetComponent<Button>();
        ChangeButtonUI(false);
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어의 위치에 따른 미션오브젝트 리스트 내역 거리 비교 및 UI변경
        Vector3 posPlayer = player.gameObject.transform.position;
        btwPlayerAndObjects(posPlayer, missions);
    }

    public void ChangeButtonUI(bool active)
    {
        //활성화하기
        if(active)
        {
            btnImage.color = normal;
            interactButton.interactable = true;
        }
        //비활성화하기
        else
        {
            btnImage.color = transparent;
            interactButton.interactable = false;
        }
    }

    //플레이어와 상호작용 오브젝트 사이의 거리 계산하는 메소드
    public double calcDistance(Vector3 posPlayer, Vector3 posObject)
    {
        double btwX = (posPlayer.x - posObject.x) * (posPlayer.x - posObject.x);
        double btwY = (posPlayer.y - posObject.y) * (posPlayer.y - posObject.y);
        double distance = Math.Sqrt(btwX + btwY);
        return distance;
    }

    //상호작용 객체(배열)들과 플레이어 객체 사이의 거리에 따른 UI변경 메소드
    public void btwPlayerAndObjects(Vector3 posPlayer, List<GameObject> missions)
    {
        Vector3 posObject;
        min = 100.0;
        distances = new double[numCoins + numHearts];
        for (int i=0;i<missions.Count;i++)
        {
            posObject = missions[i].gameObject.transform.position;
            double distance = calcDistance(posPlayer, posObject);
            distances[i] = distance;

            if (distances[i] > 4.0)
            {
                //오브젝트 태두리 설정
                objectBorder = missions[i].GetComponent<SpriteOutline>();
                objectBorder.enabled = false;
                //오브젝트 컬러 설정
                objImage = missions[i].gameObject.GetComponent<SpriteRenderer>();
                objImage.color = normal;
            }
            else if (distances[i] <= 4.0 && distances[i] > 2.0)
            {
                //오브젝트 태두리 설정
                objectBorder = missions[i].GetComponent<SpriteOutline>();
                objectBorder.enabled = true;
                //오브젝트 컬러 설정
                objImage = missions[i].gameObject.GetComponent<SpriteRenderer>();
                objImage.color = normal;
            }
            else
            {
                //오브젝트 태두리 설정
                objectBorder = missions[i].GetComponent<SpriteOutline>();
                objectBorder.enabled = true;
                //오브젝트 컬러 설정
                objImage = missions[i].gameObject.GetComponent<SpriteRenderer>();
                objImage.color = yellow;
            }

            if (min > distances[i])
            {
                min = distances[i];
                nearbyObject = missions[i];
            }
            if (i == missions.Count - 1)
            {
                if (min <= 2.0)
                {
                    ChangeButtonUI(true);

                    InteractObject paramObject = nearbyObject.gameObject.GetComponent<Interaction>().interactObject;

                    if (interactButton.onClick != null)
                    {
                        interactButton.onClick.RemoveAllListeners();
                    }
                    interactButton.onClick.AddListener(() => InteractMissionObject(paramObject));
                }
                else
                {
                    ChangeButtonUI(false);
                }
            }
        }
    }

    //근처 미션이 가능한 오브젝트 있을 경우 사용이 가능하며
    //해당 미션오브젝트의 정보를 전달받아야합니다
    //해당 미션에 해당하는 미니게임을 나타내야합니다.
    public void InteractMissionObject(InteractObject obj)
    {
        if (obj.objectType == InteractObject.ObjectType.COIN)
        {
            street_lamp.SetActive(true);
            visual_joystick.SetActive(false);
            ChangeButtonUI(false);
        }
        else if (obj.objectType == InteractObject.ObjectType.HEART)
        {
            
        }
    }
}