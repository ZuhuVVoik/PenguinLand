using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Penguin1Move : MonoBehaviour
{
    Animator anim; // переменная для ссылки на контроллер анимации
    CharacterController controller; // переменная для обращения к контроллеру
    float speedMove = 9f; // переменная для управления скоростью перемещения персонажа
    float sprintMove = 15f;
    float speedTurn = 40f;

    public AudioSource Audio;
    public AudioSource pVoice;

    bool isDancing = false;

    // Start is called before the first frame update
    void Start() 
    { 
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isDancing == false)
        {
            int h = (int)Input.GetAxis("Horizontal");
            int v = (int)Input.GetAxis("Vertical");
            Move(v);
            Turn(h);
            Animate(v);



            
        }
    }
    float jumpSpeed = 20f;
    float gravity = 15f;
    Vector3 moveDirection;

    

    void FixedUpdate()
    {
        
    }

    private void Move(int v)
    {

        if (controller.isGrounded)
        {
            AudioPlay(v);
        }
        else
        {
            AudioPlay(0);
        }


        Vector3 movement = new Vector3(0f, -1f, v);
        // вычисляем вектор направления движения (-1f для эффекта гравитации) 
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement = movement * sprintMove * Time.deltaTime;
        }
        else
        {
            movement = movement * speedMove * Time.deltaTime; // учитываем скорость и время
        }


        if (MainManager.inHouse == false)
        {
            if (controller.isGrounded)
            {
                

                moveDirection = new Vector3(0.0f, 1f, 0.0f);

                if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift))
                {
                    jumpSpeed = 30f;
                    gravity = 22f;

                    moveDirection.y = jumpSpeed;
                }
                if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift))
                {
                    jumpSpeed = 20f;
                    gravity = 15f;

                    moveDirection.y = jumpSpeed;
                }
            }

            moveDirection.y -= gravity * Time.deltaTime;

            controller.Move(moveDirection * Time.deltaTime);
        }

        controller.Move(transform.TransformDirection(movement));
        // применяем смещение к контроллеру для передвижения
    }
    private void Turn(int h)
    {
        float turn = h * speedTurn * Time.deltaTime; // выч-м величину поворота
        transform.Rotate(0f, turn, 0f); // поворачиваем на нужный угол
    }
    private void AudioPlay(int v)
    {
        if (v != 0 && !Audio.isPlaying) Audio.Play();
        else if (v == 0 && Audio.isPlaying) Audio.Stop();
    }

    private void Animate(float v)
    {
        v *= 10000000; // слетел размер, ссори
        bool walk;
        if (v != 0)
        {
            walk = true;
            anim.SetBool("walk", true); // переключаем анимацию

            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("run", true);

            }
            else
            {
                anim.SetBool("run", false);

            }
        }
        else
        {
            walk = false;
            anim.SetBool("walk", false); // переключаем анимацию
            anim.SetBool("run", false);
        }


        if(MainManager.inHouse == false)
        {
            if (Input.GetKey(KeyCode.Space) && walk)
            {
                anim.Play("Jump");
                pVoice.Play();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!walk)
            {
                anim.Play("Salsa Dancing");
                isDancing = true;
                StartCoroutine(DanceEnd());
            }
            if (walk && !Input.GetKey(KeyCode.LeftShift))
            {
                anim.Play("Mask");
            }
        }
    }

    IEnumerator DanceEnd()
    {
        yield return new WaitForSeconds(24);
        isDancing = false;
    }
}
