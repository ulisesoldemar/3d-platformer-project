using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Instances
    public static PlayerController Instance { get; private set; }
    #endregion

    #region References
    [Header("References")]
    [SerializeField]
    CharacterController _characterController;
    [SerializeField]
    GameObject _playerModel;
    [SerializeField]
    PlayerAnimation _playerAnimation;
    public GameObject[] playerParts;
    #endregion

    #region Movement
    [Header("Movement")]
    [SerializeField]
    float _walkSpeed;
    [SerializeField]
    float _runSpeed;
    float _verticalInput;
    float _horizontalInput;
    Vector3 _moveDirection;
    public bool stopMove;
    #endregion

    #region Jump
    [Header("Jump")]
    [SerializeField]
    float _jumpForce;
    bool _jumpRequest;
    bool _canDoubleJump;
    public int _avaliableJumps = 0, _maxJumps = 2;
    #endregion

    #region Gravity
    [Header("Gravity")]
    [SerializeField]
    float _gravityScale;
    #endregion

    #region Camera
    [Header("Camera")]
    [SerializeField]
    float _rotateSpeed;
    [SerializeField]
    Camera _playerCamera;
    #endregion

    #region Knockback
    [Header("Knockback")]
    [SerializeField]
    float _knockBackLength;
    [SerializeField]
    Vector2 _knockBackPower;
    bool _isKnocking;
    float _knockBackCount;
    //Fuerza de knockback tras golpear enemigo
    public float bounceForce = 8;
    #endregion

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (_playerCamera == null)
        {
            Debug.LogWarning("El personaje no tiene cámara.");
        }

        if (_characterController == null)
        {
            Debug.LogWarning("El personaje no tiene CharacterController.");
        }

        if (_playerModel == null)
        {
            Debug.LogWarning("El personaje no tiene GameObjetc _playerModel.");
        }

        if (_playerAnimation == null)
        {
            Debug.LogWarning("El personaje no tiene PlayerAnimation (script).");
        }
    }

    void Update()
    {
        // Si no se recibe daño
        if (!_isKnocking && !stopMove)
        {
            #region Movement
            float yStore = _moveDirection.y;
            _verticalInput = Input.GetAxisRaw("Vertical");
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _moveDirection = ((transform.forward * _verticalInput) + (transform.right * _horizontalInput));
            _moveDirection = Vector3.ClampMagnitude(_moveDirection, 1f);
            _moveDirection *= Input.GetButton("Walk") ? _walkSpeed : _runSpeed;
            _moveDirection.y = yStore;
            #endregion

            #region Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _jumpRequest = true;
            }
            #endregion
        }
        else
        {
            #region Knockback
            // Restando el deltatime, se genera un cronómetro
            _knockBackCount -= Time.deltaTime;
            _moveDirection = (_playerModel.transform.forward * _knockBackPower.x);
            _moveDirection.y = _knockBackPower.y;
            if (_knockBackCount <= 0)
            {
                _isKnocking = false;
            }
            #endregion
        }
        //Función para desactivar el movimiento del jugador
        if (stopMove)
        {
            _moveDirection.x = 0;
            _moveDirection.y = 0;
            _moveDirection.z = 0;
            _characterController.Move(_moveDirection);
        }

        Move();
        AnimatePlayer();
    }

    void FixedUpdate()
    {
        #region Jump
        // Si el jugador se encuentra en el piso
        /*if (_characterController.isGrounded)
        {
            // Solamente puede haber doble salto si se salta una primera vez
            _canDoubleJump = false;
            if (_jumpRequest)
            {
                // Se aplica la fuerza de salto a la posición en Y
                _moveDirection.y = _jumpForce;
                _canDoubleJump = true;
            }
            else
            {
                // Se "restablece" la velocidad vertical debido a que no está cayendo
                _moveDirection.y = 0;
            }
        }
        // Doble salto
        else if (_jumpRequest && _canDoubleJump)
        {
            _moveDirection.y = _jumpForce;
            _canDoubleJump = false;
        }*/
        #endregion

        #region Salto
        
        if (_jumpRequest && _avaliableJumps > 0)
        {
            _jumpRequest = false;
            _moveDirection.y = _jumpForce;
            _avaliableJumps--;
            
        }
        

        #endregion
    }


    void LateUpdate()
    {
        #region Rotation with camera
        // El personaje solamente rotará con la cámara si se está moviendo
        // de lo contrario, solamente la cámara rota, por eso se revisan los
        // inputs de movimiento
        if (!_isKnocking && (_verticalInput != 0 || _horizontalInput != 0))
        {
            Vector3 movement = new Vector3(_moveDirection.x, 0f, _moveDirection.z);
            
            //Si el personaje no se ha movido
            if(movement != Vector3.zero)
            {
                //Hacer la rotacion
                // Se reestablece la rotación en X y Z para modificarla nuevamente
                transform.rotation = Quaternion.Euler(0f, _playerCamera.transform.rotation.eulerAngles.y, 0f);
                // LookRotation crea una rotación específica hacia forward o upward
                Quaternion newRotation = Quaternion.LookRotation(movement);
                // Slerp nos permite mover y rotar de manera más suave
                _playerModel.transform.rotation = Quaternion.Slerp(_playerModel.transform.rotation, newRotation, _rotateSpeed * Time.deltaTime);
            }
            
        }
        #endregion
        if (_characterController.collisionFlags == CollisionFlags.Below)
        {
            _avaliableJumps = _maxJumps;
        }
        if (_characterController.collisionFlags == CollisionFlags.None && _avaliableJumps == 0 && _jumpRequest == true)
        {
            _jumpRequest = false;
        }
    }

    void Move()
    {
        if (!_characterController.isGrounded)
        {
            // Se aplica la constante de física (9.8 por defecto en Unity) y se multiplica
            // por la escala de gravedad, de esta manera se modifica la potencia del salto
            // desde el editor de Unity
            _moveDirection.y += Physics.gravity.y * _gravityScale * Time.deltaTime;
        }
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    void AnimatePlayer()
    {
        #region Animation
        _playerAnimation.SetSpeed(Mathf.Abs(_moveDirection.x) + Mathf.Abs(_moveDirection.z));
        _playerAnimation.SetGrounded(_characterController.isGrounded);
        _playerAnimation.SetGravityForce(_moveDirection.y);
        _playerAnimation.SetJump(_jumpRequest);
        #endregion
    }

    public void Knockback()
    {
        _isKnocking = true;
        _knockBackCount = _knockBackLength;
    }
    //Controla el knockback al golpear enemigo
    public void Bounce()
    {
        _moveDirection.y = bounceForce;
        _characterController.Move(_moveDirection * Time.deltaTime);  
    }
}
