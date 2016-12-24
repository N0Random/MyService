using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YandexLinguistics.NET;
/*!
* @file	Class1.cs
* @class   ServLisener
* @brief   Класс поддерживающий соединение
* @author	NoRandom
* @date	24 дек. 2016 г
   !*/
namespace WindowsService1

{

    class ServLisener
    {
        private Int32 _port;
        /// Переменная для прослушки порта 9999 по адрессу 127.0.0.1
        public TcpListener tcpServer ;
        bool done;
       public ServLisener()
       {
          
       }
       /// Функция стартующая при запуске службы
       public void start() 
       {
           done = false;

           TcpListener tcpServer = new TcpListener(9999);

           tcpServer.Start();

           while (!done)
           {
              // Console.Write("Waiting for connection...");
               TcpClient client = tcpServer.AcceptTcpClient();
              // Console.WriteLine("Connection accepted.");
               NetworkStream ns = client.GetStream();

               
               byte[] Buffer = new byte[2048];
               String data = "";
               try
               {
                   
                  
                   int i = 0;
                   do
                   {

                       ns.Read(Buffer, i, Buffer.Length);
                       data += Encoding.Unicode.GetString(Buffer).Replace("\0", "").Trim();
                       i = Buffer.Length;
                   }
                   while (ns.DataAvailable);
                  string[] trStr = data.Split('|');

                  Translator t = new Translator(trStr[0], trStr[1]);
                  data = t.Perevod(trStr[2]);
                   

               }
               catch (Exception e)
               {
                   // Console.WriteLine(e.ToString());
               }

               try
               {
                   Buffer = Encoding.Unicode.GetBytes(data);
                   ns.Write(Buffer, 0, Buffer.Length);
                   ns.Close();
                   client.Close();
               }
               catch (Exception e)
               {
                  // Console.WriteLine(e.ToString());
               }
           }
       }
        
       public void stop() {
           done = true;
       }


    }
    /// @class Translator
    class Translator 
    {
       
        YandexLinguistics.NET.Translator tr;
        LangPair lp;
        /// токен для использования API
        const string trKey = "trnsl.1.1.20161223T220940Z.1f287d777aaa8b28.187c39ec9f9a8d777bfbfb1b18eb5fc0a989bbfd";
        public Translator(string i, string o)
        {


            
            switch(i)
            {
                case ("ru"):
                lp.InputLang = Lang.Ru;
                    break;
                case ("de"):
                    lp.InputLang = Lang.De;
                    break;
                case ("be"):
                    lp.InputLang = Lang.Be;
                    break;
                case ("pl"):
                    lp.InputLang = Lang.Pl;
                    break;
                case ("en"):
                    lp.InputLang = Lang.En;
                    break;

                case ("fr"):
                    lp.InputLang = Lang.Fr;
                    break;
            }
            switch (o)
            {
                case ("ru"):
                    lp.OutputLang = Lang.Ru;
                    break;
                case ("de"):
                    lp.OutputLang = Lang.De;
                    break;
                case ("be"):
                    lp.OutputLang = Lang.Be;
                    break;
                case ("pl"):
                    lp.OutputLang = Lang.Pl;
                    break;
                case ("en"):
                    lp.OutputLang = Lang.En;
                    break;

                case ("fr"):
                    lp.OutputLang = Lang.Fr;
                    break;
            }
            tr = new YandexLinguistics.NET.Translator(trKey);
        }
        public string Perevod(string text)
        {
            return tr.Translate(text, lp).Text;
        }

    }

}
