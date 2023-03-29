using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingObject : MonoBehaviour
{

    [SerializeField] float _healValue;
    private void OnTriggerEnter(Collider other)
    {

        IHealReceivable healReceivable = other.GetComponent<IHealReceivable>();
        if (healReceivable != null)
        {
            healReceivable.HealDamage(_healValue);
            gameObject.SetActive(false);
        }
    }
}
