using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyDPDFull
{
    public class HermesLocationC
    {

        internal static refHermesLoc.Preadvice[] FillPreadvice()
        {
            refHermesLoc.Preadvice[] preadvices = new refHermesLoc.Preadvice[1];
            
            preadvices[0] = new refHermesLoc.Preadvice()
            {   ///Идентификатор бизнес-юнита клиента
                BusinessUnitCode = "1376",
                ///Валюта стоимости посылки (Доступны только рубли)
                CashOnDeliveryCurrency = "RUB",
                ///Сумма, которую необходимо внести получателю при получении посылки. Если посылка предоплачена полностью, значение установить = 0, если частично, указать, сколько необходимо внести покупателю.
                CashOnDeliveryValue = 1000,
                //Номер заказа в системе клиента, используется для дальнейшего трекинга, отчетности и т.д.
                ClientOrderNumber = "1234567890",//не обяз
                                                 //Уникальный номер посылки в системе клиента (может быть равен ParcelBarcode, если генерация штрих-кода посылки идет на стороне клиента). 
                ClientParcelNumber = "12345678901234",
                ///Код страны
                CustomerCountryCode = "RUS",
                ///Фамилия получателя
                CustomerSurname = "Иванов",

                CustomerMobilePhoneNumber = "8-903-123-44-55",

                Services = new string[] { "DIRECT_DELIVERY" },
                //Валюта страховой стоимости посылки (Доступны только рубли)
                InsuranceCurrency = "RUB",
                ///Страховая стоимость посылки. Если посылка предоплачена, значение установить = реальной стоимости посылки
                InsuranceValue = 1000,
                //Номер ПВЗ
                ParcelshopCode = "900176"
            };
            return preadvices;
        }
        /// <summary>
        /// Данный метод предназначен для первоначальной передачи информации о посылках. 
        /// </summary>
        /// <param name="client"></param>
        public static void CreatePreadvice(refHermesLoc.WebServiceClient client)
        {
           
            

        }

        internal static void UpdatePreadvice(refHermesLoc.WebServiceClient client)
        {

        }
    }

   
}
