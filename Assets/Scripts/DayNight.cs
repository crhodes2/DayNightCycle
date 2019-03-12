using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayNight : MonoBehaviour
{
    public int _hours;
    public int _minutes;
    public int _seconds;

    public int _dawnStartTime = 7;
    public int _dayStartTime = 9;
    public int _duskStartTime = 17;
    public int _darkStartTime = 20;

    public DayPhases _dayPhases;    // define naming convention for the phases of the day

    public enum DayPhases           // enums for day phases
    {
        Dawn,
        Day,
        Dusk,
        Dark
    }

    public void DawnScene()
    {
        SceneManager.LoadScene(0);
    }

    public void DayScene()
    {
        SceneManager.LoadScene(1);
    }

    public void DuskScene()
    {
        SceneManager.LoadScene(2);
    }

    public void DarkScene()
    {
        SceneManager.LoadScene(3);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("TimeOfDayFiniteStateMachine");
    }

    // Update is called once per frame
    void Update()
    {
        SecondsCounter();
        MinutesCounter();
        HoursCounter();
    }

    void SunCycle()
    {
        Dawn();
        Day();
        Dusk();
        Dark();
    }

    IEnumerator TimeOfDayFiniteStateMachine()
    {
        // INFINITE LOOP ALERT
        while (true)
        {
            switch (_dayPhases)
            {
                case DayPhases.Dawn:
                    Dawn();
                    break;

                case DayPhases.Day:
                    Day();
                    break;

                case DayPhases.Dusk:
                    Dusk();
                    break;

                case DayPhases.Dark:
                    Dark();
                    break;
            }
            yield return null;
        }
    }

    void SecondsCounter()
    {
        _seconds = System.DateTime.Now.Second;
    }

    void MinutesCounter()
    {
        _minutes = System.DateTime.Now.Minute;
    }

    void HoursCounter()
    {
        _hours = System.DateTime.Now.Hour;
    }


    void Dawn()
    {
        Debug.Log("Dawn");
        if (_hours == _dayStartTime && _hours < _duskStartTime)
        {
            _dayPhases = DayPhases.Day;
            DayScene();
        }
    }

    void Day()
    {
        Debug.Log("Day");
        if (_hours == _duskStartTime && _hours < _darkStartTime)
        {
            _dayPhases = DayPhases.Dusk;
            DuskScene();
        }
    }

    void Dusk()
    {
        Debug.Log("Dusk");
        if (_hours == _darkStartTime)
        {
            _dayPhases = DayPhases.Dark;
            DarkScene();
        }
    }

    void Dark()
    {
        Debug.Log("Dark");
        if (_hours == _dawnStartTime && _hours < _dayStartTime)
        {
            _dayPhases = DayPhases.Dawn;
            DawnScene();
        }
    }

    void OnGUI()
    {

    }
}
