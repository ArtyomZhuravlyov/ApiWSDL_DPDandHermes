using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace OnlyDPDFull
{
    class Program
    {
        static void Main(string[] args)
        {
            Program a = new Program();
            //  a.DpdLocation();
            //  a.DpdLocationHTTP();
            //a.HermesLocation();
            a.HermesLocationHTTP();

        }

        private void HermesLocation()
        {
            refHermesLoc.WebServiceClient client = new refHermesLoc.WebServiceClient();
            client.ClientCredentials.UserName.UserName = "testlogin";
            client.ClientCredentials.UserName.Password = "testpassword";
            var result = client.GetParcelShops("1000");


        }

        private void HermesLocationHTTP()
        {
            string _url = "https://test-api.hermes-dpd.ru/WS/WebService.svc";
            string _soapEnvelope = File.ReadAllText(@"getCitiesCashPay.xml"); // SOAP-конверт (запрос), который будет отправлен к API

            WebRequest _request = HttpWebRequest.Create(_url);
            _request.Proxy.Credentials = new NetworkCredential("testlogin", "testpassword");
            //_request.Credentials =  CredentialCache.DefaultNetworkCredentials.Password;
            //все эти настройки можно взять со страницы описания веб-сервиса
            _request.Method = "POST";
            _request.ContentType = "text/xml;charset=\"utf-8\"";//"text/xml; charset=utf-8";
            _request.ContentLength = _soapEnvelope.Length;

            // _request.Headers.Add("SOAPAction", "http://dpd.ru/ws/geography/2015-05-20/DPDGeography2/getCitiesCashPayRequest");
            _request.Headers.Add("SOAPAction", "https://test-api.hermes-dpd.ru/WS/WebService/?op=getCitiesCashPayRequest");
            // _request.Headers.Add(_url + @"/" + _method);
            // пишем тело
            StreamWriter _streamWriter = new StreamWriter(_request.GetRequestStream());
            _streamWriter.Write(_soapEnvelope);
            _streamWriter.Close();

            Stream dataStream = _request.GetRequestStream(); //содержит данные запроса,
            // читаем тело
            WebResponse _response = _request.GetResponse();
            StreamReader _streamReader = new StreamReader(_response.GetResponseStream());
            string _result = _streamReader.ReadToEnd(); // переменная в которую пишется результат (ответ) сервиса
        }

        private void DpdLocation()
        {
            refDpdLoc.DPDGeography2Client client = new refDpdLoc.DPDGeography2Client();
            refDpdLoc.dpdCitiesCashPayRequest request = new refDpdLoc.dpdCitiesCashPayRequest();
            client.ClientCredentials.UserName.UserName = "";
            client.ClientCredentials.UserName.Password = "";
            
            // proxy.ClientCredentials.UserName.Password = "testpassword"; " 
            request.auth = new refDpdLoc.auth();
            request.auth.clientKey = "182A17BD6FC5557D1FCA30FA1D56593EB21AEF88";
            request.auth.clientNumber = 1001027795;

            client.getCitiesCashPay(request);

        }

        private void DpdLocationHTTP()
        {
            // string _url = "http://dpd.ru/ws/geography/2015-05-20/DPDGeography2";
            string _url = "http://wstest.dpd.ru/services/geography2?wsdl";
            string _soapEnvelope = File.ReadAllText(@"getCitiesCashPay.xml"); // SOAP-конверт (запрос), который будет отправлен к API

            WebRequest _request = HttpWebRequest.Create(_url);
            _request.Credentials = CredentialCache.DefaultCredentials;
            //все эти настройки можно взять со страницы описания веб-сервиса
            _request.Method = "POST";
            _request.ContentType = "text/xml;charset=\"utf-8\"";//"text/xml; charset=utf-8";
            _request.ContentLength = _soapEnvelope.Length;

            // _request.Headers.Add("SOAPAction", "http://dpd.ru/ws/geography/2015-05-20/DPDGeography2/getCitiesCashPayRequest");
            _request.Headers.Add("SOAPAction", "http://wstest.dpd.ru/services/geography2?getCitiesCashPayRequest");
            // _request.Headers.Add(_url + @"/" + _method);
            // пишем тело
            StreamWriter _streamWriter = new StreamWriter(_request.GetRequestStream());
            _streamWriter.Write(_soapEnvelope);
            _streamWriter.Close();

            Stream dataStream = _request.GetRequestStream(); //содержит данные запроса,
            // читаем тело
            WebResponse _response = _request.GetResponse();
            StreamReader _streamReader = new StreamReader(_response.GetResponseStream());
            string _result = _streamReader.ReadToEnd(); // переменная в которую пишется результат (ответ) сервиса
      //      return _result;

        }
    }
}

