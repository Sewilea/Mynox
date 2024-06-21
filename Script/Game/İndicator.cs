using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class İndicator : MonoBehaviour
{
    public float fps, refreshtime;
    public Text fpstxt;
    void Start()
    {
        StartCoroutine(fpsindicator());
    }

    IEnumerator fpsindicator()
    {
        while (true)
        {
            fps = 1 / Time.deltaTime;

            yield return new WaitForSeconds(refreshtime);
        }
    }

    private void LateUpdate()
    {
        fpstxt.text = fps.ToString();
    }


}
