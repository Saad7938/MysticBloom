using Food;
using System;
using UnityEngine;

public class Insect : MonoBehaviour
{
    [SerializeField]public float speed = 0.2f;
    [SerializeField] public float damagePerSecond = 5;
    [SerializeField] public int clicksToKill = 2;//a/dw/adw/
     private int clickCount = 0;

    private Transform targetPlant;
    private FoodBase plantScript;
    private Animator animator;

    private bool isAttacking = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        PickRandomPlant();
    }

    // Update is called once per frame
    void Update()
    {
        if(targetPlant==null||plantScript==null||!plantScript.IsRipe||plantScript.Health<=0)
        {
            isAttacking = false;
            PickRandomPlant();
            return;
        }
        if(!isAttacking & targetPlant !=null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPlant.position, speed * Time.deltaTime);

            if(Vector3.Distance(transform.position,targetPlant.position)<0.5f)
            {
                StartAttack();
            }
        }
        if(isAttacking && plantScript!=null)
        {
            plantScript.TakeDamage(damagePerSecond * Time.deltaTime);

            if(plantScript.Health<=0)
            {
                isAttacking = false;
                PickRandomPlant();
            }
        }
    }

    private void StartAttack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
    }

    [Obsolete]
    private void PickRandomPlant()
    {
        FoodBase[] allPlants = FindObjectsOfType<FoodBase>();
        var ripePlants = System.Array.FindAll(allPlants, p => p.IsRipe && p.Health > 0);

        if(ripePlants.Length>0)
        {
            targetPlant = ripePlants[UnityEngine.Random.Range(0, ripePlants.Length)].transform;
            plantScript = targetPlant.GetComponent<FoodBase>();
            animator.SetTrigger("Walk");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /*private void OnMouseDown()
    {
        clickCount++;
        if(clickCount>=clicksToKill)
        {
            animator.SetTrigger("Die");
            Destroy(gameObject, 1f);
        }
    }*/

    public void HandleClick()
    {
        clickCount++;
        animator.SetTrigger("GetHit");
        if(clickCount>=clicksToKill)
        {
            animator.SetTrigger("Die");
            Destroy(gameObject, 1f);
        }
    }
}
