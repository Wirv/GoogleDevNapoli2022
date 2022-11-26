using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    public int hp = 5;
    private NavMeshAgent agent;
    private Animator anim;
    private GameObject target;

    public Text txt;

    public bool TakeTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<NavMeshAgent>()) agent = GetComponent<NavMeshAgent>();

        if (GetComponent<Animator>()) anim = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player");
        txt.text = hp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        txt.gameObject.transform.LookAt(Camera.main.transform.position);
        if(TakeTarget)
        {
            transform.LookAt(target.transform.position);
            agent.SetDestination(target.transform.position);
            if (Vector3.Distance(transform.position, target.transform.position) < 1.5f)
            {
                agent.speed = 0;
                anim.SetBool("Attack", true);
                anim.SetBool("Run", false);
            }
            else
            {
                agent.speed = 3.5f;
                anim.SetBool("Run", true);
                anim.SetBool("Attack", false);
            }
        }    

        if(Input.GetButtonDown("Fire2"))
        {
            TakeTarget = true;           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("weapon")) 
        { hp -= 1; 
            txt.text = hp.ToString(); } // hp = hp - 1;

        if (hp <= 0)
        {
            agent.enabled = false;
            anim.enabled = false;
            anim.Play("Dying");
            TakeTarget = false;
        }
    }
}
