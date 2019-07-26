using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using OnlyDPDFull.refHermesLoc;

namespace OnlyDPDFull
{
    class Program
    {
        static void Main(string[] args)
        {
            Program a = new Program();
            //  a.DpdLocation();
            //  a.DpdLocationHTTP();
            a.HermesLocation();
            a.HermesLocationHTTP();

        }

        private void HermesLocation()
        {
            refHermesLoc.WebServiceClient client = new refHermesLoc.WebServiceClient();
            client.ClientCredentials.UserName.UserName = "testlogin";
            client.ClientCredentials.UserName.Password = "testpassword";


            //var result = client.GetParcelShops("1000");
            /////Данный метод предназначен для первоначальной передачи информации о посылках. Передача информации о посылках возможна и без указания штрих-кодов посылок. В этом случае генерация номеров будет происходить на стороне Hermes Russia. Эта опция не включена по умолчанию, поэтому для ее активации необходимо связаться с отделом продаж Hermes Russia.
            //var preadvices =  HermesLocationC.FillPreadvice();
            //var result2 = client.CreatePreadvice(preadvices);

            /////Данный метод позволяет обновлять информацию о еще не переданных на доставку посылках. Обновление информации производится по совпадению клиентского номера посылки. При совпадении клиентского номера посылки все остальные поля будут обновлены данными из запроса.
            //var preadvices3 = HermesLocationC.FillPreadvice();
            //var result3 = client.UpdatePreadvice(preadvices3);

            /////Данный метод позволяет удалять еще не переданные на доставку посылки, если необходимость в их доставке отпала или данные были ошибочными. Удаление информации о посылках происходит по совпадению штрих-кодов.
            //string[] parcelBarcodes = new string[] { "12345678901234", "12345678901235" };
            //var resule4 = client.DeletePreadvice(parcelBarcodes);

            /////Данный метод позволяет получить список всех еще не переданных на доставку посылок заданного бизнес-юнита.
            string businessUnitCode = "1000";
            //var resule5 = client.GetPreadvices(businessUnitCode);

            PreadviceResult[] resule6 = client.SendAllPreadvicesToDelivery(businessUnitCode);


            /////Данный метод предназначен для решения задачи получения всех статусов всех посылок заданного бизнес-юнита в режиме, приближенном к реальному времени и сохранении статусов в системе клиента.
            /////Метод позволяет получить все статусы, которые появились в системе Hermes Russia с заданной даты.
            /////Алгоритм работы с методом
            ////1.При первом вызове необходимо передать в параметре dateFrom текущую дату
            ////2.Далее гарантируется передача всех статусов без потерь или повторов, при условии, что параметр dateFrom будет содержать значение, возвращенное в параметре NextRequestDateFrom из предыдущего вызова метода, а параметр dateTo необходимо оставлять пустым
            ////3.При удалении статуса, он передается еще раз, но уже с заполненным полем StatusDeleteTimestamp
            //DateTime dateTime = new DateTime(2015, 7, 20, 18, 30, 25);

            //StatusDateRangeResult resule7 = client.GetStatusesByBusinessUnit(businessUnitCode, dateTime , null);
            //dateTime = resule7.NextRequestDateFrom;

            ///Данный метод предназначен для получения всех статусов по заданному списку посылок. Удаленные статусы не возвращаются данным методом.
            ///Часто использовать этот метод крайне не рекомендуется, только для редких и частных случаев.
            string[] parcelBarCodes = new string[1] { "20000000000001" };
            client.GetStatusesByParcelBarcodes(parcelBarCodes);
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

            //string delisId = "exampleUserID";
            //string password = "examplePassword";
            //string messageLanguage = "en_US"; // optional
            //string authToken = "";

            //// create a client object
            //LoginService.LoginService loginServiceClient = new LoginService.LoginService();

            //// create a login object and provide the delisId and password
            //LoginService.Login login = new LoginService.Login();
            //login = loginServiceClient.getAuth(delisId, password, messageLanguage);

            //// get the token
            //authToken = login.authToken;

            refDpdLoc.DPDGeography2Client client = new refDpdLoc.DPDGeography2Client();
            refDpdLoc.dpdCitiesCashPayRequest request = new refDpdLoc.dpdCitiesCashPayRequest();
            client.ClientCredentials.UserName.UserName = "exampleUserID";
            client.ClientCredentials.UserName.Password = "examplePassword";
            
            // proxy.ClientCredentials.UserName.Password = "testpassword"; " 
            request.auth = new refDpdLoc.auth();
            request.auth.clientKey = "182A17BD6FC5557D1FCA30FA1D56593EB21AEF88";
            request.auth.clientNumber = 1001027795;

            client.getCitiesCashPay(request);

        }

        private void DpdLocationHTTP()
        {
            // string _url = "http://dpd.ru/ws/geography/2015-05-20/DPDGeography2";
            string _url = "https://api.hermes-dpd.ru/WS";
            string _soapEnvelope = File.ReadAllText(@"getCitiesCashPay.xml"); // SOAP-конверт (запрос), который будет отправлен к API

            WebRequest _request = HttpWebRequest.Create(_url);
            _request.Credentials = CredentialCache.DefaultCredentials;
            //все эти настройки можно взять со страницы описания веб-сервиса
            _request.Method = "POST";
            _request.ContentType = "text/xml;charset=\"utf-8\"";//"text/xml; charset=utf-8";
            _request.ContentLength = _soapEnvelope.Length;

            // _request.Headers.Add("SOAPAction", "http://dpd.ru/ws/geography/2015-05-20/DPDGeography2/getCitiesCashPayRequest");
            _request.Headers.Add("SOAPAction", "https://api.hermes-dpd.ru/WS/IWebService/GetParcelShops");
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

