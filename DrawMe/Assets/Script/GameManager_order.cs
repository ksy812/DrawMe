using UnityEngine;
using UnityEngine.UI;

public class GameManager_order : MonoBehaviour
{
    public GameObject[] customers;
<<<<<<< HEAD
    public GameObject orderbox_prefab;
    public Button okButton_prefab;
=======
    public Transform spawnPoint;
    private GameObject obj;
>>>>>>> 927c0af (commit reset to e68490d)

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
<<<<<<< HEAD
            //customer 랜덤뽑기 넣기 <<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            customer = Instantiate(customers[0], spawnPoint_customer, Quaternion.identity); //캐릭터 생성
            orderbox = Instantiate(orderbox_prefab, spawnPoint_orderbox, Quaternion.identity); //대화상자 생성
            okButton = Instantiate(okButton_prefab, spawnPoint_okbutton, Quaternion.identity, GameObject.Find("Canvas").transform); //수락버튼 생성

            orderbox.GetComponent<Orderbox>().setText(customer.GetComponent<Customer>().comment[0]); //대화상자 주문 텍스트
            
=======
            //customer 랜덤뽑기 넣기
            obj = Instantiate(customers[0], spawnPoint.position, spawnPoint.rotation);
            orderText.text = obj.GetComponent<Customer>().comment[0];
            textBox.transform.position = new Vector2(textBox.transform.position.x, textBox.transform.position.y + 1000);
            okButton.transform.position = new Vector2(okButton.transform.position.x, okButton.transform.position.y + 1000);
>>>>>>> 927c0af (commit reset to e68490d)

        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) //아래쪽 화살표 말고 만족도가 -1이 아니면 리액션 후 객체 삭제
        {
            //프리맵명대로 삭제 방법 고안
            GameObject.Find("고객(Clone)").GetComponent<Customer>().Reaction(10);

            GameObject.Find("대화상자(Clone)").GetComponent<Orderbox>().setText(GameObject.Find("고객(Clone)").GetComponent<Customer>().getNowComment());
            Invoke("DestroyOderbox", 1.1f);
            
        }
    }

    void DestroyOderbox()
    {
        Destroy(GameObject.Find("대화상자(Clone)"));
        
    }


}