using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentHandler : MonoBehaviour
{
    Text experimentText;
    Image experimentImage;
    [SerializeField] GameObject dot;

    [SerializeField] int minNumberOfDots = 20;
    [SerializeField] int maxNumberOfDots = 30;

    [SerializeField] int numberOfTrials = 10;
    [SerializeField] float instructionTime = 0.5f;
    [SerializeField] float dotTime = 3.00f;
    [SerializeField] float restTime = 2.00f;

    bool experimentRunning = false;

    enum Stimulus
    {
        Relax = 0,
        Focus = 1
    }

    void Start()
    {
        experimentText = FindObjectOfType<Text>();
        experimentImage = FindObjectOfType<Image>();
        experimentImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (experimentRunning == false && Input.GetKeyDown(KeyCode.Space))
        {
            experimentRunning = true;
            StartCoroutine(runExperiment());
        }
    }

    IEnumerator runExperiment()
    {
        //Start the experiment after 3 seconds
        string experimentOutput = "";
        for (int i = 0; i < 3; i++)
        {
            experimentText.text = "The experiment will start in " + (3-i) + " seconds";
            yield return new WaitForSeconds(1f);
        }

        //Wait 1 more second before starting the experiment
        experimentImage.gameObject.SetActive(true);
        experimentText.text = "";
        yield return new WaitForSeconds(1f);

        //Foreach trial
        for (int i = 0; i < numberOfTrials; i++)
        {
            //Present user with stimulus 
            experimentImage.gameObject.SetActive(false);
            Stimulus stimulus = (Stimulus)Random.Range(0, 2);
            experimentText.text = stimulus.ToString();
            yield return new WaitForSeconds(instructionTime);

            //Generate the dots and present them to the user
            experimentText.text = "";
            int numberOfDots = Random.Range(minNumberOfDots, maxNumberOfDots + 1);
            List<GameObject> allDots = new List<GameObject>();
            for (int j = 0; j < numberOfDots; j++)
            {
                GameObject newDot = Instantiate(dot);
                allDots.Add(newDot);
            }
            yield return new WaitForSeconds(dotTime);

            //Measure the state of the user
            bool successfulInput = (Input.GetKey(KeyCode.UpArrow) && stimulus == Stimulus.Focus) ||
                (Input.GetKey(KeyCode.UpArrow)==false && stimulus == Stimulus.Relax);
            experimentOutput += " " + stimulus.ToString() + ":" + successfulInput.ToString() + " ";
 

            //Present the user with focus square and delete the dots
            for (int j = 0; j < numberOfDots; j++)
            {
                Destroy(allDots[j]);
            }
            allDots.Clear();
            experimentImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(restTime);
        }

        //The experiment has now finished
        experimentImage.gameObject.SetActive(false);
        experimentText.text = experimentOutput + "\n" + "Press <SPACE> to Start Experiment";
        experimentRunning = false;

    }
}
