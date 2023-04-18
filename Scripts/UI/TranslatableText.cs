using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TranslatableText : MonoBehaviour
{
    [SerializeField] private int _textID;
    [SerializeField] private bool _isClassic;

    private int _numberText;
    public int TextID => _textID;
    public bool IsClassic => _isClassic;

    [HideInInspector] public TextMeshProUGUI UIText;

    private void Awake()
    {
        UIText = GetComponent<TextMeshProUGUI>();
        _numberText = Translator.AddText(this);
    }

    private void OnEnable()
    {
        Translator.UpdateCurrentText(_numberText);
    }

    private void OnDestroy()
    {
        Translator.Delete(this);
    }

    public void SetId(int ID)
    {
        _textID = ID;
        Translator.UpdateCurrentText(_numberText);
    }
}