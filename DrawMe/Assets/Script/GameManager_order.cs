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

    private GameObject customer; //고객 객체
    private GameObject orderbox; //대화 상자
    private Button okButton;     //수락 버튼



    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow)) //이제 위쪽 화살표 말고 게임 시작 | 앞 고객 완료 되면 객체 생성
        {
            //customer 랜덤뽑기 넣기 <<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            customer = Instantiate(customers[0], spawnPoint_customer, Quaternion.identity); //캐릭터 생성
            orderbox = Instantiate(orderbox_prefab, spawnPoint_orderbox, Quaternion.identity); //대화상자 생성
            okButton = Instantiate(okButton_prefab, spawnPoint_okbutton, Quaternion.identity, GameObject.Find("Canvas").transform); //수락버튼 생성

            orderbox.GetComponent<Orderbox>().setText(customer.GetComponent<Customer>().comment[0]); //대화상자 주문 텍스트
            

        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) //아래쪽 화살표 말고 만족도가 -1이 아니면 리액션 후 객체 삭제
        {
            customer.GetComponent<Customer>().Reaction(10);

            orderbox.GetComponent<Orderbox>().setText(customer.GetComponent<Customer>().getNowComment());
            Invoke("DestroyOderbox", 1.1f);
            
        }
    }

    void DestroyOderbox()
    {
        Destroy(orderbox);
        
    }


}