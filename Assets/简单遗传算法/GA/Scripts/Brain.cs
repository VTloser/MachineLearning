using UnityEngine;

public class Brain : MonoBehaviour
{
    public DNA dna;
    public Transform Eye;
    (bool left, bool forward, bool right) SeeWall;
    public float effsFound = 0; //通过找到的彩蛋 计算得分
    //public float ZScore;
    public LayerMask ignor;
    bool CanMove = false;

    public void Init()
    {
        dna = new DNA();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("egg"))
        {
            effsFound++;
            other.gameObject.SetActive(false);
        }
    }

    RaycastHit hit;
    void Update()
    {
        //effsFound = transform.position.z;
        SeeWall = (false,false,false);
        CanMove = true;
        Debug.DrawRay(Eye.position, Eye.transform.forward, Color.red);
        if (Physics.SphereCast(Eye.position, 0.1f, Eye.forward, out hit, 1, ~ignor))
        {
            if (hit.collider.gameObject.CompareTag("wall"))
            {
                SeeWall.forward = true;
                CanMove = false;
            }
        }
        Debug.DrawRay(Eye.position, Eye.transform.right, Color.blue);
        if (Physics.SphereCast(Eye.position, 0.1f, Eye.right, out hit, 1, ~ignor))
        {
            if (hit.collider.gameObject.CompareTag("wall"))
            {
                SeeWall.right = true;
            }
        }
        Debug.DrawRay(Eye.position, -Eye.transform.right, Color.yellow);
        if (Physics.SphereCast(Eye.position, 0.1f, -Eye.right, out hit, 1, ~ignor))
        {
            if (hit.collider.gameObject.CompareTag("wall"))
            {
                SeeWall.left = true;
            }
        }
    }


    void FixedUpdate()
    {
        this.transform.Rotate(0, dna.gens[SeeWall], 0);
        if (CanMove)
            this.transform.Translate(0, 0, 0.1f);
    }
}

