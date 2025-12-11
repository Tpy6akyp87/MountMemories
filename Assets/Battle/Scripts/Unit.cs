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

    void Start()// Õ≈ ﬁ«¿“‹
    {
        // Õ≈ ﬁ«¿“‹
    }

    void Update() // Õ≈ ﬁ«¿“‹
    {
        // Õ≈ ﬁ«¿“‹
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
    //            Debug.Log(" ÎËÍÌÛÎË ÔÓ: " + clickedObject.name);
    //            // «‰ÂÒ¸ Û ÚÂ·ˇ ÒÒ˚ÎÍ‡: clickedObject
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
            Debug.Log(" ÛÒÓ Ì‡‚Â‰∏Ì Ì‡: " + worldPos);
        }
    }
    public void Action()
    {
        Debug.Log(Name + "    ÔÓıÓ‰ËÎ");
        endTurn = true;
        active = false;
        placeSprite.SetActive(false);
    }
    public void TakeDamage(int damage)
    {
        Debug.Log(Name + " ÔÓÎÛ˜ËÎ " + damage + " ÛÓÌ‡");
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
}
