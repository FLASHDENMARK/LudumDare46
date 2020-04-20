using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{

    public Transform CheckPosition;
    public Transform MouthPosition;
    public float SlerpSpeed;
    public LayerMask interactionMask;
    public float FoodCheckRadius;

    private bool _isEating;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(CheckPosition.position, FoodCheckRadius);
        Gizmos.DrawSphere(MouthPosition.position, 0.25f);
    }

    public void Eat(GameObject food)
    {
        Rigidbody RB = food.GetComponent<Rigidbody>();
        if (RB == null)
            RB = food.GetComponentInParent<Rigidbody>();
        Destroy(RB);
        Destroy(food.GetComponent<Collider>());
        StartCoroutine(EatAnim(food));


    }

    IEnumerator EatAnim(GameObject food)
    {

        while(Vector3.Distance(food.transform.position, MouthPosition.position) > 0.01f)
        {


            float distance = Vector3.Distance(food.transform.position, MouthPosition.position);
            float finalSpeed = (distance / SlerpSpeed);
            food.transform.position = Vector3.Slerp(food.transform.position, MouthPosition.position, Time.deltaTime / finalSpeed);
            yield return new WaitForEndOfFrame();
        }


        Edible edible = food.GetComponent<Edible>();
        if (edible != null && edible.IsPoisonous)
        {
            
            GetComponent<AIController>().Die(edible);
        }

        Destroy(food.gameObject);
        _isEating = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckForFood());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator CheckForFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (_isEating)
                continue;

            Collider[] foods = Physics.OverlapSphere(CheckPosition.position, FoodCheckRadius, interactionMask);
            if(foods.Length > 0)
            {
                foreach(Collider food in foods)
                {
                    Debug.Log(food.transform.name);
                    Debug.DrawLine(transform.position, food.transform.position, Color.red, 0.1f);

                    if (food.GetComponent<Edible>() != null || food.GetComponentInParent<Edible>() != null)
                    {
                        _isEating = true;
                        Eat(food.gameObject);
                        break;
                    }
                }

            }

       
        }
    }

}
