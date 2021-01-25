using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;

public class InputValidator : MonoBehaviour
{
    private TMP_InputField _input;

    private void Start() 
    {
        _input = GetComponent<TMP_InputField>();
    }

    private void OnGUI() 
    {
        _input.text = Regex.Replace(_input.text, @"[^a-zA-Z0-9 ]", "");
    }
}
