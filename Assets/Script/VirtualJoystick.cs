using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 키보드, 마우스, 터치를 이벤트로 오브젝트에 보낼 수 있는 기능을 지원

public class VirtualJoystick : MonoBehaviour,  IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] // 인스텍트 뷰에 넣어주게 위해
    private RectTransform lever; // 레버 
    private RectTransform rectTransform; // 조이스틱 배경

    [SerializeField, Range(10, 150)]
    private float leverRange; // 레버 범위

    private Vector2 inputDirection;
    private bool isInput;

    [SerializeField]
    private PlayerBall playerBall;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // 오브젝트를 클릭해서 드래그 하는 도중에 들어오는 이벤트
    // 하지만 클릭을 유지한 생태로 마우스를 멈추면 이벤트가 들어오지 않음
    public void OnBeginDrag(PointerEventData eventData) // 드래그를 시작할 때 
    {
        ControlJoystickLever(eventData);
        isInput = true;
    }

    public void OnDrag(PointerEventData eventData) // 드래그 중일 때
    {
        ControlJoystickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData) // 드래그를 끝냈을 때
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
        playerBall.Move(Vector2.zero);
    }

    private void ControlJoystickLever(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTransform.anchoredPosition; // 빼주면 레버 있어야할 위치 알게됨
        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange;
    }

    private void InputControlVector()
    {
        // 캐릭터에게 입력벡터를 전달
        playerBall.Move(inputDirection);
       //Debug.Log(inputDirection.x + " / " + inputDirection.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()   
    {
        if(isInput)
        {
            InputControlVector();
        }
    }
}
