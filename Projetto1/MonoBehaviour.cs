using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Projetto1
{
    public abstract class MonoBehaviour
    {
        private Thread thread;
        private bool ativo = true;
        public bool visible = false;
        public bool input = false;

        public void Run()
        {
            Awake();
            Start();

            thread = new Thread(
                () => {
                    while (ativo) {
                        Update();
                        LateUpdate();
                        Thread.Sleep(600);
                    }
                    
                }
                );

            thread.Start();
        }

        public void Stop()
        {
            this.ativo = false;
            OnDestroy();
            thread.Join();
        }

        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void OnDestroy() { }

        public abstract void Draw();
    }
}
