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

    public HPbar hpBar;
    
    void Start()// ÍÅ ÞÇÀÒÜ
    {
        // ÍÅ ÞÇÀÒÜ
    }

    void Update() // ÍÅ ÞÇÀÒÜ
    {
        // ÍÅ ÞÇÀÒÜ
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
    //            Debug.Log("Êëèêíóëè ïî: " + clickedObject.name);
    //            // Çäåñü ó òåáÿ ññûëêà: clickedObject
    //        }
    //    }
    //}
    public void Action()
    {
        Debug.Log(Name + "    ïîõîäèë");
        //endTurn = true;
        //active = false;
    }
    public void TakeDamage(int damage)
    {
        Debug.Log(Name + " ïîëó÷èë " + damage + " óðîíà");
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
}
