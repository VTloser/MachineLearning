using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Car
{
    delegate List<GameObject> BreedMatching(List<GameObject> sortedList);


    public class PopulationManager : MonoBehaviour
    {
        public GameObject botPrefab;

        List<GameObject> population = new List<GameObject>();

        public int populationSize = 50;

        public Transform startingPos;

        /// <summary>	时间计数	 </summary>
        public static float elapsed = 0;

        /// <summary>	训练时间	 </summary>
        public float trialTime = 10;

        /// <summary>	时间缩放	 </summary>
        public float timeScale = 2;

        /// <summary>	当前是第几代	 </summary>
        int generation = 1;

        /// <summary>	生育变异几率	 </summary>
        public int breedMutate;

        BreedMatching breedMatching;

        GUIStyle guiStyle = new GUIStyle();

        void OnGUi()
        {
            guiStyle.fontSize = 25;
            guiStyle.normal.textColor = Color.white;
            GUI.BeginGroup(new Rect(10, 10, 250, 150));
            GUI.Box(new Rect(0, 0, 140, 140), "Stats", guiStyle);
            GUI.Label(new Rect(10, 25, 200, 30), "Gen: " + generation, guiStyle);
            GUI.Label(new Rect(10, 50, 200, 30), string.Format("Time: {0:0.00}", elapsed), guiStyle);
            GUI.Label(new Rect(10, 75, 200, 30), "Population: " + population.Count, guiStyle);
            GUI.EndGroup();
        }

        void Start()
        {
            for (int i = 0; i < populationSize; i++)
            {
                GameObject b = Instantiate(botPrefab, startingPos.position, this.transform.rotation);
                b.GetComponent<Car>().Init();
                population.Add(b);
            }

            Time.timeScale = timeScale;

            //breedMatching += Topbreed;
            breedMatching += Demobreed;
        }

        public List<GameObject> Demobreed(List<GameObject> sortedList)
        {
            List<GameObject> DemoList = new List<GameObject>();
            int bestParentCutoff = sortedList.Count / 4;
            for (int i = 0; i < bestParentCutoff - 1; i++)
            {
                for (int j = 1; j < bestParentCutoff; j++)
                {
                    DemoList.Add(Breed(sortedList[i], sortedList[j]));
                    if (DemoList.Count == populationSize) return DemoList;
                    DemoList.Add(Breed(sortedList[j], sortedList[i]));
                    if (DemoList.Count == populationSize) return DemoList;
                }
            }
            return DemoList;
        }

        GameObject Breed(GameObject parent1, GameObject parent2)
        {
            GameObject b = Instantiate(botPrefab, startingPos.position, this.transform.rotation);
            b.transform.Rotate(0, Mathf.Round(Random.Range(-90, 91) / 90) * 90, 0);
            b.GetComponent<Car>().Init();
            if (Random.Range(0, 100) < breedMutate)
            {
                b.GetComponent<Car>().dna
                    .Combine(parent1.GetComponent<Car>().dna, parent2.GetComponent<Car>().dna);
            }
            return b;
        }
        
        void Update()
        {
            elapsed += Time.deltaTime;
            if (elapsed > trialTime)
            {
                BreedNewPopulation();
                elapsed = 0;
            }
        }

        void BreedNewPopulation()
        {
            List<GameObject> sortedList = population.OrderBy(_ => _.GetComponent<Car>().lifeTime).ToList();
            sortedList.Reverse();
            float sum = 0;
            float max = 0;
            foreach (GameObject g in sortedList)
            {
                max = max > g.GetComponent<Car>().lifeTime ? max : g.GetComponent<Car>().lifeTime;
                sum += (g.GetComponent<Car>().lifeTime);
            }

            string eggsCollected = $"Generation:[{generation}] ||最长存货：[{max}] 总共存货：[{sum}]";
            Debug.Log($"<color=red>[{eggsCollected}]</color>");

            population.Clear();

            population = breedMatching(sortedList);

            foreach (var item in sortedList)
            {
                Destroy(item);
            }

            generation++;
        }
    }
}