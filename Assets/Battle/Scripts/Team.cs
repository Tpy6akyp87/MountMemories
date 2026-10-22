using UnityEngine;
using UnityEngine.InputSystem;

public class Team : MonoBehaviour
{
    public float teamSpeed;
    private Vector3 targetPosition;
    private bool isMoving = false;

    public TurnController turnController;

    public GameObject[] battleSlots = new GameObject[10];
    public Fighter fighter;
    public Rogue rogue;
    public RuneKeeper runeKeeper;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fighter.transform.position = battleSlots[4].transform.position;
        rogue.transform.position = battleSlots[2].transform.position;
        runeKeeper.transform.position = battleSlots[3].transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyEvent"))
        {
            Instantiate(Resources.Load<GameObject>("Enemy1"), battleSlots[5].transform.position, Quaternion.identity, this.transform);
            Instantiate(Resources.Load<GameObject>("Enemy2"), battleSlots[6].transform.position, Quaternion.identity, this.transform);
            Instantiate(Resources.Load<GameObject>("Enemy3"), battleSlots[7].transform.position, Quaternion.identity, this.transform);
            teamSpeed = 0;
        }
        turnController.InitiateQuerry();
    }
    // Update is called once per frame
    void Update()
    {
        if (float.IsNaN(targetPosition.x) || float.IsNaN(transform.position.x))
        {
            Debug.LogError("Позиция или цель стали NaN!");
            return;
        }

        // 2. Проверяем нажатие левой кнопки мыши или тапа через новый API
        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            // 3. Считываем позицию курсора по-новому
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

            mouseWorldPos.z = transform.position.z;

            // Проверка клика справа
            if (mouseWorldPos.x != transform.position.x)
            {
                targetPosition = mouseWorldPos;
                isMoving = true;
            }
        }

        if (isMoving)
        {
            if (float.IsNaN(targetPosition.x) || float.IsNaN(transform.position.x))
            {
                Debug.LogError("Позиция или цель стали NaN!");
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, transform.position.y, transform.position.z), teamSpeed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }
}
