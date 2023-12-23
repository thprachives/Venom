using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Threading;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 1f;
    private int waypointIndex = 0;

    public bool canMove = true;
    private bool diceRolled = false;
    private int diceResult = 0;

    private int count;
    private int diamondscore;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI diamondText;

    public AudioSource audioSource;
    public AudioClip clip1;
    public AudioClip clip2;




    private Dictionary<int, int> snakes = new Dictionary<int, int>
    {
        { 5, 0 },  // If player lands on 2, move back to 0 (start)
        { 9, 8 }, // If player lands on 24, move back to 10
        { 13,  8}, // If player lands on 40, move back to 15
        { 21, 16 },  // If player lands on 51, move back to 39
        { 29, 16 },
        { 33, 32 },
        { 39, 32 },
        { 43, 40 },
        { 47, 40 },
        { 52, 48 },
        { 59, 56 },
        { 62, 56 },
        { 68, 64 },
        { 70, 64 },
        { 75, 72 },
        { 78, 72 },
        { 86, 80 },
        { 92, 88 },
        { 93, 88 },
        { 97, 96 },
    };

    private int lastQuestionIndex = -8; // Initialize last question index as -8
    private int questionInterval = 8;

    public static PlayerMovement Instance { get; private set; }

    private void Start()
    {
        InitializePlayerPosition();
        count = 0;
       
        SetCountText();
        diamondscore = 0;
        SetDiamondText();
        audioSource = GetComponent<AudioSource>();


    }

    public void PlayClip1()
    {
        audioSource.clip = clip1;
        audioSource.Play();
    }

    public void PlayClip2()
    {
        audioSource.clip = clip2;
        audioSource.Play();
    }

    private void InitializePlayerPosition()
    {
        if (waypoints != null && waypointIndex >= 0 && waypointIndex < waypoints.Length)
        {
            transform.position = waypoints[waypointIndex].transform.position;
        }
        else
        {
            Debug.LogError("Invalid waypoint setup or waypointIndex in PlayerMovement.Start()");
        }
    }

    private void Awake()
    {
        SingletonCheck();
    }

    private void SingletonCheck()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (canMove && diceRolled)
        {
            MovePlayer(diceResult);
        }
    }

    private void OnEnable()
    {
        Dice.OnDiceRollComplete += HandleDiceRollComplete;
    }

    private void OnDisable()
    {
        Dice.OnDiceRollComplete -= HandleDiceRollComplete;
    }

    private void HandleDiceRollComplete(int result)
    {
        diceResult = result;
        diceRolled = true;
    }

    private void MovePlayer(int steps)
    {
        if (waypointIndex >= 0 && waypointIndex < waypoints.Length && diceRolled)
        {
            int targetIndex = Mathf.Clamp(waypointIndex + steps, 0, waypoints.Length - 1);
            Vector3 targetPosition = waypoints[targetIndex].transform.position;
            float step = moveSpeed * Time.deltaTime;
            
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (transform.position == targetPosition)
            {
                waypointIndex = targetIndex;
                diceRolled = false; // Reset the diceRolled flag


                if (snakes.ContainsKey(waypointIndex))
                {
                    PlayClip1();

                    if (count<=0)
                    {
                        //Thread.Sleep(2000);
                        

                        int tailIndex = snakes[waypointIndex]; 
                        Vector3 tailPosition = waypoints[tailIndex].transform.position;
                        transform.position = tailPosition;
                        waypointIndex = tailIndex;
                    }
                    else
                    {
                        count--;
                        SetCountText();
                    }
                }
                
                

                Collider[] Potioncolliders = Physics.OverlapSphere(targetPosition, 0.1f);
                foreach (var collider in Potioncolliders)
                {
                    if (collider.gameObject.CompareTag("potion"))
                    {
                        PlayClip2();
                        collider.gameObject.SetActive(false);
                        count++;
                        SetCountText();
                    }
                }

                Collider[] Diamondcolliders = Physics.OverlapSphere(targetPosition, 0.1f);
                foreach (var collider in Diamondcolliders)
                {
                    if (collider.gameObject.CompareTag("diamonds"))
                    {
                        collider.gameObject.SetActive(false);
                        diamondscore++;
                        SetDiamondText();
                    }
                }


                if (waypointIndex >= questionInterval  && waypointIndex != lastQuestionIndex)
                {
                    QuizManager.Instance.ShowQuestionPanel();
                    lastQuestionIndex = waypointIndex;
                    questionInterval += 8;
                }
            }
        }
    }

    void SetCountText()
    {
        countText.text = count.ToString();
        
    }
    void SetDiamondText()
    {
        diamondText.text = diamondscore.ToString();

    }
}
