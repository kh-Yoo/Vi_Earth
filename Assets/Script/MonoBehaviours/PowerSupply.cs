using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PowerSupply : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public GameObject energy;
    public GameObject handle;

    public const int numLights = 8;

    public GameObject[] lights = new GameObject[numLights];

    private Vector3 inputVector;

    private Image energyImg;

    private Transform handleAngle;

    private RectTransform handle_Rect;

    private SpriteRenderer[] lightImages = new SpriteRenderer[numLights];

    //컬러 객체
    private Color off = new Color(69, 132, 173, 255);
    private Color on = new Color(255, 255, 0, 255);

    void Start()
    {
        energyImg = energy.GetComponent<Image>();

        handleAngle = handle.GetComponent<Transform>();
        handleAngle.localRotation = Quaternion.Euler(0, 0, 0);

        handle_Rect = handle.GetComponent<RectTransform>();
        handle_Rect.Rotate(new Vector3(10f, 20f, 30f) * Time.deltaTime);

        for (int i=0;i<numLights;i++)
        {
            lightImages[i] = lights[i].gameObject.GetComponent<SpriteRenderer>();
            lightImages[i].color = off;
        }
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(energyImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / energyImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / energyImg.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x * 2 + 1, pos.y * 2 - 1, 0);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            handleAngle.Rotate(new Vector3(0, 0, 500f) * Time.deltaTime);
        }
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        handleAngle.Rotate(new Vector3(0, 0, -500f) * Time.deltaTime);
    }
}
