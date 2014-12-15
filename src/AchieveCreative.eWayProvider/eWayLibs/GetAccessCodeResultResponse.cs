using System.Collections.Generic;

namespace AchieveCreative.eWayProvider.eWayLibs
{
    public class GetAccessCodeResultResponse
    {
        public GetAccessCodeResultResponse ( )
        {
            Verification = new VerificationResult ( );
            Options = new List<Option> ( );
        }

        public string AccessCode;
        public string AuthorisationCode;
        public string ResponseCode;
        public string ResponseMessage;
        public string InvoiceNumber;
        public string InvoiceReference;
        public int? TotalAmount;
        public int? TransactionID;
        public bool? TransactionStatus;
        public long? TokenCustomerID;
        public decimal? BeagleScore;

        public List<Option> Options;

        public VerificationResult Verification;

        public class VerificationResult
        {
            public string CVN;
            public string Address;
            public string Email;
            public string Mobile;
            public string Phone;
        }
    }
}