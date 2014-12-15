using System;
using System.Collections.Generic;
using AchieveCreative.eWayProvider.eWayLibs;
using TeaCommerce.Api.Models;
using TeaCommerce.Api.Web;
using TeaCommerce.Api.Web.PaymentProviders;
using TeaCommerce.Umbraco.Web;

namespace AchieveCreative.eWayProvider
{
    [PaymentProvider("eWay")]
    public class eWay : APaymentProvider
    {
        public override IDictionary<string, string> DefaultSettings
        {
            get
            {
                var settings = new Dictionary<string, string>();
                settings.Add("RapidAPI.URL", "https://api.ewaypayments.com/AccessCodes/");
                settings.Add("RapidAPI.APIKey", "");
                settings.Add("RapidAPI.Password", "");
                settings.Add("SuccessURL", "/");
                settings.Add("FailureURL", "/");
                return settings;
            }
        }

        public override PaymentHtmlForm GenerateHtmlForm(TeaCommerce.Api.Models.Order order, string teaCommerceContinueUrl, string teaCommerceCancelUrl, string teaCommerceCallBackUrl, string teaCommerceCommunicationUrl, IDictionary<string, string> settings)
        {
            var response = CreateAccessCode(order, settings, teaCommerceContinueUrl);
            var paymentForm = new PaymentHtmlForm()
            {
                Action = response.FormActionURL
            };

            paymentForm.InputFields.Add("EWAY_ACCESSCODE", response.AccessCode);

            return paymentForm;
        }

        public override string GetCancelUrl(TeaCommerce.Api.Models.Order order, IDictionary<string, string> settings)
        {
            return settings["FailureURL"];
        }

        public override string GetContinueUrl(TeaCommerce.Api.Models.Order order, IDictionary<string, string> settings)
        {
            if (order.IsFinalized)
            {
                return settings["SuccessURL"];
            }
            else
            {
                SessionController.SetCurrentFinalizedOrderId(order.StoreId, (Guid?)null);
                SessionController.SetCurrentOrderId(order.StoreId, order.Id);
                return GetCancelUrl(order, settings);
            }
        }

        public override CallbackInfo ProcessCallback(TeaCommerce.Api.Models.Order order, System.Web.HttpRequest request, IDictionary<string, string> settings)
        {
            var accessCode = request["AccessCode"];
            var accessCodeResultRequest = new GetAccessCodeResultRequest(accessCode);
            var result = Rapid3.GetAccessCodeResult(accessCodeResultRequest, settings);

            if (result.ResponseCode == "error")
            {
                return null;
            }

            if (result.ResponseCode != "00")
            {
                return null;
            }

            return new CallbackInfo(((decimal)result.TotalAmount.Value / 100), result.AuthorisationCode, PaymentState.Authorized);
        }

        public override bool FinalizeAtContinueUrl
        {
            get { return true; }
        }

        private CreateAccessCodeResponse CreateAccessCode(Order order, IDictionary<string, string> settings, string redirectUrl)
        {
            var request = new CreateAccessCodeRequest();

            request.Customer.Reference = string.Format("P{0}", order.CartNumber.Replace("CART-", string.Empty).PadLeft(7, '0'));
            request.Customer.FirstName = order.PaymentInformation.FirstName;
            request.Customer.LastName = order.PaymentInformation.LastName;
            request.Customer.Country = TC.GetCountry(order.StoreId, order.PaymentInformation.CountryId).RegionCode;
            request.Customer.Email = order.PaymentInformation.Email;

            request.Payment.TotalAmount = (int)(order.TotalPrice.WithVat * 100);
            request.Payment.InvoiceNumber = string.Format("P{0}", order.CartNumber.Replace("CART-", string.Empty).PadLeft(7, '0'));
            request.Payment.InvoiceDescription = order.Id.ToString();
            request.Payment.InvoiceReference = string.Format("P{0}", order.CartNumber.Replace("CART-", string.Empty).PadLeft(7, '0'));
            request.Payment.CurrencyCode = TC.GetCurrency(order.StoreId, order.CurrencyId).IsoCode;
            request.RedirectUrl = redirectUrl;
            request.Method = Method.ProcessPayment;

            return Rapid3.CreateAccessCode(request, settings);
        }
    }
}
