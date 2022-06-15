using UnityEngine;
using UnityEngine.UI;

public class GameManager_order : MonoBehaviour
{
    public GameObject[] customers;
    public GameObject orderbox_prefab;
    public Button okButton_prefab;

    public Vector2 spawnPoint_customer;
    public Vector2 spawnPoint_orderbox;
    public Vector2 spawnPoint_okbutton;

    public static GameObject customer; //고객 객체
    public static GameObject orderbox; //대화 상자

    public Text moneyText;
    public static int money = 0;

    private void Update()
    {

        if (customer==null && orderbox==null) // 게임 시작 | 앞 고객 완료 되면 객체 생성
        {           
            int random = Random.Range(0, customers.Length); //customer 랜덤뽑기
            customer = Instantiate(customers[random], spawnPoint_customer, Quaternion.identity); //캐릭터 생성
            orderbox = Instantiate(orderbox_prefab, spawnPoint_orderbox, Quaternion.identity); //대화상자 생성
            Instantiate(okButton_prefab, spawnPoint_okbutton, Quaternion.identity, GameObject.Find("Canvas").transform); //수락버튼 생성
            Customer.object_is_destory = false;
            orderbox.GetComponent<Orderbox>().setText(customer.GetComponent<Customer>().comment[0]); //대화상자 주문 텍스트

        }
        else if (orderbox != null && Customer.satisfaction != -1) //-1이 아니면 리액션 후 객체 삭제
        {

            customer.GetComponent<Customer>().Reaction(Customer.satisfaction);
            orderbox.GetComponent<Orderbox>().setText(customer.GetComponent<Customer>().getNowComment());
            Customer.satisfaction = -1;
            Invoke("DisappearOrderbox", 1.5f);
            Destroy(orderbox, 2f);
            
        }        
    }

    private void Awake()
    {
        Time.watch.Start();
    }
    private void DisappearOrderbox()
    {
        orderbox.SetActive(false);
        Customer.object_is_destory = true;
    }

}