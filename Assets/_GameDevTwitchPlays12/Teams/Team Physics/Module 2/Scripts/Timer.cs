/*===============================================================
Product:    Unity3dTips&Tricks
Developer:  Dimitry Pixeye - pixeye@hbrew.store
Company:    Homebrew - http://hbrew.store
Date:       08/10/2017 21:54
================================================================*/

using System;
using UniRx;
using UnityEngine;

namespace Homebrew
{

    public class Timer : IDisposable
    {
        public float timer;
        private Action actionComplete;
        private float finishTime;       
        public float MyTime
        {
            get { return finishTime; }
            set { finishTime = value; }
        }

        private bool isRunning;
        private IDisposable observerDisposable;

        public Timer(float finishTime, Action actionComplete)
        {
            this.finishTime = finishTime;
            this.actionComplete = actionComplete;
            observerDisposable = Observable.EveryGameObjectUpdate().
                Where(x => isRunning).Subscribe(_ => Update());

        }
        public void Restart()
        {
            isRunning = true;
            timer = 0.0f;
        }
        public void ShutDown()
        {
            isRunning = false;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer < finishTime) return;

            timer = 0.0f;
            isRunning = false;
            actionComplete();
        }
        public void Dispose()
        {
            observerDisposable.Dispose();
            observerDisposable = null;
            actionComplete = null;
            
        }

    }
}
