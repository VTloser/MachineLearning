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
            this.transform.rotation = Quaternion.Euler(0, this.transform.eulerAngles.y + dna.gens[temp], 0);
            this.transform.Translate(0, 0, 50 * Time.deltaTime);
            
            lifeTime = this.transform.position.z;
            
            turn = 0;
            
            if (Physics.Raycast(left.transform.position, left.forward, out hit, wallLayer))
            {
                L = Vector3.Distance(hit.point, left.transform.position);
                turn += Vector3.Distance(hit.point, left.transform.position);
                Debug.DrawLine(left.transform.position, hit.point, Color.blue);
            }
            else
            {
                turn += float.MaxValue;
            }

            if (Physics.Raycast(right.transform.position, right.forward, out hit, wallLayer))
            {
                R = Vector3.Distance(hit.point, right.transform.position);
                turn -= Vector3.Distance(hit.point, right.transform.position);
                Debug.DrawLine(right.transform.position, hit.point, Color.red);
            }
            else
            {
                turn -= float.MaxValue;
            }
            // turn = 1f / (1 + Math.Pow(Math.E, -turn)); //0-1 之间
            // turn = turn * 2 - 1;
            // turn = Math.Round(turn, 1); //保留一位 -1 1之间
            // temp = (int)(turn * 10);
            // turn = temp;
            turn = Math.Clamp(turn, -10, 10);
            temp = (int)turn; //保留一位 -1 1之间
            
            look = dna.gens[temp];



        }



        public float look;
        private int temp = 0;

        public float L;
        public float R;
    }
}