namespace Admins_Transportation.helper
{

    public static class ConstansValuesClass
    {
        #region General

        public static readonly int defaultBranchId = 1;
        public static readonly int defaultVehiclId = 1;
        public static readonly decimal MeloneNumber = 1000000;
        public static readonly decimal TenMeloneNumber = 10000000;
        public static readonly string languageCodeEn = "En";
        public static readonly string laguageCodeAr = "Ar";
        public static readonly string defaultImageVehicle = "/__DefaultFiles/defaultImageVehicle.png";
        public static readonly string defaultImageProfile = "/_DefaultFiles/defaultImageProfile.png";
        public static readonly string defaultImageCategory = "/_DefaultFiles/defaultImageCategory.png";
        public static readonly string defaultImageCustomers = "/_DefaultFiles/defaultImageCustomers.png";
        public static readonly string defaultImageTreasuryTransactionsImagePath = "/_DefaultFiles/defaultImageTreasuryTransactionsImagePath.png";
        public static readonly string defaultImageNotes = "/_DefaultFiles/defaultImageNotes.png";
        public static readonly string defaultImageProduct = "/_DefaultFiles/defaultImageProduct.png";
        public static readonly string defaultImageMalfunctionsRequest = "/_DefaultFiles/defaultImageMalfunctionsRequest.png";
        public static readonly string formatDate = "yyyy/MM/dd";
        public static readonly string formatDateToCode = "yyyyMMdd";
        public static readonly string formatTime = "hh:mm:ss tt";
        public static readonly string formatDateTimeWithComa = "yyyy/MM/dd، hh:mm:ss tt";
        public static readonly string formatDateTime = "yyyy/MM/dd hh:mm:ss tt";
        public static readonly int PageSize = 16;

        #endregion General

        #region enums
        #region  MoneyTypeTransaction
        public enum MoneyTypeTransaction
        {
            MoneyDeposit = 1,
            MoneyDrawdown = 2,
        }
        public enum MoneyPaymentMethodId
        {
            Cash = 1,
            Check = 2,
            Online = 3,
            Other = 4,
            FromBalance = 5,
        }
        #endregion

        #region  Customers Financial Action Types
        public enum CustomersFinancialActionTypes
        {
            AddCredit = 1,
            ReturnCredit = 2,
            PayingJobOrderDebt = 3,
            ReturnPayingJobOrderDebt = 4,
        }
        public enum statusReturn
        {
            isReturn = 1,
            isNotReturn = 2,
        }
        #endregion

        #region  ExpenseTypes
        public enum ExpenseTypes
        {
            Custoday = 1,
            Other = 2,
        }
        #endregion

        #region EmployeeMeduole
        public enum EmployeeUserType
        {
            admin = 1,
            employee = 2,
        }
        #endregion

        #region CustodyModule
        public enum TransactionTypesPurchases
        {
            BuyingFromMerchant = 1,
            ReturnToMerchant = 2,
            Update = 3,
        }
        #endregion CustodyModule

        #region NotfcationPagesId
        public enum NotfcationPagesId
        {
            NoPage = 1,
        }
        #endregion

        #region MovementCycleModule

        public enum JobOrderStates
        {
            InGarage = 1,
            InGarageWaitingFollwUp = 2,
            InOrder = 3,
            Moved = 4,
            IsBack = 5,
            Finished = 6,
            Canceled = 7,
            HoldingOutGarage = 8,
            HoldingInGarage = 9
        };

        public enum vehicleMovement
        {
            InGarage = 1,
            InGarageWaitingMove = 2,
            Moved = 3,
            Holding = 4,
        };

        public enum MovementPathes
        {
            Waiting = 1,
            CurrentPath = 2,
            Arriving = 3,
            Finished = 4,
            Canceled = 5
        };

        public enum TypesOfPaths
        {
            Main = 1,
            OutConverted = 2,
            InConverted = 3
        };

        public enum TypesCalcolateTankKM
        {
            Positive = 1,
            Neutral = 2,
            Minus = 3
        };

        public enum ProblemesTypes
        {
            ProblemInPlace = 1,
            ProblemInGarage = 2,
        }

        public enum SettlementTypes
        {
            Normal = 1,
            AddAmount = 2,
            DiscountAmount = 3,
        }

