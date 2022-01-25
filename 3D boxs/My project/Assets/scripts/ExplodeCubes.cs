
using UnityEngine;

public class ExplodeCubes : MonoBehaviour
{
    private bool _collisionSet;
    public GameObject RestartButton;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cube" && !_collisionSet)
        {
            for (int i = collision.transform.childCount - 1; i >= 0; i--)
            {
                Transform child = collision.transform.GetChild(i);
                child.gameObject.AddComponent<Rigidbody>();
                child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(70f, Vector3.up, 5f);
                child.SetParent(null);
            }
            RestartButton.SetActive(true);
          //  Camera.main.transform.position += new Vector3(0, 3f, 0);
            Destroy(collision.gameObject);
            _collisionSet = true;
        }
    }
}
