namespace AchieveCreative.eWayProvider.eWayLibs
{
    public class CreateAccessCodeResponse
    {
        public CreateAccessCodeResponse ( string error )
        {
            Errors = error;
        }

        public TokenCustomer Customer;

        public Payment Payment;

        public string Errors;

        public string AccessCode;

        public string FormActionURL;
    }
}
