namespace AchieveCreative.eWayProvider.eWayLibs
{
    /// <summary>
    /// Summary description for Payment
    /// </summary>
    public class Payment
    {
        private int _TotalAmount;
        private string _InvoiceNumber;
        private string _InvoiceDescription;
        private string _InvoiceReference;
        private string _CurrencyCode;


        public int TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }

        public string InvoiceNumber
        {
            get { return _InvoiceNumber; }
            set { _InvoiceNumber = value; }
        }

        public string InvoiceDescription
        {
            get { return _InvoiceDescription; }
            set { _InvoiceDescription = value; }
        }

        public string InvoiceReference
        {
            get { return _InvoiceReference; }
            set { _InvoiceReference = value; }
        }

        public string CurrencyCode
        {
            get { return _CurrencyCode; }
            set { _CurrencyCode = value; }
        }
    }
}