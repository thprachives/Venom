
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuizManager : MonoBehaviour
{
    public List<QuesandAns> QnA;
    public GameObject[] options;
    public int currentQuestion;
    public GameObject Quizpanel;
    
    public Text QuestionTxt;
    public Text ScoreTxt;

    private int totalQuestions;
    private int score;

    //private int questionsShownCount = 0;

    private void Start()
    {
        totalQuestions = QnA.Count;
        //GoPanel.SetActive(false);
        generateQuestion();
    }
    public static QuizManager Instance { get; private set; }

    


    private void Awake()
    {
        // Ensure there is only one instance of the QuizManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // Destroy duplicate instances
            Destroy(gameObject);
        }
    }
    public void ShowQuestionPanel()
    {
        // Disable player movement while the question is displayed
        PlayerMovement.Instance.canMove = false;

        // Show your question panel UI and populate it with a question from your list
        Quizpanel.SetActive(true);
        generateQuestion();
    }

    public void GameOver()
    {
        Quizpanel.SetActive(false);
        //GoPanel.SetActive(true);
        PlayerMovement.Instance.canMove = true;
        //ScoreTxt.text = score + "/" + totalQuestions;
    }

    public void correct()
    {
        score++;
        QnA.RemoveAt(currentQuestion);
        //questionsShownCount = 0;
        GameOver();
    }

    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        //questionsShownCount = 0;
        GameOver();
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if (QnA.Count > 0 )
        {
            currentQuestion = Random.Range(0, QnA.Count);
            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswers();
            //questionsShownCount++; // Increment the count to limit to one question per movement
        }
        else
        {
            
            Quizpanel.SetActive(false);
            PlayerMovement.Instance.canMove = false;
            //GoPanel.SetActive(true);
            //ScoreTxt.text = score + "/" + totalQuestions;
        }
    }

    public void ExitQuestion()
    {
        if (QnA.Count > 0)
        {
            QnA.RemoveAt(currentQuestion);
        }

    }
}
