using UnityEngine;

public class ArcanoidBall : MonoBehaviour
{
    public ArcanoidPlatform platform;
    // Находится ли шар на платформе
    public bool IsPlatformed { get; private set; }
    // Нанести ли урон при сбросе положения
    bool isDamageOwnerOnReset;
    // Игнорировать ли триггеры границ которые сбрасывают положение шара
    bool isBoundIgnore;

    private AudioSource _audioSource;
    private Rigidbody _rb;

    private Vector3 _previousDirection;
    [SerializeField] private Vector3 startForce;

    [SerializeField] private float maxVelocity;
    private void Awake()
    {
        isDamageOwnerOnReset = true;

        IsPlatformed = true;

        isBoundIgnore = false;

        _previousDirection = Vector3.zero;
    }
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _rb = GetComponent<Rigidbody>();
        _rb.maxLinearVelocity = maxVelocity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(IsPlatformed == false)
            _audioSource.Play();

        //Vector3 velocity = _rb.linearVelocity;

        //_previousDirection = velocity;

        //_rb.linearVelocity = velocity;


        //StringBuilder sb = new StringBuilder();
        //sb.Append("LVelocity: ").Append(rb.linearVelocity);
        //sb.Append("\nLVEL Magnitude: ").Append(rb.linearVelocity.magnitude);
        //sb.Append("\nLVEL Normalized: ").Append(rb.linearVelocity.normalized);
        //Debug.Log(sb);
    }
    public void ResetToPlatform()
    {
        if(isBoundIgnore == false)
        {
            _rb.linearVelocity = Vector3.zero;

            IsPlatformed = _rb.isKinematic = true;

            if (isDamageOwnerOnReset)
                platform.owner.health.Damage(1);
        }
    }
    void Update()
    {
        if(IsPlatformed)
        {
            gameObject.transform.position = platform.transform.position + new Vector3(0, 0 ,1);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsPlatformed)
        {
            IsPlatformed = _rb.isKinematic = false;
            _rb.AddForce(startForce);
        }
    }
}
