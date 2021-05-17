using UnityEngine;

public class JsonData : MonoBehaviour
{
    private string path = "C:/Users/dylan/Ironica2/Assets/FichiersJson/data.json";
    public PlayerJson p1 = new PlayerJson();
    void Start()
    {
        p1.life = 500;
        p1.posX = 3.5f;
        p1.posY = 2.0f;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            p1.life -= 50;
            string contents = JsonUtility.ToJson(p1, true);
            System.IO.File.WriteAllText(path,contents);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            string contents = System.IO.File.ReadAllText(path);
            p1 = JsonUtility.FromJson<PlayerJson>(contents);
            Debug.Log(p1.life + "--" + p1.posX + "--" + p1.posY);
        }
    }
}
