
using System.Collections;
using UnityEngine;

public class BoxLoot : MonoBehaviour
{
    private CardBafManager _cardBafManager;
    public GameObject textPrefab;
    public GameObject posText;

    private Animator _animator;
    private GameObject _player;
    private GameObject _canvas;
    private void Awake()
    {
        _cardBafManager = FindObjectOfType<CardBafManager>();
        _player = GameObject.FindWithTag("Player");
        _canvas = GameObject.FindWithTag("CanvasTwo");
        _animator = GetComponent<Animator>();
    }

    private GameObject _text;
    private void Start()
    {
        _text = Instantiate(textPrefab, _canvas.transform);
        _text.transform.position = posText.transform.position;
        
    }

    private bool _isActive = true;
    private void Update()
    {
        if (_isActive)
        {
            if (Vector2.Distance(_player.transform.position, transform.position) < 2f)
            {
                _text.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    _text.SetActive(false);
                    _animator.SetBool(Animator.StringToHash("Start"), true);
                    _cardBafManager.StartBafActive();
                    StartCoroutine(Delete());
                    _isActive = false;
                }
            }
            else
            {
                _text.SetActive(false);
            }
        }
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(1f);
        Destroy(_text);
        Destroy(gameObject);
    }
}
