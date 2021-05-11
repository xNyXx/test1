using System;

namespace RefactoringGuru.DesignPatterns.Proxy.Conceptual
{
    public interface ISubject
    {
        void Request();
        void RocketLaunch(); ///
    }
    class RealSubject : ISubject // not supposed to be rocket launcher
    {
        public void Request()
        {
            Console.WriteLine("RealSubject: Handling Request.");
        }

        public void RocketLaunch()   //..........?????? 
        {
            Console.WriteLine("LaunchRocket"); 
        }
    }
    class Proxy : ISubject
    {
        private RealSubject _realSubject;
        
        public Proxy(RealSubject realSubject)
        {
            this._realSubject = realSubject;
        }
        public void Request()
        {
            if (this.CheckAccess())
            {
                this._realSubject.Request();

                this.LogAccess();
            }
        }

        public void RocketLaunch()
        {
            //Checking if it not launched yet
            Console.WriteLine("Just in two minutes....");
        }

        public bool CheckAccess()
        {
            Console.WriteLine("Proxy: Checking access prior to firing a real request.");

            return true;
        }
        
        public void LogAccess()
        {
            Console.WriteLine("Proxy: Logging the time of request.");
        }
    }
    
    public class Client
    {
        public void ClientCode(ISubject subject)
        {

            subject.Request();
            
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            
            Console.WriteLine("Client: Executing the client code with a real subject:");
            RealSubject realSubject = new RealSubject();
            client.ClientCode(realSubject);
            
            realSubject.RocketLaunch();
            
            
            Console.WriteLine();

            Console.WriteLine("Client: Executing the same client code with a proxy:");
            Proxy proxy = new Proxy(realSubject);
            client.ClientCode(proxy);
        }
    }
}