using UnityEngine;

public class MovimientoPelota : MonoBehaviour
{
    [SerializeField]private float speed = 20.0f;
    // Update is called once per frame
    void Update()
    {
        if(this.transform.localPosition.x < -68.0f)
        {
            this.transform.localPosition = new Vector3(-68.0f, this.transform.localPosition.y, this.transform.localPosition.z);
            speed = -speed;
        }
        if(this.transform.localPosition.x > 68.0f)
        {
            this.transform.localPosition = new Vector3(68.0f, this.transform.localPosition.y, this.transform.localPosition.z);
            speed = -speed;
        }
        this.transform.localPosition = new Vector3(this.transform.localPosition.x + (speed * Time.deltaTime), this.transform.localPosition.y, this.transform.localPosition.z);
    }
}
