using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir; //Вектор направления
    [SerializeField] private float speed; //Скорость игрока
    [SerializeField] private float jumpForce; //Сила прыжка
    [SerializeField] private float gravity; //Сила гравитации
    [SerializeField] private int coins;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Text coinsText;
    [SerializeField] private Sourse scoreScript;


    public int linetoMove = 1;     // 0 - Левая // 1 - Средняя // 2 - Правая       //Сами линии
    public float lineDistance = 5;  // Расстояние между линиями 
    private const float maxSpeed = 110;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        StartCoroutine(SpeedIncrease());
        Time.timeScale = 1;
        coins = PlayerPrefs.GetInt("coins");   //Загрузка данных о коинах в реестр
        coinsText.text = coins.ToString();  //Обновляем значение коинов на панели
    }

    void Update()
    {
        if (SwipeController.swipeRight)
        {
            if (linetoMove < 2)
            {
                linetoMove++;
            }
        }

        if (SwipeController.swipeLeft)
        {
            if(linetoMove > 0)
            {
                linetoMove--;
            }
        }

        if (SwipeController.swipeUp)
        {
            Jump();
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (linetoMove == 0) targetPosition += Vector3.left * lineDistance;
        else if(linetoMove == 2) targetPosition += Vector3.right * lineDistance;

        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }
    
    private void Jump()
    {
        if(controller.isGrounded)
            dir.y = jumpForce; 
    }

    void FixedUpdate()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "obstacle")
        {   
            losePanel.SetActive(true);
            int lastRunScore = int.Parse(scoreScript.scoreText.text.ToString());
            PlayerPrefs.SetInt("lastRunScore", lastRunScore);
            Time.timeScale = 0;
        }
    }
    

    //Метод для сбора коинов
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            coins++;
            PlayerPrefs.SetInt("coins", coins);        //Загружаем данные в реестр
            coinsText.text = coins.ToString();
            Destroy(other.gameObject);
        }
    }

    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(4);
        if(speed < maxSpeed)
        {
            speed += 3;
            StartCoroutine(SpeedIncrease());
        }

    }
}
