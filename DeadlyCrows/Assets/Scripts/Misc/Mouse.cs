using UnityEngine;

public class Mouse : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] SpriteRenderer cursor;
    [SerializeField] Vector3 offset;

    [Header("Properties")]
    [SerializeField] Sprite gameplayCursor;
    [SerializeField] Sprite menuCursor;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GetPosition();
    }

    public static Vector3 GetPosition()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.y = 0;
        return mousePosition;
    }
}
