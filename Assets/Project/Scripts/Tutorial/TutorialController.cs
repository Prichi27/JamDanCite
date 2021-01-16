using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _panels;
    [SerializeField] private GameObject _nextButton;
    [SerializeField] private GameObject _previousButton;

    private int _panelIndex = 0;

    public void NextPanel()
    {
        // Show previous button
        _previousButton.SetActive(true);
        
        // Deactivate current panel
        _panels[_panelIndex].SetActive(false);

        _panelIndex++;
        
        // Activate next panel
        _panels[_panelIndex].SetActive(true);
        
        // If last panel, hide button
        if(_panelIndex == _panels.Count - 1) _nextButton.SetActive(false);
    }

    public void PreviousPanel()
    {
        // Show next button
        _nextButton.SetActive(true);
        
        // Deactivate current panel
        _panels[_panelIndex].SetActive(false);

        _panelIndex--;
        
        // Activate previous panel
        _panels[_panelIndex].SetActive(true);

        // If first panel, hide button
        if(_panelIndex == 0) _previousButton.SetActive(false);
    }
}
