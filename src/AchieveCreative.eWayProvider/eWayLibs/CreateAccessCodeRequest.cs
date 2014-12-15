using System.Collections.Generic;

namespace AchieveCreative.eWayProvider.eWayLibs
{
    public class CreateAccessCodeRequest
    {
        public CreateAccessCodeRequest ( )
        {
            _Customer = new Customer ( );
            _ShippingAddress = new ShippingAddress ( );
            _Items = new List<LineItem> ( );
            _Options = new List<Option> ( );
            _Payment = new Payment ( );
        }

        private Customer _Customer;
        private ShippingAddress _ShippingAddress;
        private List<LineItem> _Items;
        private List<Option> _Options;
        private Payment _Payment;
        private string _RedirectUrl;
        private Method _Method;

        public Customer Customer
        {
            get { return _Customer; }
            set { _Customer = value; }
        }

        public ShippingAddress ShippingAddress
        {
            get { return _ShippingAddress; }
            set { _ShippingAddress = value; }
        }

        public List<LineItem> Items
        {
            get { return _Items; }
            set { _Items = value; }
        }

        public List<Option> Options
        {
            get { return _Options; }
            set { _Options = value; }
        }

        public Payment Payment
        {
            get { return _Payment; }
            set { _Payment = value; }
        }

        public string RedirectUrl
        {
            get { return _RedirectUrl; }
            set { _RedirectUrl = value; }
        }

        public Method Method
        {
            get { return _Method; }
            set { _Method = value; }
        }
    }
}