using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayNightCycle : MonoBehaviour
{
    public int _days;
    public int _hours;
    public int _minutes;
    public int _seconds;
    public float _counter;

    public int _dawnStartTime = 7;
    public int _dayStartTime = 9;
    public int _duskStartTime = 17;
    public int _darkStartTime = 20;

    public int currentHour = System.DateTime.Now.Hour;

    // float of sun intensity for testing... 
    // soon I should replace it with scenes for each 

    public float _sunDimTime = 0.01f;
    public float _dawnSunIntensity = 0.5f;
    public float _daySunIntensity = 1f;
    public float _duskSunIntensity = 0.25f;
    public float _nightSunIntensity = 0f;

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
        if (_hours >= _dawnStartTime && _hours < _dayStartTime)
        {
            _dayPhases = DayPhases.Dawn;

        }
        else if (_hours >= _dayStartTime && _hours < _duskStartTime)
        {
            _dayPhases = DayPhases.Day;
        }
        else if(_hours >= _duskStartTime && _hours < _darkStartTime)
        {
            _dayPhases = DayPhases.Dusk;
        }
        else
        {
            _dayPhases = DayPhases.Dark;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SecondsCounter();
        
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
        while(true)
        {
            switch(_dayPhases)
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
        Debug.Log("SecondsCounter");

        // if counter reaches 60 seconds, go back to zero
        if(_counter == 60)
        {
            _counter = 0;
        }

        // counter plus time sync to pc speed
        _counter += Time.deltaTime;

        // take the counter value and cast it as an int to match the value of the seconds.
        // in layman's term, make the counter value equal the seconds value.
        _seconds = (int)_counter;

        // if the counter is less than 60, read the secoundcounter() code again from the start
        if (_counter < 60)
            return;

        // consider this code a failsafe... if counter is more than 60, make counter 60... 
        // so that the if statement above can work
        if (_counter > 60)
            _counter = 60;

        if (_counter == 60)
            MinutesCounter();
    }

    void MinutesCounter()
    {
        Debug.Log("MinutesCounter");
        _minutes++;

        if (_minutes == 60)
        {
            HoursCounter();
            _minutes = 0;
        }

    }

    void HoursCounter()
    {
        Debug.Log("HoursCounter");
        _hours++;
        if (_hours == 24)
        {
            DaysCounter();
            _hours = 0;
        }

    }

    void DaysCounter()
    {
        Debug.Log("DaysCounter");
        _days++;
    }

    void Dawn()
    {
        Debug.Log("Dawn");
        if(_hours == _dayStartTime && _hours < _duskStartTime)
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
