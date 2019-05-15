using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    
    public GameObject Player;
    public GameObject[] Minions;

    [SerializeField] private GameObject healthbar;

    private bool _attack;
    public bool Controlling => _attack;
    
    void Awake ()
    {
        Instance = this;

        if (!Player)
        {
            Player = GameObject.FindWithTag("Player");
        }

        if (Minions.Length < 1)
        {
            Minions = GameObject.FindGameObjectsWithTag("Minion");
        }
    }

    public void UpdateHealthBar()
    {
        Image hbFiller = healthbar.GetComponent<Image>();
        hbFiller.fillAmount = Player.GetComponent<PlayerStats>().CurrentHealth / 100f;
    }

    public void MinionsBerserkMode()
    {        
        _attack = false;
        
        foreach (var minion in Minions)
        {
            MinionController mc = minion.GetComponent<MinionController>();
            mc.CurrentState = State.BERSERK;
        }
    }
    
    public void MinionsPlayerFollowing()
    {
        _attack = false;
        
        foreach (var minion in Minions)
        {
            MinionController mc = minion.GetComponent<MinionController>();
            mc.CurrentState = State.FOLLOWING;
        }
    }
    
    public void MinionsAttacking()
    {
        _attack = true;
    }

    private void Update()
    {
        if (_attack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    foreach (var minion in Minions)
                    {
                        MinionController mc = minion.GetComponent<MinionController>();
                        mc.CheckPoint = hit.point;
                        mc.CurrentState = State.ATTACKING;
                    }
                }

                StartCoroutine("DisableControlling");
            }
        }
    }

    private IEnumerator DisableControlling()
    {
        yield return new WaitForSeconds(1f);
        _attack = false;
    }
}
