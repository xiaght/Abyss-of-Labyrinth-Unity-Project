using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    public RectTransform m_rectBack;
    public RectTransform m_rectJoystick;

    public float m_fRadius;
    public float m_fSpeed = 1.0f;
    public float m_fSqr = 0f;

    public Vector2 m_vecMove;

    Vector2 m_vecNormal;

    public bool m_bTouch = false;


    void Start()
    {
 
        // JoystickBackground�� �������Դϴ�.
        m_fRadius = m_rectBack.rect.width * 0.5f;
    }

    void Update()
    {

    }

    void OnTouch(Vector2 vecTouch)
    {
        Vector2 vec = new Vector2(vecTouch.x - m_rectBack.position.x, vecTouch.y - m_rectBack.position.y);


        // vec���� m_fRadius �̻��� ���� �ʵ��� �մϴ�.
        vec = Vector2.ClampMagnitude(vec, m_fRadius);
        m_rectJoystick.localPosition = vec;

        // ���̽�ƽ ���� ���̽�ƽ���� �Ÿ� ������ �̵��մϴ�.
        float fSqr = (m_rectBack.position - m_rectJoystick.position).sqrMagnitude / (m_fRadius * m_fRadius);

        // ��ġ��ġ ����ȭ
        Vector2 vecNormal = vec.normalized;

        m_vecMove = new Vector2(vecNormal.x * m_fSpeed * Time.deltaTime * fSqr, vecNormal.y * m_fSpeed * Time.deltaTime * fSqr);
     }

    public void OnDrag(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        m_bTouch = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        m_bTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // ���� ��ġ�� �ǵ����ϴ�.
        m_rectJoystick.localPosition = Vector2.zero;
        m_vecMove = Vector2.zero;
        m_bTouch = false;
    }


}
