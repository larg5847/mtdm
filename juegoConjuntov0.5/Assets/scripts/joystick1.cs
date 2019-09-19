using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class joystick1 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{

    public RectTransform baseJ;
    public RectTransform joystick;

    public float Horizontal = 0;
    public float Vertical = 0;


    public float separacion;
    Vector2 posicionPunto;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    void moverJugador(Vector2 direccion)
    {
        
    }



    /// <summary>
    /// interfaces de touch
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        posicionPunto = new Vector2((eventData.position.x - baseJ.position.x) / ((baseJ.rect.size.x - joystick.rect.size.x) / 2), (eventData.position.y - baseJ.position.y) / ((baseJ.rect.size.y - joystick.rect.size.y) / 2));

    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
