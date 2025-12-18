using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
    public string Name;
    public int maxhp;
    public int hp;
    public int initiative;
    public float Xpos;
    public bool active;
    public bool endTurn;
    public int damage;
    public int magic;
    public int abilityNum;

    public GameObject placeSprite;

    public HPbar hpBar;
    public Camera cam;
    public static GameData CurrentGameData { get; private set; }
    public GDataMb gDataMb;

    public Animator animator;

    void Start()// НЕ ЮЗАТЬ
    {
        // НЕ ЮЗАТЬ
    }

    void Update() // НЕ ЮЗАТЬ
    {
        // НЕ ЮЗАТЬ
    }
    public void LoadStats()
    {
        gDataMb = FindAnyObjectByType<GDataMb>();
        CurrentGameData = SaveSystem.Load(gDataMb.loadingGame);
    }
    public void GetStats(string setname, int setmaxhp, int sethp, int setInitiative)
    {        
        Name = setname;
        maxhp = setmaxhp;
        hp = sethp;
        initiative = setInitiative;
    }
    //public void ImATarget()
    //{
    //    if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
    //        RaycastHit hit;

    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            GameObject clickedObject = hit.collider.gameObject;
    //            Debug.Log("Кликнули по: " + clickedObject.name);
    //            // Здесь у тебя ссылка: clickedObject
    //        }
    //    }
    //}
    public void CursorOnMe()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Ray ray = cam.ScreenPointToRay(mouseScreenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 worldPos = hit.point; 
            //Debug.Log("Курсор наведён на: " + worldPos);
        }
    }
    public void Action()//времееная заглушка для передачи хода
    {
        Debug.Log(Name + "    походил");
        EndTurn();
    }
    public void TakeDamage(int damage)
    {
        Debug.Log(Name + " получил " + damage + " урона");
        if ((hp - damage) > 0)
        {
            hp -= damage;
        }
        else Die();
    }
    public void Die()
    {
        Debug.Log(Name + " died");
    }
    public void SetAbility(int number)
    {
        abilityNum = number;
    }
    public void EndTurn()
    {
        endTurn = true;
        active = false;
        placeSprite.SetActive(false);
    }
    public void FindTarget(int position)//, Unit target1, Unit target2, Unit target3) // pos = 1/2/3/23/123
    {
        Debug.Log("_______________________________________");
        Unit[] units;
        int tg1 = 0;
        int tg2 = 0;
        int tg3 = 0;
        units = FindObjectsByType<Unit>(FindObjectsSortMode.None);
        for (int i = 0; i < units.Length; i++)
        {
            if (units[i].transform.position.x == -1) { tg1 = i; Debug.Log(units[i].name); }
            if (units[i].transform.position.x == -3) { tg2 = i; Debug.Log(units[i].name); }
            if (units[i].transform.position.x == -5) { tg3 = i; Debug.Log(units[i].name); }
            Debug.Log("Позиция " + units[i].Name + " = " + units[i].transform.position.x);
        }
        switch (position)
        {
            case 1:
                units[tg1].TakeDamage(damage);
                break;
        }
        //if (pos == 1) return;
    }
}
