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

        public float lifeTime;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            dna = new DNA();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                this.gameObject.SetActive(false);
            }
        }


        RaycastHit hit;
        private double turn = 0;

        private void Update()
        {
            lifeTime += Time.deltaTime;
                
            if (Physics.Raycast(left.transform.position, left.forward, out hit))
            {
                turn += Vector3.Distance(hit.point, left.transform.position);
                Debug.DrawLine(left.transform.position,  hit.point, Color.blue);
            }

            if (Physics.Raycast(right.transform.position, right.forward, out hit))
            {
                turn -= Vector3.Distance(hit.point, right.transform.position);
                Debug.DrawLine(right.transform.position, hit.point, Color.red);
            }

            turn = Math.Clamp(turn, -1, 1);
            turn = Math.Round(turn, 1); 
        }
        
        
        void FixedUpdate()
        {
            //this.transform.Rotate(0, dna.gens[(int)(turn * 10)], 0);
            //this.transform.Translate(0, 0, 0.1f);
        }
    }
}