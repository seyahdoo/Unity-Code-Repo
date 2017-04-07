using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace seyahdoo.triggertrigger
{


    public class TriggerTrigger : MonoBehaviour
    {

        public List<string> invokeTags;

        public UnityEvent TriggerEnter;
        public UnityEvent TriggerStay;
        public UnityEvent TriggerExit;


        void OnTriggerEnter(Collider other)
        {
            if (invokeTags.Contains(other.tag))
                TriggerEnter.Invoke();
        }
        void OnTriggerStay(Collider other)
        {
            if (invokeTags.Contains(other.tag))
                TriggerStay.Invoke();
        }
        void OnTriggerExit(Collider other)
        {
            if (invokeTags.Contains(other.tag))
                TriggerExit.Invoke();
        }



    }

}
