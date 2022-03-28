using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class KartController : MonoBehaviour
{
    //Componentes do objeto 
    Rigidbody rb;
    [Header("Pneus")]
    [SerializeField] WheelCollider FRWheel;
    [SerializeField] WheelCollider FLWheel;
    [SerializeField] WheelCollider BRWheel;
    [SerializeField] WheelCollider BLWheel;
    WheelCollider[] FWheels;
    WheelCollider[] BWheels;

    [Header("Configurações")]
    //Torque aplicado ao carro 
    [SerializeField] float motorTorque;
    //Força maxima aplicada ao carro
    [SerializeField] float maxSpeed;

    //Angulação de rotacao das rodas frontais 
    [SerializeField] float steerAngle;

    //Força de freio do carro 
    [SerializeField] float brakeForce;
    bool isBreaking;

    //Mudança do centro de massa do objeto no eixo Y, para balançear o peso do objeto
    [SerializeField] float centroDeMassaY;

    // Gravidade adicionada ao objeto quando fora do chão
    [SerializeField] float gravidadeAdicionada;

    //Angulação projetada para o drift do carro 
    [SerializeField] private float driftAngle;
    // Boost adquirido do carro apos drift
    [SerializeField] private float driftBoostMinimo;
    [SerializeField] private float driftBoostMaximo;
    //Tempo necessario para chegar no boost maximo do drift
    [SerializeField] private float driftTempoDeCarga;

    bool grounded;
    private void Awake()
    {
        //Adiciona ambas rodas a seus devidos grupos, para facilitar referienciar(todas rodas frontais e todas rodas traseiras)
        FWheels = new WheelCollider[2];
        FWheels[0] = FRWheel;
        FWheels[1] = FLWheel;

        BWheels = new WheelCollider[2];
        BWheels[0] = BRWheel;
        BWheels[1] = BLWheel;

        //Salva o rigidbody do objeto para poder acessa-lo sem precisar procura-lo dentre seus componentes
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, centroDeMassaY, 0);
    }
    private void FixedUpdate()
    {
        //Input.GetAxisRaw("Vertical") pega um valor de -1 a 1, baseado no pressionar das teclas W e S ou setinhas cima e baixo, se caso nenhuma delas esteja pressionada o valor sera de 0 
        BWheelsMotorTorqueInputAxis(Input.GetAxisRaw("Vertical"));
        SpinWheel(Input.GetAxisRaw("Vertical"));
        //Input.GetAxisRaw("Horizontal") pega um valor de -1 a 1, baseado no pressionar das teclas A e D ou setinhas esquerda e direita, se caso nenhuma delas esteja pressionada o valor sera de 0 
        FWheelsSteerAngleInputAxis(Input.GetAxisRaw("Horizontal"));
        BreakUpdate();
        if (GroundedPercentage() < 1)
        {
            rb.velocity += Physics.gravity * Time.fixedDeltaTime * gravidadeAdicionada;
        }
    }
    /// <summary>
    /// Acelera as rodas traseiras, com a direcao determinada pelo inputAxis
    /// </summary>
    private void BWheelsMotorTorqueInputAxis(float inputAxis)
    {
        //Calcula a velocidade total do objeto, somando ao valor absoluto da velocidade aplicada dele de todas as direções, visto que ao se mover para tras, eh considerado uma velocidade negativa
        float speed = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y) + Mathf.Abs(rb.velocity.z);
        if (speed < maxSpeed)
        {
            // Se a velocidade atual for menor que a velocidade maxima permitida, será adicionado velocidade ao carro 
            foreach (WheelCollider wheelCollider in BWheels)
            {
                //Foreach = Para cada WheelCollider dentro de BWheels aplicar scripts dentro
                wheelCollider.motorTorque = inputAxis * motorTorque;
            }
        }
        else
        {
            //Se a velocidade atual for igual ou exceder a velocidade maxima permitida, o jogador mesmo pressionando o botao de acelerar, não acelerara mais que o perimitido
            foreach (WheelCollider wheelCollider in BWheels)
            {
                //Foreach = Para cada WheelCollider dentro de BWheels aplicar scripts dentro
                wheelCollider.motorTorque = 0;
            }
        }
    }
    /// <summary>
    /// Aplica a rotacao steerAngle as rodas frontais, com sua direção sendo definida pelo inputAxis
    /// </summary>
    private void FWheelsSteerAngleInputAxis(float inputAxis)
    {
        foreach (WheelCollider wheelCollider in FWheels)
        {
            //Foreach = Para cada WheelCollider dentro de FWheels aplicar scripts dentro
            //steerAngle eh multiplicado pelo inputAxis para definir sua direção visto que os valores possiveis de inputAxis são -1,0 e 1
            wheelCollider.steerAngle = inputAxis * steerAngle;
            //salva o transform do visual das rodas para facil acesso depois 
            Transform visual = wheelCollider.transform.parent.Find("Visual");
            //aplica uma rotacao local, rotação baseada na rotação do objeto aplicado Ex: obejto parente tem rotação de 30 no eixo X o objeto filho ao aplicar um valor de rotacao local de 10 no eixo X, tera como seu angulo final a rotação de 40
            visual.localRotation = Quaternion.Euler(new Vector3(visual.rotation.x, 270 + steerAngle * inputAxis, visual.rotation.z));
            //Aplicando a ele a rotação atual dele de X e Z, apenas modificando a de Y, 270 sendo o centro dentre os pontos maximos deles e adicionando a rotação do angulo atual para o visual da roda 
        }
    }
    /// <summary>
    /// Checa o pressionamento da tecla espaço e se caso estiver ativa aplica a força de breque as rodas
    /// </summary>
    private void BreakUpdate()
    {
        //booleano(true ou false), se caso a tecla Space estiver pressionada vai ser true e se n vai ser false
        isBreaking = Input.GetKey(KeyCode.Space);
        //define a força do breque atual baseado no booleano, se caso for true vai ser igual a brakeForce e se caso false 0
        float currentBrakeForce = isBreaking ? brakeForce : 0f;
        //aplica a todas rodas a força do breque atual
        foreach (WheelCollider wheelCollider in FWheels)
        {
            wheelCollider.brakeTorque = currentBrakeForce;
        }
        foreach (WheelCollider wheelCollider in BWheels)
        {
            wheelCollider.brakeTorque = currentBrakeForce;
        }
    }
    /// <summary>
    /// Rotaciona a roda do carro, simulando ele estar andando baseado no input das teclas 
    /// </summary>
    private void SpinWheel(float VerticalInputAxis)
    {
        //Aplica a cada roda uma rotação adicional baseada no input do jogador em sua direção oposta simulando o movimento do carro 
        foreach (WheelCollider wheelCollider in BWheels)
        {
            Transform visual = wheelCollider.transform.parent.Find("Visual");
            visual.Rotate(new Vector3(0, 0, -VerticalInputAxis));
        }
        foreach (WheelCollider wheelCollider in FWheels)
        {
            Transform visual = wheelCollider.transform.parent.Find("Visual");
            visual.Rotate(new Vector3(0, 0, -VerticalInputAxis));
        }
    }
    /// <summary>
    /// Retorna a porcentagem do Kart no chão
    /// </summary>
    private float GroundedPercentage()
    {
        float percentage = 0;
        foreach (WheelCollider wheelCollider in BWheels)
        {
            if (wheelCollider.isGrounded) percentage += 1;
        }
        foreach (WheelCollider wheelCollider in FWheels)
        {
            if (wheelCollider.isGrounded) percentage += 1;
        }
        return percentage / 4;
    }
    ///<summary>
    /// Inicia o processo de drift se caso o carro esteja devidamente dentro de suas necessidades 
    ///</summary>
    private void DriftStart()
    {
        if (FLWheel.steerAngle != 0 && drifting == null && GroundedPercentage)
        {
            drifting = StartCoroutine(Drifting());
        }
    }
    private Coroutine drifting;
    private IEnumerator Drifting()
    {
        //Aplica as modificações para permitir drift
        float tempoDriftando = 0;
        while (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Driftando");
            tempoDriftando += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        //Apos drift salva o tempo driftado para calcular a força do boost aplicado ao jogador 
        Debug.Log("Aplica o debuff");
        float driftBoost;
        if (tempoDriftando / driftTempoDeCarga < 1)
        {
            driftBoost = driftBoostMinimo + tempoDriftando / driftTempoDeCarga * (driftBoostMaximo - driftBoostMinimo);
        }
        else
        {
            driftBoost = driftBoostMaximo;
        }
        //rb.AddForce...
        drifting = null;
        yield break;
    }
}
