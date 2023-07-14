using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

delegate List<GameObject> BreedMatching(List<GameObject> sortedList);

public class PopulationManager : MonoBehaviour
{

    public GameObject botPrefab;
    public GameObject[] startingPos;
    public int populationSize = 50;
    List<GameObject> population = new List<GameObject>();

    /// <summary>	时间计数	 </summary>
    public static float elapsed = 0;

    /// <summary>	训练时间	 </summary>
    public float trialTime = 10;

    /// <summary>	时间缩放	 </summary>
    public float timeScale = 2;

    /// <summary>	当前是第几代	 </summary>
    int generation = 1;

    public GenerateMaze maze;

    /// <summary>	生育变异几率	 </summary>
    public int breedMutate;

    BreedMatching breedMatching;

    GUIStyle guiStyle = new GUIStyle();

    void OnGUI()
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


    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            int starti = Random.Range(0, startingPos.Length);
            GameObject b = Instantiate(botPrefab, startingPos[starti].transform.position, this.transform.rotation);
            b.transform.Rotate(0, Mathf.Round(Random.Range(-90, 91) / 90) * 90, 0);
            b.GetComponent<Brain>().Init();
            population.Add(b);
        }
        Time.timeScale = timeScale;

        //breedMatching += Topbreed;
        breedMatching += Demobreed;
    }

    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        int starti = Random.Range(0, startingPos.Length);
        GameObject b = Instantiate(botPrefab, startingPos[starti].transform.position, this.transform.rotation);
        b.transform.Rotate(0, Mathf.Round(Random.Range(-90, 91) / 90) * 90, 0);
        b.GetComponent<Brain>().Init();
        if (Random.Range(0, 100) < breedMutate)
        {
            b.GetComponent<Brain>().dna.Combine(parent1.GetComponent<Brain>().dna, parent2.GetComponent<Brain>().dna);
        }
        return b;
    }

    void BreedNewPopulation()
    {
        List<GameObject> sortedList = population.OrderBy(_ => _.GetComponent<Brain>().effsFound).ToList();
        sortedList.Reverse();
        float sum = 0;
        float max = 0;
       // EditorApplication.isPaused = true;

        foreach (GameObject g in sortedList)
        {
            max = max > g.GetComponent<Brain>().effsFound ? max : g.GetComponent<Brain>().effsFound;
            sum += (g.GetComponent<Brain>().effsFound);
        }
        string eggsCollected = $"Generation:[{generation}] ||最好成绩：[{max}] 总共收集：[{sum}]";
        Debug.Log($"<color=red>[{eggsCollected}]</color>");

        population.Clear();

        population = breedMatching(sortedList);

        foreach (var item in sortedList)
        {
            Destroy(item);
        }
        generation++;
    }


    public List<GameObject> Topbreed(List<GameObject> sortedList)
    {
        List<GameObject> TopList = new List<GameObject>();
        for (int i = 0; i < sortedList.Count - 1; i += 2)
        {
            TopList.Add(Breed(sortedList[i], sortedList[i+1]));
            TopList.Add(Breed(sortedList[i+1], sortedList[i]));
        }
        return TopList;
    }
    public List<GameObject> Randombreed(List<GameObject> sortedList)
    {
        List<GameObject> RandomList = new List<GameObject>();
        for (int i = 0; i < sortedList.Count / 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                RandomList.Add(Breed(sortedList[i], sortedList[Random.Range(0, sortedList.Count)]));
                if (RandomList.Count == populationSize) return RandomList;
                RandomList.Add(Breed(sortedList[Random.Range(0, sortedList.Count)], sortedList[i]));
                if (RandomList.Count == populationSize) return RandomList;
            }
        }
        return RandomList;
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

    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > trialTime)
        {
            maze.Reset();
            BreedNewPopulation();
            elapsed = 0;
        }
    }
}
