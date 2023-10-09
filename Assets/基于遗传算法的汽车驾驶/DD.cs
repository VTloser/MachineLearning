using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float value;
    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(0, this.transform.eulerAngles.y + value, 0);
    }
}
