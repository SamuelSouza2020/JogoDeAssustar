using UnityEngine;
using UnityEngine.UI;

public class ObjectFollower : MonoBehaviour
{
    /// <summary>
    /// Game de susto:
    /// Após iniciar começa uma contagem, assim que passar da contagem
    /// determinada e encostar em uma das linhas "AtivarSusto", ativará 
    /// a imagem com o grito.
    /// </summary>
    private bool isBeingDragged = false;
    private Camera mainCamera;
    private Vector3 initialPosition;

    [SerializeField] float timeGame = 0f;//Inicio da contagem
    [SerializeField] Image SustoImage;
    [SerializeField] AudioSource BackgroundSong;

    private void Start()
    {
        mainCamera = Camera.main;
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Atualiza o timer com o deltaTime
        timeGame += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider = Physics2D.OverlapPoint(mousePosition);

            if (collider != null && collider.gameObject == gameObject)
            {
                isBeingDragged = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isBeingDragged = false;
        }

        if (isBeingDragged)
        {
            Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0f;

            transform.position = newPosition;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se colidiu com uma parede
        if (collision.gameObject.CompareTag("Parede"))
        {
            isBeingDragged = false;
            // Retorna para a posição inicial
            transform.position = initialPosition;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //60 é o valor minimo para ativar o susto, pode ser alterado por outro valor.
        if(timeGame > 60 && collision.gameObject.CompareTag("Susto"))
        {
            BackgroundSong.Stop();
            SustoImage.gameObject.SetActive(true);
            isBeingDragged = false;
        }
    }
}
