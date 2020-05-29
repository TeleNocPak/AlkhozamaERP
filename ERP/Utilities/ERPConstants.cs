using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Utilities
{
    public struct ApplicationKeys
    {
        public const string SqlConnectionString = "SqlConnectionString";
    }

    public struct ApplicationUrls
    {}

    public struct GenericConstants
    {
        public const char UserNameSplitter = '@';
        public const char ColumnSplitter = '.';
        public const char TableNameSplitter = '\'';
        public const char TimeSplitter = ':';
        public const char RangeSplitter = ',';
        public const char BillNumberSplitter = '-';
        public const char AppendSplitter = '&';
        public const string DomainSplitter = "//";

        public const string SortAscending = "{0} ASC";
        public const string SortDescending = "{0} DESC";
    }

    public struct SessionNames
    {
        public const string UserID = "UserID";
    }

    public struct CompanyName
    {
        public const string UnitedMoibile = "1";
        public const string TeleTech = "2";
    }

    public struct FormsCode
    {
        public const string PurchaseOrder = "PO";
        public const string OrderConfirmation = "OC";
        public const string LetterCredit = "LC";
        public const string GoodsReceiptVoucher = "GR";
        public const string CreditNotes = "CN";
        public const string ShipmentInformation = "SI";
        public const string PurchaseOrderLocal = "PL";
        public const string StockTransferNote = "ST";
        public const string StockReceivedNote = "SR";
        public const string SaleOrder = "SO";
        public const string SaleInvoice = "UM";
        public const string SaleReturn = "RI";
        public const string CNTNo = "CNT";
    }

    public struct BankLimitMarginPer
    {
        public const int MarginPercentage100 = 100;
        public const int MarginPercentage15 = 15; 
    }

    public struct SetupsReportParameter
    {
        public const string UserInformation = "101";
        public const string ProductInformation = "102";
        public const string ProductRatesInformation = "103";
        public const string ColorInformation = "104";
        public const string CurrencyInformation = "105";
        public const string ModelNoInformation = "106";
        public const string InsuranceInformation = "107";
        public const string BankInformation = "108";
        public const string VendorInformation = "109";
        public const string PortInformation = "110";
        public const string CompanyInformation = "111";
        public const string BranchInformation = "112";
        public const string WarehouseInformation = "113";
        public const string LocationInformation = "114";
        public const string CourierInformation = "115";
        public const string ZoneInformation = "116";
        public const string BrandInformation = "117";
        public const string CityInformation = "118";
    }

    public struct SalesReportParameter
    {
        public const string DealerType = "101";
        public const string LimitInformation = "102";
        public const string DealerSignBoard = "103";
        public const string SupplierIncentive = "104";
        public const string Dealers = "105";
        public const string DealerMonthlyTarget = "106";
        public const string DealerRebateInspection = "107";
        public const string PostDatedCheque = "108";
        public const string DealerIncentive = "109";
        public const string SalesManTarget = "110";
        public const string OnlineTransaction = "111";
        public const string SalesOrder = "112";
        public const string SalesInvoice = "113";
        public const string SalesReturn = "114";
        public const string DetailSalesOrder = "115";
        public const string DetailSalesInvoice = "116";
        public const string DetailSalesReturn = "117";
        public const string IMEINoLocation = "118";  
    }

    public struct ImportsReportParameter
    {
        public const string ProformaInvoice = "101";
        public const string ProformaInvoiceSummary = "102";
        public const string ProformaInvoiceDetail = "103";
        public const string LCSummary = "104";
        public const string LCDetail = "105";
        public const string EndorsementLetter = "106";
        public const string OrderRequistion = "107";
        public const string OrderRequistionDetail = "108";
        public const string ShipmentDetail = "109";
        public const string ShipmentSummary = "110";
    }

    public struct InventoryReportParameter
    {
        public const string StockTranferSummary = "101";
        public const string StockTranferDetail = "102";
        public const string InventoryReport = "103";
        public const string WarrantyCardReport = "104";
        
    }
}
