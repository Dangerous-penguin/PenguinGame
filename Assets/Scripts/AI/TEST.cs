using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin
{
    public class TEST : MonoBehaviour
    {
        [SerializeField] Transform temp;
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<NavMeshAgent>().SetDestination(temp.position);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
