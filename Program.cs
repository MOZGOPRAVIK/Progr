using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IStrategy strategyA = new ConcreteStrategyA();
            IStrategy strategyB = new ConcreteStrategyB();

            Context context = new Context(strategyA);
            context.ExecuteStrategy(); // Executing Strategy A

            context.SetStrategy(strategyB);
            context.ExecuteStrategy(); // Executing Strategy B
            Console.WriteLine();

            //---------------------------------------------------------------------

            Subject subject = new Subject();
            IObserver observerA = new ConcreteObserverA();
            IObserver observerB = new ConcreteObserverB();

            subject.Attach(observerA);
            subject.Attach(observerB);

            subject.Notify("Event 1");
            Console.WriteLine();

            //---------------------------------------------------------------------

            Handler handlerA = new ConcreteHandlerA();
            Handler handlerB = new ConcreteHandlerB();
            Handler handlerC = new ConcreteHandlerC();

            handlerA.SetSuccessor(handlerB);
            handlerB.SetSuccessor(handlerC);

            handlerA.HandleRequest(5);
            handlerA.HandleRequest(15);
            handlerA.HandleRequest(25);
            handlerA.HandleRequest(-5);
        }
    }

    interface IStrategy
    {
        void Execute();
    }

    class ConcreteStrategyA : IStrategy
    {
        public void Execute()
        {
            Console.WriteLine("Executing Strategy A");
        }
    }

    class ConcreteStrategyB : IStrategy
    {
        public void Execute()
        {
            Console.WriteLine("Executing Strategy B");
        }
    }

    class Context
    {
        private IStrategy _strategy;

        public Context(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public void ExecuteStrategy()
        {
            _strategy.Execute();
        }
    }

//---------------------------------------------------------------------
    interface IObserver
    {
        void Update(string eventType);
    }

    class ConcreteObserverA : IObserver
    {
        public void Update(string eventType)
        {
            Console.WriteLine($"Observer A received event: {eventType}");
        }
    }

    class ConcreteObserverB : IObserver
    {
        public void Update(string eventType)
        {
            Console.WriteLine($"Observer B received event: {eventType}");
        }
    }

    class Subject
    {
        private List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(string eventType)
        {
            foreach (var observer in _observers)
            {
                observer.Update(eventType);
            }
        }
    }

//---------------------------------------------------------------------
    abstract class Handler
    {
        protected Handler successor;

        public void SetSuccessor(Handler successor)
        {
            this.successor = successor;
        }

        public abstract void HandleRequest(int request);
    }

    class ConcreteHandlerA : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 0 && request < 10)
            {
                Console.WriteLine($"ConcreteHandlerA handled request: {request}");
            }
            else if (successor != null)
            {
                successor.HandleRequest(request);
            }
        }
    }

    class ConcreteHandlerB : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 10 && request < 20)
            {
                Console.WriteLine($"ConcreteHandlerB handled request: {request}");
            }
            else if (successor != null)
            {
                successor.HandleRequest(request);
            }
        }
    }

    class ConcreteHandlerC : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 20)
            {
                Console.WriteLine($"ConcreteHandlerC handled request: {request}");
            }
            else
            {
                Console.WriteLine($"No handler for request: {request}");
            }
        }
    }
}
