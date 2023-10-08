using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    public class Car : MonoBehaviour
    {
        public DNA dna;

        public Transform left;
        public Transform right;

        public LayerMask wallLayer;
        
        public float lifeTime;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            dna = new DNA();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("wall"))
            {
                this.gameObject.SetActive(false);
            }
        }

        RaycastHit hit;
        
        public double turn = 0;

        private void Update()
        {
            lifeTime += this.transform.position.z;

            if (Physics.Raycast(left.transform.position, left.forward, out hit, wallLayer))
            {
                turn += Vector3.Distance(hit.point, left.transform.position);
                Debug.DrawLine(left.transform.position, hit.point, Color.blue);
            }

            if (Physics.Raycast(right.transform.position, right.forward, out hit, wallLayer))
            {
                turn -= Vector3.Distance(hit.point, right.transform.position);
                Debug.DrawLine(right.transform.position, hit.point, Color.red);
            }
            

            turn = Math.Clamp(turn, -10, 10);
            
            turn = Math.Round(turn, 0);//保留一位
        }


        void FixedUpdate()
        {
            this.transform.Rotate(0, dna.gens[(int)turn], 0);
            this.transform.Translate(0, 0, 0.1f);
        }
    }
}