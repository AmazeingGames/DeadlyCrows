using UnityEngine;

public class Mouse : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] SpriteRenderer cursor;
    [SerializeField] Vector2 offset;

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
        transform.position = Position;
    }

    public static Vector2 Position => Camera.main.ScreenToWorldPoint(Input.mousePosition);
}