        #endregion MovementCycleModule

        #region MalfunctionsCycleModule

        public enum MalfunctionsRequestsStatuse
        {
            Waiting = 1,
            WaitingFollwUp = 2,
            OnWorking = 3,
            Finished = 4,
            Canceled = 5
        }

        #endregion MalfunctionsCycleModule

        #region StoreModule

        public enum BatchStatuseTypes
        {
            NotEmpty = 1,
            IsEmpty = 2,
            Completed = 3,
            HaveWaste = 4,
            TotalWaste = 5,
            RemainingExpiry = 6,
            TotalExpired = 7,
        }

        public enum TransactionTypes
        {
            //StoreAdd = 1,
            StoreDrawdown = 2,
            StoreDeposit = 3,
            StoreWast = 4,
            RetunDeposit = 5,
            RetunDrawdown = 6,
            RetunWast = 7,
            UpdateData = 8,
            WaiverCustody = 9,
            WastCustody = 10,
            RetunWastCustody = 11,
            RetunToStore = 12,
        }
        public enum PriceTypes
        {
            One = 1,
            Total = 2,
        }

        #endregion StoreModule

        #endregion enums

        #region UsersController ConstansValues

        public static readonly string defoultPasswordEmployees = "Elsakr@0000";
        public static readonly string defoultPasswordCustomers = "Elsakr@0000";

        //platforms
        public static readonly string platFormAndroid = "android";

        public static readonly string platFormIos = "ios";
        public static readonly string platFormWeb = "web";

        #endregion UsersController ConstansValues

        #region Keys Security ConstansValues

        //Name Developer+Name Project+year
        public static readonly string securityKey = "At2020";

        #endregion Keys Security ConstansValues

        #region SendingAlertMessages ConstansValues

        public static readonly string sendFrom = "A0dminsTransportation2020@gmail.com";
        public static readonly string password = "!Admins&Transportation*2020*!";
        public static readonly string stmpServer = "smtp.gmail.com";
        public static readonly int portNumber = 587;

        // Notification
        public static readonly string serverKey = "AAAABg_lmmE:APA91bGG8cCsbVsqUmLqKBBrfDXHjePWVkWLtYviAx3QqvOu3P5F1womvO1fkg5RV7jYsiL2rkuUMzAtR7b3AJvZeK-jfAvL4QMUEhaaNfdM__1Gzd9RhE_GqPoV1A_TtwP2cY4ZcojC";
        public static readonly string senderId = "26036509281";

        #endregion SendingAlertMessages ConstansValues

        #region General ConstansValues

        //User In Request
        public static readonly int statusSucsses = 1;
        public static readonly int statusFiled = 0;
        public static readonly int statusCatch = -1;
        public static readonly int statusOtherCase = 2;

        //Use in Valid Phone
        public static readonly string Mobile = "Mobile";

        public static readonly string FIXED_LINE = "FIXED_LINE";
        public static readonly string FIXED_LINE_OR_MOBILE = "FIXED_LINE_OR_MOBILE";

        //Use General
        public static readonly string GenderMale = "ذِكرٌ";

        public static readonly string GenderFeMale = "اُنثي";
        public static readonly string NotSetData = "";
        public static readonly string errorSelect = "فشلٌ في التحديدِ";

        //Use in Vehicles
        public static readonly int LenghtCodeTankSize = 6;
        public static readonly int MaxLenthCategorySymbol = 12;
        public static readonly int MaxLenthVicalDataCode = 2;
        public static readonly string startDateInMonth = "01";

        public static readonly string Departed = "Departed";
        public static readonly string Attended = "Attended";

        public static readonly int MaxCountInProvince = 10;
        public static readonly int MaxCountOutProvince = 10;
        public static readonly int MaxCountRequestProblems = 10;

        public static readonly string SymbolFormMovement = "F";
        public static readonly string SymbolToMovement = "T";
        public static readonly string timeSpanFormat = "{0:00}:{0:00}:{0:00}";

        //Serever Constance
        public static readonly string StandardTimeZone = "Egypt Standard Time";
        //public static readonly double DefranceTimeServer = +10;

        public static readonly string customerManagementType = "customerManagement";
        public static readonly string customerFactoryType = "customerFactory";


        #endregion General ConstansValues
    }
}
