using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skyscraper : MonoBehaviour
{
    public float minWaitTime;
    public float maxWaitTime;
    public int fractionToSwitch;
    public float switchTime;

    public List<GameObject> Windows = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TurnWindowsOffOnCoroutine());
    }


    IEnumerator TurnWindowsOffOnCoroutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minWaitTime, maxWaitTime);

            int nrOfWindowsToSwitch = Random.Range(0, Windows.Count / fractionToSwitch);

            for (int i = 0; i <= nrOfWindowsToSwitch; i++)
            {
                int windowToSwitch = Random.Range(0, Windows.Count);
                Windows[windowToSwitch].SetActive(!Windows[windowToSwitch].activeSelf);
                yield return new WaitForSeconds(switchTime);
            }

            yield return new WaitForSeconds(waitTime);
        }
    }





}
