using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSpawn : MonoBehaviour
{
    public int maxnum = 5;
    private int currentnum = 0;
    public float time = 3;
    private float timer = 0;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentnum < maxnum)
        {
            timer += Time.deltaTime;
            if (timer > time)
            {
                Vector3 pos = transform.position;
                pos.x += Random.Range(-5, 5);
                pos.z += Random.Range(-5, 5);
                GameObject go = Instantiate(prefab, transform.position, Quaternion.identity);
                go.GetComponent<WolfBaby>().spawn = this;
                timer = 0;
                currentnum++;
            }
        }
    }

    public void MinusNumber()
    {
        currentnum--;
    }
}
