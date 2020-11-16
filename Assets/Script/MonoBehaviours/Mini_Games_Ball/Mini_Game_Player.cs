using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Game_Player : MonoBehaviour
{
    public int move_method;
    public GameObject MiniGames_Panel, miniPlayer;
    public float speed;
    public Vector2 speed_vec;
    Vector3 miniPlayerPos;

    public InteractBtn interactBtn;
    public Directory directory;
    public TaskBar taskBar;
    public GameObject joystick;

    GameObject coin;

    // Start is called before the first frame update
    void Start()
    {
        //게임 시작 했을때 미니게임 플레이어의 좌표 초기화
        miniPlayerPos = miniPlayer.gameObject.transform.position;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌한 게임 오브젝트의 태그이름이 "Item"인 경우
        if (collision.CompareTag("Item"))
        {
            //미니게임 패널 비활성화
            MiniGames_Panel.SetActive(false);

            coin = interactBtn.nearbyObject;

            InteractObject paramObject = coin.gameObject.GetComponent<Interaction>().interactObject;
            ClearMiniGame(paramObject);

            //미니게임 플레이어의 좌표 초기화
            miniPlayer.transform.position = miniPlayerPos;
        }
        //충돌한 게임 오브젝트의 태그이름이 "Enemy"인 경우
        else if (collision.CompareTag("Enemy"))
        {
            //미니게임 플레이어의 좌표 초기화
            miniPlayer.transform.position = miniPlayerPos;
        }
    }

    // Update is called once per frame
    void Update()//방향키를 누르는 대로 움직인다.
    {
        if (move_method == 0)
        {
            speed_vec = Vector2.zero;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                speed_vec.x += speed;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                speed_vec.x -= speed;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                speed_vec.y += speed;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                speed_vec.y -= speed;
            }
            transform.Translate(speed_vec);
        }

        else if (move_method == 1) //관성의 법칙을 적용하기 떄문에 움직일 때 부드러움이 추가된다.
        {
            speed_vec.x = Input.GetAxis("Horizontal") * speed;
            speed_vec.y = Input.GetAxis("Vertical") * speed;

            transform.Translate(speed_vec);
        }

        else if (move_method == 2)
        {
            speed_vec = Vector2.zero;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                speed_vec.x += speed;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                speed_vec.x -= speed;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                speed_vec.y += speed;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                speed_vec.y -= speed;
            }

            GetComponent<Rigidbody2D>().velocity = speed_vec;
        }
    }

    void ClearMiniGame(InteractObject taskObject)
    {
        if (directory.ModifyQuest(taskObject))
        {
            //인벤토리 내 해당 객체를 추가
            taskBar.AdjustTaskPoints(taskObject.taskPoints);
        }

        joystick.SetActive(true);
    }
}