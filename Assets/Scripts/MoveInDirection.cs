using UnityEngine;

public class MoveInDirection : MonoBehaviour
{
    public float velocidad = 20;
    public float x = 0;
    public float y = 0;

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x + (Time.deltaTime * velocidad * x),this.transform.localPosition.y + (Time.deltaTime * velocidad * y),0);
    }
}
