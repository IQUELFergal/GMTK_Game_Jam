using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Door : MonoBehaviour, IInteractable
{
    public SpriteRenderer interruptorSr;
    SpriteRenderer sr;
    public Color baseColor = new Color(1, 1, 1, 0.2f);
    public Color waitingColor = Color.yellow;
    public Color activatedColor = Color.green;

    public Vector2 translation = Vector3.up;
    [Min(0.001f)] public float duration = 0.001f;

    bool opened = false;
    bool isMoving = false;
    Vector2 basePosition;
    public Transform door;

    BoxCollider2D boxCol;
    CircleCollider2D circleCol;


    // Start is called before the first frame update
    void Start()
    {
        basePosition = door.position;
        interruptorSr.color = baseColor;
        sr = GetComponent<SpriteRenderer>();
        boxCol = GetComponent<BoxCollider2D>();
        circleCol = GetComponent<CircleCollider2D>();
    }

    public void Interact()
    {
        if (!isMoving) StartCoroutine(MoveDoor());
    }

    IEnumerator MoveDoor()
    {
        isMoving = true;
        interruptorSr.color = activatedColor;
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            door.position = opened ? Vector2.Lerp(basePosition + translation, basePosition, timer / duration) : Vector2.Lerp(basePosition, basePosition + translation, timer / duration);

            yield return null;
        }
        isMoving = false;
        opened = !opened;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<ColliderInteractor>() != null)
        {
            interruptorSr.color = waitingColor;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<ColliderInteractor>() != null)
        {
            interruptorSr.color = baseColor;
        }

    }

}
