using UnityEngine;

public class CameraShake : MonoBehaviour {

    public Transform camTranform;

    private float shakeDuration = 1f, shakeAmout = 0.04f, decreaseFactor = 1.5f;

    private Vector3 originPos;

    private void Start() {
        camTranform = GetComponent<Transform>();
        originPos = camTranform.localPosition;
    }

    private void Update() {
        if(shakeDuration > 0) {
            camTranform.localPosition = originPos + Random.insideUnitSphere * shakeAmout;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        } else {
            shakeDuration = 0;
            camTranform.localPosition = originPos;
        }
    }

}