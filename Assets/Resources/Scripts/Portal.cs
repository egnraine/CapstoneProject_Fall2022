using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Colliable
{
    public string[] sceneNames;
    public Player player;

    protected override void OnCollide(Collider2D coll)
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            if (coll.name == "Player")
            {
                // Teleport Player
                string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
