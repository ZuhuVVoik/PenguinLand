using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandController : MonoBehaviour
{
    IcrecreamPlacer IcecreamPlacer;/*Ссылка на менеджер мороженного и очков*/

    Transform interactObject; // объект для взаимодействия
    public Transform inHand;

    public AudioSource audio;

    [SerializeField]
    IKAnimation playerIK; // ссылка на экземпляр скрипта IKAnimation

    /*Сылки для отображения подобранных вещей*/
    public GameObject backpack;
    public GameObject scarf;
    public GameObject hat;
    private void Start()
    {
        IcecreamPlacer = FindObjectOfType<IcrecreamPlacer>();

        if (!MainManager.HaveBackpack)
        {
            backpack.SetActive(false);
        }
        if (!MainManager.HaveHat)
        {
            hat.SetActive(false);
        }
        if (!MainManager.HaveScarf)
        {
            scarf.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) { ThroughItem(); }
    }
    private void OnTriggerEnter(Collider other) // рука попадает в триггер
    {
        if (other.CompareTag("item") || other.CompareTag("itemForTransfer"))
        // если у триггера один из этих тегов
        {
            interactObject = other.transform; // записываем объект для взаимодействия
            playerIK.StartInteraction(other.gameObject.transform.position); // сообщаем скрипту 
            //IKAnimation о начале взаимодействия для запуска IK - анимации
        }
    }
    private void FixedUpdate()
    {
        CheckDistance(); // проверка дистанции с объектом
    }
    void CheckDistance() // метод для проверки дистанции, чтобы была возможность прекратить взаимодействие с объектом при отдалении
    {
        if (interactObject != null && Vector3.Distance(transform.position, interactObject.position) > 30)
        // если происходит взаимодействие и дистанция стала больше 2-ух
        {
            interactObject = null; // обнуляем ссылку на объект
            playerIK.StopInteraction(); // прекращаем IK-анимацию
        }
    }

    void TakeItemInPocket(GameObject item)
    {
        audio.Play();
        playerIK.StopInteraction(); // прекращение IK-анимации
        Destroy(interactObject.gameObject); // удалить объект
        MainManager.Inventory.AddItem(interactObject.gameObject);
    }
    void TakeItemInHand(Transform item) // добавим метод для переноса объекта
    {
        MainManager.IsBananaPickedUp = true;
        MainManager.CurrentBanana = item.gameObject;
        inHand = item; // запоминаем объект для взаимодействия
        inHand.parent = transform; // делаем руку, родителем объекта
        inHand.localPosition = new Vector3(0, 0, 0); // устанавливаем положение
        inHand.localEulerAngles = new Vector3(0, 0, 0); // устанавливаем поворот
        playerIK.StopInteraction(); // останавливаем IK-анимацию
    }

    private void OnCollisionEnter(Collision collision) // при коллизии с коллайдером предмета 
    {
        if (collision.gameObject.CompareTag("item")) // только объекты с тегом item будем удалять
        {
            if (collision.gameObject.name == "Scarf")
            {
                TakeItemInPocket(collision.gameObject); // передаем в метод объект для удаления


                MainManager.HaveScarf = true;
                MainManager.Messenger.WriteMessage("Вы подобрали шарфик");
                scarf.SetActive(true);
                MainManager.Temperature.CalculateMultiplier("Scarf");
            }
            if (collision.gameObject.name == "Hat")
            {
                TakeItemInPocket(collision.gameObject); // передаем в метод объект для удаления


                MainManager.HaveHat = true;
                MainManager.Temperature.CalculateMultiplier("Hat");
                MainManager.Messenger.WriteMessage("Вы подобрали шапку");
                hat.SetActive(true);
            }
            if (collision.gameObject.name == "Backpack")
            {
                TakeItemInPocket(collision.gameObject); // передаем в метод объект для удаления

                MainManager.Messenger.WriteMessage("Вы подобрали рюкзак");
                MainManager.HaveBackpack = true;
                backpack.SetActive(true);
            }
            else
            {
                if (MainManager.HaveBackpack && !(collision.gameObject.name == "Backpack") && !(collision.gameObject.name == "Hat") && !(collision.gameObject.name == "Scarf"))
                {
                    MainManager.Messenger.WriteMessage("Вы подобрали мороженное");
                    MainManager.scores += 100;
                    IcecreamPlacer.text.text = MainManager.scores.ToString();
                    TakeItemInPocket(collision.gameObject); // передаем в метод объект для удаления
                }
            }
        }
        if (collision.gameObject.CompareTag("itemForTransfer") && !inHand)
        // если это объект для перемещения и в руке нет другого предмета
        {
            TakeItemInHand(collision.gameObject.transform);
        }

    }
    void ThroughItem()
    {
        if (inHand != null) // если персонаж держит объект
        {
            inHand.parent = null; // отвязываем объект      
            MainManager.IsBananaPickedUp = false;
            MainManager.CurrentBanana = null;
            StartCoroutine(ReadyToTake()); // запускаем корутину
        }
    }
    IEnumerator ReadyToTake()
    {
        yield return new WaitForSeconds(1f); // ждем одну секунду
        inHand = null; // обнуляем ссылку
    }

}
