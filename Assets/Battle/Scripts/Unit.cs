using UnityEngine;

public class Unit : MonoBehaviour
{
    public string Name;
    public int maxhp;
    public int hp;
    public int initiative;
    public float Xpos;
    public float Ypos;
    public bool active;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void GetStats(string setname, int setmaxhp, int sethp)
    {
        Name = setname;
        maxhp = setmaxhp;
        hp = sethp;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
