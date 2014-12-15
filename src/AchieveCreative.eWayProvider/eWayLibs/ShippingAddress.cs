namespace AchieveCreative.eWayProvider.eWayLibs
{
    public class ShippingAddress
    {
        private string _FirstName;
        private string _LastName;
        private string _Street1;
        private string _Street2;
        private string _City;
        private string _State;
        private string _Country;
        private string _PostalCode;
        private string _Email;
        private string _Phone;
        private ShippingMethod _ShippingMethod;

        public ShippingMethod ShippingMethod
        {
            get { return _ShippingMethod; }
            set { _ShippingMethod = value; }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
    
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public string Street1
        {
            get { return _Street1; }
            set { _Street1 = value; }
        }

        public string Street2
        {
            get { return _Street2; }
            set { _Street2 = value; }
        }

        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        public string PostalCode
        {
            get { return _PostalCode; }
            set { _PostalCode = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
    }
}