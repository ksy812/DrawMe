using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public enum COLOR : int
{
    BLACK = 0,
    RED = 1,
    GREEN = 2,
    BLUE = 3
};*/

public class Drawing : MonoBehaviour
{
    public Camera cam;
    [SerializeField]
    public Material defaultMaterial; //라인 렌더러의 Material -> 기본

    private List<LineRenderer> lines;
    private LineRenderer curLine; //지금 그리고있는 라인
    private int positionCount = 2; //처음 생성하는 라인 렌더러의 코너 점의 갯수를 지정. -> 여기서 코너 점이란 무엇인지???
    private Vector2 prevPos = Vector2.zero;

    //********************
    [HideInInspector]
    public Transform hitPage;
    bool d_enable = false;

    [SerializeField]
    private Color lineColor;

    private float lineThickness;

    void Start()
    {
        SetColor(0);
        lineThickness = 0.1f;
        Debug.Log("생성");
    }

    void Update()
    {
        Draw();
    }

    void Draw()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        if (Input.GetMouseButtonDown(0))
        {
            //if(IsBoard() == true)
                CreateLine(mousePos);
        }
        else if (Input.GetMouseButton(0))
        {
            ConnectLine(mousePos);
        }
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    lines.Add(curLine);
        //    //EndLine(gameObject);
        //}
    }

    void CreateLine(Vector2 mousePos)
    {
        GameObject line = new GameObject("Line");
        LineRenderer lineRend = line.AddComponent<LineRenderer>();
        positionCount = 2;

        line.transform.parent = cam.transform;
        line.transform.position = mousePos;

        //lineRend.material = new Material(Shader.Find("UI/Defualt"));
        lineRend.material = new Material(defaultMaterial);//defaultMaterial;
        //lineRend.sharedMaterial.SetColor("_Color", lineColor);

        //color error
        //lineRend.startColor = lineColor;
        //lineRend.endColor = lineColor;
        //lineRend.SetColors(lineColor, lineColor);
        lineRend.material.color = lineColor;
        //lineRend.material.color = new Color(lineColor);

        lineRend.startWidth = lineThickness;
        lineRend.endWidth = lineThickness;
        lineRend.numCornerVertices = 5;

        lineRend.SetPosition(0, mousePos);
        lineRend.SetPosition(1, mousePos);

        curLine = lineRend;
    }

    void ConnectLine(Vector2 mousePos)
    {
        //움직임이 있다면
        if (prevPos != null && Mathf.Abs(Vector2.Distance(prevPos, mousePos)) >= 0.001f)
        {
            prevPos = mousePos;
            positionCount++;
            curLine.positionCount = positionCount;
            curLine.SetPosition(positionCount - 1, mousePos);
        }
    }

    public void SetColor(int color)
    {
        switch (color)
        {
            case 0://(int)COLOR.BLACK:
                Debug.Log("BLACK");
                lineColor = Color.black;
                break;
            case 1://(int)COLOR.RED:
                Debug.Log("RED");
                lineColor = Color.red;
                break;
            case 2://(int)COLOR.GREEN:
                Debug.Log("GREEN");
                lineColor = Color.green;
                break;
            case 3://(int)COLOR.BLUE:
                Debug.Log("BLUE");
                lineColor = Color.blue;
                break;
        }
    }

    bool IsBoard()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if (hitPage != hit.transform)
            {
                hitPage = hit.transform;
            }
            return true;
        }
        return false;
    }

    public void SetClear()
    {

    }
/*    Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return ray.origin + ray.direction * 10;
        
    }*/

    //hitPage, drawDistance, pageMast

    // 클릭한 오브젝트가 보드인지 확인
/*
    bool CheckClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, pageMask))
        {
            return true;
        }

        return false;
    }
*/
}