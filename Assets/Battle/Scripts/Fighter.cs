using UnityEngine;
using UnityEngine.InputSystem;

public class Fighter : Unit
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    void Start()
    {
        GetStats("Boring",22,12, 6);
        hpBar.SetHP(hp, maxhp);
        active = false;
        endTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.SetHP(hp, maxhp);
        if (active)
        {
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Unit target = hit.collider.GetComponent<Unit>();
                    Debug.Log("Кликнули по: " + target.name);
                    MeeleeAttack(target);
                    // Здесь у тебя ссылка: clickedObject
                }
            }
            
            //Action();
        }
    }
    public void MeeleeAttack(Unit target)
    {
        target.TakeDamage(damage);
        endTurn = true;
        active = false;
    }
    public void ThrowAxe()
    {
        
    }

}
