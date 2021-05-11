using System;

namespace test_dotnet
{
    public interface ISubject
    {
        void Request();
    }

    public interface IRocketLauncher
    {
        void RocketLaunch(Rocket rocket); 
    }

    public class Rocket
    {
        public string State;

        public Rocket()
        {
            State = "not launched"; 
        }
        
        public void Launch()
        {
            State = "Launched";
        }
    }

    public class RocketLauncher : IRocketLauncher
    {
        public void RocketLaunch(Rocket rocket)
        {
            Console.WriteLine("LaunchRocket");
            rocket.Launch();
        }
    }
    class RealSubject : ISubject // not supposed to be rocket launcher
    {
        public void Request()
        {
            Console.WriteLine("RealSubject: Handling Request.");
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

        public void RocketLaunch( Rocket rocket)
        {
            //Checking if it not launched yet
            Console.WriteLine("Just in two minutes....");

            if (rocket.State == "not launched") ;
            
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

    class ProxyLauncher : IRocketLauncher
    {
        public void RocketLaunch(Rocket rocket)
        {
            if (rocket.State == "launched")
                Console.WriteLine("Already Launched");
            else
            {
                Console.WriteLine("just in two minutes");
                rocket.Launch();
            }
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
            Rocket rocket = new Rocket();
            
            RocketLauncher rocketLauncher = new RocketLauncher();
            
            rocketLauncher.RocketLaunch(rocket);
            
            ProxyLauncher proxyLauncher = new ProxyLauncher();
            
            proxyLauncher.RocketLaunch(rocket);


            Console.WriteLine();

            Console.WriteLine("Client: Executing the same client code with a proxy:");
            Proxy proxy = new Proxy(realSubject);
            client.ClientCode(proxy);
        }
    }
}