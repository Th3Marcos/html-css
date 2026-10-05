using UnityEngine;

public class ControleCarroXbox : MonoBehaviour
{
    [Header("Configuracoes de Movimento")]
    public float velocidadeCurva = 50f;
    public float velocidadeMaxima = 100f;
    public float forcaFreio = 80f;

    [Header("Configuracoes de Marcha")]
    public int marchaAtual = 1;
    public int marchaMaxima = 6;
    private bool botaoxPressionado = false;
    private bool botaobPressionado = false;

    void Update()
    {
        // 1. DIREÇÃO: Analógico Esquerdo ou Setas do Teclado (A/D)
        float inputHorizontal = Input.GetAxis("Horizontal");

        // 2. ACELERADOR: Gatilho Direito (RT) do Xbox ou Seta para Cima / W no Teclado
        // Configuração padrão para RT é "JoystickTriggerRight" no Input Manager
        float aceleradorControle = Input.GetAxis("JoystickTriggerRight"); // Configuração padrão para RT
        float aceleradorTeclado = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ? 1f : 0f;
        float inputAcelerar = Mathf.Max(aceleradorControle, aceleradorTeclado);

        // 3. FREIO: Gatilho Esquerdo (LT) do Xbox ou Seta para Baixo / S no Teclado
        float freioControle = Input.GetAxis("JoystickTriggerLeft"); // Configuração padrão para LT
        float freioTeclado = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ? 1f : 0f;
        float inputFreio = Mathf.Max(freioControle, freioTeclado);

        // 4. TROCA DE MARCHAS MANUAL (X diminui, B aumenta)
        // Joystick Button 2 = Botão X do Xbox | Joystick Button 1 = Botão B do Xbox
        // Teclado: Q diminui, E aumenta marcha
        
        // Passar Marcha (Botão B ou Tecla E)
        if ((Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.E)) && !botaobPressionado)
        {
            if (marchaAtual < marchaMaxima)
            {
                marchaAtual++;
                Debug.Log("Marcha Cima: " + marchaAtual);
            }
            botaobPressionado = true;
        }
        if (!Input.GetKey("joystick button 1") && !Input.GetKey(KeyCode.E)) botaobPressionado = false;

        // Reduzir Marcha (Botão X ou Tecla Q)
        if ((Input.GetKeyDown("joystick button 2") || Input.GetKeyDown(KeyCode.Q)) && !botaoxPressionado)
        {
            if (marchaAtual > 1)
            {
                marchaAtual--;
                Debug.Log("Marcha Baixo: " + marchaAtual);
            }
            botaoxPressionado = true;
        }
        if (!Input.GetKey("joystick button 2") && !Input.GetKey(KeyCode.Q)) botaoxPressionado = false;


        // --- MOVIMENTAÇÃO DO CARRO ---

        // Virar para os lados
        if (inputHorizontal != 0)
        {
            transform.Translate(Vector3.right * inputHorizontal * velocidadeCurva * Time.deltaTime);
        }

        // Acelerar para frente (Velocidade é influenciada pela marcha atual)
        if (inputAcelerar > 0.1f)
        {
            float velocidadeComMarcha = velocidadeMaxima * ((float)marchaAtual / marchaMaxima);
            transform.Translate(Vector3.forward * inputAcelerar * velocidadeComMarcha * Time.deltaTime);
        }

        // Frear / Ré
        if (inputFreio > 0.1f)
        {
            transform.Translate(Vector3.back * inputFreio * forcaFreio * Time.deltaTime);
        }
    }
}
