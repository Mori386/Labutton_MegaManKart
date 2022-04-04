using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemCapsuleController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.3f;
    [SerializeField] float spawnDelay = 4;
    [SerializeField] float spinPerTick = 200;
    private Vector3 spawnPosition;
    private CapsuleCollider capsuleCollider;
    private GameObject questionMark;

    void Start()
    {
        spawnPosition = transform.position;
        capsuleCollider = GetComponent<CapsuleCollider>();
        questionMark = GameObject.Find("questionMark");
    }

    private void FixedUpdate()
    {
        float y = Mathf.PingPong(Time.time * moveSpeed, 0.2f);
        transform.position = new Vector3(spawnPosition.x, spawnPosition.y + y, spawnPosition.z);
        questionMark.transform.Rotate(0, spinPerTick * Time.fixedDeltaTime, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Ap�s o trigger, � chamado a fun��o goneController, que desativara o render de mesh e o collider. Apos um delay, essa fun��o sera chamada mais uma vez pra reativar o collider e o render.
            goneController();
            Invoke("goneController", spawnDelay);
        }
    }

    private void goneController()
    {
        //Pega os componentes de renderer nos objetos child, e os d
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        { r.enabled = !r.enabled; }
        capsuleCollider.enabled = !capsuleCollider.enabled;
    }
}
