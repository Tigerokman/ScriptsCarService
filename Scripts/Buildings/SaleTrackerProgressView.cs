using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaleTrackerProgressView : MonoBehaviour
{
    [SerializeField] private List<ProgressViewImage> _progressViewList; //Door,wheel,steel,engine

    private SaleTracker _saleTracker;

    private void OnDisable()
    {
        _saleTracker.DetailGet -= UpdateProgress;
    }

    public void Instantiate(int doors, int wheels, int steelToHead, int steelToBody, SaleTracker saleTracker, int engine = 1)
    {
        _saleTracker = saleTracker;
        _saleTracker.DetailGet += UpdateProgress;
        int allSteel = steelToHead + steelToBody;
        List<int> maxCounts = new List<int>();
        maxCounts.AddRange(new int[] { doors, wheels, allSteel, engine });

        for (int i = 0; i < maxCounts.Count; i++)
        {
            _progressViewList[i].Initialiaze(maxCounts[i]);
        }

    }

    public void ResetProgress()
    {
        for (int i = 0; i < _progressViewList.Count; i++)
        {
            _progressViewList[i].ResetProgress();
        }
    }

    private void UpdateProgress(Detail detail)
    {
        for (int i = 0; i < _progressViewList.Count; i++)
        {
            if(detail.NameID == _progressViewList[i].NameID)
            {
                _progressViewList[i].ProgressUpdate();
                break;
            }
        }
    }
}
