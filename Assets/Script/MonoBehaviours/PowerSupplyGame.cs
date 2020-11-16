using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PowerSupplyGame : MonoBehaviour
{
    public Transform HandleTF;

    [SerializeField] private Vector3 FirstTouch;
    [SerializeField] private Vector3 LastTouch;

    public GameObject Handle;

    private RectTransform Energy;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    public const int numLights = 8;

    public GameObject[] lights = new GameObject[numLights];

    private Image[] lightImages = new Image[numLights];

    private int numRotations;

    //컬러 객체
    private Color off = new Color(69, 132, 173, 255);
    private Color on = new Color(255, 255, 0, 255);

    void Start()
    {
        numRotations = 0;

        Energy = this.gameObject.GetComponent<RectTransform>();
        minX = Energy.position.x;
        Debug.Log(minX);
        for (int i = 0; i < numLights; i++)
        {
            lightImages[i] = lights[i].gameObject.GetComponent<Image>();
            lightImages[i].color = off;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FirstTouch.x = Input.mousePosition.x;
            FirstTouch.y = Input.mousePosition.y;
        }

        if (Input.GetMouseButton(0))
        {
            LastTouch.x = Input.mousePosition.x;
            LastTouch.y = Input.mousePosition.y;
            
            float angle = Mathf.Atan2(LastTouch.x - FirstTouch.x, LastTouch.y - FirstTouch.y) * Mathf.Rad2Deg;
            Debug.Log("angle is " + angle);

            Handle.transform.rotation = Quaternion.Euler(Handle.transform.rotation.x, Handle.transform.rotation.y, -angle);

            if(angle + 180 == 0 || angle + 180 == 360)
            {
                numRotations++;
            }
        }

        Quaternion target = Quaternion.LookRotation(HandleTF.position - Handle.transform.position);
        Handle.transform.rotation = Quaternion.Slerp(Handle.transform.rotation, target, Time.deltaTime * 3f);

        if(numRotations > 0 && numRotations <= numLights)
        {
            for(int  i =0; i<numRotations; i++)
            {
                lightImages[i].color = on;
            }
        }
    }
}