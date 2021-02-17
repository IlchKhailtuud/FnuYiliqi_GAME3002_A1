using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Text cubeText;

    [SerializeField]
    private Text countDownText;
    
    //set the cube count
    public int cubeCnt;
    private float currentTime;
    private float startingTime; 

    // Start is called before the first frame update
    void Start()
    {
        cubeCnt = 5;
        startingTime = 60.0f;
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        //change the remaining cube text
        cubeText.text = cubeCnt.ToString() + " / 5";
       
        //deduct current time by delta sceonds
        currentTime -= 1 * Time.deltaTime;

        //change the timerUI text
        countDownText.text = currentTime.ToString();

        //change the text color to red once it reaches 10
        if (currentTime <= 10.0f)
        {
            countDownText.color = Color.red;

            //stop counting down once the timer reaches 0
            if (currentTime <= 0)
            {
                currentTime = 0;
                //Load Lose Scene if the timer reaches 0 & there is more than 0 cube remaining in GameScene
                if (cubeCnt > 0)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("LoseScene", LoadSceneMode.Single);
                }
            }
        }

        //Load Win Scene if there is no cube remaining in GameScene
        if (cubeCnt == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
        }
    }
}
